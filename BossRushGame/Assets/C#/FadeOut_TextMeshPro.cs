using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeOut_TextMeshPro : MonoBehaviour {

    TextMeshPro textMesh;
    float timer;

    public bool MoveUpwards = false;
    public bool FadeoutOverTime = true;

    public float FadeoutTime = 2;
    public float UpwardMovementSpeed = 0;


	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMeshPro>();
	}
	
	// Update is called once per frame
	void Update () {
        if(timer<FadeoutTime) timer += Time.deltaTime;
        if(FadeoutOverTime) textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, (1 - timer/2));
        if(MoveUpwards) transform.localPosition = new Vector3(0, timer * UpwardMovementSpeed, 0);
    }
}
