using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraOrbit : MonoBehaviour
{
    Transform camera;
    //public Transform target;

    float speed = 1f;
    public float mousespeed = 0.1f;
    public float gamepadspeed = 40f;

    float x;
    float y;

    float x_;
    float y_;

    bool Mouse = true;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Mouse)
        {
            x_ += x * speed;
            y_ -= y * speed;
        }
        else
        {
            x_ += x * speed * Time.deltaTime / Time.timeScale;
            y_ -= y * speed * Time.deltaTime / Time.timeScale;
        }

        Quaternion rotation = Quaternion.Euler(y_,x_,0);
        camera.position = rotation * new Vector3(0, 1.5f, -5) + transform.position;
        camera.rotation = rotation;
        CheckForWallCollisions();
    }
    void CheckForWallCollisions()
    {

        RaycastHit wall;
        if (Physics.Linecast(transform.position + Vector3.up * 1.7f, camera.position, out wall, LayerMask.GetMask("Terrain")))
        {

            camera.position = wall.point + camera.forward * 0.25f;
        }

        x_ = camera.eulerAngles.y;
        y_ = camera.eulerAngles.x;

    }
    void OnMouseMove(InputValue movementValue)
    {
        Mouse = true;
        speed = mousespeed;
        Vector2 movementVector = movementValue.Get<Vector2>();
        x = movementVector.x;
        y = movementVector.y;
    }
    void OnRightStickMove(InputValue movementValue)
    {
        Mouse = false;
        speed = gamepadspeed;
        Vector2 movementVector = movementValue.Get<Vector2>();
        x = movementVector.x;
        y = movementVector.y;
    }
}
