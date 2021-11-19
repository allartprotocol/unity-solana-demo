using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public SpriteRenderer playerRenderer;
    public Sprite[] playerSprites;

    public GameObject projectilePrefab;
    public Pool<Projectile> projectilePool;

    public bool inactive = true;

    public int currentHP = 3;

    public Action<int> onTakeDamage;

    private void Start()
    {
        projectilePool = new Pool<Projectile>(projectilePrefab, 30);
    }

    // Update is called once per frame
    void Update()
    {
        if (inactive)
            return;

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;
            playerRenderer.sprite = playerSprites[1];
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            playerRenderer.sprite = playerSprites[2];
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        { 
            playerRenderer.sprite = playerSprites[0];        
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void Fire() { 
        Projectile proj = projectilePool.GetFromPool();
        
        proj.transform.position = transform.position;
        proj.gameObject.SetActive(true);
    }

    public void TakeDamage() {
        currentHP--;

        onTakeDamage?.Invoke(currentHP);
    }

}
