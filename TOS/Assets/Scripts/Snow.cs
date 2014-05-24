using UnityEngine;
using System.Collections;

public class Snow : MonoBehaviour {

    public bool followControl;
    public GameObject player;
    //The offset of the camera to centrate the player in the X axis
    public float offsetX = 0;
    //The offset of the camera to centrate the player in the Z axis
    public float offsetZ = 0;
    //The maximum distance permited to the camera to be far from the player, its used to     make a smooth movement
    public float maximumDistance = 2;
    //The velocity of your player, used to determine que speed of the camera
    public float playerVelocity = 10;

    private float movementX;
    private float movementZ;

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (followControl)
            Follow();
	
	}

    void Follow()
    {
        if (player != null)
        {
            movementX = ((player.transform.position.x + offsetX - this.transform.position.x)) / maximumDistance;
            movementZ = ((player.transform.position.z + offsetZ - this.transform.position.z)) / maximumDistance;
            this.transform.position += new Vector3((movementX * playerVelocity * Time.deltaTime), 0, (movementZ * playerVelocity * Time.deltaTime));
        }
    }
}
