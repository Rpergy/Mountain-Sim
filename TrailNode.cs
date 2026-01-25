using System;
using UnityEngine;

[ExecuteAlways]
public class TrailNode : MonoBehaviour
{
    public Vector3 position;
    public TrailNode[] connections;

    void Start()
    {
        position = gameObject.GetComponent<Transform>().position;
    }

    void Update()
    {
        position = gameObject.GetComponent<Transform>().position;
    }

    public TrailNode choosePath()
    {
        Boolean switchTrails = (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5) ? true : false;
        if (switchTrails && connections.Length > 0)
        {
            int randTrailIndex = UnityEngine.Random.Range(0, connections.Length-1);
            TrailNode connectingNode = connections[randTrailIndex];
            return connectingNode;
        }
        return null;
    }

    public Trail GetTrail()
    {
        return gameObject.GetComponentInParent<Trail>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.orange;
        Gizmos.DrawSphere(position, 0.2f);
    }
}
