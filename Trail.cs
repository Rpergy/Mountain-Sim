using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public Vector3[] points;
    public Dictionary<int, Trail> connections;
    public float trailWidth;
    public Object endLandmark;

    public Trail(Vector3[] points, Dictionary<int, Trail> connections, float trailWidth)
    {
        this.points = points;
        this.connections = connections;
        this.trailWidth = trailWidth;
    }

    void OnDrawGizmos()
    {
        foreach (Vector3 p in points)
        {
            Gizmos.DrawSphere(p, 0.2f);
        }

        Gizmos.DrawLineStrip(points, false);
    }
}
