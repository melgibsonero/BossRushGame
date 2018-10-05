using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathe : MonoBehaviour {

    private Vector3 originalSize;

    public bool Rescale = false;

    public bool bypassButtonSelected = false;

    public float CurveHeight = 1f;

    [SerializeField]
    private AnimationCurve curve;

    private UnitSlot thisSlot;

    private MeshRenderer meshRenderer;

    [SerializeField]
    private bool isRandomStart;

    private float randomStart;

    private void Start()
    {
        originalSize = transform.localScale;
        randomStart = (isRandomStart) ? Random.Range(0f, 0.8f) : 0; 
        thisSlot = GetComponentInParent<UnitSlot>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update () {
        
        if (Rescale)
        {
            meshRenderer.enabled = thisSlot.IsHighlighted;
            float localCurveHeight;
            if (thisSlot.IsHighlighted)
            {
                localCurveHeight = CurveHeight;
            }
            else
            {
                localCurveHeight = 0;
            }
            float RescaleTimer = Mathf.PingPong(Time.unscaledTime + randomStart, 1);
            transform.localScale = originalSize * (1 + curve.Evaluate(RescaleTimer) * localCurveHeight);
        }
        else
        {
            transform.localScale = originalSize;
        }
	}
}
