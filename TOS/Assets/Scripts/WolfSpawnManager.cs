using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WolfSpawnManager : MonoBehaviour {

    public Monster wolf;

    private GameObject[] _spawners;
    private List<Vector3> _waypoints = new List<Vector3>();

    void Awake()
    {
       // wolf.CreatePool();
        _spawners = GameObject.FindGameObjectsWithTag("Spawn Points");

        foreach (var points in _spawners)
        {
            _waypoints.Add(points.transform.position);
        }
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnWolves(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int position = i % 4;
            //wolf.Spawn(_waypoints[position]);
            Instantiate(wolf, _waypoints[position], Quaternion.identity);
        }
    }


}
