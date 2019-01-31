using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oof : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player _player = collision.collider.GetComponent<Player>();
        if (_player!=null)
        {
            _player.DamagePlayer(1);
        }
    }
}
