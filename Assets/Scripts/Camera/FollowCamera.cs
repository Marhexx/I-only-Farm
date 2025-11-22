
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private int sensibility;
    private float _rotationX;
    private Transform _playerTrf;
    private Transform _cameraTrf;


    private void Awake()
    {
        _playerTrf = gameObject.transform.parent;
        _cameraTrf = transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;        
        float mouseY = Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;
        
        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        _playerTrf.Rotate(Vector3.up * mouseX);
    }
}
