using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    public Object[] trails;

    public int getTrailIndex(Trail trail)
    {
        for (int i = 0; i < trails.Length; i++)
        {
            if (trails[i].GetComponent<Trail>() == trail) return i;
        }
        return -1;
    }

    void Awake()
    {
        trails = FindObjectsByType<Trail>(FindObjectsSortMode.None);
    }
}
