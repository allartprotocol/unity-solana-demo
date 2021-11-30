using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimationComponent : MonoBehaviour
{
    [Header("")]
    public bool bRotate;
    public bool bScale;
    public bool bTranslate;
    public bool bAlpha;

    [Header("Curves")]
    public AnimationCurve rotation;
    public AnimationCurve scale;
    public AnimationCurve translation;
    public AnimationCurve alpha;

    [Header("Params")]
    public float rotationAngle;
    public Vector3 minScale;
    public float scaleModifier;
    public float maxTranslationDistance;
    public float toAlpha;
    public float fromAlpha;

    public float animationLength;

    public bool runOnStart = false;

    public TextMeshProUGUI textLabel;

    public enum EAnimMode
    {
        PING_PONG,
        LOOP,
        ONESHOT
    }

    [Header("Modes")]
    public EAnimMode animMode;

    Coroutine startCoroutine;
    Vector3 startPosition;
    CanvasGroup cg;

    void Start()
    {
        startPosition = transform.localPosition;
        cg = GetComponent<CanvasGroup>();
        if (runOnStart)
            PlayAnimation();
    }

    public void PlayOneShot(string text)
    {
        textLabel.text = text;
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        if (!gameObject.activeSelf)
            return;

        if (startCoroutine != null)
            StopCoroutine(startCoroutine);
        try
        {
            startCoroutine = StartCoroutine(AnimationRoutine());
        }
        catch { }
    }

    public void StopAnimation()
    {
        StopCoroutine(startCoroutine);
    }

    IEnumerator AnimationRoutine()
    {
        Vector3 startScale = transform.localScale;
        float animationTime = animationLength / 2;
        if (animMode == EAnimMode.LOOP)
        {
            while (true)
            {
                Vector3 toPos = Vector3.up * maxTranslationDistance;

                for (float c = 0, t = 0; c <= animationLength; c += Time.deltaTime)
                {
                    t = c / animationLength;

                    if (bScale)
                        transform.localScale = Vector3.Lerp(minScale, Vector3.one * scaleModifier, scale.Evaluate(t));

                    if (bRotate)
                        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(-rotationAngle, rotationAngle, rotation.Evaluate(t))));

                    if (bTranslate)
                        transform.localPosition = Vector3.Lerp(startPosition, toPos, translation.Evaluate(t));

                    if (bAlpha)
                        cg.alpha = Mathf.Lerp(fromAlpha, toAlpha, alpha.Evaluate(t));

                    yield return null;
                }

                if (bAlpha)
                    cg.alpha = Mathf.Lerp(fromAlpha, toAlpha, alpha.Evaluate(1));

                if (bTranslate)
                    transform.localPosition = Vector3.Lerp(startPosition, toPos, translation.Evaluate(1));

                if (bScale)
                    transform.localScale = Vector3.Lerp(minScale, Vector3.one * scaleModifier, scale.Evaluate(1));
                if (bRotate)
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(-rotationAngle, rotationAngle, rotation.Evaluate(1))));

            }
        }
        if (animMode == EAnimMode.ONESHOT)
        {
            Vector3 toPos = Vector3.up * maxTranslationDistance;

            for (float c = 0, t = 0; c <= animationLength; c += Time.deltaTime)
            {
                t = c / animationLength;

                if (bScale)
                    transform.localScale = Vector3.Lerp(minScale, Vector3.one * scaleModifier, scale.Evaluate(t));

                if (bRotate)
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(-rotationAngle, rotationAngle, rotation.Evaluate(t))));

                if (bTranslate)
                    transform.localPosition = Vector3.Lerp(startPosition, toPos, translation.Evaluate(t));

                if (bAlpha)
                    cg.alpha = Mathf.Lerp(fromAlpha, toAlpha, alpha.Evaluate(t));

                yield return null;
            }

            if (bAlpha)
                cg.alpha = Mathf.Lerp(fromAlpha, toAlpha, alpha.Evaluate(1));

            if (bTranslate)
                transform.localPosition = Vector3.Lerp(startPosition, toPos, translation.Evaluate(1));

            if (bScale)
                transform.localScale = Vector3.Lerp(minScale, Vector3.one * scaleModifier, scale.Evaluate(1));
            if (bRotate)
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(-rotationAngle, rotationAngle, rotation.Evaluate(1))));
            yield break;
        }

        if (animMode == EAnimMode.PING_PONG)
        {
            while (true)
            {
                for (float c = 0, t = 0; c <= animationTime; c += Time.deltaTime)
                {
                    t = c / animationTime;

                    if (bScale)
                        transform.localScale = Vector3.Lerp(startScale, Vector3.one * scaleModifier, scale.Evaluate(t));

                    if (bRotate)
                        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(-rotationAngle, rotationAngle, rotation.Evaluate(t))));

                    yield return null;
                }
                if (bScale)
                    transform.localScale = Vector3.Lerp(startScale, Vector3.one * scaleModifier, scale.Evaluate(1));
                if (bRotate)
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(-rotationAngle, rotationAngle, rotation.Evaluate(1))));
                for (float c = animationTime, t = 0; c >= 0; c -= Time.deltaTime)
                {
                    t = c / animationTime;

                    if (bScale)
                        transform.localScale = Vector3.Lerp(startScale, Vector3.one * scaleModifier, scale.Evaluate(t));

                    if (bRotate)
                        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(-rotationAngle, rotationAngle, rotation.Evaluate(t))));


                    yield return null;
                }
                if (bScale)
                    transform.localScale = Vector3.Lerp(startScale, Vector3.one * scaleModifier, scale.Evaluate(0));
                if (bRotate)
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(-rotationAngle, rotationAngle, rotation.Evaluate(0))));
                yield return null;
            }
        }
    }
}
