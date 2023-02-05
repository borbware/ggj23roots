using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum State
{
    Grounded,
    InAir,
    Peeing,
    Dashing
}

public class PerusKoira : MonoBehaviour
{

    State state;
    CharacterController1 controller;
    Animator anim;
    AudioSource haukku;

    public float KoiranNopeus = 4;
    public float jumpPower = 10;
    float jump = 0;

    float gravity = 0;
    float gravitymultiplier = 4;

    float jumpBuffer = 0.0f;
    float dashBuffer = 0.0f;

    float peeFrames = 0f;
    int AirJumpCounter = 0;
    int AirDashCounter = 0;
    float DashCoolDown = 0;
    float DashFrames = 0;
    int AirPissCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController1>();
        anim = GetComponentInChildren<Animator>();
        haukku = GetComponent<AudioSource>();

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

                    if (controller.isGrounded()) // reset double jump
                    {
                        AirJumpCounter = 1;
                        AirPissCounter = 1;
                        AirDashCounter = 2;
                    }

                    if (dashBuffer > 0)
                    {
                        Dash();
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
                    if (jumpBuffer > 0 && AirJumpCounter > 0) // double jump
                    {
                        Jump();
                        AirJumpCounter--;
                    }

                    if (dashBuffer > 0)
                    {
                        Dash();
                    }

                }
                break;
            case State.Peeing:
                {
                    if (peeFrames <= 0.3f)
                    {

                        anim.CrossFadeInFixedTime("DogDefault", 0.3f, -1, 0.0f, 0.0f);
                    }
                    if (peeFrames <= 0)
                    {
                        state = State.Grounded;
                    }
                    peeFrames -= Time.deltaTime;

                    AirPissCounter--;
                }
                break;
            case State.Dashing:

                dashBuffer = 0;
                controller.Move(transform.forward, 20 );
                gravity = 0;
                transform.rotation = Quaternion.LookRotation(controller.targetDirection);
                DashFrames += Time.deltaTime;
                if (DashFrames > 0.15)
                {
                    if (controller.isGrounded())
                    {
                        state = State.Grounded;
                    }
                    else
                        state = State.InAir;

                    DashFrames = 0;
                    if (DashCoolDown <= 0)
                        DashCoolDown = 0.1f;
                    else DashCoolDown = 1.2f;
                }


                break;
        }

        if (jumpBuffer > 0 )
            jumpBuffer -= Time.deltaTime;
        if (dashBuffer > 0 )
            dashBuffer -= Time.deltaTime;

        if (state != State.Dashing)
            DashCoolDown -= Time.deltaTime;
    }
    void OnJump() // tämä on hyppyinputti
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
        gravity = 0;
    }
    void OnBark() // tämä on haukkuinputti
    {
        Bark();
    }
    void Bark() // tämä on haukku
    {
        haukku.Play();
    }
    void OnPiss()
    {
        Piss();
    }
    void Piss()
    {
        if (AirPissCounter > 0)
        {
            peeFrames = 1;
            anim.Play("pee");
            state = State.Peeing;
        }
    }
    void OnDash()
    {
        if (AirDashCounter > 0 && DashCoolDown <= 0)
            dashBuffer = 0.2f;
    }
    void Dash()
    {
        AirDashCounter--;
        state = State.Dashing;
    }
}
