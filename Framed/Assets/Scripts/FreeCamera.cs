using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public float sensitivity = 10f;

    void Update()
    {
        var c = Camera.main.transform;
        c.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        c.Rotate(-Input.GetAxis("Mouse Y") * sensitivity, 0, 0);
        c.Rotate(0, 0, -Input.GetAxis("QandE") * 90 * Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
    }
}
