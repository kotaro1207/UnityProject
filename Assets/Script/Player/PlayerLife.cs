using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public Image[] hearts;           // �n�[�g3�iUI Image�j
    public string bulletTag = "bullet"; // �e�̃^�O
    public string HARITag = "HARI"; // �e�̃^�O
    public int life { get; private set; } = 3;
    private int maxLife = 3;



    public void TakeDamage()
    {
        if (life <= 0) return;

        life--;

        // HP�I�u�W�F�N�g��1���炷
        if (life >= 0 && life < hearts.Length)
        {
            StartCoroutine(Animate());
        }

        if (life == 0)
        {
            Debug.Log("�Q�[���I�[�o�[�I");
        }

    }

    public void Heal(int amount)
    {
        if (life >= maxLife) return;

        // �n�[�g��1�\���i�ŏ��ɏ�����������߂��j
        hearts[life].enabled = true;
        life += amount;

        if (life > maxLife) life = maxLife;
    }

    private IEnumerator Animate()
    {

        float elapsed = 0f;
        float duration = 1f;

        while (elapsed < duration)
        {
            elapsed += 0.2f;

            //flashInterval�҂��Ă���
            yield return new WaitForSeconds(0.15f);

            //spriteRenderer���I�t
            hearts[life].enabled = false;

            //flashInterval�҂��Ă���
            yield return new WaitForSeconds(0.15f);
            //spriteRenderer���I��
            hearts[life].enabled = true;
        }

        hearts[life].enabled = false;
        yield return null;
    }
}