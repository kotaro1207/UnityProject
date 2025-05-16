using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// �U�������s����Ԋu�͈̔�
    [SerializeField, Header("�ŏ��Ԋu")] float minAttackInterval = 1f;
    [SerializeField,Header("�ő�Ԋu")] float maxAttackInterval = 3f;  

    // ���˂���I�u�W�F�N�g�ibullet�j
    [SerializeField,Header("���˂���I�u�W�F�N�g�̃v���n�u")] GameObject bulletPrefab;  // ���˂���I�u�W�F�N�g�̃v���n�u

    // ���ˈʒu
    [SerializeField, Header("���˂���ʒu")] Transform firePoint;  

    [SerializeField] GunMove gun;

    [SerializeField] AnimationScript _animation;

    private GameObject player;

    private float attackTimer;  // �^�C�}�[

    void Start()
    {
        // �ŏ��̍U���Ԋu�������_���Ɍ���
        SetRandomAttackInterval();

        player = GameObject.FindWithTag("Player");

    }

    void Update()
    {
        // �^�C�}�[��i�߂�
        attackTimer -= Time.deltaTime;

        // �U���^�C�~���O
        if (attackTimer <= 0f && player.transform.position.x <= 34.45f)
        {
            Attack();

            gun.Recoil();               //�e�̔����A�j���[�V����

            SetRandomAttackInterval();  // �V���������_���ȍU���Ԋu��ݒ�
        }
        else if(player.transform.position.x >= 34.45f)
        {
            _animation.enabled = false;
        }
    }

    // �U������
    void Attack()
    {
        // ���˂���I�u�W�F�N�g�ibullet�j�𐶐�
        if (bulletPrefab != null && firePoint != null)
        {
            // �e�𐶐�
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Rigidbody2D ���擾���Ĕ��˗͂�������
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        }

    }

    // �U���Ԋu�������_���ɐݒ�
    void SetRandomAttackInterval()
    {
        attackTimer = Random.Range(minAttackInterval, maxAttackInterval);
    }

    // �v���C���[�ƏՓ˂����ꍇ
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("Hit: " + collision.name); // ���ɓ������������O

        if (collision.CompareTag("Player"))
        {
            // �����œG�������鏈����ǉ�
            Destroy(gameObject);
        }
    }
}