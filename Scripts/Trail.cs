using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class Trail : MonoBehaviour
{
    public TrailNode[] nodes;

    public int GetNodeIndex(TrailNode node)
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            if (node == nodes[i]) return i;
        }
        return -1;
    } 

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        foreach (TrailNode n in nodes)
        {
            Gizmos.DrawSphere(n.position, 0.2f);
        }

        for (int i = 0; i < nodes.Length-1; i++)
        {
            Vector3 pos1 = nodes[i].position;
            Vector3 pos2 = nodes[i+1].position;
            Gizmos.DrawLine(pos1, pos2);
        }
    }
}
