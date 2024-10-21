using SojaExiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public TextController textController;

    public CharacterController controller;

    public MouseLook mouseLook;

    public float speed = 1f;
    public float gravity = -9.81f;

    public bool runFlag;

    public float walkspeed;
    public float runspeed = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Animator animator;

    public float x;
    public float z;

    Vector3 velocity;
    Vector3 move;

    bool isGrounded;

    public float stopf = 1;

    public State state;

    public enum State
    {
        Normal,
        Talk,
        MouseOnly,
    }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //if (stopf == 1)
        //{
        //    x = Input.GetAxis("Horizontal");
        //    z = Input.GetAxis("Vertical");
        //}

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        // 左シフトキーを押している間はプレイヤーのスピードを上げる
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runspeed;
            runFlag = true;
        }
        else
        {
            speed = walkspeed;
            runFlag = false;
        }

        move = Vector3.zero;

        if (stopf == 1)
        {
            move = transform.right * x + transform.forward * z;
        }

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (stopf == 0)
        {
            animator.enabled = false;
            return;
        }

        int charamove = (move.x != 0 || move.z != 0) ? (runFlag ? 3 : 2) : 0;
        animator.SetInteger("charamove", charamove);
    }

    public void SetState(State state)
    {
        this.state = state;

        if (state == State.Normal)
        {
            mouseLook.mousestopf = 1;
            stopf = 1;
            animator.enabled = true;
        }
        else if (state == State.Talk)
        {
            mouseLook.mousestopf = 0;
            stopf = 0;
            animator.enabled = false;
        }
        else if(state == State.MouseOnly)
        {
            mouseLook.mousestopf = 1;
            stopf = 0;
            animator.enabled = false;
        }
    }

    public void SignalNormal()
    {
        SetState(State.Normal);
    }

    public void SignalMouseOnly()
    {
        SetState(State.MouseOnly);
    }

    public State GetState()
    {
        return state;
    }

    private IEnumerator OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "coltext")
        {
            hit.gameObject.GetComponent<Test1>().sendtalklist();
            textController.movieFlag = true;
            hit.gameObject.tag = "Untagged";
            yield return new WaitForSeconds(3);
            hit.gameObject.tag = "coltext";
        }
    }

}