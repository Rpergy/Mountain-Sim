using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class TrailManager : MonoBehaviour
{
    public Object[] trails;

    public int GetTrailIndex(Trail trail)
    {
        for (int i = 0; i < trails.Length; i++)
        {
            if (trails[i].GetComponent<Trail>().trailName.Equals(trail.trailName)) return i;
        }
        return -1;
    }
}
