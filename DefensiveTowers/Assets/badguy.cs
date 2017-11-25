using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badguy : MonoBehaviour {

    public Transform destinationGroup;
    private List<Transform> destinations;
    public float health = 100;
    public float damage = 1;
    public int currentIndex = 0;
    public float moveSpeed = 10;

    private float arrivalDistance = 1f;

    //take the transforms of all the children of linked object destinations
    void fillDestinations()
    {
        destinations = new List<Transform>();
        foreach (Transform child in destinationGroup)
            destinations.Add(child);
    }

    //change the destination index
    void changeIndex()
    {
        if (currentIndex + 1 == destinations.Count)
            currentIndex = 0;
        else
            currentIndex++;
    }

    void CheckHealth()
    {
        if (health <= 0)
            Die();
    }

    void Die()
    {
        Object.Destroy(this.gameObject);
    }

    //called when last destination in list is reached
    void endReached()
    {
        Die();
    }

	// Use this for initialization
	void Start () {
        fillDestinations();
	}
	
	void Update()
    {
        CheckHealth();
    }

	void FixedUpdate () {
        if (destinations.Count > 0)
        {
            float distance = (transform.position - destinations[currentIndex].position).magnitude;
            //hasn't reached destination
            if (distance > arrivalDistance)
            {
                Vector3 directionToward = destinations[currentIndex].position - transform.position;
                directionToward = directionToward / directionToward.magnitude;
                Vector3 horizontal = new Vector3(directionToward.x, 0, directionToward.z);
                transform.position += horizontal * moveSpeed;
            }
            //switch destination
            else
            {
                if (currentIndex + 1 == destinations.Count)
                    endReached();
                changeIndex();
            }
        }
	}
}
