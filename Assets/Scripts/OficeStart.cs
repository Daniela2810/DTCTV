using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OficeStart : MonoBehaviour
{
    void Start()
    {
        AnimationManager.instance.StartAnimation(7);
    }
}
