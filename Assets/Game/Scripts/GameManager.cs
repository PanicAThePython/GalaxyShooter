using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;

    public GameObject player;
    private UIManager _uiManager;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && (gameOver==true))
        {
            gameOver = false;
            Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
            _uiManager.HideMainMenu();
        }
    }

}
