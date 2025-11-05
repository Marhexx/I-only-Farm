using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

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

    //Tool slots, seed slots, etc can be added here later
    [Header("Tools")]
    //Tool Section  
    public ItemData[] tools = new ItemData[8];
    //tool in hand
    public ItemData equipedTool = null;

    [Header("Items")]
    //Item Section
    public ItemData[] items = new ItemData[8];
    //item in hand
    public ItemData selectedItem = null;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
