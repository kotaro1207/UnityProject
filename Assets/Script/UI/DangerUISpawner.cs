using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DangerUISpawner : MonoBehaviour
{
    [SerializeField] private Image DangerUI;
    private GameObject _canvas;
    private Camera _camera;

    void Start()
    {
        _canvas = GameObject.Find("DangerCanvas");
        _camera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            // �e�̃��[���h���W���擾
            Vector3 worldPos = collision.transform.position;

            // ���[���h���X�N���[�����W�ɕϊ�
            Vector3 screenPos = _camera.WorldToScreenPoint(worldPos);

            // �X�N���[����UI���[�J�����W�ɕϊ�
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.GetComponent<RectTransform>(),
                screenPos,
                _camera,
                out Vector2 localPos
            );

            // UI�𐶐����Ĉʒu���Z�b�g
            Image image = Instantiate(DangerUI, _canvas.transform);
            image.rectTransform.anchoredPosition = new Vector2(845, localPos.y); // x=�Œ�, y=�e�ɍ��킹��
        }
    }
}
