using UnityEngine;
using System.Collections;

public class SlightDarkenAfterDelay : MonoBehaviour
{
    public float delaySeconds = 3f;
    public float darkenDuration = 2f;
    public float darknessFactor = 0.8f; // 1.0 = ���̐F, 0.0 = ��, 0.8 = �����Â�

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color targetColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        // �����Â������F���v�Z�iRGB�l��darknessFactor�{�j
        targetColor = new Color(
            originalColor.r * darknessFactor,
            originalColor.g * darknessFactor,
            originalColor.b * darknessFactor,
            originalColor.a // �����x�͂��̂܂�
        );

        StartCoroutine(DarkenAfterDelayCoroutine());
    }

    IEnumerator DarkenAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(delaySeconds);

        float elapsed = 0f;
        while (elapsed < darkenDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / darkenDuration;

            spriteRenderer.color = Color.Lerp(originalColor, targetColor, t);
            yield return null;
        }

        // �O�̂��߁A�Ō�Ɋm��
        spriteRenderer.color = targetColor;
    }
}


