using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private AudioSource _audio;
    private UIManager _uiManager;


    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();

                if (player != null)
                {
                    player.GiveCoin();
                    _audio.Play();
                    _uiManager.EnableCoin();
                    Destroy(this.gameObject, 0.3f);
                }
            }
        }
    }

}
