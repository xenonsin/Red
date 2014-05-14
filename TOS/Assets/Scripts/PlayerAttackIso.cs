using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteManager))]
public class PlayerAttackIso : MonoBehaviour {

    private float attackDistance = 5.0f;
    private float attackDelay = 0.5f;

    private SpriteManager _spriteManager;
    private CharacterControllerIso _characterController;

    private Vector3 newPos;

	// Use this for initialization
	void Start () {

        _spriteManager = this.GetComponent<SpriteManager>();
        _characterController = this.GetComponent<CharacterControllerIso>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(0) && !_spriteManager.IsAttacking)
        {
            if (!_spriteManager.IsWalking)
            TurnTowardsMouse();


            _spriteManager.IsAttacking = true;
           // _characterController.CanMove = false;
        }
        



        
        Debug.DrawRay(transform.position, transform.forward, Color.green);
        


	
        //turn when mouse button is clicked

        //

	}

    void TurnTowardsMouse()
    {
        //not rotating when raycast doesnt hit anything. maybe just take mouse position?

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            newPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
        transform.LookAt(newPos);
    }
}
