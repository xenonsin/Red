using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

    private Camera camera;

    public float minSize = 5.0f;
    public float maxSize = 10.0f;

	// Use this for initialization
	void Start () {
        camera = this.GetComponent<Camera>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && camera.orthographicSize > minSize)
            camera.orthographicSize--;

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && camera.orthographicSize < maxSize)
            camera.orthographicSize++;
            

	}
}
