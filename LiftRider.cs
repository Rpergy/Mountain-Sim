using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LiftRider : IComparable<LiftRider> {
    public Skier skier;
    public double liftTime;
    int chairNum;

    public LiftRider(Skier skier, double liftTime, int chairNum) {
        this.skier = skier;
        this.liftTime = liftTime;
        this.chairNum = chairNum;
    }

    public int CompareTo(LiftRider other)
    {
        if (other == null) return 1;
        if (other.liftTime > this.liftTime) return -1;
        else if (other.liftTime < this.liftTime) return 1;
        else return 0;
    }

    public string toString()
    {
        return skier.toString() + " (" + chairNum + "): " + liftTime + "s";
    }
}
