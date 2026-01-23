using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public String trailName;
    public Vector3[] points;

    public float trailWidth;
    public UnityEngine.Object endLandmark;

    public Trail[] connectionTrails;
    public int[] connectionIndices;

    void OnDrawGizmos()
    {
        foreach (Vector3 p in points)
        {
            Gizmos.DrawSphere(p, 0.2f);
        }

        Gizmos.DrawLineStrip(points, false);
    }
}
