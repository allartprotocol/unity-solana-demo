using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerGameMode : GameMode
{
    public float points;
    public ClickerScreen screen;
    public Camera camera;
    public GameObject clickparticle;

    private void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "nft_region")
                {
                    Instantiate(clickparticle, hit.point, Quaternion.identity);
                    FindObjectOfType<NftAnimation>().PlayAnimation();
                    RewardPoints(1);
                }
            }
        }
    }

    

    public override void StartGameMode()
    {
        base.StartGameMode();
    }

    public override void StopGameMode()
    {
        base.StopGameMode();
    }

    public override void InitGameMode()
    {
        base.InitGameMode();
    }


    public override void RewardPoints(float score)
    {
        base.RewardPoints(score);
        points += score;
        screen.UpdateScore(points);
    }

    public void SellNft() {
        points = 0;
        screen.UpdateScore(points);
        //airdrop here
    }
}
