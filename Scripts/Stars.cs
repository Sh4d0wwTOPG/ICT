using UnityEngine;
using System.Collections;

public class Stars : MonoBehaviour {
    public float speed;    // Rychlost pohybu hvězdy

    void Start () {
        // Prázdná metoda Start, není potřeba pro tento skript
    }

    void Update () {
        Vector2 position = transform.position;    // Získání aktuální pozice hvězdy
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        transform.position = position;    // Aktualizace pozice hvězdy
        // Pokud se hvězda přesune mimo obrazovku, přesuneme ji zpět na horní část obrazovky
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));    // Levý dolní roh
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));    // Pravý horní roh
        if (transform.position.y < min.y) {
            transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }
    }
}
