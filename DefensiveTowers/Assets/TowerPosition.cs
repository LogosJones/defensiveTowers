using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPosition : MonoBehaviour {

    public GameObject tower;
    public bool useTowerParent = true;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider c)
    {
        if (!tower && c.gameObject.layer == LayerMask.NameToLayer("tower_spawner"))
        {
            Vector3 towerPosition = transform.position;
            Quaternion towerRotation = transform.rotation;
            if (useTowerParent)
            {
                tower = Instantiate(c.transform.parent.gameObject, towerPosition, towerRotation);
                tower.GetComponent<Rigidbody>().useGravity = false;
                tower.GetComponent<Rigidbody>().isKinematic = true;
                tower.GetComponent<Tower>().active = true;
            }
        }
    }
}
