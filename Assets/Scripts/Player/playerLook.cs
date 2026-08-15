using UnityEngine;

public class playerLook : MonoBehaviour
{
    [SerializeField] private Transform playerBody;

    [SerializeField] private float mouseSensitivity = 150f;
    [SerializeField] private float maxLookAngle = 85f;

    private float verticalRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Guides camera in a direction based off movement and sensitivity
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Looking vertical rotates around holder
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);

        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
