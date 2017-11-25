using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public float spawnTimer = 5;
    public Transform destinationGroup;
    public int[] schedule;
    public GameObject[] library;

    private float timerOriginal;
    public int currentIndex = 0;


	// Use this for initialization
	void Start () {
        timerOriginal = spawnTimer;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentIndex < schedule.Length)
        {
            if (spawnTimer <= 0)
            {
                int monster = schedule[currentIndex];
                if (monster >= 0 && monster < library.Length)
                {
                    GameObject spawn = Instantiate(library[monster], transform.position, transform.rotation);
                    spawn.GetComponent<badguy>().destinationGroup = destinationGroup;
                }
                spawnTimer = timerOriginal;
                currentIndex++;
            }
            else
                spawnTimer -= Time.deltaTime;
        }
	}
}
