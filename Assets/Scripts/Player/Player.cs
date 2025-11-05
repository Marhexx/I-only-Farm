using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform itemSpawn;
    private Inventory _inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inventory = Inventory.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E)) _inventory.ToogleInventory();
    }
}
