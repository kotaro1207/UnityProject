using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;                  // �e�̃X�s�[�h
    public float lockYThreshold = -3.0f;      // Y�����Œ肷�鍂��

    private Vector2 moveDirection;            // ��x�����v�Z����ړ�����
    private Rigidbody2D rb;
    private bool yLocked = false;
    private float lockedY;
    private float previousY;
    private GameObject player;                // �v���C���[�I�u�W�F�N�g�̎Q��
    private bool isPlayerAlive = true;        // �v���C���[�������Ă��邩�ǂ������`�F�b�N

    void Start()
    {
        // �v���C���[�̌��݈ʒu���擾���Č���������
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && player.transform.position.x <= 34.45f)
        {
            Vector2 targetPos = player.transform.position;
            moveDirection = (targetPos - (Vector2)transform.position).normalized;
        }
        else
        {
            // �v���C���[��������Ȃ���Β��i
            moveDirection = Vector2.left;
            isPlayerAlive = false;  // �v���C���[�����Ȃ��ꍇ�́A�e�𓮂����Ȃ�
        }

        rb = GetComponent<Rigidbody2D>();
        previousY = transform.position.y;
    }

    void FixedUpdate()
    {
        // �v���C���[�����������ǂ����𖈃t���[���`�F�b�N
        if (player == null)
        {
            isPlayerAlive = false;
        }

        if (isPlayerAlive)
        {
            // Y�����b�N�����i�ォ�痎����ݒ�Ɋ�Â��j
            if (!yLocked && previousY > lockYThreshold && transform.position.y <= lockYThreshold)
            {
                yLocked = true;
                lockedY = transform.position.y;

                rb.gravityScale = 0f;
                rb.linearVelocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            if (yLocked)
            {
                // Y���W���Œ肵�Ȃ���i�s�����ɐi�ށiY�͕ς��Ȃ��j
                Vector3 pos = transform.position;
                pos.y = lockedY;
                transform.position = pos;

                rb.linearVelocity = new Vector2(moveDirection.x * speed, 0f); // X�����̂�
            }
            else
            {
                // �ʏ�̈ړ��i�ǔ����Ȃ��j
                rb.linearVelocity = moveDirection * speed;
            }
        }
        else
        {
            // �v���C���[�����Ȃ��ꍇ�͒e���~
            rb.linearVelocity = Vector2.zero;
        }

        previousY = transform.position.y;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player�ɖ����I");
            Destroy(gameObject);
        }

        if (collision.gameObject.name == "ground")
        {
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
