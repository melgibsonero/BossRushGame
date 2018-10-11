using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    IEnumerator MoveToPoint(Transform toMove, Vector3 from, Vector3 to, float timeInSeconds = 1)
    {
        // prevent infinite loop
        if (timeInSeconds <= 0)
            timeInSeconds = 1;

        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / timeInSeconds;
            toMove.position = Vector3.Lerp(from, to, timer);
            yield return new WaitForEndOfFrame();
        }
    }
}
