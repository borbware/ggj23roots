using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraOrbit : MonoBehaviour
{
    Transform camera;
    //public Transform target;

    float speed = 40f;
    float x;
    float y;

    float x_;
    float y_;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        speed = 0.1f;
        x_ += x * speed;
        y_ -= y * speed;

        Quaternion rotation = Quaternion.Euler(y_,x_,0);
        camera.position = rotation * new Vector3(0, 1.5f, -5) + transform.position;
        camera.rotation = rotation;
    }
    void OnMouseMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        x = movementVector.x;
        y = movementVector.y;
    }
}
