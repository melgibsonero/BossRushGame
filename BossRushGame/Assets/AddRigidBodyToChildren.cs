using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRigidBodyToChildren : MonoBehaviour {

    public Transform[] transforms;

    public bool done = true;
    private bool activated = false;

    public int HowManyNonBoneParts = 3;

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
    }
	
	// Update is called once per frame
	void Update () {
        if (!done)
        {
            if (GetComponent<Animator>()) GetComponent<Animator>().enabled = false;
            done = true;
            for(int i = HowManyNonBoneParts; i < transforms.Length; i++)
            {
                transforms[i].gameObject.AddComponent<Rigidbody>();
                transforms[i].gameObject.AddComponent<CapsuleCollider>();
                transforms[i].parent = null;

                CapsuleCollider box = transforms[i].GetComponent<CapsuleCollider>();
                Rigidbody rb = transforms[i].GetComponent<Rigidbody>();
                box.radius = 0.05f / transforms[i].localScale.x;
                box.height = 1f / transforms[i].localScale.y;

                Destroy(box, 5f);
                Destroy(rb, 5f);
            }
            Invoke("CleanUp", 6f);
        }
	}

    void CleanUp()
    {
        for (int i = HowManyNonBoneParts; i < transforms.Length; i++)
        {
            transforms[i].parent = transform;
        }
    }
}
