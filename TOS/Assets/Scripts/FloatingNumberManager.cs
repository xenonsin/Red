using UnityEngine;
using System.Collections;

public class FloatingNumberManager : MonoBehaviour {

    public dfGUIManager uiRoot;

    public dfLabel numberLabel;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayDamage(float damage, GameObject goj, float offset)
    {
        dfLabel itemPickupLabel = ((dfLabel)Instantiate(numberLabel)).GetComponent<dfLabel>();

        uiRoot.AddControl(itemPickupLabel);

        var temp = (int)damage;

        itemPickupLabel.Text = temp.ToString();
        itemPickupLabel.Opacity = 1;

        var follow = itemPickupLabel.GetComponent<DamageEvents>();

        follow.attach = goj;
        follow.offset = new Vector3(0, offset, 0);
        follow.enabled = true;
    }
}
