using AllArt.Solana.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectWalletScreen : SimpleScreen
{
    Coroutine routine;
    AnimationComponent animComponent;

    // Start is called before the first frame update
    public override void ShowScreen(object data = null)
    {
        base.ShowScreen(data);
        SetupScreen();
    }

    private void SetupScreen()
    {
        StartAwait();
        GetComponentInChildren<AnimationComponent>(true)?.PlayAnimation();
    }

    public void StartAwait() {
        if (routine != null)
        {
            StopCoroutine(routine);
        }

        if (SimpleWallet.instance != null)
        { 
            routine = StartCoroutine(AwaitWalletConnetion());
        }
    }

    IEnumerator AwaitWalletConnetion()
    {
        while (SimpleWallet.instance.wallet == null)
        {
            yield return null;
        }
        manager.ShowScreen(this, "[Level_Selection_Screen]");
    }

    public override void HideScreen()
    {
        base.HideScreen();
        gameObject.SetActive(false);
    }
}
