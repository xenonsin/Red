using UnityEngine;
using System.Collections;

public class GrandmaSpriteManager : MonoBehaviour {

    private tk2dSprite _sprite;

	// Use this for initialization
	void Start () {
        _sprite = this.GetComponentInChildren<tk2dSprite>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator FreezeFrame(float hitPower)
    {
        //_animator.Pause();
        yield return new WaitForSeconds(hitPower);
       // _animator.Resume();
    }

    public IEnumerator FlashRed(float seconds)
    {
        _sprite.color = Color.red;
        yield return new WaitForSeconds(seconds);
        _sprite.color = Color.white;

    }
}
