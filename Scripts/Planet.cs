using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
    public float speed;     // Rychlost pohybu planety
    public bool isMoving;   // Příznak, zda se planeta pohybuje

    Vector2 min;    // Minimální pozice planety ve světových souřadnicích
    Vector2 max;    // Maximální pozice planety ve světových souřadnicích

    void Awake() {
        isMoving = false;

        // Výpočet minimální a maximální pozice planety vzhledem k pohledovým souřadnicím kamery
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Upravení maximální a minimální pozice planety o rozměry jejího sprite
        max.y = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
        min.y = min.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
    }

    void Start () {
        // Prázdná metoda Start, není potřeba pro tento skript
    }

    void Update () {
        // Pokud se planeta nepohybuje, nedělej nic
        if (!isMoving)
            return;

        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        transform.position = position;

        // Pokud se planeta dostane pod minimální pozici, zastaví se pohyb
        if (transform.position.y < min.y) {
            isMoving = false;
        }
    }

    // Metoda pro resetování pozice planety
    public void ResetPosition() {
        transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }
}
