using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum Rating
{
    NONE,
    GREEN,
    BLUE,
    BLACK,
    DBLACK
}

[ExecuteAlways]
public class Trail : MonoBehaviour
{
    public List<TrailNode> nodes;
    public Rating trailRating;
    public double avgGrade;

    public Vector2 min, max;

    void Awake()
    {
        nodes = new List<TrailNode>();
        trailRating = Rating.NONE;
        min = new Vector2(float.MaxValue, float.MaxValue);
        max = new Vector2(float.MinValue, float.MinValue);
    }

    public int GetNodeIndex(TrailNode node)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (node == nodes[i]) return i;
        }
        return -1;
    } 

    // TODO: Change from using average to using maximum grade for more accurate trail ratings
    public void CalculateRating()
    {
        Vector3 firstNode = nodes[0].position;
        Vector3 lastNode = nodes[nodes.Count - 1].position;
        double rise = Math.Abs(lastNode.y - firstNode.y);
        double run = Vector2.Distance(new Vector2(firstNode.x, firstNode.z), new Vector2(lastNode.x, lastNode.z));
        avgGrade = rise / run * 100;
        if (avgGrade > 15 && avgGrade < 25) trailRating = Rating.BLUE;
        else if (avgGrade >= 25 && avgGrade < 40) trailRating = Rating.BLACK;
        else if (avgGrade >= 40) trailRating = Rating.DBLACK;
        else trailRating = Rating.GREEN;
    }

    // Trail optimization should be done BEFORE any trail connections are made
    public void OptimizeTrail()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        foreach (TrailNode n in nodes)
        {
            Gizmos.DrawSphere(n.position, 0.2f);
        }

        for (int i = 0; i < nodes.Count-1; i++)
        {
            Vector3 pos1 = nodes[i].position;
            Vector3 pos2 = nodes[i+1].position;
            Gizmos.DrawLine(pos1, pos2);
        }
    }
}
