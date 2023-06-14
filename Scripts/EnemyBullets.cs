using UnityEngine;
using System.Collections;

public class EnemyBullets : MonoBehaviour {
    float speed;
    Vector2 bulletDirection;
    bool isReady;

    // Metoda Awake se volá při inicializaci objektu
    void Awake() {
        speed = 5f;
        isReady = false;
    }

    void Start () {}

    void Update () {
        // Pokud jsou střely připravené k pohybu
        if (isReady) {
            Vector2 position = transform.position;

            // Pohyb střely ve směru daném bulletDirection s danou rychlostí
            position += bulletDirection * speed * Time.deltaTime;

            // Aktualizace pozice střely
            transform.position = position;

            // Získání minimálního a maximálního bodu zobrazeného v okně kamery
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            // Zničení střely, pokud překročí hranice okna kamery
            if (transform.position.x > max.x || transform.position.y > max.y ||
                transform.position.x < min.x || transform.position.y < min.y) {
                Destroy(gameObject);
            }
        }
    }

    // Metoda pro nastavení směru střely
    public void setBulletsDirection(Vector2 dir) {
        bulletDirection = dir.normalized;
        isReady = true;
    }

    // Detekce kolize s objektem
    void OnTriggerEnter2D(Collider2D _collider) {
        // Zničení střely, pokud dojde ke kolizi s objektem označeným jako "PlayerShipTag"
        if (_collider.tag == "PlayerShipTag") {
            Destroy(gameObject);
        }
    }

}
