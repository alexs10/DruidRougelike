using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Assets.Scripts.Map;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public float turnDelay = 0.5f;
    public float levelStartDelay = 2f;
    [HideInInspector] public bool playersTurn = true;

    private BoardManager boardScript;
    private List<Enemy> enemyList;
	private bool enemiesMoving;
	private bool doingSetup = true;

    private Map map;

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
        enemyList = new List<Enemy>();

        map = new Map();
	}

    public void Start() {
        InitGame();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization() {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    //This is called each time a scene is loaded.
    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) {
        //instance.level++;
        instance.InitGame();
    }

    public void GameOver() {
		Debug.Log ("GAME OVER **%*%*%*%*%*%*%*%*%*%*%*%*%*");
        enabled = false;
    }

    void InitGame() {
        doingSetup = true;

        //TODO: transition stuff
        Invoke("HideLevelImage", levelStartDelay);
        map.currentRoom.layout.BuildRoom();
    }

    void HideLevelImage() {
        //levelImage.SetActive(false);

        doingSetup = false;
    }

    // Update is called once per frame
    void Update () {
        if (playersTurn || enemiesMoving || doingSetup) {
            return;
        }

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy enemy) {
        enemyList.Add(enemy);
    }

    //Coroutine to move enemies in sequence.
    IEnumerator MoveEnemies() {
        Debug.Log("EnemyTurn");
        enemiesMoving = true;

        yield return new WaitForSeconds(turnDelay);

        if (enemyList.Count == 0) {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemyList.Count; i++) {
            enemyList[i].MoveEnemy();
            yield return new WaitForSeconds(enemyList[i].moveTime);
        }
        playersTurn = true;
        enemiesMoving = false;
    }
}
