using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Mouse")]
    [SerializeField, Range(50, 500)] private float mouseSensitivity = 200f;

    private Rigidbody _rb;
    
    private bool _canMoveCam = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true; // evita que la física rote el cuerpo
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!_canMoveCam) return;
        HandleRotation();
        HandleMovement();
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX); // rota el PLAYER (yaw)
    }

    private void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = (transform.forward * v + transform.right * h).normalized;

        if (dir.sqrMagnitude > 0.001f)
            _rb.linearVelocity = new Vector3(dir.x * moveSpeed, _rb.linearVelocity.y, dir.z * moveSpeed);
        else
            _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f); // detiene en seco
    }

    public void SetCanMoveCamera(bool allowMovement)
    {
        _canMoveCam = allowMovement;

        if (allowMovement)
        {
            // Se permite el movimiento: Bloqueamos el cursor.
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        
            // No es necesario modificar la velocidad aquí, ya que HandleMovement se ejecutará.
        }
        else
        {
            // Se prohíbe el movimiento (Inventario Abierto): Desbloqueamos el cursor.
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        
            // DETENER EL DESPLAZAMIENTO
            // Forzamos la velocidad horizontal (X y Z) a cero, manteniendo la gravedad (Y).
            _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f); 
        }
    }
}