using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShooterScreen : MonoBehaviour
{
    public Image hp_image;
    public TextMeshProUGUI point_text;

    public GameObject info_text;
    public GameObject game_over_text;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && App.instance.gameState == App.EGameState.ESTART)
        {
            App.instance.gameState = App.EGameState.EGAME;
            info_text.SetActive(false);
        }
        if (Input.anyKey && App.instance.gameState == App.EGameState.EEND)
        {
            FindObjectOfType<GameSelectionScreen>(true).gameObject.SetActive(true);
            SceneManager.UnloadSceneAsync("shooter_game");
        }
    }

    public void UpdateHP(int hp) {
        hp_image.fillAmount = (float)hp / 3;
        if (hp <= 0)
        {
            game_over_text.SetActive(true);
        }
    }

    public void UpdateScore(float score) {
        point_text.text = score.ToString();
    }
}
