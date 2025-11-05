using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Inventory UI")]
    public GameObject inventoryPanel;


    public InventorySlot[] toolSlots;
    public InventorySlot[] itemSlots;

    //Item info box

    public Text itemNameText;
    public Text itemDescriptionText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        RenderInventoryUI();
    }

    //Render inventory UI
    public void RenderInventoryUI()
    {
        //Get inventory items from InventoryManager
        ItemData[] inventoryItemSlots = InventoryManager.Instance.items;
        //Get tool items from InventoryManager
        ItemData[] inventoryToolSlots = InventoryManager.Instance.tools;

        //Render items in UI
        RenderInventoryPanel(inventoryItemSlots, itemSlots);

        //Render tools in UI
        RenderInventoryPanel(inventoryToolSlots, toolSlots);
    }

    //Iterate through slots and display items
    void RenderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].DisplayItem(slots[i]);
        }
    }

    public void ToggleInventoryUI()
    {
        //if the inventory panel is hidden, activate it, and vice versa
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        RenderInventoryUI();
    }
    
    public void DisplayItemInfo(ItemData itemData)
    {
        if (itemData != null)
        {
            itemNameText.text = itemData.name;
            itemDescriptionText.text = itemData.description;
            return;
        }

        itemNameText.text = "";
        itemDescriptionText.text = "";
    }
}
