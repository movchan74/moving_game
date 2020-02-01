using System.Collections;
using System.Collections.Generic;
using HyperCasual.Extensions;
using UnityEngine;

public class MidSegment : MonoBehaviour
{
    public Transform Platform;

    public void SetLength(float length)
    {
        Platform.SetLocalScaleZ(length);
    }
}
