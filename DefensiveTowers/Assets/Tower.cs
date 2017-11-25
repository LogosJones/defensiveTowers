using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public bool active = false;

    public GameObject bullet;
    public float towerHeight = 10;
    public float bulletSpeed = 5;
    public float damage = 30;
    public float shootSpeed = 2.0f;
    public float towerRange = 10;
    public float towerRange_Height = 1000;
    public LayerMask shootAtLayer;

    private GameObject target;
    private float shootTimer = 0;

    //shoot at target with bullet
    void Shoot()
    {
        if (shootTimer <= 0)
        {
            shootTimer = shootSpeed;

            Vector3 bullet_position = transform.position + Vector3.up * towerHeight;
            Quaternion bullet_rotation = transform.rotation;
            GameObject bullet_clone = Instantiate(bullet, bullet_position, bullet_rotation);
            Bullet bullet_code = bullet_clone.GetComponent<Bullet>();
            bullet_code.target = target;
            bullet_code.active = true;
            bullet_code.speed = bulletSpeed;
            bullet_code.damage = damage;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        shootTimer -= Time.deltaTime;
        if (active) {
            if (!target)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, towerRange, shootAtLayer);
                if (colliders.Length > 0)
                    target = colliders[0].gameObject;
            }
            else
            {
                Shoot();
                if ((transform.position - target.transform.position).magnitude > towerRange)
                    target = null;
            }
        }
	}
}
