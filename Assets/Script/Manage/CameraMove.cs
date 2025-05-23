using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField, Header("PlayerのTransform")]
    private Transform target;
    [SerializeField, Header("ずらし")]
    private Vector3 offsetX = new Vector3(7, 0, -10);

    private CameraShake cameraShake;
    private float fixedY = 0f;
    private float offsetZ = -10f;

    [SerializeField, Header("敵の位置に合わせたX軸の制限")]
    private float fixedX;


    private void Awake()
    {
        cameraShake = GetComponent<CameraShake>();
    }

    private void LateUpdate()
    {
        if (target != null && !cameraShake.isHit)
        {
            // X座標だけClampする
            float clampedX = Mathf.Clamp(target.position.x, -10f, fixedX);

            Vector3 newPosition = new Vector3(clampedX, fixedY, offsetZ);
            transform.position = newPosition + offsetX;
        }
    }
}