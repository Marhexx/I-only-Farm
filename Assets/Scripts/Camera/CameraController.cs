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

    [Header("Look Behind")]
    [SerializeField] private KeyCode lookBehindKey = KeyCode.B; // tecla para mirar atrás
    [SerializeField, Range(2f, 15f)] private float lookBehindSmooth = 8f; // suavizado del giro 180°

    private float pitch = 10f;
    private float yawOffset = 0f; // desplazamiento lateral de cámara (rotación orbital)
    private float targetYawOffset = 0f; // para transición suave
    private bool lookingBehind = false;

    private void Start()
    {
        if (cameraTransform == null) 
            cameraTransform = transform.GetChild(0); // callback automático
    }

    private void LateUpdate()
    {
        HandlePitch();
        HandleLookBehind();
        UpdateOrbitPosition();
    }

    private void HandlePitch()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    private void HandleLookBehind()
    {
        // Al presionar la tecla, alternar el estado de "mirar atrás"
        if (Input.GetKeyDown(lookBehindKey))
            lookingBehind = true;
        else if (Input.GetKeyUp(lookBehindKey))
            lookingBehind = false;

        // Definir yaw objetivo (0 o 180)
        targetYawOffset = lookingBehind ? 180f : 0f;
        yawOffset = Mathf.LerpAngle(yawOffset, targetYawOffset, lookBehindSmooth * Time.deltaTime);
    }

    private void UpdateOrbitPosition()
    {
        // Calcula rotación orbital combinando pitch + yaw del jugador + offset de cámara
        Quaternion orbitRot = Quaternion.Euler(pitch, playerTransform.eulerAngles.y + yawOffset, 0f);

        // Posición deseada de la cámara
        Vector3 desiredPos = transform.position - orbitRot * Vector3.forward * distance;

        // Suavizado de posición
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPos, smoothSpeed * Time.deltaTime);

        // Cámara mira hacia el pivot (ligero offset hacia arriba)
        cameraTransform.LookAt(transform.position + Vector3.up * 0.15f);
    }
}