using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteManager))]
public class PlayerAttackIso : MonoBehaviour {

    private float attackRadius = 5f;
    private float attackKnockback = 7f;

    private float attackDistance = 5.0f;
    private float attackDelay = 0.5f;
    private float attackAngle = 30f;

    private float attackMagnitude = 0.2f;
    private float attackShakeDuration = 0.3f;
    private float attackShakeSpeed = 5.0f;


    private SpriteManager _spriteManager;
    //private CharacterControllerIso _characterController;
    private tk2dSpriteAnimator _animator;
    private CameraShake _cameraShake;

    private Vector3 newPos;

	// Use this for initialization
	void Start () {

        _spriteManager = this.GetComponent<SpriteManager>();
       // _characterController = this.GetComponent<CharacterControllerIso>();
        _animator = this.GetComponentInChildren<tk2dSpriteAnimator>();
        _animator.AnimationEventTriggered += AnimationEventHandler;
        _cameraShake = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraShake>();
	
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

    void AnimationEventHandler(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frameNum)
    {
        //string str = animator.name + "\n" + clip.name + "\n" + "INFO: " + clip.GetFrame(frameNum).eventInfo;
        string eventInfo = clip.GetFrame(frameNum).eventInfo;

        if (eventInfo == "Attack Hit")
            CheckRange();
            //StartCoroutine(DealDamage());
    }

    void CheckRange()
    {     
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);

        foreach (var hit in hitColliders)
        {
            if (hit && hit.tag == "Enemy")
            {
               var cone = Mathf.Cos(attackAngle * Mathf.Deg2Rad);
                Vector3 dir = (hit.transform.position - transform.position).normalized;

                if(Vector3.Dot(transform.forward, dir) > cone)
                {
                    //Target is within the cone.
                    Debug.Log("Attack hit!");

                    ScreenShake();

                    //play sound

                    //freeze frame

                    //deal damage

                    //knockback
                    hit.rigidbody.AddForce(transform.forward * attackKnockback, ForceMode.Impulse);
                }
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
    //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, attackRadius);
}

    void ScreenShake()
    {
        _cameraShake.PlayShake(attackShakeDuration, attackShakeSpeed, attackMagnitude);
    }
}
