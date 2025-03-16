using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask doorLayer;

    [SerializeField]
    private float interactionDistance = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (
                Physics.Raycast(
                    cam.transform.position,
                    cam.transform.forward,
                    out hit,
                    interactionDistance,
                    doorLayer
                )
            )
            {
                Door door = hit.collider.GetComponent<Door>();
                if (door != null)
                {
                    Debug.Log("Door toggle");
                    door.ToggleDoor();
                }
            }
        }
    }
}
