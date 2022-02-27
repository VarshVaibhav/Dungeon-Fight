using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    Rigidbody2D _rigid;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float speed = 5f;
    private bool _resetJump = false;
    private bool _grounded = false;

    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    public int diamonds;
    public int health { get; set; }
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        health = 4;
    }


    void Update()
    {
        Movement();
        if(CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded() == true)
        {
            _playerAnim.Attack();
        }
    }

    void Movement()
    {
        float move = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }
        if (CrossPlatformInputManager.GetButtonDown("B_Button")&& IsGrounded() == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }
        _rigid.velocity = new Vector2(move * speed, _rigid.velocity.y);

        _playerAnim.Move(move);
    }
    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
        if (hitInfo.collider != null)
        {
            if (_resetJump == false)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }
        return false;
    }
    void Flip(bool faceRight)
    {
        if(faceRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipY = false;
            _swordArcSprite.flipX = false;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (faceRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipY = true;
            _swordArcSprite.flipX = true;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }
    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        if(health < 1)
        {
            return;
        }
        health--;
        UIManager.Instance.UPdateLives(health);
        if(health < 1)
        {
            _playerAnim.Death();
        }
    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }
}