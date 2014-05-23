using UnityEngine;
using System.Collections;

public class GrandmaBarEvents : MonoBehaviour {

    private dfProgressBar _progressBar;
    private Grandma _player;
    private dfLabel _hpLabel;

    void OnEnable()
    {
        Grandma.HpChange += UpdateValue;
    }

    void OnDisable()
    {
        Grandma.HpChange -= UpdateValue;
    }

    // Called by Unity just before any of the Update methods is called the first time.
    public void Start()
    {
        // Obtain a reference to the dfProgressBar instance attached to this object
        this._progressBar = GetComponent<dfProgressBar>();
        _player = GameObject.FindGameObjectWithTag("Grandma").GetComponent<Grandma>();
        _hpLabel = GetComponentInChildren<dfLabel>();

        this._progressBar.MaxValue = _player.MaxHealth;
        this._progressBar.Value = _player.Health;
        UpdateLabel();

    }

    void UpdateValue()
    {
        this._progressBar.Value = _player.Health;
        UpdateLabel();
    }

    void UpdateLabel()
    {
        var value = (int)_progressBar.Value;
        _hpLabel.Text = value.ToString() + "/" + _progressBar.MaxValue.ToString();
    }

}
