using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {
    public GameObject enemyBullets;
    public GameObject enemyObject;

    void Start () {
        // Zavolání metody EnemyFireBullets s časovým zpožděním 1 sekundy
        Invoke ("EnemyFireBullets", 1f);
    }

    void Update () {
    }

    // Metoda pro vystřelení střel nepřátelského objektu
    void EnemyFireBullets() {
        // Hledání objektu hráčova letadla
        GameObject playerAirplane = GameObject.Find ("AirCraft");
        if (playerAirplane != null) {
            // Spuštění animace střelby u nepřátelského objektu
            enemyObject.GetComponent<EnemyController> ().animationChooser ("shooting");

            // Vytvoření tří nepřátelských střel
            GameObject bullets = (GameObject)Instantiate (enemyBullets);
            GameObject bullets2 = (GameObject)Instantiate (enemyBullets);
            GameObject bullets3 = (GameObject)Instantiate (enemyBullets);

            // Nastavení pozice střel na pozici nepřátelského objektu
            bullets.transform.position = transform.position;
            bullets2.transform.position = transform.position;
            bullets3.transform.position = transform.position;

            // Výpočet směru střelby na základě pozice hráčova letadla
            Vector2 direction = playerAirplane.transform.position - bullets.transform.position;

            // Nastavení směru střelby pro jednotlivé střely
            bullets.GetComponent<EnemyBullets> ().setBulletsDirection (direction);
            bullets2.GetComponent<EnemyBullets> ().setBulletsDirection (new Vector2(direction.x + 2, direction.y));
            bullets3.GetComponent<EnemyBullets> ().setBulletsDirection (new Vector2(direction.x - 2, direction.y));
        }
    }
}
