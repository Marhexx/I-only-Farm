using UnityEngine;

public class Chuleta : BaseItem
{
    [SerializeField] private float healthRestoreAmount = 10f;

    public override void Use()
    {
        Debug.Log($"Health increased: {healthRestoreAmount}HP");
    }
}
