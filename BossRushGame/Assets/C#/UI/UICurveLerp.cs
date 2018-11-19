using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICurveLerp : MonoBehaviour
{
    [Header("Gizmo stuff")]
    public bool initDone = false;
    public Color curveColor;
    public bool hideLines;
    public float angleTolerance = 1f;
    
    [Header("Curve stuff")]
    public Transform target;
    [Range(0.01f, 10f)]
    public float timeInSec = 1f;
    public bool hide, done, clearTargetAtZero;

    private Transform[] _curvePoints;
    private int _curveCount, _index;
    private float _totalTime, _maxClamp, _angle;
    
    private void OnDrawGizmosSelected()
    {
        if (!initDone || _curvePoints == null)
            InitCurvePoints();

        #region draw curve

        for (int i = 0; i < _curvePoints.Length - 1; i += 2)
        {
            Vector3 start = _curvePoints[i].position;
            float t = 0f;

            while (t < 1)
            {
                t += 0.015625f; // 1 divided by 64

                Vector3 end = MathHelp.GetCurvePosition(
                    _curvePoints[i].position,
                    _curvePoints[i + 1].position,
                    _curvePoints[i + 2].position,
                    t);

                Debug.DrawLine(start, end, curveColor);

                start = end;
            }
        }

        #endregion

        if (hideLines) return;

        #region draw lines

        for (int i = 0; i < _curvePoints.Length; i += 2)
        {
            // first line no angle check needed
            if (i == 0)
            {
                Debug.DrawLine(_curvePoints[i].position, _curvePoints[i + 1].position, Color.white);
                continue;
            }

            // last line no angle check needed
            if (i == _curvePoints.Length - 1)
            {
                Debug.DrawLine(_curvePoints[i - 1].position, _curvePoints[i].position, Color.white);
                continue;
            }

            // check angle and set correct color
            _angle = MathHelp.AngleBetweenVector3(
                _curvePoints[i - 1].position,
                _curvePoints[i].position,
                _curvePoints[i + 1].position);

            if (Mathf.Abs(_angle) < angleTolerance)
            {
                Debug.DrawLine(_curvePoints[i - 1].position, _curvePoints[i].position, Color.white);
                Debug.DrawLine(_curvePoints[i].position, _curvePoints[i + 1].position, Color.white);
            }
            else
            {
                Debug.DrawLine(_curvePoints[i - 1].position, _curvePoints[i].position, Color.yellow);
                Debug.DrawLine(_curvePoints[i].position, _curvePoints[i + 1].position, Color.yellow);
            }
        }

        #endregion
    }

    private void Awake()
    {
        InitCurvePoints();

        hide = true;
        _totalTime = _maxClamp;
    }

    private void Update()
    {
        if (hide)
            _totalTime += Time.deltaTime * _curveCount / timeInSec;
        else
            _totalTime -= Time.deltaTime * _curveCount / timeInSec;

        #region transition end logic

        done = false;

        if (_totalTime < 0)
        {
            _totalTime = 0f;
            done = true;
        }
        if (_totalTime > _maxClamp)
        {
            _totalTime = _maxClamp;
            done = true;
        }

        #endregion

        if (target == null) return;

        // jump to next curve
        if ((int)_totalTime % 2 == 1)
        {
            if (hide) _totalTime++;
            else _totalTime--;
        }

        _index = (int)_totalTime;

        target.position = MathHelp.GetCurvePosition(
            _curvePoints[_index].position,
            _curvePoints[_index + 1].position,
            _curvePoints[_index + 2].position,
            _totalTime - _index);

        if (_totalTime == 0 && clearTargetAtZero)
                target = null;
    }

    private void InitCurvePoints()
    {
        _curvePoints = new Transform[transform.childCount];
        for (int i = 0; i < _curvePoints.Length; i++)
        {
            _curvePoints[i] = transform.GetChild(i);

            #region Child names

            if (i % 2 == 1)
                _curvePoints[i].name = "Curve";
            else
                _curvePoints[i].name = "Middle";
            if (i == 0)
                _curvePoints[i].name = "Start";
            if (i == _curvePoints.Length - 1)
                _curvePoints[i].name = "End";
            
            #endregion
        }

        _curveCount = (int)(_curvePoints.Length * 0.5f);
        _maxClamp = _curvePoints.Length - 2.001f;

        initDone = true;
    }

    public void SetCurveStartPos(Vector3 pos)
    {
        _curvePoints[0].position = pos;
    }
}
