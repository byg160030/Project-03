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

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.Health;
        ammoText.text = "Ammo: " + player.Ammo;

        int aliveEnemies = 0;
        foreach (Enemy enemy in enemyContainer.GetComponentsInChildren<Enemy>())
        {
            if (enemy.Killed == false)
            {
                aliveEnemies++;
            }
        }

        enemyText.text = "Enemies: " + aliveEnemies;
    }
}
