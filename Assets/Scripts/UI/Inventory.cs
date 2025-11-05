using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
   public GraphicRaycaster graphRay;
   public DataBase db;
   public bool isOpen;
   
   private int _slotsCount = 24; // Slots que aparecerán en el UI del inventario
   
   [SerializeField] private Player player;
   [SerializeField] private Transform slotPrefab;
   [SerializeField] private Transform itemPrefab;
   
   public DescriptionUI descriptionUI;

   [SerializeField] private List<ItemUI> items = new List<ItemUI>();
   [SerializeField] private Transform slotsContainer;
   
   private List<Transform> _slots = new List<Transform>();
   public static Inventory Instance {get; private set;}

   private void Awake()
   {
      if (Instance != null || Instance != this)
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
      for (int i = 0; i < _slotsCount; i++)
      {
         Transform newSlot = Instantiate(slotPrefab, slotsContainer);
         _slots.Add(newSlot);
      }
      
      isOpen = true;
      ToogleInventory();
   }

   public void UpdateParent(ItemUI item, Transform newParent)
   {
      item.exParent =  newParent; // Hacemos que el transform de nuestro nuevo padre se convierta en el exParent del item para saber en donde se va a almacenar 
      item.transform.SetParent(newParent); // Asignamos al item como hijo de este newParent (o sea que movemos al gameObject de ser hijo del antiguo slot/Padre a ser del nuevo)
      item.transform.localPosition = Vector3.zero; // Reconfiguramos el seteo de la posición del item a la del nuevo padre para que quede centrado en este nuevo espacio asignado - recolocado
   }

   public void AddItem(int id, int quantity)
   {
      ItemUI preexistenValidItem = items.Find(item => item.id == id && item.itemData.maxStack >= item.quantity + quantity);
      if (preexistenValidItem != null)
      {
         preexistenValidItem.quantity += quantity;
         return;
      }

      for (int i = 0; i < _slotsCount; i++)
      {
         ItemUI itemSlot = _slots[i].childCount == 0 ? null : _slots[i].GetChild(0).GetComponent<ItemUI>();

         if (itemSlot == null)
         {
            ItemUI itemCopy = Instantiate(itemPrefab, transform).GetComponent<ItemUI>();
            
            itemCopy.InitializeItem(id, quantity);
            items.Add(itemCopy);
            
            UpdateParent(itemCopy, _slots[i]);
            break;
         }
      }
   }

   public void DeleteItem(int id, int quantity, bool byUse)
   {
      
   }
   
   public void ToogleInventory()
   {
      GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
      isOpen = !isOpen;
   }

   public void ShowDescription(ItemUI item)
   {
      descriptionUI.gameObject.SetActive(true);
      //descriptionUI.Show(item);
   }

   public void HideDescription()
   {
      descriptionUI.gameObject.SetActive(false);
   }

   public void AddMoreSpace(int slotsToAdd)
   {
      for (int i = 0; i < _slotsCount; i++)
      {
         Transform newSlot = Instantiate(slotPrefab, slotsContainer);
         _slots.Add(newSlot);
      }
   }
}
