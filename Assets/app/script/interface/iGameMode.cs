using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iGameMode
{
    public void StartGameMode();
    public void StopGameMode();
    public void InitGameMode();
    public void RewardPoints(float points);
}
