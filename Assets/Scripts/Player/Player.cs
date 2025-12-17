using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private KeyCode openInventoryKey;
    [SerializeField] private KeyCode showPanelEsc;
    
    [SerializeField] private GameObject escPanel;
    
    public Transform itemSpawn;
    private Inventory _inventory;

    private PlayerController _playerController;
    private CameraController _cameraController;
    //private FollowCamera _followCamera;

    void Start()
    {
        escPanel.SetActive(false);
        _inventory = Inventory.Instance;
        _playerController = GetComponent<PlayerController>();

        // Si están en hijos, los obtenemos con GetComponentInChildren
        _cameraController = GetComponentInChildren<CameraController>();
      //  _followCamera = GetComponentInChildren<FollowCamera>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(openInventoryKey))
        {
            _inventory.ToogleInventory();

            bool canMove = !_inventory.isOpen;
            
            // El PlayerController maneja el movimiento, rotación y cursor.
            _playerController.SetCanMoveCamera(canMove);

            // Habilita/deshabilita la cámara.
            if (_cameraController) _cameraController.enabled = canMove;

            //if (_followCamera) _followCamera.enabled = canMove;
        }

        if (Input.GetKeyUp(showPanelEsc))
        {
           
            escPanel.SetActive(true);
        }
    }
}
