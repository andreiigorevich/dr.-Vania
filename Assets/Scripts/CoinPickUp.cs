using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{

    [SerializeField] AudioClip sfx;
    [SerializeField] int _scoireToAdd = 100;

    private bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddScore(_scoireToAdd);
            AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
