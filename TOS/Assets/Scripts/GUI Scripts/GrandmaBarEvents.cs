using UnityEngine;
using System.Collections;

public class GrandmaBarEvents : MonoBehaviour {

    private dfProgressBar _progressBar;
    //private Grandma _player;
   public dfLabel _hpLabel;
    public dfLabel name;
   // private BigBadWolf _bbWolf;

    private Entity hpTarget;

    void OnEnable()
    {
        Grandma.HpChange += UpdateValue;
        BigBadWolf.HpChange += UpdateValue;
        BigBadWolf.Alive += ChangeToWolf;
        
    }

    void OnDisable()
    {
        Grandma.HpChange -= UpdateValue;
        BigBadWolf.HpChange -= UpdateValue;
        BigBadWolf.Alive -= ChangeToWolf;
    }

    // Called by Unity just before any of the Update methods is called the first time.
    public void Start()
    {
        // Obtain a reference to the dfProgressBar instance attached to this object
        this._progressBar = GetComponent<dfProgressBar>();
        hpTarget = GameObject.FindGameObjectWithTag("Grandma").GetComponent<Grandma>();
       // _hpLabel = GetComponentInChildren<dfLabel>();

        this._progressBar.MaxValue = hpTarget.MaxHealth;
        this._progressBar.Value = hpTarget.Health;
        UpdateLabel();

    }

    void UpdateValue()
    {
        this._progressBar.Value = hpTarget.Health;
        UpdateLabel();
    }

    void UpdateLabel()
    {
        var value = (int)_progressBar.Value;
        _hpLabel.Text = value.ToString() + "/" + _progressBar.MaxValue.ToString();
    }

    void ChangeToWolf()
    {
        hpTarget = GameObject.FindGameObjectWithTag("Big Bad Wolf").GetComponent<BigBadWolf>();
        this._progressBar.MaxValue = hpTarget.MaxHealth;
        this._progressBar.Value = hpTarget.MaxHealth;
        name.Text = "Big Bad Wolf";
    }

}
