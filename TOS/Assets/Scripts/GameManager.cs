using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

//    'Oh! grandmother,' she said, 'what big ears you have!'
//'The better to hear you with, my child,' was the reply.
//'But, grandmother, what big eyes you have!' she said.
//'The better to see you with, my dear.'
//'But, grandmother, what large hands you have!'
//'The better to hug you with.'
//'Oh! but, grandmother, what a terrible big mouth you have!'
//'The better to eat you with!'

    public delegate void ChangeStage(Stages stage);
    public static event ChangeStage StageChanged;

    public delegate void ChangeLevel(Levels level);
    public static event ChangeLevel LevelChanged;

    public enum Stages
    {
        EARS,
        EYES,
        HANDS,
        MOUTH,
        WIN,
        LOSE
    }

    public enum Levels
    {
        INTRO,
        MAIN,
        END
    };

    public Stages _currentStage;
    public Levels currentLevel;

    private bool hasSpawned = false;
    private WolfSpawnManager _wolfSpawnManager;

    private bool dialogVisible = false;
    public dfLabel dialogLabel;
    public float duration = 20f;
    private bool wolfResponse;

    private bool showEndPanel;
    public dfPanel endPanel;
    public bool endButtonClicked;

    private int _wolfDeathCount = 0;
    private int _wolfDeathCountEndGame = 0;

    public dfPanel winLosePanel;
    public dfLabel winLoseLabel;
    public dfLabel scoreLabel;

    public Grandma grandma;
    public BigBadWolf bigBadWolf;

    private CameraShake _cameraShake;

    public string[] Dialog = new string[]
    {
        "'Oh! grandmother,' she said, 'what big ears you have!'",
        "'But, grandmother, what big eyes you have!' she said.",
        "'But, grandmother, what large hands you have!'",
        "'Oh! but, grandmother, what a terrible big mouth you have!'"
    };
    public int[] WolfSpawn;

    void OnEnable()
    {
        Monster.Dead += WolfDeathCount;
        Grandma.Dead += Lose;
        Player.Dead += Lose;
        BigBadWolf.Dead += Win;
    }

    void OnDisable()
    {
        Monster.Dead -= WolfDeathCount;
        Grandma.Dead -= Lose;
        Player.Dead -= Lose;
        BigBadWolf.Dead -= Win;
    }


    void Awake()
    {
        _wolfSpawnManager = GameObject.FindGameObjectWithTag("Wolf Manager").GetComponent<WolfSpawnManager>();
        _cameraShake = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraShake>();
        WolfSpawn = new int[4] { 10, 20, 30, 40 };
        //WolfSpawn = new int[4] { 0, 0, 0, 1 }; //Debug
    }

	// Use this for initialization
	IEnumerator Start () {
        endButtonClicked = true;
        _currentStage = Stages.EARS;
        currentLevel = Levels.INTRO;

        while(true)
        {
            switch(_currentStage)
            {
                case Stages.EARS:
                    Ears();
                    break;
                case Stages.EYES:
                    Eyes();
                    break;
                case Stages.HANDS:
                    Hands();
                    break;
                case Stages.MOUTH:
                    Mouth();
                    break;
                case Stages.WIN:
                    Win();
                    break;
                case Stages.LOSE:
                    Lose();
                    break;
            }
            yield return 0;
        }

       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    #region Helper
    public void EndPanelClicked(dfControl ignore, dfMouseEventArgs args)
    {
        endButtonClicked = true;
    }

    public void WolfDeathCount()
    {
        _wolfDeathCount++;
        _wolfDeathCountEndGame++;

    }

    public void ChangeStageTo(Stages stage)
    {
        if (StageChanged != null)
            StageChanged(stage);

        _currentStage = stage;
        ChangeLevelTo(Levels.INTRO);

        
    }

    public void ChangeLevelTo(Levels level)
    {
        if (LevelChanged != null)
            LevelChanged(level);

        currentLevel = level;

    }

    public int ToInt(Stages stage)
    {
        int value = (int)stage;
        return value;
    }

    public int ToInt(Levels stage)
    {
        int value = (int)stage;
        return value;
    }
    #endregion

    #region Stages
    void Win() 
    {
        winLosePanel.IsVisible = true;
        winLoseLabel.Text = "You Saved Grandmother!";
        scoreLabel.Text = "You killed " + _wolfDeathCountEndGame + " Wolves";
    }

    void Lose() 
    {
        winLosePanel.IsVisible = true;
        winLoseLabel.Text = "The Wolf Got You!";
        scoreLabel.Text = "You killed " + _wolfDeathCountEndGame + " Wolves";
    }

    void GenericStage()
    {
        switch (currentLevel)
        {
            case Levels.INTRO:
                Intro();
                break;
            case Levels.MAIN:
                Mains();
                break;
            case Levels.END:
                End();
                break;
        }
    }

    void Ears()
    {
        GenericStage();
    }

    void Eyes()
    {
        GenericStage();
    }

    void Hands()
    {
        GenericStage();
    }

    void Mouth()
    {
        GenericStage();
    }
    #endregion

    #region Levels
    void Intro()
    {
        //horrible code ahead.
        if (_currentStage != Stages.MOUTH)
        {

            if (endButtonClicked)
            {
                if (!dialogVisible)
                {
                    ShowLabel();
                    ResetValues();
                }
                else
                {
                    if (dialogLabel.Opacity > 0)
                    {
                        dialogLabel.Opacity -= Time.deltaTime / duration;
                    }
                    else
                    {
                        dialogVisible = false;
                        ChangeLevelTo(Levels.MAIN);
                    }
                }
            }
        }
        else 
        {
            LastStageIntro();
        }
    }

    void LastStageIntro()
    {
        //horrible code ahead. crunch crunch time
        if (endButtonClicked)
        {
            if (!dialogVisible && !wolfResponse)
            {
                ShowLabel();
                ResetValues();
            }
            else if (dialogVisible && !wolfResponse)
            {
                if (dialogLabel.Opacity > 0)
                {
                    dialogLabel.Opacity -= Time.deltaTime / duration;
                }
                else
                {
                    dialogVisible = false;
                    wolfResponse = true;
                   // ChangeLevelTo(Levels.MAIN);
                }
            }

            else if (wolfResponse && !dialogVisible)
            {
                ShowResponse();
                _cameraShake.PlayShake(4f, 5f, 0.3f);
                
            }
            else if (dialogVisible && wolfResponse)
            {
                if (dialogLabel.Opacity > 0)
                {
                    dialogLabel.Opacity -= Time.deltaTime/10f;
                }
                else
                {
                    wolfResponse = false;
                    dialogVisible = false;
                    ChangeLevelTo(Levels.MAIN);
                }
            }

        }
    }

    void Mains()
    {
        if (_currentStage != Stages.MOUTH)
        {
            if (!hasSpawned)
            {
                PlayHowl();
                SpawnWolves();
            }

            if (_wolfDeathCount >= WolfSpawn[ToInt(_currentStage)])
                ChangeLevelTo(Levels.END);
        }
        else
        {
            LastStageMain();
        }
    }

    void LastStageMain()
    {
        if (!hasSpawned)
        {
            PlayHowl();
            SpawnWolves();
            KillGrandma();
            SpawnBigBadWolf();

        }

        if (_wolfDeathCount >= WolfSpawn[ToInt(_currentStage)])
            ChangeLevelTo(Levels.END);
    }

    void End()
    {
        if (_currentStage != Stages.MOUTH)
        {
            if (showEndPanel)
            {
                showEndPanel = false;
                StartCoroutine(ShowEndPanel());
            }
        }
        else
        {
            LastStageEnd();
        }

    }
    void LastStageEnd()
    {
        ChangeStageTo(Stages.WIN);
    }
    #endregion

    #region Intro
    void ShowLabel()
    {
        dialogLabel.Opacity = 1;
        dialogLabel.Text = Dialog[ToInt(_currentStage)];
        dialogVisible = true;
        
    }

    void ShowResponse()
    {
        dialogLabel.Opacity = 1;
        dialogLabel.Color = Color.red;
        dialogLabel.Text = "'The better to eat you with!'";      
        dialogVisible = true;
    }

    void HealPlayer()
    {

    }

    void ResetValues()
    {
        //HealPlayer();
        _wolfDeathCount = 0;
        hasSpawned = false;
        showEndPanel = true;
    }
    #endregion

    #region Main
    void PlayHowl()
    {

    }

    void SpawnWolves()
    {
        _wolfSpawnManager.SpawnWolves(WolfSpawn[ToInt(_currentStage)]);
        Debug.Log(WolfSpawn[ToInt(_currentStage)]);
        hasSpawned = true;
    }

    void KillGrandma()
    {
        grandma.Kill();
    }
    void SpawnBigBadWolf()
    {
        _wolfSpawnManager.SpawnBigBadWolf();
        
    }
    #endregion

    #region End
    IEnumerator ShowEndPanel()
    {
        yield return new WaitForSeconds(3f);


        SwitchToNextStage();

    }

    void SwitchToNextStage()
    {
        endButtonClicked = false;
        endPanel.IsVisible = true;
        switch (_currentStage)
        {
            case Stages.EARS:
                ChangeStageTo(Stages.EYES);
                break;
            case Stages.EYES:
                ChangeStageTo(Stages.HANDS);
                break;
            case Stages.HANDS:
                ChangeStageTo(Stages.MOUTH);
                break;
            case Stages.MOUTH:
                ChangeStageTo(Stages.WIN);
                break;
        }
    }
    #endregion
}
