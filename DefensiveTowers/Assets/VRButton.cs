using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRButton : MonoBehaviour {

    public float pressDistance = 0.01f;
    public float resetDistance = 0.02f;
    private Dictionary<Collider, Vector3> touching = new Dictionary<Collider, Vector3>();
    private Vector3 originalPosition;
    private Vector3 touchPosition;
    public float resetSpeed = 0.03f;
    private bool reseting;
    public bool pressed;
    public bool toggled = false;

    private Color oldColor;

	// Use this for initialization
	void Start () {
        originalPosition = transform.localPosition;
        oldColor = GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {
        changeColor();
        //something is touching the button
        if (touching.Count > 0 && !reseting)
        {
            //get list of things touching
            List<Collider> colliders = new List<Collider>(touching.Keys);

            //use first entry as transform (for one hand only)
            Collider targetCollider = colliders[0];
            Transform target = targetCollider.transform;
            //get distance between target and target original position projected onto button's normal
            Vector3 projVector = Vector3.Project((target.position - touching[targetCollider]), transform.up);
            float projDistance = projVector.magnitude;
            //move button that distance from original position
            //hand distance is right direction (not following when pulling away)
            if (Vector3.Dot(projVector, transform.up) < 0)
                if(projDistance > resetDistance)
                {
                    pressed = false;
                    reseting = true;
                }
                else
                {
                    //follow hand
                    transform.localPosition = touchPosition - Vector3.left * projDistance;

                    if (projDistance > pressDistance && !pressed)
                        buttonPress(target.gameObject);
                }
        }
        else {
            //move back
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPosition, resetSpeed);
            if (touching.Count <= 0)
            {
                if (pressed)
                    toggled = !toggled;
                pressed = false;
                reseting = false;
            }
        }
    }

    //called when the button is pushed
    void buttonPress(GameObject hand)
    {
        pressed = true;
        hand.GetComponent<OculusHaptics>().Vibrate(VibrationForce.Medium);
    }

    void changeColor()
    {
        if (!pressed)
        {
            GetComponent<Renderer>().material.color = oldColor;
        }
        else
        {
            GetComponent<Renderer>().material.color = new Color(0, 1, 0);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        //if thing touching is a hand
        if(collider.gameObject.layer == LayerMask.NameToLayer("hand"))
        {
            touchPosition = transform.localPosition;
            touching.Add(collider, collider.transform.position);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        //if thing leaving collider is a hand
        if(collider.gameObject.layer == LayerMask.NameToLayer("hand"))
        {
            touching.Remove(collider);
        }
    }
}
