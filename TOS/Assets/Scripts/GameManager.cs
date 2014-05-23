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
        MOUTH
    }

    public enum Levels
    {
        INTRO,
        MAIN,
        END
    };

    private Stages _currentStage;
    public Levels currentLevel;

    private bool hasSpawned = false;
    private WolfSpawnManager _wolfSpawnManager;


    private bool dialogVisible = false;
    public dfLabel dialogLabel;
    public float duration = 20f;

    private int _wolfDeathCount = 0;

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
    }

    void OnDisable()
    {
        Monster.Dead -= WolfDeathCount;
    }


    void Awake()
    {
        _wolfSpawnManager = GameObject.FindGameObjectWithTag("Wolf Manager").GetComponent<WolfSpawnManager>();
        WolfSpawn = new int[4] { 10, 20, 30, 40 };
    }

	// Use this for initialization
	IEnumerator Start () {

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
            }
            yield return 0;
        }

       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    #region Helper
    public void WolfDeathCount()
    {
        _wolfDeathCount++;
    }

    public void ChangeStageTo(Stages stage)
    {
        _currentStage = stage;
        ChangeLevelTo(Levels.INTRO);

        if(StageChanged != null)
        StageChanged(stage);
    }

    public void ChangeLevelTo(Levels level)
    {
        currentLevel = level;
        if(LevelChanged != null)
        LevelChanged(level);
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
        if (!dialogVisible)
        {
            ShowLabel();
            HealPlayer();
            _wolfDeathCount = 0;
            hasSpawned = false;
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

    void Mains()
    {
        if (!hasSpawned)
        {
            PlayHowl();
            SpawnWolves();
        }

        if (_wolfDeathCount >= WolfSpawn[ToInt(_currentStage)])
            ChangeLevelTo(Levels.END);
    }

    void End()
    {
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
                Debug.Log("You win!");
                break;
        }
    }
    #endregion

    #region Intro
    void ShowLabel()
    {
        dialogLabel.Opacity = 1;
        dialogLabel.Text = Dialog[ToInt(_currentStage)];
        dialogVisible = true;
        
    }

    void HealPlayer()
    {

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
    #endregion

}
