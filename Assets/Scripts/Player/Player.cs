using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform itemSpawn;
    private Inventory _inventory;

    private PlayerController _playerController;
    private CameraController _cameraController;
    //private FollowCamera _followCamera;

    void Start()
    {
        _inventory = Inventory.Instance;
        _playerController = GetComponent<PlayerController>();

        // Si están en hijos, los obtenemos con GetComponentInChildren
        _cameraController = GetComponentInChildren<CameraController>();
      //  _followCamera = GetComponentInChildren<FollowCamera>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _inventory.ToogleInventory();

            bool canMove = !_inventory.isOpen;
            
            // El PlayerController maneja el movimiento, rotación y cursor.
            _playerController.SetCanMoveCamera(canMove);

            // Habilita/deshabilita la cámara.
            if (_cameraController) _cameraController.enabled = canMove;

            //if (_followCamera) _followCamera.enabled = canMove;
        }
    }
}
