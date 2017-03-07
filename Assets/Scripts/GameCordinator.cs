using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCordinator : MonoBehaviour, Observer {
    private LevelGenMediator levelGenMediator;
    private GameState gameState;

	// Use this for initialization
	void Start () {
        levelGenMediator = GetComponent<LevelGenMediator>();
        levelGenMediator.Register(this);
        levelGenMediator.GenerateMap();
	}

    void StartGame() {
        gameState = new MainState();
        gameState.OnEnterState();
    }


    //notified once map gen is done
    public void Notify() {
        StartGame();
    }
}
