using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

    public float health = 100;
    private float maxHealth;
    private int currentRenderMode = 0;
    public GameObject[] renderGroups;

	// Use this for initialization
	void Start () {
        maxHealth = health;
        currentRenderMode = renderGroups.Length;
	}
	
    void Die()
    {
        //GameObject.Destroy(this.gameObject);
    }

    void CheckHealth()
    {
        if (health <= 0)
            Die();

        if (renderGroups.Length > 0)
        {
            float delta = maxHealth / renderGroups.Length;
            for (int i = 0; i < renderGroups.Length; i++)
                if (health < (i + 1) * delta)
                {
                    if (currentRenderMode > i)
                    {
                        Debug.Log("toggling renderGroup " + i);
                        currentRenderMode = i;
                        foreach (Transform child in renderGroups[i].transform)
                        {
                            Renderer r = child.GetComponent<Renderer>();
                            r.enabled = !r.enabled;
                        }
                    }
                }
        }
    }

	// Update is called once per frame
	void Update () {
        CheckHealth();
	}

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.layer == LayerMask.NameToLayer("enemy"))
            health -= c.gameObject.GetComponent<badguy>().damage;
    }

}
