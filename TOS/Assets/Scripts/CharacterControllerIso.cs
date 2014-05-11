using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class CharacterControllerIso : MonoBehaviour {



    public float speed = 5.0f;
    public float roationSpeed = 10.0f;

    private bool _canMove;
    private bool dashed;
    private float dashCoolDown = 2.0f;

    private SpriteManager _spriteManager;

	// Use this for initialization
	void Start () {
        _canMove = true;
        _spriteManager = this.GetComponent<SpriteManager>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
       var moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
       moveDirection = Camera.main.transform.TransformDirection(moveDirection);
       moveDirection.y = 0;

        //Makes sure the player faces the direction they're moving.
       if (moveDirection != Vector3.zero)
       {
           _spriteManager.IsWalking = true;
           transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * roationSpeed);
       }
       else
           _spriteManager.IsWalking = false;
       Debug.Log(moveDirection);
        //try to move where mouse is facing?
       if(_canMove)
            rigidbody.MovePosition(rigidbody.position + moveDirection.normalized * speed * Time.deltaTime);

        /*
       if (!dashed && Input.GetKey(KeyCode.Space))
       {
           dashed = true;
           rigidbody.AddForce(transform.forward * 50f, ForceMode.Impulse);
           StartCoroutine(CoolDown(() => dashed = false, dashCoolDown));
       }
        */
      }

    public IEnumerator CoolDown(System.Action operation, float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        operation();
    }
}


