using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6.0f;

    [SerializeField]
    private GameObject _enemyExplosion;

    
    public UIManager _uiManager;

    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (transform.position.y < -8)
        {
            transform.position = new Vector3(Random.Range(-6.0f, 7.0f), 6.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Laser laser = other.GetComponent<Laser>();

            if (laser != null)
            {
                Destroy(laser.gameObject);
                Instantiate(_enemyExplosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                _uiManager.UpdateScore();
            }
        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.OneLifeLess();
            }
            
            Instantiate(_enemyExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            if (_uiManager != null)
            {
                _uiManager.LoseScore();
            }

        }
    }
}
