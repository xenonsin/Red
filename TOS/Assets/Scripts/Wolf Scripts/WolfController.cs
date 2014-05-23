using UnityEngine;
using System.Collections;

public class WolfController : MonoBehaviour {


    private float _distanceFromPlayer;
    private GameObject _player;
    private WolfFollow _wolfAI; //wolf follow
    private WolfSpriteManager _spriteManager;
    private tk2dSpriteAnimator _animator;
    private CameraShake _cameraShake;
    private AudioManager _audioManager;
    private MeleeWeapon _meleeWeapon = new Claws();

    public float minRange = 4f;
    public float attackDelay = 1f;

    [SerializeField]
    private bool _canMove;
    public bool CanMove { get { return _canMove; } set { _canMove = value; } }
	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        _wolfAI = this.GetComponent<WolfFollow>();
        _spriteManager = this.GetComponent<WolfSpriteManager>();
        _animator = this.GetComponentInChildren<tk2dSpriteAnimator>();
        _animator.AnimationEventTriggered += AnimationEventHandler;
        _cameraShake = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraShake>();
        _audioManager = GameObject.FindGameObjectWithTag("Audio Manager").GetComponent<AudioManager>();
        _canMove = true;
	}
	
	// Update is called once per frame
	void Update () {

        if(_canMove)
        GetDistance();
	
	}

    void AnimationEventHandler(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frameNum)
    {
        //string str = animator.name + "\n" + clip.name + "\n" + "INFO: " + clip.GetFrame(frameNum).eventInfo;
        string eventInfo = clip.GetFrame(frameNum).eventInfo;

        if (eventInfo == "Attack Hit")
            CheckRange();
    }

    void GetDistance()
    {
        var distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance > minRange)
        {
            _spriteManager.IsWalking = true;
            _wolfAI.canFollow = true;
        }
        else
        {
            _spriteManager.IsWalking = false;
            StartCoroutine(AttackDelay(attackDelay));
            _wolfAI.canFollow = false;
        }
    }

    IEnumerator AttackDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _spriteManager.IsAttacking = true;
    }

    void CheckRange()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _meleeWeapon.Range);

        foreach (var hit in hitColliders)
        {
            if (hit && hit.tag == "Player")
            {
                var cone = Mathf.Cos(_meleeWeapon.Angle * Mathf.Deg2Rad);
                Vector3 dir = (hit.transform.position - transform.position).normalized;

                if (Vector3.Dot(transform.forward, dir) > cone)
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _meleeWeapon.Range);


        Debug.DrawRay(transform.position, transform.forward * _meleeWeapon.Range, Color.red);
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
        var objectThatWasHit = hit.transform.GetComponent<Entity>();
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
