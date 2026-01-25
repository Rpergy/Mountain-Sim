using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Lift : MonoBehaviour {
    public Object liftEnd;
    public int PASSENGERS = 4;
    public int LENGTH = 1000; // ft
    public int SPEED = 10; // ft/s
    public double LOADING_SPEED = 5.0; // seconds per chair
    public int NUM_CHAIRS = 100;

    Queue<Skier> queue;
    ArrayList lift;

    double boardTimer;
    int chairNum = 1;

    public void queueSkier(Skier s) {
        s.status = SkierStatus.Queued;
        queue.Enqueue(s);
        Debug.Log(s.skierName + " entered the queue");
    }

    void Start() {
        queue = new Queue<Skier>();
        lift = new ArrayList();

        boardTimer = LOADING_SPEED;
    }

    void Update() {
        boardTimer -= Time.deltaTime; // Update boarding timer

        // Board new passengers
        if (boardTimer <= 0) {
            // Fill the chair with passengers
            for (int i = 0; i < PASSENGERS; i++) {
                if (queue.Count == 0) break;
                Skier nextRider = (Skier) queue.Dequeue();
                nextRider.status = SkierStatus.Lift;
                double timeLeft = LENGTH / SPEED;
                lift.Add(new LiftRider(nextRider, timeLeft, chairNum, gameObject));
                Debug.Log(nextRider.skierName + " entered lift on chair " + chairNum);
            }

            chairNum++;
            if (chairNum > NUM_CHAIRS) chairNum = 1;

            // Reset boarding timer
            boardTimer = LOADING_SPEED;
        }

        // Update rider timers
        foreach (LiftRider s in lift) {
            s.liftTime -= Time.deltaTime;
            s.UpdateLiftPosition();
            // Remove from lift if timer elapsed
            if (s.liftTime < 0) { 
                lift.Remove(s); 
                s.skier.SetPosition(liftEnd.GetComponent<Transform>().position);
                s.skier.status = SkierStatus.Skiing;

                Debug.Log(s.skier.skierName + " exited the lift");
            }
        }
    }
}
