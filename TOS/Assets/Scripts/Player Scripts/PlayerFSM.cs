using UnityEngine;
using System.Collections;

public class PlayerFSM : MonoBehaviour {

    public enum States
    {
        IDLE,
        WALK,
        ATTACK,
        HIT,
        STUNNED,
        DEAD,
        DASH,
        SHOOT,
        JUMP
    };

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
