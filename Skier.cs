using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum SkierStatus {
    Skiing,
    Queued,
    Lift,
    Break
}

public class Skier {
    public string name;
    public SkierStatus status;

    public Skier(string name, SkierStatus status) {
        this.name = name;
        this.status = status;
    }

    public string toString()
    {
        return name;
    }
}
