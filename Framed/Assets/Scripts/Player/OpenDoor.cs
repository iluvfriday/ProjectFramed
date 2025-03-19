using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask doorLayer;

    [SerializeField]
    private float interactionDistance = 3f;

    private Door lastHighlightedDoor;

    void Update()
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
                if (lastHighlightedDoor != door)
                {
                    if (lastHighlightedDoor != null)
                    {
                        lastHighlightedDoor.SetOutline(false);
                    }

                    door.SetOutline(true);
                    lastHighlightedDoor = door;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    door.ToggleDoor();
                }
            }
        }
        else
        {
            if (lastHighlightedDoor != null)
            {
                lastHighlightedDoor.SetOutline(false);
                lastHighlightedDoor = null;
            }
        }
    }
}
