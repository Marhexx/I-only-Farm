using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{

    Player playerController;

    Land selectedLand;

    void Start()
    {
        playerController = transform.parent.GetComponent<Player>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            OninteractableHit(hit);
        }
    }

    //Function to handle interaction with land
    void OninteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;

        if (other.tag == "Land")
        {
            Land land = other.GetComponent<Land>();
            SelectLand(land);
            if (land != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (land.currentLandType == Land.LandType.Soil)
                    {
                        land.SwitchlandType(Land.LandType.Farmland);
                    }
                    else if (land.currentLandType == Land.LandType.Farmland)
                    {
                        land.SwitchlandType(Land.LandType.Watered);
                    }
                }
            }
        }

        if(selectedLand != null && other.tag != "Land")
        {
            selectedLand.Select(false);
            selectedLand = null;
        }
    }
    
    //Handle land selection
    void SelectLand(Land land)
    {
        if (selectedLand != null && selectedLand != land)
        {
            selectedLand.Select(false);
        }

        selectedLand = land;
        selectedLand.Select(true);
    }
}
