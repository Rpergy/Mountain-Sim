using UnityEngine;
using Unity.VisualScripting;
using System;
using System.Collections;
using System.Collections.Generic;

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

    public void SetPosition(Vector3 position)
    {
        GetComponent<Transform>().position = position;
    }

    void Start()
    {
        Trail currentTrail = trailManager.trails[trailIndex].GetComponent<Trail>();
        GetComponent<Transform>().position = currentTrail.points[pointIndex];
    }

    void Update()
    {
        if (status == SkierStatus.Skiing) UpdateSkiing();
    }

    void UpdateSkiing()
    {
        Trail currentTrail = trailManager.trails[trailIndex].GetComponent<Trail>();
        if (t < 1.0f)
        {
            // Calculate the new position
            Vector3 startPos = currentTrail.points[pointIndex];
            Vector3 shift = new Vector3(currentTrail.trailWidth * (float)Math.Sin(t * 4 * Math.PI), 0, 0);
            Vector3 endPos = currentTrail.points[pointIndex + 1] + shift;
            Vector3 newPosition = Vector3.Lerp(startPos, endPos, t);
            GetComponent<Transform>().position = newPosition;
            t += Time.deltaTime * speed / Vector3.Distance(currentTrail.points[pointIndex], currentTrail.points[pointIndex+1]);
        }
        else // Reach trail intersection point
        {
            t = 0; // Reset the lerp timer
            pointIndex++;

            ArrayList intersections = new ArrayList();

            for (int i = 0; i < currentTrail.connectionIndices.Length; i++)
            {
                if (currentTrail.connectionIndices[i] == pointIndex)
                {
                    intersections.Add(currentTrail.connectionTrails[i]);
                }
            }

            Boolean stayOnTrail = (UnityEngine.Random.Range(0.0f, 1.0f) < 0.5) ? true : false;

            if (!stayOnTrail && intersections.Count > 0)
            {
                int randTrail = UnityEngine.Random.Range(0, intersections.Count);
                int randTrailIndex = trailManager.GetTrailIndex((Trail)intersections[randTrail]);
                
                trailIndex = randTrailIndex;
                pointIndex = 0;
            }

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
}
