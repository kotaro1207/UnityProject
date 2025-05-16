using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField, Header("Player��Transform")]
    private Transform target;
    [SerializeField, Header("���炵")]
    private Vector3 offsetX = new Vector3(7, 0, -10);

    private CameraShake cameraShake;
    private float fixedY = 0f;
    private float offsetZ = -10f;

    [SerializeField, Header("�G�̈ʒu�ɍ��킹��X���̐���")]
    private float fixedX;


    private void Awake()
    {
        cameraShake = GetComponent<CameraShake>();
    }

    private void LateUpdate()
    {
        if (target != null && !cameraShake.isHit)
        {
            // X���W����Clamp����
            float clampedX = Mathf.Clamp(target.position.x, -10f, fixedX);

            Vector3 newPosition = new Vector3(clampedX, fixedY, offsetZ);
            transform.position = newPosition + offsetX;
        }
    }
}