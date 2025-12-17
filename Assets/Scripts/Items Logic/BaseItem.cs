using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public abstract class BaseItem : MonoBehaviour
// Una clase abstracta es una clase en particular que usamos como blueprint o como base (de ahí el nombre BASEitem), de la cual nosotros vamos a heredar futuros scripts 
// Cuando más adelante creemos los items, no de UI, sino del juego, van a heredad de BaseItem para usar todos los métodos de este script. 
// Funcionamiento similar a la de una interfaz, con la diferencia de que con una clase abstracta no estamos forzados a instanciarla como sí es el caso de primero.
{
    public int id;
    public int quantity = 1;
    [ReadOnly] public DataBase.InventoryItem itemData;
    
    void Awake()
    {
        // Aseguramos que el inventario se haya inicializado antes de intentar usarlo.
        if (Inventory.Instance != null)
        {
            SetDataById(id, quantity);
        }
    }
    void Start()
    {
        SetDataById(id, quantity);
    }

    public void SetDataById(int id, int quantity = 1)
    {
        itemData.id = id;
        itemData.accumulator = Inventory.Instance.db.dataBase[id].accumulator;
        itemData.description =  Inventory.Instance.db.dataBase[id].description;
        itemData.icon = Inventory.Instance.db.dataBase[id].icon;
        itemData.name = Inventory.Instance.db.dataBase[id].name;
        itemData.type = Inventory.Instance.db.dataBase[id].type;
        itemData.maxStack  = Inventory.Instance.db.dataBase[id].maxStack;
        itemData.item =  Inventory.Instance.db.dataBase[id].item;
        
        this.quantity = quantity;
    }

    
    // Este método Use() al ser abstracto no nos obliga a llenarlo con una lógica ahorita, esto se hará para cada item que se cree y le asignemos a cada uno su lógica propia. 
    // Así que tendremos para cada items creado un método Use(), como si fuera un papel en blanco, para escribirle nuestra lógica a disposición
    public abstract void Use();

    public void OnTriggerEnter(Collider other)
    // Lo que hacemos aquí es detectar que si el player tocó alguno de estos items, sean añadidos al inventario
    {
        if (other.transform.CompareTag("Player"))
        {
            Inventory.Instance.AddItem(id, quantity);
            Destroy(this.gameObject);
        }   
    }
}
