using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject _crate;

    public void DestroyCrate()
    {
        Destroy(this.gameObject);
        Instantiate(_crate, transform.position, transform.rotation);
    }
}
