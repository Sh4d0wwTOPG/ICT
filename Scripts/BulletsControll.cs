Copy code
using UnityEngine;
using System.Collections;

public class BulletsControll : MonoBehaviour {
    float speed;
    
    void Start () {
        speed = 8f;
    }
    
    
    void Update () {
        
        Vector2 position = transform.position;

        // Pohyb bulletu směrem nahoru se zadanou rychlostí
        position = new Vector2 (position.x, position.y + speed * Time.deltaTime);

        // Aktualizace pozice bulletu
        transform.position = position;
        
        // Získání maximálních hodnoty X a Y zobrazených v okně kamery
        Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
        
        // Zničení bulletu, pokud překročí horní okraj kamerového pohledu
        if (transform.position.y > max.y) {
            Destroy (gameObject);
        }
    }

    // Detekce kolize s objektem
    void OnTriggerEnter2D(Collider2D _collider) {
        // Zničení bulletu, pokud dojde ke kolizi s objektem označeným jako "EnemyShipTag"
        if (_collider.tag == "EnemyShipTag") {
            Destroy (gameObject);
        }
    }
}
