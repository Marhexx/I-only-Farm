using UnityEngine;

public class Land : MonoBehaviour
{
    public enum LandType
    {
        Soil,
        Farmland,
        Watered
    }

    public LandType currentLandType;

    [SerializeField] private Material soilMat, farmlandMat, wateredMat;
    new Renderer renderer;

    public GameObject select;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        SwitchlandType(LandType.Soil);

        Select(false);
    }

    public void SwitchlandType(LandType newType)
    {
        currentLandType = newType;

        Material materialToApply = soilMat;

        switch (currentLandType)
        {
            case LandType.Soil:
                materialToApply = soilMat;
                break;
            case LandType.Farmland:
                materialToApply = farmlandMat;
                break;
            case LandType.Watered:
                materialToApply = wateredMat;
                break;
        }

        renderer.material = materialToApply;
    }

    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }
}
