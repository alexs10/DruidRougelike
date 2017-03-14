using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Assets.Scripts.Map;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private Room currentRoom = null;
    private Vector2 playerSpawn = new Vector2(0, 0);
    public float turnDelay = 0.5f;
    public float levelStartDelay = 2f;
    [HideInInspector] public bool playersTurn = false;

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
            DontDestroyOnLoad(gameObject);
            boardScript = GetComponent<BoardManager>();
            enemyList = new List<Enemy>();

            map = new Map();
            Debug.Log("GameManager");

        } else if (instance != this)
        {
            Destroy(gameObject);
        }



        
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

    public void ChangeRoom(Room room) {
        if (room.x < currentRoom.x) {
            playerSpawn = room.layout.
        }
    }

    void InitGame() {
        doingSetup = true;

        //TODO: transition stuff
        Invoke("HideLevelImage", levelStartDelay);
        if (currentRoom == null) {
            Debug.Log("going to room 0");
            map.currentRoom.layout.BuildRoom();
        } else {
            Debug.Log("some other room");
            currentRoom.layout.BuildRoom();
        }
    }

    void HideLevelImage() {
        //levelImage.SetActive(false);

        doingSetup = false;
    }

    public bool IsPlayersTurn() {
        return playersTurn && !doingSetup;
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
