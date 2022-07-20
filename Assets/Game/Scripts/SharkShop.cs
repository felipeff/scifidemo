using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour {


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();

                if (player != null)
                {
                    // check if player has coin
                    if (player.HasCoin())
                    {
                        player.RemoveCoin();
                        player.EnableWeapon();
                        AudioSource source = GetComponent<AudioSource>();
                        if (source != null)
                            source.Play();
                    }
                }
            }
        }
    }
	
}
