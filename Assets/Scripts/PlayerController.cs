using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform transformPlayer;
    [SerializeField] private Transform transformAnimation;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera camera;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale = 2f;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool isKnocking;
    [SerializeField] private float knockBackLength = 0.5f;
    [SerializeField] private float knockBackCounter;
    [SerializeField] private Vector2 knockBackPower;
    [SerializeField] private float flashCounter;
    [SerializeField] private float flashDelay;
    
    
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
        if (!isKnocking)
        {
            float lastYValue = moveDirection.y;
            // moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveDirection = (transformPlayer.forward * Input.GetAxisRaw("Vertical")) + (transformPlayer.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = lastYValue;
            if (characterController.isGrounded)
            {
                //moveDirection.y = startTrasform.y;
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
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            
            float lastYValue = moveDirection.y;
            moveDirection = (transformPlayer.forward * knockBackPower.x * -1f);
            moveDirection.y = lastYValue;

            if (characterController.isGrounded)
            {
                moveDirection.y = 0f;
            }
            moveDirection.y += (Physics.gravity.y * Time.deltaTime * gravityScale);
            characterController.Move(moveDirection * Time.deltaTime);
            if (knockBackCounter <= 0)
            {
                isKnocking = false;
            }
        }
        
        animator.SetFloat("Speed", Math.Abs(moveDirection.x+ moveDirection.z));
        animator.SetBool("Grounded", characterController.isGrounded);
    }

    public void KnockBack()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        Debug.Log("knocked back");
        moveDirection.y = knockBackPower.y;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    public async UniTask FlashPlayer()
    {
        gameObject.SetActive(false);
        await UniTask.Delay(TimeSpan.FromSeconds(flashDelay));
        gameObject.SetActive(true);
    } 
}
