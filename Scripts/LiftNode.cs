using System;
using UnityEngine;

[ExecuteAlways]
public class LiftNode : MonoBehaviour
{
    public TrailManager trailManager;
    public Vector3 position;
    public TrailNode[] trailConnections;
    public LiftNode[] liftConnections;

    public Lift GetLift()
    {
        return gameObject.GetComponent<Lift>();
    }

    public void choosePath(Skier s)
    {
        if (trailConnections.Length > 0)
        {
            int randTrailIndex = UnityEngine.Random.Range(0, trailConnections.Length);
            TrailNode newTrail = trailConnections[randTrailIndex];
            s.trailIndex = trailManager.GetTrailIndex(newTrail.GetTrail());
            s.pointIndex = newTrail.GetTrail().GetNodeIndex(newTrail);
        }
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
}
