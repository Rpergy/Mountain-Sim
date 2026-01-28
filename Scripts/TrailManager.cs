using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    public List<Trail> trails;

    static int trailCount = 1;

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
        GameObject newTrailObject = new GameObject("Trail " + trailCount);

        newTrailObject.AddComponent<Trail>();
        Trail newTrail = newTrailObject.GetComponent<Trail>();

        for (int i = 0; i < trailNodes.Count; i++)
        {
            // Create new node object
            GameObject nodeObject = new GameObject(trailCount + " Node " + i);
            nodeObject.AddComponent<TrailNode>();
            TrailNode node = nodeObject.GetComponent<TrailNode>();

            // Set position and inheritance of node
            nodeObject.GetComponent<Transform>().SetParent(newTrailObject.GetComponent<Transform>());
            nodeObject.GetComponent<Transform>().position = trailNodes[i];
            node.position = trailNodes[i];
            node.trailManager = this;

            // Calculate trail bounding box
            if (trailNodes[i].x > newTrail.max.x) newTrail.max.x = trailNodes[i].x;
            if (trailNodes[i].x < newTrail.min.x) newTrail.min.x = trailNodes[i].x;
            if (trailNodes[i].y > newTrail.max.y) newTrail.max.y = trailNodes[i].y;
            if (trailNodes[i].y < newTrail.min.y) newTrail.min.y = trailNodes[i].y;

            // Add node to new trail
            newTrail.nodes.Add(node);
        }

        newTrail.OptimizeTrail();
        newTrail.CalculateRating();

        CalculateStartConnections(newTrail);

        // Add trail to list of all trails
        trails.Add(newTrail);
        trailCount++;
    }

    void CalculateStartConnections(Trail trail)
    {
        foreach (Trail testTrail in trails)
        {
            if (testTrail == trail) continue;

            // Overlap = (xmax1 > xmin2 && xmin1 < xmax2 && ymax1 > ymin2 && ymin1 < ymax2)
            // Skip the trail if their bounding boxes don't overlap
            if (!(trail.max.x >= testTrail.min.x && trail.min.x <= testTrail.max.x && trail.max.y >= testTrail.min.y && trail.min.y <= testTrail.max.y))
                continue;
            
            float searchRadius = 2;
            TrailNode node = trail.nodes[0];

            TrailNode closestNode = null;
            float closestDist = float.MaxValue;
            foreach (TrailNode testNode in testTrail.nodes)
            {
                float dist = Vector3.Distance(node.position, testNode.position);
                if (dist < closestDist && dist <= searchRadius && testNode != node)
                {
                    closestNode = testNode;
                    closestDist = dist;
                }
            }
            
            if (closestNode != null)
            {
                Debug.Log("Creating connection at" + closestNode);
                closestNode.trailConnections.Add(node);
                node.position = closestNode.position;
            }
        }
    }

    void Awake()
    {
        trails = FindObjectsByType<Trail>(FindObjectsSortMode.None).ToList<Trail>();
    }
}
