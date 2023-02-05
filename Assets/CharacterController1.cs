using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController1 : MonoBehaviour
{

    Transform camera;
    public Vector3 MoveDirection;
    public float height = 0.6f;
    public Vector3 currentMoveDirection;
    public RaycastHit ground; // for isgrounded
    bool isgrounded = false;
    bool wasgroundedlastframe = false;
    bool checkedforslope = false;
    LayerMask terrain;
    public float analog = 0;

    public float SlopeLimit = 90;

    Vector3 platformpreviouspoint = Vector3.zero;
    Vector3 platformoffset = Vector3.zero;
    string platform_name = "";
    [System.NonSerialized] public Vector3 targetDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        terrain = LayerMask.GetMask("Terrain");

    }

    // Update is called once per frame
    void Update()
    {
        checkedforslope = false;
        if (currentMoveDirection.y == 0 && wasgroundedlastframe && !isgrounded)
            CheckForSlope();

        if (isgrounded)
            wasgroundedlastframe = true;

        if (ground.transform != null)
        {

            transform.rotation = Quaternion.LookRotation(transform.forward - ground.normal * Vector3.Dot(transform.forward, ground.normal), ground.normal); // koira k‰‰ntyy maaston mukaan

            platformoffset = ground.transform.position - platformpreviouspoint;

            if (platform_name == ground.transform.name && platformpreviouspoint != ground.transform.position)
                transform.position += platformoffset;

        }

        Quaternion targetrotation = Quaternion.identity;
        if (MoveDirection != Vector3.zero)
        {
            targetDirection = camera.TransformDirection(MoveDirection);
            targetDirection.y = 0.0f;
            targetrotation = Quaternion.LookRotation(targetDirection);
        }

        if (!checkedforslope)
        {
            CheckForGround();
        }

    }
    void LateUpdate()
    {
        if (ground.transform != null)
        {
            platformpreviouspoint = ground.transform.position;
            platform_name = ground.transform.name;
        }

        if (isgrounded && currentMoveDirection.y <= 0)
        {
            transform.position = new Vector3(transform.position.x, ground.point.y, transform.position.z);
        }
    }
    public void Move(Vector3 direction, float speed)
    {
        currentMoveDirection = direction;

        direction = direction.normalized;
        Vector3 nextposition = transform.position + direction * speed * Time.deltaTime * 2;
        Vector3 previousposition = transform.position;

        RaycastHit wall;
        if (Physics.Raycast(previousposition + Vector3.up * height, direction, out wall, 0.5f, LayerMask.GetMask("Terrain")))
        {
            Vector3 slidedirection = (new Vector3(wall.point.x, transform.position.y, wall.point.z) - previousposition).normalized + wall.normal;

            if (!Physics.Raycast(transform.position + Vector3.up * height, slidedirection, out wall, 0.5f, LayerMask.GetMask("Terrain")))
                transform.position += slidedirection * speed * Time.deltaTime;

        }
        else if (Physics.Raycast(previousposition + Vector3.up, direction, out wall, 0.1f, LayerMask.GetMask("Character")))
        {
            
            Vector3 slidedirection = (new Vector3(wall.point.x, transform.position.y, wall.point.z) - previousposition).normalized + wall.normal;

            if (!Physics.Raycast(transform.position + Vector3.up * height, slidedirection, out wall, 0.5f, LayerMask.GetMask("Terrain")))
                transform.position += slidedirection * speed * Time.deltaTime;

        }
        /**
        else if (isGrounded() && ThereIsAPit(nextposition))
        {

        }
        **/
        else transform.position += direction * speed * Time.deltaTime;

    }
    public void CheckForGround()
    {
        float additional = 0.1f;
        RaycastHit slope;
        if (!isGrounded())
            additional = 0;
        if (Physics.Raycast(transform.position + Vector3.up * height, Vector3.down, out ground, height + additional, terrain))
        {
            isgrounded = true;
        }
        else isgrounded = false;

        Vector3 directionwithouty = targetDirection;
        directionwithouty.y = 0;

        Physics.Raycast(transform.position + Vector3.up * 0.1f - directionwithouty, directionwithouty * 0.1f, out slope, height + additional, terrain);
        if (Vector3.Angle(slope.normal, targetDirection) > SlopeLimit)
        {
            //transform.position = transform.position - directionwithouty.normalized * 0.1f;
        }

    }
    public bool isGrounded()
    {
        return isgrounded;
    }
    public void CheckForSlope()
    {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out ground, height + 1, terrain))
        {
            isgrounded = true;
            checkedforslope = true;
        }
        else isgrounded = false;
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        MoveDirection = new Vector3(movementVector.x, 0, movementVector.y);
        analog = movementVector.magnitude;
    }
    public bool ThereIsAPit(Vector3 direct, float length)
    {

        RaycastHit floor;

        float scanner = 0.01f;
        float step = 0.01f;
        bool result = false;


        while (scanner < length * Time.deltaTime && result == false)
        {
            if (!Physics.Raycast(transform.position + Vector3.up + direct * scanner, Vector3.down, out floor, 2, terrain))
            {
                result = true;
            }
            scanner = scanner + step;
        }

        return result;
    }
    public bool ThereIsAPit(Vector3 point)
    {

        RaycastHit floor;

        if (!Physics.Raycast(point + Vector3.up, Vector3.down, out floor, 2, LayerMask.GetMask("Terrain")))
            return true;
        else return false;
    }
}
