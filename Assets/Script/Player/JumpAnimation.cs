using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class JumpAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Player player;
    private Rigidbody2D rb;

    [Header("�W�����v��Ԃ��Ƃ̃X�v���C�g")]
    public Sprite spriteJumpStart;  // �摜1
    public Sprite spriteJumpUp;     // �摜2
    public Sprite spriteJumpDown;   // �摜3

    private float jumpElapsedTime = 0f;
    private bool wasGroundedLastFrame = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        jumpElapsedTime = 0f;
    }
    private void Update()
    {

        bool isGrounded = player.isShell;
        float yVel = rb.linearVelocity.y;

        // --- �󒆂ł� Animator �𖳌������ĉ摜�ɐ؂�ւ� ---
        if (!isGrounded)
        {
            jumpElapsedTime += Time.deltaTime;

            if (jumpElapsedTime <= 0.15f)
            {
                spriteRenderer.sprite = spriteJumpStart;  // �W�����v����
            }
            else if (yVel > 0.1f)
            {
                spriteRenderer.sprite = spriteJumpUp;     // �㏸��
            }
            else
            {
                spriteRenderer.sprite = spriteJumpDown;   // ���~��
            }

        }

        wasGroundedLastFrame = isGrounded;
    }
}