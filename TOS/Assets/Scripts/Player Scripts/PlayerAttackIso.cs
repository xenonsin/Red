using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteManager))]
public class PlayerAttackIso : MonoBehaviour {

    private SpriteManager _spriteManager;
    //private CharacterControllerIso _characterController;
    private tk2dSpriteAnimator _animator;
    private CameraShake _cameraShake;
    private AudioManager _audioManager;

    private Vector3 newPos;

    private MeleeWeapon _meleeWeapon = new Scythe();
    //private RangeWeapon _rangeWeapon = new DualPistols();

	// Use this for initialization
	void Start () {

        _spriteManager = this.GetComponent<SpriteManager>();
       // _characterController = this.GetComponent<CharacterControllerIso>();
        _animator = this.GetComponentInChildren<tk2dSpriteAnimator>();
        _animator.AnimationEventTriggered += AnimationEventHandler;
        _cameraShake = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraShake>();
        _audioManager = GameObject.FindGameObjectWithTag("Audio Manager").GetComponent<AudioManager>();

	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(0) && !_spriteManager.IsAttacking)
        {
            if (!_spriteManager.IsWalking)
            TurnTowardsMouse();


            _spriteManager.IsAttacking = true;
        }

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
    }

    void CheckRange()
    {     
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _meleeWeapon.Range);

        foreach (var hit in hitColliders)
        {
            if (hit && hit.tag == "Enemy" || hit.tag == "Big Bad Wolf")
            {
               var cone = Mathf.Cos(_meleeWeapon.Angle * Mathf.Deg2Rad);
                Vector3 dir = (hit.transform.position - transform.position).normalized;

                if(Vector3.Dot(transform.forward, dir) > cone)
                {
                    //Target is within the cone.
                    Debug.Log("Attack hit!");

                    ScreenShake();

                    PlaySound(_meleeWeapon.AudioClipName); 

                    FreezeFrame();

                    DealDamage(hit);

                    KnockBack(hit);
                    
                }
            }
            else
            {
                //some how this plays even though it hits. Not sure why
                PlaySound("miss3"); 
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _meleeWeapon.Range);


        Debug.DrawRay(transform.position,  transform.forward * _meleeWeapon.Range, Color.red);
        Debug.DrawRay(transform.position, (Quaternion.Euler(0, _meleeWeapon.Angle, 0) * transform.forward).normalized * _meleeWeapon.Range, Color.red);
        Debug.DrawRay(transform.position, (Quaternion.Euler(0, _meleeWeapon.Angle, 0) * transform.forward).normalized * _meleeWeapon.Range, Color.red);
}

    void ScreenShake()
    {
        _cameraShake.PlayShake(_meleeWeapon.ShakeDuration, _meleeWeapon.ShakeSpeed, _meleeWeapon.Magnitude);
    }

    void PlaySound(string sound)
    {
        _audioManager.PlaySoundWithRandomScale(sound, transform.position);
    }

    void DealDamage(Collider hit)
    {
       var objectThatWasHit =  hit.transform.GetComponent<Entity>();
       float damage = Random.Range(_meleeWeapon.MinDamage, _meleeWeapon.MaxDamage);
       objectThatWasHit.Hit(damage);
    }

    void FreezeFrame()
    {
        StartCoroutine(_spriteManager.FreezeFrame(_meleeWeapon.Magnitude));
    }

    void KnockBack(Collider hit)
    {
        hit.rigidbody.AddForce(transform.forward * _meleeWeapon.Knockback, ForceMode.Impulse); 
    }
}
