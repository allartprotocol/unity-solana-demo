using AllArt.Solana.Example;
using Solnet.Rpc.Core.Http;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Solnet.Rpc.Models;

public class GameSelectionScreen : SimpleScreen
{
    [Header("UI Elements (Buttons)")]
    public Button galaxy_wars_btn;
    public Button nft_clicker_btn;
    public GameObject hidden_game;

    public Button buy_token;

    [Header("UI Elements (Text)")]
    public TextMeshProUGUI token_count;
    public TextMeshProUGUI error_log;

    public int heldTokens = 0;

    private void Start()
    {
        BindButtonEvents();
        CheckUserTokens();
        CheckForDLCNft();
        ResetError();
    }

    private void ResetError()
    {
        error_log.text = "";
    }

    private void CheckUserTokens()
    {
        if (PlayerPrefs.HasKey("tokens"))
        {
            heldTokens = PlayerPrefs.GetInt("tokens");
            UpdateTokens(heldTokens);
        }
        else
        {
            heldTokens = 0;
        }
    }

    private void BindButtonEvents()
    {
        galaxy_wars_btn.onClick.AddListener(() => StartGalaxyWars());
        nft_clicker_btn.onClick.AddListener(() => StartNFTClicker());
        buy_token.onClick.AddListener(() => { BuyToken(); });
    }

    bool CheckTookens()
    {
        if (heldTokens <= 0)
        {
            HandleErrorMessage("YOU NEED TOKENS TO PLAY");
            return false;
        }
        else
        {
            HandleErrorMessage();
            return true;
        }
    }

    private void HandleErrorMessage(string errorMsg = "")
    {
        error_log.text = errorMsg;
    }

    void UpdateTokens(int tokens) {
        heldTokens = tokens;
        PlayerPrefs.SetInt("tokens", tokens);
        token_count.text = $"Tokens owned: {tokens}";
    }

    public override void ShowScreen(object data = null)
    {
        base.ShowScreen(data);
        CheckUserTokens();
        CheckForDLCNft();
    }

    public override void HideScreen()
    {
        base.HideScreen();
    }

    public async void BuyToken() {
        RequestResult<string> result = await SimpleWallet.instance.TransferSol("7DZDmN8CGbiSV74gGwMzqMfv14Y4BSM9ViaemMyR9apZ", 100000000);
        
        HandleResponse(result);
    }

    private void HandleResponse(RequestResult<string> result)
    {
        if (result.Result == null)
        {
            HandleErrorMessage(result.Reason);
        }
        else
        {
            heldTokens++;
            UpdateTokens(heldTokens);
            HandleErrorMessage();
        }
    }

    public void StartGalaxyWars() {
        if(!CheckTookens())
            return;

        heldTokens--;
        UpdateTokens(heldTokens);
        string sceneName = "shooter_game";
        LoadNewScene(sceneName);
        gameObject.SetActive(false);
    }

    public void StartNFTClicker()
    {
        if (!CheckTookens())
            return;

        heldTokens--;
        UpdateTokens(heldTokens);
        string sceneName = "clicker_scene";
        LoadNewScene(sceneName);
        gameObject.SetActive(false);
    }

    private void LoadNewScene(string sceneName)
    {
        StartCoroutine(AwaitSceneLoad(sceneName));
    }

    IEnumerator AwaitSceneLoad(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    public async void CheckForDLCNft() {
        if (SimpleWallet.instance == null || SimpleWallet.instance.wallet == null)
        {
            return;
        }

        TokenAccount[] result = await SimpleWallet.instance.GetOwnedTokenAccounts(SimpleWallet.instance.wallet.GetAccount(0));

        if (result.Length > 0)
        {
            foreach (TokenAccount item in result)
            {
                if (item.Account.Data.Parsed.Info.Mint == "GYPKdK84W1WMSzRUFy7CnySS7tamPhEG1bXS6Q2zzF9")
                {
                    hidden_game.SetActive(false);
                    nft_clicker_btn.gameObject.SetActive(true);
                    return;
                }
            }
        }
        hidden_game.SetActive(true);
        nft_clicker_btn.gameObject.SetActive(false);
    }
}
