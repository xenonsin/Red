﻿using UnityEngine;
using System.Collections;

public class FloatingNumberManager : MonoBehaviour {

    public dfGUIManager uiRoot;

    public dfLabel numberLabel;

	// Use this for initialization
	void Start () {
        numberLabel.CreatePool();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayDamage(float damage, GameObject goj, float offset)
    {
        //dfLabel itemPickupLabel = numberLabel.Spawn().GetComponent<dfLabel>();

        dfLabel itemPickupLabel = numberLabel.Spawn();

        uiRoot.AddControl(itemPickupLabel);

        var temp = (int)damage;

        itemPickupLabel.Text = temp.ToString();
        itemPickupLabel.Opacity = 1;

        var follow = itemPickupLabel.GetComponent<DamageEvents>();

        follow.attach = goj;
        follow.offset = new Vector3(0, offset, 0);
        follow.enabled = true;
    }

    public void DisplayHeal(float heal, GameObject goj, float offset)
    {
        dfLabel itemPickupLabel = numberLabel.Spawn().GetComponent<dfLabel>();

        uiRoot.AddControl(itemPickupLabel);

        var temp = (int)heal;

        itemPickupLabel.Color = Color.green;
        itemPickupLabel.Text = temp.ToString();
        itemPickupLabel.Opacity = 1;

        var follow = itemPickupLabel.GetComponent<DamageEvents>();

        follow.attach = goj;
        follow.offset = new Vector3(0, offset, 0);
        follow.enabled = true;
    }

    public void DisplayNumber(float damage, GameObject goj, float offset, Color color)
    {
        //dfLabel itemPickupLabel = numberLabel.Spawn().GetComponent<dfLabel>();

        dfLabel itemPickupLabel = numberLabel.Spawn();

        uiRoot.AddControl(itemPickupLabel);

        var temp = (int)damage;

        itemPickupLabel.Color = color;
        itemPickupLabel.Text = temp.ToString();
        itemPickupLabel.Opacity = 1;

        var follow = itemPickupLabel.GetComponent<DamageEvents>();

        follow.attach = goj;
        follow.offset = new Vector3(0, offset, 0);
        follow.enabled = true;
    }
}
