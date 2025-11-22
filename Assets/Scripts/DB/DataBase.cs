using UnityEngine;

[CreateAssetMenu(fileName = "DataBase", menuName = "Inventory/new Database", order = 1)]
public class DataBase : ScriptableObject
{
    [System.Serializable]
    public struct InventoryItem
    {
        public string name;
        public int id;
        public Sprite icon;
        public Type type;
        public bool accumulator;
        public int maxStack;
        public string description;
        public BaseItem item;
    }

    public enum Type
    {
        Consumable,
        Equippable,
        Arable
    }
    
    public InventoryItem[] dataBase;
    
    // se llama cuando se carga el SO y cuando se lo modifica en el inspector
    
    // Cuando se haga un cambio (como quitar uno de la lista) para no modificar manualmente el ID con el número de la lista esto lo hace
    private void OnValidate()
    {
        if (dataBase != null)
        {
            for (int i = 0; i < dataBase.Length; i++)
            {
                if (dataBase[i].id != i) dataBase[i].id = i; 
            }
        }
    }
}
