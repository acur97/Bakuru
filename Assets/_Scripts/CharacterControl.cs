using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float Gravity = -9.81f;
    public float GroundDistance = 0.2f;
    //public float DashDistance = 5f;
    public LayerMask Ground;
    public Vector3 Drag;

    private CharacterController _controller;
    private Vector3 _velocity;
    public bool _isGrounded = true;
    public Transform dolly;
    public float velocidadY;
    public Animator anim;
    [Range(0, 1)]
    public float PotenciaGroundC;
    public Transform groundCheker2;
    private DisolveTrigger groundCheker2T;
    private Vector3 move;

    private void Awake()
    {
        groundCheker2T = groundCheker2.GetComponent<DisolveTrigger>();
        _controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        groundCheker2.localPosition = new Vector3(0, Mathf.Clamp((_controller.velocity.y * PotenciaGroundC), -10, 0), 0);

        dolly.position = new Vector3(dolly.position.x, Mathf.Lerp(transform.position.y, dolly.position.y, velocidadY), dolly.position.z);
        _isGrounded = _controller.isGrounded;

        //if (groundCheker3T.stayAllC | _controller.isGrounded)
        //{
        //    _isGrounded = true;
        //}
        //else
        //{
        //    _isGrounded = false;
        //}

        //if (groundCheker1.stayAllC | groundCheker2.stayAllC | groundCheker3T.stayAllC)
        //{
        //    _isGrounded = true;
        //}
        //else
        //{
        //    _isGrounded = false;
        //}

        //_isGrounded = groundCheker.stayAllC;

        //_isGrounded = Physics.CheckSphere(groundCheker3.transform.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = 0f;

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        _controller.Move(move * Time.unscaledDeltaTime * Speed);
        if (move != Vector3.zero)
            transform.forward = move;

        //if (Input.GetButtonDown("Jump") && _isGrounded)
        //{
        //    _velocity.y += Mathf.Sqrt(JumpHeight * -2f * Gravity);
        //    anim.SetTrigger("Salto");
        //}

        /*if (Input.GetButtonDown("Dash"))
        {
            Debug.Log("Dash");
            _velocity += Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
        }*/


        _velocity.y += Gravity * Time.unscaledDeltaTime;

        _velocity.x /= 1 + Drag.x * Time.unscaledDeltaTime;
        _velocity.y /= 1 + Drag.y * Time.unscaledDeltaTime;
        _velocity.z /= 1 + Drag.z * Time.unscaledDeltaTime;

        _controller.Move(_velocity * Time.unscaledDeltaTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void Update()
    {
        anim.SetFloat("velocidad", Mathf.Abs(move.x));

        if (Input.GetButtonDown("Jump") && groundCheker2T.stayAllC)
        {
            _velocity.y += Mathf.Sqrt(JumpHeight * -2f * Gravity);
            anim.SetTrigger("Salto");
        }
    }
}