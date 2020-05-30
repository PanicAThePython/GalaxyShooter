using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool canSpeedUp = false;
    public bool canShieldUp = false;

    public int qtdLife = 3;

    [SerializeField]
    private GameObject _shieldPlayer;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _playerExplosion;


    [SerializeField]
    private float _fireRate = 0.25f;
    
    private float _canFire = 0.0f;

    
    [SerializeField]
    private float _speed = 5.0f;

    public UIManager _uiManager;
    public GameManager _gameManager;
    public SpawnManager _spawnManager;


    private void Start()
    {

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_uiManager != null)
        {
            _uiManager.UpdateLives(qtdLife);
        }

        if (_spawnManager != null && _gameManager.gameOver == false)
        {
            _spawnManager.StartSpawnCoroutines();
        }

    }

    
    private void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            VerifyTripleShoot();
        }

        
    }

    

    public void OneLifeLess()
    {
        if (canShieldUp == true)
        {
            canShieldUp = false;
            _shieldPlayer.SetActive(false);
        }
        else
        {
            qtdLife -= 1;
            _uiManager.UpdateLives(qtdLife);
            if (qtdLife == 2)
            {
                _leftEngine.SetActive(true);
            }
            else if (qtdLife == 1)
            {
                _rightEngine.SetActive(true);
            }
            else if (qtdLife == 0)
            {
                Instantiate(_playerExplosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                _gameManager.gameOver = true;
                _uiManager.ShowMainMenu();
                _uiManager.score = 0;
                _uiManager.points = 0;
            }
        }   
    }

    private void VerifyTripleShoot()
    {
        if (canTripleShot == false)
        {
            Shoot();
        }
        else
        {
            DoTripleShot();
        }
    }

    public void ShieldPowerUpOn()
    {
        canShieldUp = true;
        _shieldPlayer.SetActive(true);
    }


    public void SpeedPowerUpOn()
    {
        canSpeedUp = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }


    public void TripleShotPowerUpOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }


    public IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedUp = false;

    }

    private void DoTripleShot()
    {
        if (Time.time > _canFire)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            Instantiate(_laserPrefab, transform.position + new Vector3(0.55f, 0.06f, 0), Quaternion.identity);
            Instantiate(_laserPrefab, transform.position + new Vector3(-0.55f, 0.06f, 0), Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (canSpeedUp == false)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed*2.0f * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed *2.0f* verticalInput);
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //if (transform.position.x > 8)
        //{
        //    transform.position = new Vector3(8, transform.position.y, 0);
        //}
        //else if (transform.position.x < -8 )
        //{
        //    transform.position = new Vector3(-8, transform.position.y, 0);
        //}

        if (transform.position.x > 9.4f)
        {
            transform.position = new Vector3(-8.7f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.4)
        {
            transform.position = new Vector3(8.7f, transform.position.y, 0);
        }
    }
}
