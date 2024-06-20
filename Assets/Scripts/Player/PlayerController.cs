using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControls inputControl;
    private Rigidbody2D rb;
    public float speed = 0.0f;
    public float jumpForce = 0.0f;
    public Vector2 inputDirection;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    public float hurtForce;
    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
        inputControl = new PlayerInputControls();
        //跳跃
        inputControl.Gameplay.Jump.started += Jump;
        //攻击
        inputControl.Gameplay.Attack.started += PlayerAttack;
    }

    //测试
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    Debug.Log(other.name);
    //}

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!isHurt)
            Move();
    }

    public void Move()
    {
        //人物移动
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;

        //人物翻转
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        //Debug.Log("JUMP");
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
        {
            playerAnimation.PlayAttack();
            isAttack = true;
        }
    }

    // 触发反弹
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }
}
