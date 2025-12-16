using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI header; // Referencia al título de la descripción
    [SerializeField] private TextMeshProUGUI content; // Referencia a la descripción propia
    public int characterWrapLimit; // Para detectar la cantidad máxima de caracteres que se muestren en línea para que después haga un salto
    
    [SerializeField] private LayoutElement layoutElement;
    private RectTransform _rect;
    
    private void Awake() => _rect = GetComponent<RectTransform>();

    public void Show(ItemUI item)
    {
        header.text = item.itemData.name;    
        content.text = item.itemData.description;
        
        int headerLength = header.text.Length;
        int contentLength = content.text.Length;
        
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
    }

    private void LateUpdate()
    {
        Vector2 position = Input.mousePosition;
        
        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        float finalPivotX;
        float finalPivotY;
        
        // Si el mouse esta en la parte izquierda de la pantalla muevo el cursor a la derecha y viceversa
        if (pivotX < 0.05) finalPivotX = -0.1f;
        else finalPivotX = 1.01f;
        
        // Si el mouse está en la parte inferior de la pantalla mueve el cursor hacia arriba y viceversa
        if (pivotY < 0.05) finalPivotY = 0f;
        else finalPivotY = 1f;
        
        _rect.pivot = new Vector2(finalPivotX, finalPivotY);
        transform.position = position;

// -----------------------------------------------------------------------
        /*// CORRECCIÓN: Calcular el centro de la pantalla
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
    
        // Calcular la posición del mouse relativa al centro de la pantalla.
        Vector2 relativePosition = position - screenCenter; 
    
        // Asignar la posición relativa a anchoredPosition.
        _rect.anchoredPosition = relativePosition;*/
    }
}