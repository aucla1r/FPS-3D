using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float sensitivity = 2.0f;
    [SerializeField] private float _speed = 8.0f;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private GameObject _muzzleFlashParticle;
    [SerializeField] private GameObject _hitMarkerPrefab;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private Destructible _crate;
    [SerializeField] private AudioSource _weaponSound;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private int _ammoCount;
    [SerializeField] private int _maxAmmo = 70;
    [SerializeField] public bool hasCoins = false;
    private CharacterController _controller;
    private bool _isReloading = false;
    private bool _isShoot = true;
    public bool isWeaponEnabled = false;
    // Start is called before the first frame update
    void Start()
    {   
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _controller = GetComponent<CharacterController>();
        if (isWeaponEnabled == true)
        {
            _weaponSound = GameObject.Find("Weapon").GetComponent<AudioSource>();
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _ammoCount = _maxAmmo;

        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        InvisibleCursor();
        if (isWeaponEnabled == true)
        {
            Shoot();
            _uiManager.AmmoDisplay(_ammoCount, _maxAmmo);
        }
        
        if (hasCoins == true)
        {
            _uiManager.CoinDisplay(true);
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput );
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * _speed * Time.deltaTime);
    }

    void InvisibleCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }    
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0) && _ammoCount > 0 && _isShoot == true)
        {
            StartCoroutine(DestroyHitMarker());
            _muzzleFlashParticle.SetActive(true);
            _ammoCount--;
            if (_weaponSound.isPlaying == false)
            {
                _weaponSound.Play();
            }
        }
        else 
        {
            _muzzleFlashParticle.SetActive(false);
            _weaponSound.Stop();
            StartCoroutine(ReloadWeapon());
        }
    }

    public void WeaponVisible()
    {
        _weapon.SetActive(true);
    }

    IEnumerator DestroyHitMarker()
    {
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log("Hit" + hitInfo.transform.name);
            GameObject hitmarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            
            yield return new WaitForSeconds(1f);
            Destroy(hitmarker);
        }

        _crate = hitInfo.transform.GetComponent<Destructible>();
        if (_crate != null)
        {
            _crate.DestroyCrate();
        }
    }

    IEnumerator ReloadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isReloading == false)
        {
            _isReloading = true;
            _isShoot = false;
            _uiManager.ReloadTextDisplay();
            yield return new WaitForSeconds(1.5f); 
            _ammoCount = _maxAmmo;
            _isReloading = false;
            _isShoot = true;
        }
    }
}
