using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum gameStatus
{
    next, play, gameover, win
}

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    private int totalWaves = 10;
    [SerializeField]
    private Text totalMoneyLabel;
    [SerializeField]
    private Text currentWaveLabel;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Text totalEscapedLalbel;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private int maxEnemiesOnScreen;
    [SerializeField]
    private int totalEnemies;
    [SerializeField]
    private int enemiesPerSpawn;
    [SerializeField]
    private Text playButtonLabel;

    private int waveNumber = 0;
    private int totalMoney = 10;
    private int totalEscaped = 0;
    private int roundEscaped = 0;
    private int totalKilled = 0;
    private int whichEnemiesToSpawn = 0;
    private gameStatus currentState = gameStatus.play;

    public List<Enemy> EnemyList = new List<Enemy>();

    const float spawnDelay = 0.5f;

    public int TotalMoney
    {
        get
        {
            return totalMoney;
        }
        set
        {
            totalMoney = value;
            totalMoneyLabel.text = totalMoney.ToString();
        }
    }

	// Use this for initialization
	void Start () {
        playButton.gameObject.SetActive(false);
        showMenu();
	}

    void Update()
    {
        handleEscape();
    }


    IEnumerator spawn()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[1]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                }
            }
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(spawn());
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyAllEnemies()
    {
        foreach(Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }

        EnemyList.Clear();
    }

    public void addMoney(int amount)
    {
        TotalMoney += amount;
    }

    public void subtractMoney(int amount)
    {
        TotalMoney -= amount;
    }

    public void showMenu()
    {
        switch (currentState)
        {
            case gameStatus.gameover:
                playButtonLabel.text = "Play Again!";
                break;
            case gameStatus.next:
                playButtonLabel.text = "Next Wave";
                break;
            case gameStatus.play:
                playButtonLabel.text = "Play";
                break;
            case gameStatus.win:
                playButtonLabel.text = "Play";
                break;
        }
        playButton.gameObject.SetActive(true);
    }


    private void handleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.disableDragSprite();
            TowerManager.Instance.towerBtnPressed = null;
        }
    }
}
