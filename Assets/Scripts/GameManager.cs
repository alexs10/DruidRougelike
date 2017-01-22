using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardManager boardScript;

    [HideInInspector]
    public bool playersTurn = true;

    private int level = 3;

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
        StartGame();
	}

    public void GameOver() {
        enabled = false;
    }

    void InitGame() {
        boardScript.setup(level);
    }

    void StartGame() {
        //TurnKeeper.GetInstance().TakeTurn();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
