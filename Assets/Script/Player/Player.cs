using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Grounded,     // 地面にいる
        Air,          // 空中にいる
        ShellGround,  // 甲羅状態で地面にいる
    }
    private PlayerState _state;

    [SerializeField, Header("移動速度")] private float _speed = 3;     //Unity側で設定して
    private Rigidbody2D _rb;
    private CameraShake cameraShake;  // CameraShakeスクリプトへの参照

    private float _move => CalculateMoveSpeed();
    public float JumpPower;//一時的にpublicにしてます
    public float MaxJumpPower = 15f;

    [SerializeField, Header("歩き")]
    private AnimationScript walkAnimation;
    [SerializeField, Header("甲羅")]
    private AnimationScript shellAnimation;
    [SerializeField, Header("ジャンプ")]
    private JumpAnimation jumpAnimation;

    float alpha_Sin;
    public int PlayerHP;


    private PlayerLife playerLife;
    private SpriteRenderer spriteRenderer;


    public bool isGround { get; private set; }

    public bool isShell => Input.GetKey(KeyCode.Space) && isGround;

    private float CalculateMoveSpeed()
    {
        if (playerLife.life <= 0||transform.position.x >= 37f)
            return 0;
        if (isShell && isGround)  //地面にいるかつ甲羅状態
            return _speed / 2;
        if (!isShell && isGround) //地面にいるとき
            return _speed;

        return _speed * 2f;     //空中
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerLife = GetComponent<PlayerLife>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        JumpPower = 0;

        walkAnimation.enabled = true;
        shellAnimation.enabled = false;
        jumpAnimation.enabled = false;
    }
    void Start()
    {
        // プレイヤーオブジェクトにCameraShakeスクリプトがアタッチされていることを確認
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void Update()
    {
        LookHP();
        UpdateGroundStatus();
        Move();
        Jump();
        AnimationChange();
    }

    private void LookHP()
    {
        PlayerHP = playerLife.life;
    }

    private void Move()
    {
        _rb.linearVelocity = new Vector2(_move, _rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            JumpPower += 0.075f;
            if (JumpPower > MaxJumpPower)
            {
                JumpPower = MaxJumpPower;
            }
            else if (JumpPower < 5)
            {
                JumpPower = 5f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            JumpPower = 0;
        }
    }

    private void AnimationChange()
    {
        if (isShell)
        {
            shellAnimation.enabled = true;
            walkAnimation.enabled = false;
            jumpAnimation.enabled = false;
        }
        else if (isGround)
        {
            walkAnimation.enabled = true;
            shellAnimation.enabled = false;
            jumpAnimation.enabled = false;
        }
        else
        {
            jumpAnimation.enabled = true;
            shellAnimation.enabled = false;
            walkAnimation.enabled = false;
        }

        if(transform.position.x >= 34.45f)
        {
            jumpAnimation.enabled = false;
            shellAnimation.enabled = false;
            walkAnimation.enabled = false;
        }
    }
    private void UpdateGroundStatus()
    {
        Vector2 rayOrigin = transform.position + Vector3.down * 1.3f; // 足元にずらす
        float rayLength = 0.1f; // 少し長めに
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, LayerMask.GetMask("Ground"));
        isGround = hit.collider != null;

        Debug.DrawRay(rayOrigin, Vector2.down * rayLength, isGround ? Color.green : Color.red);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            if (!isShell)
            {
                cameraShake._ShakeCheck();
                playerLife.TakeDamage();
                StartCoroutine(InvisibleAnimation());
                Debug.Log("hit");
            }
            else
            {
                Debug.Log("gard");
            }
        }
        else if (collision.CompareTag("HARITag"))
        {
            playerLife.TakeDamage();
            cameraShake._ShakeCheck();
        }


        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            Vector2 currentVelocity = _rb.linearVelocity;
            currentVelocity.x = 0f;
            _rb.linearVelocity = currentVelocity;
            StartCoroutine(TimeScale());
        }
    }

    private IEnumerator TimeScale()
    {
        while (!isGround)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        Time.timeScale = 0f;
    }

    private IEnumerator InvisibleAnimation()
    {
        float elapsed = 0f;
        float duration = 1f;

        Physics2D.IgnoreLayerCollision(7, 8, true);//PlayerとDamageを与えるオブジェクトの当たり判定を一時的にFalseにする

        while (elapsed < duration)
        {
            elapsed += 0.2f;

            //flashInterval待ってから
            yield return new WaitForSeconds(0.15f);

            //spriteRendererをオフ
            spriteRenderer.enabled = false;

            //flashInterval待ってから
            yield return new WaitForSeconds(0.15f);
            //spriteRendererをオン
            spriteRenderer.enabled = true;
        }

        Physics2D.IgnoreLayerCollision(7, 8, false);//PlayerとDamageを与えるオブジェクトの当たり判定を戻す。

    }
}
