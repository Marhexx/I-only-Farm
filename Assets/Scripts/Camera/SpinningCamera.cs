using System;
using UnityEngine;

public class SpinningCamera : MonoBehaviour
{
    [SerializeField] private float speedToRotate;

    private Camera _cam;

    private void Awake() => _cam = GetComponent<Camera>();

    void Update()
    {
        _cam.transform.Rotate(Vector3.up * (speedToRotate * Time.deltaTime), Space.World);
    }
}
