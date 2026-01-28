using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    public List<Object> trails;

    public int GetTrailIndex(Trail trail)
    {
        for (int i = 0; i < trails.Count; i++)
        {
            if (trails[i].GetComponent<Trail>() == trail) return i;
        }
        return -1;
    }

    public float GetMeshHeight(Vector3 position)
    {
        MountainGenerator generator = GetComponent<MountainGenerator>();
        float closestHeight = 0.0f;
        float closestDist = Vector3.Distance(generator.verts[0], position);
        foreach (Vector3 vertex in generator.verts)
        {
            Vector2 vertex2 = new Vector2(vertex.x, vertex.z);
            Vector2 position2 = new Vector2(position.x, position.z);
            float distance = Vector2.Distance(vertex2, position2);
            if (distance < closestDist)
                closestHeight = vertex.y;
                closestDist = distance;
        }
        return closestHeight;
    }

    public void CreateNewTrail(List<Vector3> trailNodes)
    {
        // Create new trail object
        GameObject newTrailObject = new GameObject("Trail");
        newTrailObject.AddComponent<Trail>();
        Trail newTrail = newTrailObject.GetComponent<Trail>();

        for (int i = 0; i < trailNodes.Count; i++)
        {
            // Create new node object
            GameObject nodeObject = new GameObject("Node" + i);
            nodeObject.AddComponent<TrailNode>();
            TrailNode node = nodeObject.GetComponent<TrailNode>();

            // Set position and inheritance of node
            nodeObject.GetComponent<Transform>().SetParent(newTrailObject.GetComponent<Transform>());
            nodeObject.GetComponent<Transform>().position = trailNodes[i];
            node.position = trailNodes[i];
            node.trailManager = this;

            // Add node to new trail
            newTrail.nodes.Add(node);
        }

        // Add trail to list of all trails
        trails.Add(newTrail);
    }

    void Awake()
    {
        trails = FindObjectsByType<Trail>(FindObjectsSortMode.None).ToList<Object>();
    }
}
