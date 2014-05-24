using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WolfSpawnManager : MonoBehaviour {

    public Monster wolf;
    public BigBadWolf bigBadWolf;

    private GameObject[] _spawners;
    private List<Vector3> _waypoints = new List<Vector3>();
    private bool isLastStage;

    private Transform _player;
    private Transform _grandma;

    private Vector3 grandmaSpot;

    public void OnEnable()
    {
        GameManager.StageChanged += IsLastStage;


    }
    public void OnDisable()
    {
        GameManager.StageChanged -= IsLastStage;
 
    }

    public void IsLastStage(GameManager.Stages stage)
    {
        if (stage == GameManager.Stages.MOUTH)
            isLastStage = true;
        Debug.Log(stage);
        Debug.Log(isLastStage);
    }


    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _grandma = GameObject.FindGameObjectWithTag("Grandma").transform;
        grandmaSpot = _grandma.position;
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
            var wolfs = wolf.GetComponent<WolfFollow>();
            wolfs.target = GetTarget();
            Instantiate(wolf, _waypoints[position], Quaternion.identity);
            
        }
    }

    public void SpawnBigBadWolf()
    {
        var wolfs = bigBadWolf.GetComponent<WolfFollow>();
        wolfs.target = GetTarget();
        Instantiate(bigBadWolf, grandmaSpot, Quaternion.identity);
        
    }

    Transform GetTarget()
    {

        var targets = new List<Transform>();
        targets.Add(_player);
        targets.Add(_grandma);

        int random = Random.Range(0, 300);
        int index = random % 2;

        if (!isLastStage)
            return targets[index];
        else
            return _player;
    }


}
