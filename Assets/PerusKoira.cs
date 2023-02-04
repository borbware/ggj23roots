using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum State
{
    Grounded,
    InAir
}

public class PerusKoira : MonoBehaviour
{

    State state;
    CharacterController1 controller;
    Animator anim;

    public float KoiranNopeus = 4;
    public float jumpPower = 10;
    float jump = 0;

    float gravity = 0;
    float gravitymultiplier = 4;

    float jumpBuffer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController1>();
        anim = GetComponentInChildren<Animator>();
        state = State.Grounded;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Grounded:
                {
                    anim.Play("DogDefault");
                    anim.SetFloat("Blend", controller.analog);
                    if (controller.MoveDirection != Vector3.zero)
                    {
                        controller.Move(controller.targetDirection, KoiranNopeus);
                        transform.rotation = Quaternion.LookRotation(controller.targetDirection);
                    }

                    if (!controller.isGrounded())
                    {
                        state = State.InAir;
                    }
                    if (jumpBuffer > 0)
                    {
                        Jump();
                    }

                }
                break;
            case State.InAir:
                {
                    if (controller.MoveDirection != Vector3.zero) // voi liikkua ilmassa
                    {
                        controller.Move(controller.targetDirection, KoiranNopeus);
                        transform.rotation = Quaternion.LookRotation(controller.targetDirection);
                    }
                    if (jump > 0) // koira liikkuu ylös
                    {
                        controller.Move(Vector3.up, jump);
                        jump -= Time.deltaTime * gravitymultiplier;
                    }
                    else // koira liikkuu alas
                    {
                        controller.Move(Vector3.down, gravity);
                        gravity += Time.deltaTime * gravitymultiplier;
                        anim.CrossFadeInFixedTime("jump end", 0.25f, -1, 0.0f, 0.0f);
                    }

                    if (jump <= 0 && controller.isGrounded())
                    {
                        state = State.Grounded;
                        gravity = 0;
                        jump = 0;
                    }

                }
                break;
        }

        if (jumpBuffer > 0 )
            jumpBuffer -= Time.deltaTime;
    }
    void OnJump() // tämä on inputti
    {
        jumpBuffer = 0.1f;
    }
    void OnJumpRelease() // tämä on inputti
    {
        jump = 0;
    }
    void Jump() // tämä on itse hyppy
    {
        jumpBuffer = 0;
        jump = jumpPower;
        state = State.InAir;
        anim.CrossFadeInFixedTime("jump start", 0.1f, -1, 0.0f, 0.0f);
    }
}
