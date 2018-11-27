using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PCEyeTracking : MonoBehaviour {

    [SerializeField]
    Transform _leftEye;

    [SerializeField]
    Transform _rightEye;

    EventSystem Es;

    public Transform target;
    private Transform _target;

    private Vector3 _leftRotation;
    private Vector3 _rightRotation;

    private void Start()
    {
        Es = EventSystem.current;
        if(_leftEye)_leftRotation = _leftEye.eulerAngles;
        if(_rightEye)_rightRotation = _rightEye.eulerAngles;
    }

    // Update is called once per frame
    void Update () {
        _target = target;
        if (_leftEye)
        {
            _leftEye.LookAt(_target);
            _leftEye.right = -_leftEye.forward;
        }
        if (_rightEye) 
        {
            _rightEye.LookAt(_target);
            _rightEye.right = -_rightEye.forward;
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_leftEye.position, _leftEye.position + -_leftEye.right);
        Gizmos.DrawLine(_leftEye.position, _target.position);

        Gizmos.DrawLine(_rightEye.position, _rightEye.position + -_rightEye.right);
        Gizmos.DrawLine(_rightEye.position, _target.position);
    }
}
