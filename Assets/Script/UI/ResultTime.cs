using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ResultTime : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // �X�R�A��\������TextMeshProUGUI

    void Start()
    {
        float Time = Score.elapsedTime;

        // �X�R�A��\��
        scoreText.text = Mathf.FloorToInt(Time).ToString();
    }
}