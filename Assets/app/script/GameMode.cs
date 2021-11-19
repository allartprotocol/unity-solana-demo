using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour, iGameMode
{
    public virtual void StartGameMode() { }
    public virtual void StopGameMode() { }
    public virtual void InitGameMode() { }
    public virtual void RewardPoints(float points) { }

}
