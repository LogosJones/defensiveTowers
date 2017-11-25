using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject target;
    public bool active = false;
    public float speed;
    public float damage;

    void Die()
    {
        Object.Destroy(this.gameObject);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	void FixedUpdate () {
        if (active)
        {
            if (target)
            {
                Vector3 directionToward = (target.transform.position - transform.position) / (target.transform.position - transform.position).magnitude;
                transform.position += directionToward * speed;
            }
            else
                Die();
        }
	}

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject == target)
        {
            badguy hit = c.gameObject.GetComponent<badguy>();
            hit.health -= damage;
            Die();
        }
    }
}
