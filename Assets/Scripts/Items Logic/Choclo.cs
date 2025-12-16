using UnityEngine;

public class Choclo : BaseItem
{
    [SerializeField] private float healthRestoreAmount = 30f;

    public override void Use()
    {
        Debug.Log($"Health increased: {healthRestoreAmount}HP de choclo");
    }
}
