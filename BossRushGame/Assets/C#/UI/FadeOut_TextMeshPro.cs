using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeOut_TextMeshPro : MonoBehaviour {

    TextMeshPro textMesh;
    float timer;

    public bool MoveUpwards = false;
    public bool FadeoutOverTime = true;
    public bool LookAtCamera = true;

    public float FadeoutTime = 2;
    public float UpwardMovementSpeed = 0;

    [SerializeField]
    private Vector3 ogPos;


	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMeshPro>();
        ogPos = textMesh.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer < FadeoutTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Invoke("Destroy", 2f);
        }
        if (LookAtCamera) transform.rotation = FindObjectOfType<Camera>().transform.rotation;
        if(FadeoutOverTime) textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, (1 - timer/FadeoutTime));
        if(MoveUpwards) transform.localPosition = new Vector3(ogPos.x, ogPos.y + timer * UpwardMovementSpeed, ogPos.z);
    }
}
