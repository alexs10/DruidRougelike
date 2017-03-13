using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardManager boardScript;

	private List<Enemy> enemyList;
	private bool enemiesMoving;
	private bool doingSetup = true;

    [HideInInspector]
    public bool playersTurn = true;

    private int level = 3;
    private GameState gameState;

    // Use this for initialization
    void Awake () {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        Debug.Log("GameManager");

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
	}

    void Start() {
        StartGame();
    }

    public void GameOver() {
		Debug.Log ("GAME OVER **%*%*%*%*%*%*%*%*%*%*%*%*%*");
        enabled = false;
    }

    void InitGame() {
        boardScript.setup(level);
    }

    void StartGame() {
        gameState = new MainState();
        gameState.OnEnterState();
    }
	
	// Update is called once per frame
	void Update () {
        gameState.Update();
	}
}
