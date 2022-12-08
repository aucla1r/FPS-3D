using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _reloadText;
    [SerializeField] private Text _ammoText;
    [SerializeField] private Text _pickupText;
    [SerializeField] private Text _shopText;
    [SerializeField] private Image _coinImage;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void AmmoDisplay(int currentAmmo, int maxAmmo)
    {
        _ammoText.gameObject.SetActive(true);
        _ammoText.text = "Ammo : " + currentAmmo + " / " + maxAmmo;
    }

    public void ReloadTextDisplay()
    {
        _reloadText.text = "Reloading.";
        _reloadText.gameObject.SetActive(true);
        StartCoroutine(ReloadTextAnimation());
    }

    public void CoinDisplay(bool hasCoins)
    {
        _coinImage.gameObject.SetActive(hasCoins);
    }

    public void PickupTextDisplay(bool isCollected)
    {
        _pickupText.gameObject.SetActive(isCollected);
    }

    public void ShopDisplay(bool isPlayerHasCoin)
    {
        if (isPlayerHasCoin == true)
        {
            _pickupText.gameObject.SetActive(true);
            _pickupText.text = "Press 'E' to Buy";
            _shopText.gameObject.SetActive(true);
            _shopText.text = "Need a weapon?";
            _shopText.color = Color.green;
        }
        else if (isPlayerHasCoin == false)
        {
            _pickupText.gameObject.SetActive(false);
            _shopText.gameObject.SetActive(true);
            _shopText.text = "Doesn't have coin? Get away!";
            _shopText.color = Color.red;
        }
    }

    public void ShopDisplayFalse()
    {
        _pickupText.gameObject.SetActive(false);
        _shopText.gameObject.SetActive(false);
    }

    IEnumerator ReloadTextAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        _reloadText.text = "Reloading..";
        yield return new WaitForSeconds(0.5f);
        _reloadText.text = "Reloading...";
        yield return new WaitForSeconds(0.1f);
        _reloadText.gameObject.SetActive(false);
    }
}
