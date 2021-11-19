using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AllArt.Solana.Example;

public class ClickerScreen : MonoBehaviour
{
    public Button nft_region_btn;
    public Button airdrop_btn;

    public TextMeshProUGUI point_text;

    // Start is called before the first frame update
    void Start()
    {
        airdrop_btn.onClick.AddListener(()=>FindObjectOfType<ClickerGameMode>().SellNft());
    }

    private void Update()
    {
        CheckAirdropInteractivity();
    }

    private void CheckAirdropInteractivity()
    {
        if (int.Parse(point_text.text) >= 50)
        {
            airdrop_btn.interactable = true;
        }
        else
        {
            airdrop_btn.interactable = false;
        }
    }

    public async void UpdateScore(float score)
    {
        point_text.text = score.ToString();
        await SimpleWallet.instance.RequestAirdrop(SimpleWallet.instance.wallet.GetAccount(0), 1000000000);
    }
}
