using UnityEngine;

public class BoardMove : MonoBehaviour
{
    public RectTransform[] targets;          // 移動させたい複数のImage
    public Vector2 targetPosition = new Vector2(0f, 0f); // 中央位置（Canvasのサイズに応じて）
    public float speed = 200f;

    void Update()
    {
        foreach (RectTransform rect in targets)
        {
            if (rect != null)
            {
                rect.anchoredPosition = Vector2.MoveTowards(
                    rect.anchoredPosition,
                    targetPosition,
                    speed * Time.deltaTime
                );
            }
        }
    }
}


