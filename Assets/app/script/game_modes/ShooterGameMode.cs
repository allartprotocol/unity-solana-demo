using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterGameMode : GameMode
{
    public Player player;
    public EnemySpawner spawner;
    public ShooterScreen screen;
    public float points;

    public override void StartGameMode()
    {
        base.StartGameMode();
        player.inactive = false;
        spawner.StartSpawn();
    }

    public override void StopGameMode()
    {
        base.StopGameMode();
        player.onTakeDamage -= PlayerTakeDamage;
        player.inactive = true;
        spawner.StopSpawner();
    }
    
    public override void InitGameMode()
    {
        base.InitGameMode();
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<EnemySpawner>();
        screen = FindObjectOfType<ShooterScreen>();

        if (player) {
            player.onTakeDamage += PlayerTakeDamage;     
        }
    }

    void PlayerTakeDamage(int hp) {
        screen?.UpdateHP(hp);

        FindObjectOfType<CameraShake>().shakeDuration = 0.3f;
        if (hp <= 0)
        {
            App.instance.gameState = App.EGameState.EEND;        
        }
    }

    public override void RewardPoints(float score) {
        base.RewardPoints(score);
        points += score;
        screen.UpdateScore(points);
    }
}
