using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(3, 6)] private int speed;
    
    private Rigidbody _rigidbodyPlayer;

    private void Awake()
    {
        _rigidbodyPlayer = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbodyPlayer.linearVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
    }
}
