using System;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    public float detectRange = 4.5f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        try
        {
            if (CheckInteractableObject(out RaycastHit hit))
                EnableCursor();
            else
                DisableCursor();
        }
        catch (NullReferenceException) { }
    }

    private bool CheckInteractableObject(out RaycastHit hit)
    {
        Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out hit,
            detectRange
        );

        if (hit.collider.CompareTag("Interactable Object"))
        {
            if (Vector3.Distance(transform.position, hit.transform.position) < detectRange)
            {
                return true;
            }
        }
        return false;
    }

    private void EnableCursor()
    {
        Cursor.visible = true;
    }

    private void DisableCursor()
    {
        Cursor.visible = false;
    }
}
