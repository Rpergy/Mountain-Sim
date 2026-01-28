using UnityEngine;
using System;
using Unity.VisualScripting;

public class LiftRider : IComparable<LiftRider> {
    public Skier skier;
    public double liftTime;
    double initialTime;
    public UnityEngine.Object currentLift;
    int chairNum;

    public LiftRider(Skier skier, double liftTime, int chairNum, UnityEngine.Object currentLift) {
        this.skier = skier;
        this.liftTime = liftTime;
        this.initialTime = liftTime;
        this.chairNum = chairNum;
        this.currentLift = currentLift;
    }

    public int CompareTo(LiftRider other)
    {
        if (other == null) return 1;
        if (other.liftTime > this.liftTime) return -1;
        else if (other.liftTime < this.liftTime) return 1;
        else return 0;
    }

    public void UpdateLiftPosition()
    {
        if (skier.status != SkierStatus.Lift) return;
        Vector3 startPos = currentLift.GetComponent<Transform>().position;
        Vector3 endPos = currentLift.GetComponent<Lift>().liftEnd.GetComponent<Transform>().position;
        double t = 1 - (liftTime / initialTime);
        Vector3 newPosition = Vector3.Lerp(startPos, endPos, (float)t);
        skier.SetPosition(newPosition);
    }

    public string toString()
    {
        return skier.toString() + " (" + chairNum + "): " + liftTime + "s";
    }
}
