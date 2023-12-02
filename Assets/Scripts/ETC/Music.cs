using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music insatance;

    void Awake()
    {
        if (!insatance) {
            insatance = this;
            DontDestroyOnLoad(insatance);
        } else {
            Destroy(gameObject);
        }
    }
}
