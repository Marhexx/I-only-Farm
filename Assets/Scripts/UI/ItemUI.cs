using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ItemUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private DataBase dB;
    
    public int id;
    public int quantity;
    
    [HideInInspector] public DataBase.InventoryItem itemData;
    [HideInInspector] public Transform exParent;
    
    private TextMeshProUGUI _quantityText;
    private Image _iconImage;
    private Vector3 _dragOffset;
    
    private void Awake()
    {
        _quantityText = transform.GetComponentInChildren<TextMeshProUGUI>();
        _iconImage = GetComponent<Image>();
        
        exParent = transform.parent;
        
        InitializeItem(id,  quantity);
    }
    
    private void Update()
    {
        if (quantity != null) _quantityText.text = quantity.ToString();
    }

    public void InitializeItem(int id, int quantity)
    {
        itemData.id = id;
        itemData.accumulator = dB.dataBase[id].accumulator;
        itemData.description = dB.dataBase[id].description;
        itemData.icon = dB.dataBase[id].icon;
        itemData.name = dB.dataBase[id].name;
        itemData.type = dB.dataBase[id].type;
        itemData.maxStack = dB.dataBase[id].maxStack;
        //itemData.item = db.dataBase[id].item;
        
        _iconImage.sprite = itemData.icon;
        
        this.quantity = quantity;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Inventory.Instance.HideDescription();
            //itemData.item.Use();
            //Inventory.Instance.DeleteItem(this, 1, true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!eventData.dragging)
        {
            Inventory.Instance.ShowDescription(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.Instance.HideDescription();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Inventory.Instance.HideDescription();
        _quantityText.enabled = false;
        exParent = transform.parent;
        transform.SetParent(Inventory.Instance.transform);
        _dragOffset = transform.position - Input.mousePosition; // Restando estos dos valores obtenemos un efecto de desplazamiento dependiendo de la posición en la que el cursor tomó el item
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + _dragOffset; // Para respetar el punto desde donde se hizo clic al item y se empezó a arrastrar
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _quantityText.enabled = true;
        
        // Son rayos que se envían desde el mouse y atraviesa todos los elementos de la UI. Funciona como colisionador para elemento de UI
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        Transform _slot = null; // Guarda la posición del item que se está arrastrando
        
        // Casteo un ray desde la posición del mouse y guardo todo lo que toca el raycastResult
        //Inventory.Instance.graphRay.Raycast(eventData, raycastResults);
        
        // Itero todos los collider tocados
        foreach (RaycastResult hit in raycastResults)
        {
            var hitObj = hit.gameObject;

            if (hitObj.CompareTag("Slot") && hit.gameObject.transform.childCount == 0) // Si tocamos un slot y este esta vacío
            {
                _slot = hitObj.gameObject.transform;
                break;
            }

            if (hitObj.CompareTag("ItemUI"))
            {
                // Verificamos que no tome el hit con el objeto mismo que estoy arrastrando
                // porque el RaycastResult lamentablemente puede detectar el mismo item inicial como diferente
                // y esto puede generar bugs
                if (hitObj != this.gameObject)
                {
                    ItemUI hitObjItemData = hitObj.GetComponent<ItemUI>(); // Guardamos el script del item que chocamos
                    if (hitObjItemData.itemData.id != id)
                    {
                        _slot = hitObjItemData.transform.parent;
                        //Inventory.Instance.UpdateParent(hitObjItemData, exParent);
                        break;
                    }
                    else
                    {
                        if (itemData.accumulator && hitObjItemData.quantity + quantity <= itemData.maxStack)
                        {
                            quantity = hitObjItemData.quantity + quantity;  
                            _slot = hitObjItemData.transform.parent;
                            // Inventory.Instance.DeleteItem(hitObjItemData, hitObjItemData.quantity, true);
                            break;
                        }
                        else
                        {
                            _slot = hitObjItemData.transform.parent;
                            Inventory.Instance.UpdateParent(hitObjItemData, exParent);
                            break;
                        }
                    }
                }
            }
        }
        Inventory.Instance.UpdateParent(this, _slot != null ? _slot : exParent);
    }
}
