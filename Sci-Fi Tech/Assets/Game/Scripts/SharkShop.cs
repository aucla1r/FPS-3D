using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    private UIManager _uiManager;
    private Player _player;
    private AudioSource _audio;

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player")
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
            _audio = GetComponent<AudioSource>();
            if (_player.hasCoins == true)
            {
                _uiManager.ShopDisplay(true);
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _player.hasCoins = false;
                    _uiManager.CoinDisplay(false);
                    _audio.Play();
                    _player.isWeaponEnabled = true;
                    _player.WeaponVisible();
                }
            }
            else if (_player.hasCoins == false)
            {
                _uiManager.ShopDisplay(false);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.ShopDisplayFalse();
    }
}