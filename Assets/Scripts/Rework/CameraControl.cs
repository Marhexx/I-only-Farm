using UnityEngine;  

public class CameraControl: MonoBehaviour
{
    Transform playerpos;

    void Start()
    {
        playerpos = GameObject.FindAnyObjectByType<PlayerRework>().transform;
    }


    void Update()
    {
        FollowPlayer();
    }
    
    void FollowPlayer()
    {
        Vector3 desiredPosition = playerpos.position + new Vector3(0, 5, -10);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, desiredPosition, Time.deltaTime * 5);
        Camera.main.transform.LookAt(playerpos);
    }
}
