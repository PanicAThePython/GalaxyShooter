using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    private int _powerUpID;

    [SerializeField]
    private AudioClip _clip;
    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            if (player != null)
            {
                if (_powerUpID == 0)
                {
                    player.TripleShotPowerUpOn();
                }
                else if (_powerUpID == 1)
                {
                    player.SpeedPowerUpOn();
                }
                else if (_powerUpID == 2)
                {
                    player.ShieldPowerUpOn();
                }
            }

            Destroy(this.gameObject);
        }
        
    }


}
