using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrailDrawing : MonoBehaviour
{
    private Camera cam;
    public Object mountain;

    public Vector2 mousePos;

    public List<Vector3> trailNodes;

    Ray ray;

    void Start()
    {
        cam = Camera.main;
        mousePos = new Vector2();
        trailNodes = new List<Vector3>();
    }

    void FixedUpdate()
    {
        var mouse = Mouse.current;

        mousePos = mouse.position.ReadValue();
        if (mouse.leftButton.isPressed)
            SendTrailRaycast();
        if (mouse.leftButton.wasReleasedThisFrame) { // Done with trail
            mountain.GetComponent<TrailManager>().CreateNewTrail(trailNodes);
            trailNodes = new List<Vector3>();
        }
    }

    public void SendTrailRaycast()
    {
        ray = cam.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 0));
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.white);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider == mountain.GetComponent<MeshCollider>())
        {
            trailNodes.Add(hit.point);
        }
    }
}
