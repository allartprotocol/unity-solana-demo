using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AllArt.Solana;

public class NftAnimation : MonoBehaviour
{
    public AnimationCurve scaleCurve;
    Coroutine routine;

    public Image image_nft;
    public TextMeshProUGUI nft_name;

    private void Start()
    {
        PlayAnimation();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            PlayAnimation();
        }
    }

    public void PlayAnimation() {
        if (routine != null) {
            StopCoroutine(routine);
        }

        routine = StartCoroutine(ZoomAnimation());
    }

    // Start is called before the first frame update
    IEnumerator ZoomAnimation()
    {
        string mnem = WalletKeyPair.GenerateNewMnemonic();
        string[] newName = mnem.Split(' ');

        nft_name.text = $"{newName[0]} #{Random.Range(0, 99)}";
        image_nft.color = Random.ColorHSV();

        transform.localScale = Vector3.one;
        for (float i = 0f; i < 0.2f; i += Time.deltaTime)
        {
            float t = i / 0.2f;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.9f, scaleCurve.Evaluate(t));
            yield return null;
        }
    }
}
