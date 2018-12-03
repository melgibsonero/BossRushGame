using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRigidBodyToChildren : MonoBehaviour {

    public Transform[] transforms;

    public bool done = true;
    private bool activated = false;

	// Use this for initialization
	void Start () {
        transforms = GetComponentsInChildren<Transform>();
	}

    public void ActivateCollapse()
    {
        if (!activated)
        {
            done = false;
        }
        else
        {
            Debug.Log("no more!");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!done)
        {
            done = true;
            for(int i = 3; i < transforms.Length; i++)
            {
                transforms[i].gameObject.AddComponent<Rigidbody>();
                transforms[i].gameObject.AddComponent<BoxCollider>();

                BoxCollider box = transforms[i].GetComponent<BoxCollider>();
                Rigidbody rb = transforms[i].GetComponent<Rigidbody>();
                box.size = new Vector3(0.01f, 0.04f, 0.01f);

                Destroy(box, 5f);
                Destroy(rb, 5f);
            }
        }
	}
}
