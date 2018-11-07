using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathe : MonoBehaviour {

    private Vector3 originalSize;
    private Vector3 originalPosition;

    public bool Rescale = false;

    public bool FloatUpDown = false;

    public float CurveHeight = 1f;

    [SerializeField]
    private AnimationCurve curveRescale;

    [SerializeField]
    private AnimationCurve curveFloater;

    private UnitSlot thisSlot;

    private MeshRenderer meshRenderer;

    [SerializeField]
    private bool isRandomStart;

    private float randomStart;

    private void Start()
    {
        originalSize = transform.localScale;
        originalPosition = transform.localPosition;
        randomStart = (isRandomStart) ? Random.Range(0f, 0.8f) : 0; 
        thisSlot = GetComponentInParent<UnitSlot>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update () {
        
        if (Rescale)
        {
            float localCurveHeight;
            if (thisSlot != null)
            {
                meshRenderer.enabled = thisSlot.IsHighlighted;
                if (thisSlot.IsHighlighted)
                {
                    localCurveHeight = CurveHeight;
                }
                else
                {
                    localCurveHeight = 0;
                }
            }
            else
            {
                localCurveHeight = CurveHeight;
            }

            float RescaleTimer = Mathf.PingPong(Time.unscaledTime + randomStart, 1);
            transform.localScale = originalSize * (1 + curveRescale.Evaluate(RescaleTimer) * localCurveHeight);
        }
        else
        {
            transform.localScale = originalSize;
        }
        if (FloatUpDown)
        {
            float localCurveHeight;
            localCurveHeight = CurveHeight;
            float RescaleTimer = Mathf.PingPong(Time.time + randomStart, 1);
            transform.localPosition = originalPosition * (1 + curveFloater.Evaluate(RescaleTimer) * localCurveHeight);
        }
	}
}
