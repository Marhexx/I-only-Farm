using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform cameraTransform; // Main Camera (hija)
    [SerializeField] private Transform playerTransform; // Player (padre del pivot)

    [Header("Camera settings")]
    [SerializeField, Range(0.5f, 5f)] private float distance = 3f;
    [SerializeField, Range(1f, 20f)] private float smoothSpeed = 10f;
    [SerializeField, Range(50f, 500f)] private float mouseSensitivity = 200f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;

    private float pitch = 10f;
    private float desiredOrbitAngle = 0f; // se usa si quieres girar la cámara 180 con tecla
    private float currentOrbitAngle = 0f;

    private void Start()
    {
        if (cameraTransform == null) cameraTransform = transform.GetChild(0); // fallback
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        HandlePitch();
        UpdateOrbitPosition();
    }

    private void HandlePitch()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    private void UpdateOrbitPosition()
    {
        // El pivot (this.transform) debe estar posicionado localmente en la cabeza (hijo del Player).
        // La cámara es hija: la posicion deseada respecto al pivot
        Quaternion orbitRot = Quaternion.Euler(pitch, playerTransform.eulerAngles.y + currentOrbitAngle, 0f);
        Vector3 desiredPos = transform.position - orbitRot * Vector3.forward * distance;

        // Suavizado de posición y mirar al pivot
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPos, smoothSpeed * Time.deltaTime);
        cameraTransform.LookAt(transform.position + Vector3.up * 0.15f); // mira al pivot (ligero offset)
    }

    // Opcional: función pública para forzar rotación orbital (ej. 180°)
    public void SetOrbitAngle(float angle)
    {
        desiredOrbitAngle = angle;
        currentOrbitAngle = Mathf.LerpAngle(currentOrbitAngle, desiredOrbitAngle, 10f * Time.deltaTime);
    }
}