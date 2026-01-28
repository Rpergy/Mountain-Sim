using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TrailNode : MonoBehaviour
{
    public TrailManager trailManager;
    public Vector3 position;
    public List<TrailNode> trailConnections;
    public List<LiftNode> liftConnections;

    void Awake()
    {
        trailConnections = new List<TrailNode>();
        liftConnections = new List<LiftNode>();
    }

    void Start()
    {
        position = gameObject.GetComponent<Transform>().position;
    }

    void Update()
    {
        // Vector3 newPos = new Vector3(gameObject.GetComponent<Transform>().position.x, trailManager.GetMeshHeight(position), gameObject.GetComponent<Transform>().position.z);
        // gameObject.GetComponent<Transform>().position = newPos;
        // position = newPos;
        position = GetComponent<Transform>().position;
    }

    public void choosePath(Skier s)
    {
        if (liftConnections.Count > 0)
        {
            Boolean enterLift = (UnityEngine.Random.Range(0.0f, 1.0f) > 0.75) ? true : false;
            if (enterLift || trailConnections.Count == 0)
            {
                int randLiftIndex = UnityEngine.Random.Range(0, liftConnections.Count);
                LiftNode newLiftNode = liftConnections[randLiftIndex];
                newLiftNode.GetLift().queueSkier(s);
            }
        }

        if (trailConnections.Count > 0) {
            Boolean switchTrails = (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5) ? true : false;
            Boolean lastNode = GetTrail().GetNodeIndex(this) == GetTrail().nodes.Count - 1;
            if (switchTrails || lastNode)
            {
                int randTrailIndex = UnityEngine.Random.Range(0, trailConnections.Count);
                TrailNode connectingNode = trailConnections[randTrailIndex];
                s.trailIndex = trailManager.GetTrailIndex(connectingNode.GetTrail());
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
