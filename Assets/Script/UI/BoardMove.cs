using UnityEngine;

public class BoardMove : MonoBehaviour
{
    public RectTransform[] targets;          // �ړ���������������Image
    public Vector2 targetPosition = new Vector2(0f, 0f); // �����ʒu�iCanvas�̃T�C�Y�ɉ����āj
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


