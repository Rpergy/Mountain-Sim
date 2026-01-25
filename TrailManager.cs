using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    public static Object[] trails;

    public static int GetTrailIndex(Trail trail)
    {
        for (int i = 0; i < trails.Length; i++)
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

    void Awake()
    {
        trails = FindObjectsByType<Trail>(FindObjectsSortMode.None);
    }
}
