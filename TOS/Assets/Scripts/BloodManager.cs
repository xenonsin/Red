using UnityEngine;
using System.Collections;

public class BloodManager : MonoBehaviour {

    public Transform bloodSplat;
    public Transform bloodDecal;

	// Use this for initialization
	void Start () {

        bloodSplat.CreatePool();
        bloodDecal.CreatePool();
	}

    public void EmitBlood(Vector3 position, float offset)
    {
        var particle = position;
        particle.y += offset;
        bloodSplat.Spawn(particle);
        var rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        bloodDecal.Spawn(position, rotation);
        var scale = Random.Range(0.1f, 0.5f);
        bloodDecal.localScale = Vector3.one * scale;
    }

    public void EmitBlood(Vector3 position)
    {
        EmitBlood(position, 0f);
    }
}
