
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    SpriteRenderer sprite;
    private float time, changeSpeed;
    private bool transparent, pressed;

    [SerializeField]
    private Image image;

    public bool isUI;

    private void Awake()
    {
        transparent = true;
        pressed = false;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        LoadScene();

        if (!pressed && isUI) AlphaChange();
    }

    private void AlphaChange()
    {
        changeSpeed = Time.deltaTime * 0.7f;

        if (time < 0)
        {
            transparent = true;
        }
        if (time > 0.5f)
        {
            transparent = false;
        }

        if (transparent == true)
        {
            time += Time.deltaTime;
            sprite.color = sprite.color - new Color(0, 0, 0, changeSpeed); //�����xUP
        }
        else
        {
            time -= Time.deltaTime;
            sprite.color = sprite.color + new Color(0, 0, 0, changeSpeed);//�����xDOWN
        }
    }

    private void LoadScene()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("oo");
            pressed = true;
            sprite.color = sprite.color + new Color(0, 0, 0, 1);
            StartCoroutine(Confirmed());
        }
    }

    private IEnumerator Confirmed()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 squishedScale = originalScale * 0.85f;
        float squishDuration = 0.05f;
        float restoreDuration = 0.15f;

        // �k��
        float elapsed = 0f;
        while (elapsed < squishDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsed / squishDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = squishedScale;

        yield return new WaitForSeconds(0.025f);

        // �߂�
        elapsed = 0f;
        while (elapsed < restoreDuration)
        {
            transform.localScale = Vector3.Lerp(squishedScale, originalScale, elapsed / restoreDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;

        yield return new WaitForSeconds(0.5f);

        image.GetComponent<Animator>().enabled = true;

    }

}