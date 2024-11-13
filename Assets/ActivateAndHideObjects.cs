using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAndHideObjects : MonoBehaviour
{
    // 目标激活对象 (物体 B)
    public GameObject targetObject;

    // 需要隐藏的对象 (物体 C 和 D)
    public GameObject objectC;
    public GameObject objectD;
    public GameObject objectE;


    void OnMouseDown()
    {
        // 激活目标对象 (物体 B)
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }

        // 隐藏物体 C
        if (objectC != null)
        {
            objectC.SetActive(false);
        }

        // 隐藏物体 D
        if (objectD != null)
        {
            objectD.SetActive(false);
        }
        // 隐藏物体 E
         if (objectE != null)
        {
            objectE.SetActive(false);
        }
    }
}
