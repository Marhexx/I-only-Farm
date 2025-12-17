using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Mouse")]
    [SerializeField, Range(50, 500)] private float mouseSensitivity = 200f;

    private GameObject _panelEsc;
    private Rigidbody _rb;
    private Animator _animator;
    private bool _canMoveCam = true;

    private void Awake()
    {
        _panelEsc = GameObject.Find("EscPanel");
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true; // evita que la física rote el cuerpo
        _animator = GetComponent<Animator>();
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
        
        if(_panelEsc.activeSelf)  Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX); // rota el PLAYER (yaw)
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = (transform.forward * vertical + transform.right * horizontal).normalized;

        if (dir.sqrMagnitude > 0.001f)
        {
            _rb.linearVelocity = new Vector3(dir.x * moveSpeed, _rb.linearVelocity.y, dir.z * moveSpeed);
            _animator.SetBool("Moving", true);
        }
        else
        {
            _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f); // detiene en seco
            _animator.SetBool("Moving", false);
        }
            
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