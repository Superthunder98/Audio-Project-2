using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splinemover : MonoBehaviour
{
    public Spline spline;
    public Transform followObj;

    private Transform thisTransform;

    private void Start()
    {
        thisTransform = transform;
    }

    private void Update()
    {
        thisTransform.position = spline.WhereOnSpline(followObj.position);
    }

}

