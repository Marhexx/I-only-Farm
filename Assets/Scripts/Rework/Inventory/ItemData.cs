using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemData : ScriptableObject
{ 
    public string description;

    //For inventory UI
    public Sprite thumbnail;

    //GameObject to scene representation
    public GameObject gameModel;
    
}
