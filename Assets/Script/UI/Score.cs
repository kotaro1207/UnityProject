using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static float score = 0f;               // �X�R�A�𑼃X�N���v�g������Q�Ƃł���悤��
    public static float elapsedTime = 0f;         // ? ���Ԍv���p��static�ϐ���ǉ��I

    public float initialScore = 10000f;
    public float timeOffset = 1f;
    public string PlayerTag = "Player";
    public TextMeshProUGUI scoreText;

    private bool isPlayerAlive = true;

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("scoreText���ݒ肳��Ă��܂���BInspector�Őݒ肵�Ă��������B");
        }
    }

    void Update()
    {
        if (GameObject.FindWithTag(PlayerTag) == null)
        {
            isPlayerAlive = false;
        }

        if (isPlayerAlive)
        {
            elapsedTime += Time.deltaTime; // ? ���Ԃ𖈃t���[�����Z�i�b�j

            score = initialScore / (elapsedTime + timeOffset);

            if (scoreText != null)
            {
                scoreText.text = Mathf.FloorToInt(score).ToString();
            }
        }
        else
        {
            if (scoreText != null)
            {
                scoreText.text = Mathf.FloorToInt(score).ToString();
            }
        }
    }
}
