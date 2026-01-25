using System;
using UnityEngine;

[ExecuteAlways]
public class TrailNode : MonoBehaviour
{
    public TrailManager trailManager;
    public Vector3 position;
    public TrailNode[] trailConnections;
    public LiftNode[] liftConnections;

    void Start()
    {
        position = gameObject.GetComponent<Transform>().position;
    }

    void Update()
    {
        Vector3 newPos = new Vector3(gameObject.GetComponent<Transform>().position.x, trailManager.GetMeshHeight(position), gameObject.GetComponent<Transform>().position.z);
        gameObject.GetComponent<Transform>().position = newPos;
        position = newPos;
    }

    public void choosePath(Skier s)
    {
        if (liftConnections.Length > 0)
        {
            Boolean enterLift = (UnityEngine.Random.Range(0.0f, 1.0f) > 0.75) ? true : false;
            if (enterLift || trailConnections.Length == 0)
            {
                int randLiftIndex = UnityEngine.Random.Range(0, liftConnections.Length);
                LiftNode newLiftNode = liftConnections[randLiftIndex];
                newLiftNode.GetLift().queueSkier(s);
            }
        }

        if (trailConnections.Length > 0) {
            Boolean switchTrails = (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5) ? true : false;
            Boolean lastNode = GetTrail().GetNodeIndex(this) == GetTrail().nodes.Length - 1;
            if (switchTrails || lastNode)
            {
                int randTrailIndex = UnityEngine.Random.Range(0, trailConnections.Length);
                TrailNode connectingNode = trailConnections[randTrailIndex];
                s.trailIndex = TrailManager.GetTrailIndex(connectingNode.GetTrail());
                s.pointIndex = connectingNode.GetTrail().GetNodeIndex(connectingNode);
            }
        }
    }

    public Trail GetTrail()
    {
        return gameObject.GetComponentInParent<Trail>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.orange;
        Gizmos.DrawSphere(position, 0.21f);
    }
}
