using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemy;
    float maxSpawnRateInSecond = 5f;

    void Start () {
    }

    void Update () {
    }

    // Metoda pro vytvoření nepřátelského objektu
    void SpawnEnemy() {
        // Získání minimálního a maximálního bodu zobrazeného v okně kamery
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Vytvoření nepřátelského objektu a umístění na náhodnou pozici v horní části obrazovky
        GameObject enemy01 = (GameObject)Instantiate(enemy);
        enemy01.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        // Naplánování dalšího vytvoření nepřátelského objektu
        NextSpawnScheduler();
    }

    // Metoda pro naplánování dalšího vytvoření nepřátelského objektu
    void NextSpawnScheduler() {
        float spawnInNSec;
        if (maxSpawnRateInSecond > 1f) {
            // Náhodný časový interval pro vytvoření dalšího nepřátelského objektu
            spawnInNSec = Random.Range(1f, maxSpawnRateInSecond);
        } else {
            spawnInNSec = 1f;
        }
        Invoke("SpawnEnemy", spawnInNSec);
    }

    // Metoda pro zvýšení rychlosti vytváření nepřátelských objektů
    void IncreaseSpawnRate() {
        if (maxSpawnRateInSecond > 1f) {
            maxSpawnRateInSecond--;
        }

        if (maxSpawnRateInSecond == 1f) {
            CancelInvoke("IncreaseSpawnRate");
        }
    }

    // Metoda pro spuštění vytváření nepřátelských objektů
    public void StartEnemySpawn() {
        maxSpawnRateInSecond = 5f;
        Invoke("SpawnEnemy", maxSpawnRateInSecond);

        // Pravidelné zvyšování rychlosti vytváření nepřátelských objektů každých 30 sekund
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    // Metoda pro zastavení vytváření nepřátelských objektů
    public void StopEnemySpawn() {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
