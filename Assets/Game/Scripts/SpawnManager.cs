using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShip;

    [SerializeField]
    private GameObject[] powerups;

    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    public void StartSpawnCoroutines()
    {
        StartCoroutine(CreateEnemysRoutine());
        StartCoroutine(CreatePowerUpsRoutine());
    }

    IEnumerator CreateEnemysRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            Instantiate(_enemyShip, new Vector3(Random.Range(-6f, 6f), 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator CreatePowerUpsRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            Instantiate(powerups[Random.Range(0, 3)], new Vector3(Random.Range(-6f, 6f), 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(10.0f);
        }
    }

   
}
