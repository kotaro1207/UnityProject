using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ResultScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // �X�R�A��\������TextMeshProUGUI

    void Start()
    {
        // ScoreManager��static�X�R�A���擾
        float score = Score.score;

        // �X�R�A��\��
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }
}
