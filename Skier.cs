using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

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
    public int trailIndex;
    public int pointIndex;

    public float t = 0.0f;

    public Skier(string name, SkierStatus status) {
        this.skierName = name;
        this.status = status;
    }

    public string toString()
    {
        return skierName;
    }

    public void SetPosition(Transform transform)
    {
        GetComponent<Transform>().position = transform.position;
    }

    void Start()
    {
        Trail currentTrail = trailManager.trails[trailIndex].GetComponent<Trail>();
        GetComponent<Transform>().position = currentTrail.points[pointIndex];
    }

    void Update()
    {
        if (status == SkierStatus.Skiing) UpdateSkiing();
        if (status == SkierStatus.Lift) UpdateLift();
    }

    void UpdateSkiing()
    {
        Trail currentTrail = trailManager.trails[trailIndex].GetComponent<Trail>();
        if (t < 1.0f)
        {
            // Calculate the new position
            Vector3 newPosition = Vector3.Lerp(currentTrail.points[pointIndex], currentTrail.points[pointIndex + 1], t);
            GetComponent<Transform>().position = newPosition;
            t += Time.deltaTime * speed / Vector3.Distance(currentTrail.points[pointIndex], currentTrail.points[pointIndex+1]);
        }
        else
        {
            t = 0; // Reset the lerp timer
            pointIndex++;
            // Once we've reached the end of the trail, check if there are any landmarks to enter
            if (pointIndex == currentTrail.points.Length-1 && currentTrail.endLandmark != null)
            {
                if (currentTrail.endLandmark.GetComponent<Lift>() != null)
                {
                    currentTrail.endLandmark.GetComponent<Lift>().queueSkier(this);
                }
            }
        }
    }

    void UpdateLift()
    {
        
    }
}
