using Unity.VisualScripting;
using UnityEngine;

public enum SkierStatus {
    Skiing,
    Queued,
    Lift,
    Break
}

public class Skier : MonoBehaviour 
{
    public TrailManager trailManager;
    public string skierName;
    public SkierStatus status;
    public float speed = 1.0f;

    public float t = 0.0f;

    public int trailIndex;
    public int pointIndex;

    public Skier(string name, SkierStatus status) {
        this.skierName = name;
        this.status = status;
    }

    public string toString()
    {
        return skierName;
    }

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
    }

    public void SetPosition(Vector3 position)
    {
        GetComponent<Transform>().position = position;
    }

    void Update()
    {
        if (status == SkierStatus.Skiing) UpdateSkiing();
    }

    void UpdateSkiing()
    {
        Trail currentTrail = trailManager.trails[trailIndex].GetComponent<Trail>();

        Vector3 start = currentTrail.nodes[pointIndex].position;
        Vector3 end = currentTrail.nodes[pointIndex + 1].position;
        t += Time.deltaTime / Vector3.Distance(start, end) * speed;
        if (t < 1.0)
        {
            Vector3 newPos = Vector3.Lerp(start, end, t);
            gameObject.GetComponent<Transform>().position = newPos;
        }
        else
        {
            t = 0;
            pointIndex++;

            TrailNode currentNode = currentTrail.nodes[pointIndex];
            currentNode.choosePath(this);
        }
    }
}
