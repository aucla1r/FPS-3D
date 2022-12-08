using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _coinCollectedSound;
    private Player _player;
    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("_player is NULL on Coin.cs ");
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player")
        {
            _uiManager.PickupTextDisplay(true);
        }

        if (Input.GetKeyDown(KeyCode.E) && other.tag != null)
        {
            _player.hasCoins = true;
            AudioSource.PlayClipAtPoint(_coinCollectedSound, transform.position, 1f);
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            _uiManager.PickupTextDisplay(false);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        _uiManager.PickupTextDisplay(false);
    }
}
