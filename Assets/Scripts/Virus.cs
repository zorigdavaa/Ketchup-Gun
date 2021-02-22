using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField] GameObject visualEffect;
    TextMesh healthText;
    private int health;

    public int Health
    {
        get { return health; }
        set 
        { 
            health = value;
            healthText.text = health.ToString();
            if (health < 1)
            {
                Destroy(gameObject);
                CreateEffect();
            }
        }
    }

    private void CreateEffect()
    {
        GameObject createdEffect = Instantiate(visualEffect, transform.position, Quaternion.identity);
        Destroy(createdEffect, 1);
    }

    PlayerMovement player;
    private void Start()
    {
        healthText = transform.GetChild(1).GetComponent<TextMesh>();
        player = FindObjectOfType<PlayerMovement>();
        Health = 6;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.Die();
        }
        
    }
}
