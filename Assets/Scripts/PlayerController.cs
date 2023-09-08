using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Transform transformPlayer;
    [SerializeField] public Transform transformAnimation;
    [SerializeField] public CharacterController characterController;
    [SerializeField] public Camera camera;
    [SerializeField] public Animator animator;
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 2f;
    private Vector3 moveDirection;
    public float rotateSpeed;
    public Vector3 startTrasform { get; private set; }

    public void Awake()
    {
        startTrasform = transformPlayer.position;
        Debug.Log(startTrasform);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float lastYValue = moveDirection.y;
       // moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveDirection = (transformPlayer.forward * Input.GetAxisRaw("Vertical")) + (transformPlayer.right * Input.GetAxisRaw("Horizontal"));
        moveDirection.Normalize();
        moveDirection = moveDirection * moveSpeed;
        moveDirection.y = lastYValue;
        if (characterController.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        moveDirection.y += (Physics.gravity.y * Time.deltaTime * gravityScale);
        characterController.Move(moveDirection * Time.deltaTime);
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transformPlayer.rotation = Quaternion.Euler(0f, camera.transform.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            //transformAnimation.rotation = newRotation;
            transformAnimation.rotation = Quaternion.Slerp(transformAnimation.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
        animator.SetFloat("Speed", Math.Abs(moveDirection.x+ moveDirection.z));
        animator.SetBool("Grounded", characterController.isGrounded);
    }
}
