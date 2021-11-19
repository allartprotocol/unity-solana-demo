using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panner : MonoBehaviour
{
    public Renderer renderer;

    public float horizontalScrollSpeed = 0.25f;
    public float verticalScrollSpeed = 0.25f;

    private bool scroll = true;

    public void FixedUpdate()
    {
        if (scroll)
        {
            float verticalOffset = Time.time * verticalScrollSpeed;
            float horizontalOffset = Time.time * horizontalScrollSpeed;
            renderer.material.mainTextureOffset = new Vector2(horizontalOffset, verticalOffset);
        }
    }

    public void DoActivateTrigger()
    {
        scroll = !scroll;
    }

}
