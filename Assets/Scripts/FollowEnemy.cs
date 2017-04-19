using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowEnemy : MonoBehaviour {
    private Transform followTarget;
    private Vector3 positionOffset = new Vector3(0, 50, 0) ;
    private Text healthText;
    private Enemy enemyTarget;

    private RectTransform rt;
    private RectTransform canvasRt;
    private Vector3 enemyPos;
    private Vector3 enemyScreenPos;

    // Use this for initialization
    void Start () {
        enemyTarget = GetComponentInParent<Enemy>();
        followTarget = transform.parent.transform;
        transform.parent = GameObject.Find("HealthBars").transform;

        rt = GetComponent<RectTransform>();
        canvasRt = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        enemyScreenPos = Camera.main.WorldToViewportPoint(followTarget.TransformPoint(enemyPos));
        rt.anchoredPosition = enemyScreenPos + positionOffset;




        healthText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (followTarget != null) {
            enemyScreenPos = Camera.main.WorldToViewportPoint(followTarget.TransformPoint(enemyPos));
            rt.anchorMax = enemyScreenPos;
            rt.anchorMin = enemyScreenPos;
            if (GameManager.instance.doingSetup) {
                healthText.text = "";
            } else {
                healthText.text = enemyTarget.health + "/" + enemyTarget.maxHealth;
            }
        } else {
            healthText.text = "";
            Destroy(this);
        }
    }
}
