using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [Header("Game")]
    public Player player;
    public GameObject enemyContainer;

    [Header("UI")]
    public Text healthText;
    public Text ammoText;
    public Text enemyText;

    private int initialEnemyCount;

    void Start()
    {
        initialEnemyCount = enemyContainer.GetComponentsInChildren<Enemy>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.Health;
        ammoText.text = "Ammo: " + player.Ammo;

        int killedEnemies = 0;
        foreach (Enemy enemy in enemyContainer.GetComponentsInChildren<Enemy>())
        {
            if (enemy.Killed == true)
            {
                killedEnemies++;
            }
        }

        enemyText.text = "Enemies: " + (initialEnemyCount - killedEnemies);
    }
}
