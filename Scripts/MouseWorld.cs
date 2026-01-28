using UnityEngine;
using UnityEngine.InputSystem;

public class MouseWorld : MonoBehaviour
{
    private Camera cam;
    public MeshCollider mountainMesh;

    public Vector2 mousePos;

    Ray ray;

    void Start()
    {
        cam = Camera.main;
        mousePos = new Vector2();
    }

    void Update()
    {
        var mouse = Mouse.current;

        mousePos = mouse.position.ReadValue();

        ray = cam.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 0));
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.white);
    }

    void OnDrawGizmos()
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider == mountainMesh)
        {
            Gizmos.DrawSphere(hit.point, 1f);
        }
    }
}
