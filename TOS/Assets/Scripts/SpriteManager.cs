using UnityEngine;
using System.Collections;


public class SpriteManager : MonoBehaviour {

    private Animator _animator;

    private int _currentDirection = 0;

	// Use this for initialization
	void Start () {
        _animator = this.GetComponentInChildren<Animator>();

	
	}
	
	// Update is called once per frame
	void Update () {
        var camPos = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
        var parent = new Vector2(transform.forward.x, transform.forward.z);
        float enemyAngle = Vector2.Angle(camPos, parent);
        Vector3 cross = Vector3.Cross(camPos, parent);

        if (cross.z > 0)
            enemyAngle = 360 - enemyAngle;

       float arrayIndexFloat = Mathf.Round(enemyAngle / 360f * 8.0f) % 8.0f;

       int arrayIndexInt = Mathf.FloorToInt(arrayIndexFloat);
       if (_currentDirection != arrayIndexInt)
       {
           _currentDirection = arrayIndexInt;

          _animator.SetInteger("Direction", arrayIndexInt);
       }
        
       Debug.Log("angle " + rigidbody.velocity);
	}
}
