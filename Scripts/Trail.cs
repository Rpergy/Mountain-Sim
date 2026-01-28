using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class Trail : MonoBehaviour
{
    public List<TrailNode> nodes;

    void Awake()
    {
        nodes = new List<TrailNode>();
    }

    public int GetNodeIndex(TrailNode node)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (node == nodes[i]) return i;
        }
        return -1;
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
