using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneGizmos : MonoBehaviour {

    Transform[] _bones;
    [SerializeField]
    private float radius = 0.2f;
    [SerializeField]
    private Vector3 footsteparea;

    [SerializeField]
    private bool ShowGizmos = true;
    

    private void OnDrawGizmos()
    {
        
        _bones = GetComponentsInChildren<Transform>(true);
        foreach (Transform bone in _bones)
        {
            if (ShowGizmos && !bone.name.Contains("Weapon"))
            {
                if (bone.name.Contains("Tip") || bone.name.Contains("Nub"))
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(bone.position, radius * 0.6f);
                    bone.gameObject.SetActive(false);
                }
                else if (bone.name.Contains("Footsteps"))
                {
                    Vector3 offset = new Vector3(0, 0, footsteparea.z);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawCube(bone.position - offset/2f, footsteparea);
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(bone.position + offset / 2f, footsteparea);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(bone.position, radius);
                    if (bone.parent)
                    {
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawLine(bone.position, bone.parent.position);
                    }
                    if (bone.name.Contains("L"))
                    {
                        IconManager.SetIcon(bone.gameObject, IconManager.Icon.DiamondGreen);
                    }
                    else if (bone.name.Contains("R"))
                    {
                        IconManager.SetIcon(bone.gameObject, IconManager.Icon.DiamondBlue);
                    }
                    else
                    {
                        IconManager.SetIcon(bone.gameObject, IconManager.Icon.DiamondRed);
                    }
                    continue;
                }
            }
            else
            {
                IconManager.RemoveIcon(bone.gameObject);
            }
        }
    }
}
