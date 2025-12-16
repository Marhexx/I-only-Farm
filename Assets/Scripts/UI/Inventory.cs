using System.Collections.Generic;
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
   
   private CanvasGroup _canvasGroup;

   private void Awake()
   {
      if (Instance != null && Instance != this) 
      {
           Destroy(gameObject);
           return;
      }
      Instance = this;
     
      _canvasGroup = GetComponent<CanvasGroup>();
   }

   private void Start()
   {
      for (int i = 0; i < _slotsCount; i++)
      {
         Transform newSlot = Instantiate(slotPrefab, slotsContainer);
         _slots.Add(newSlot);
      }
      
      // Inicializar cerrado
      isOpen = false;

      // Ocultar la visualización y la interacción
      if (_canvasGroup != null)
      {
         _canvasGroup.alpha = 0f;
         _canvasGroup.interactable = false;
         _canvasGroup.blocksRaycasts = false;
      }
      
      
   }

   public void UpdateParent(ItemUI item, Transform newParent)
   {
      item.exParent =  newParent; // Hacemos que el transform de nuestro nuevo padre se convierta en el exParent del item para saber en donde se va a almacenar 
      item.transform.SetParent(newParent); // Asignamos al item como hijo de este newParent (o sea que movemos al gameObject de ser hijo del antiguo slot/Padre a ser del nuevo)
      item.transform.localPosition = Vector3.zero; // Reconfiguramos el seteo de la posición del item a la del nuevo padre para que quede centrado en este nuevo espacio asignado - recolocado
   }

   public void AddItem(int id, int quantity)
   {
      ItemUI preexistentValidItem = items.Find(item => item.itemData.id == id && item.itemData.maxStack >= item.quantity + quantity);
      if (preexistentValidItem != null)
      {
         preexistentValidItem.quantity += quantity;
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
   
   public void DeleteItem(ItemUI item, int quantity, bool byUse)
   {
      ItemUI itemToDelete = items.Find(ITEM => ITEM == item); //Aquí comparamos de script a script
      
      itemToDelete.quantity -= quantity;

      if (!byUse) // Cuando se elimina el elemento a disposición propia
      {
         BaseItem spawnedItem = Instantiate(item.itemData.item);
         spawnedItem.transform.position = player.itemSpawn.position;
         spawnedItem.SetDataById(item.id, quantity);
      }

      if (itemToDelete.quantity <= 0) // Cuando eliminamos la misma cantidad de elementos que tenemos
      {
         items.Remove(itemToDelete); // Lo removemos de la lista
         Destroy(itemToDelete.gameObject);
      }
      
   }
   
   public void ToogleInventory()
   {
      if (!_canvasGroup) return;

      isOpen = !isOpen;
   
      if (isOpen)
      {
         // Mostrar y Habilitar
         _canvasGroup.alpha = 1f;
         _canvasGroup.interactable = true;
         _canvasGroup.blocksRaycasts = true;
      
         // Reposicionar al centro (siempre que esté abierto)
         GetComponent<RectTransform>().anchoredPosition = Vector2.zero; 
      }
      else
      {
         // Ocultar y Deshabilitar
         _canvasGroup.alpha = 0f;
         _canvasGroup.interactable = false;
         _canvasGroup.blocksRaycasts = false;
      }
   }

   public void ShowDescription(ItemUI item)
   {
      descriptionUI.gameObject.SetActive(true);
      descriptionUI.Show(item);
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
