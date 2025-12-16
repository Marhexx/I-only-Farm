 using UnityEngine;

public class PlayerRework : MonoBehaviour{

    private CharacterController controller;
    private Animator animator;

    private float moveSpeed = 5f;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        Move();
    }

    public void Move()
        {
            float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0f, v).normalized;
        Vector3 velocity = moveSpeed * Time.deltaTime * dir;

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.LookRotation(dir);

            controller.Move(velocity);

            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        
    }
    
}
