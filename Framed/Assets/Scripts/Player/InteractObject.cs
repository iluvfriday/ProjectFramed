using UnityEngine;

public class InteractObject : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask interactLayer;

    [SerializeField]
    private float interactionDistance = 3f;

    private IInteractableObject lastHighlightedObject;

    void Update()
    {
        RaycastHit hit;
        if (
            Physics.Raycast(
                cam.transform.position,
                cam.transform.forward,
                out hit,
                interactionDistance,
                interactLayer
            )
        )
        {
            IInteractableObject iObject = hit.collider.GetComponent<IInteractableObject>();
            Debug.Log($"Object name: {hit.collider.name}");

            if (iObject != null)
            {
                if (lastHighlightedObject != iObject)
                {
                    if (lastHighlightedObject != null)
                    {
                        lastHighlightedObject.SetOutline(false);
                    }

                    iObject.SetOutline(true);
                    lastHighlightedObject = iObject;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    iObject.ToggleObject();
                }
            }
        }
        else
        {
            if (lastHighlightedObject != null)
            {
                lastHighlightedObject.SetOutline(false);
                lastHighlightedObject = null;
            }
        }
    }
}
