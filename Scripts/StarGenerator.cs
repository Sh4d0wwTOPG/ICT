using UnityEngine;
using System.Collections;

public class StarGenerator : MonoBehaviour {
    public GameObject starBg;    // Prefab hvězdy
    public int maxStars;    // Maximální počet hvězd

    Color[] starColors = {
        new Color(0.5f, 0.5f, 1f),    // Modrá
        new Color(0, 1f, 1f),    // Zelená
        new Color(1f, 1f, 0),    // Žlutá
        new Color(1f, 0, 0)    // Červená
    };

    void Start () {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));    // Levý dolní roh
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));    // Pravý horní roh

        for (int i = 0; i < maxStars; ++i) {
            GameObject star = (GameObject)Instantiate(starBg);
            star.GetComponent<SpriteRenderer>().color = starColors[Random.Range(0, 3)];    // Nastavení náhodné barvy hvězdy
            star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));    // Nastavení náhodné pozice hvězdy
            star.GetComponent<Stars>().speed = -(1f * Random.value + 0.5f);    // Nastavení náhodné rychlosti hvězdy
            star.transform.parent = transform;    // Nastavení nově vytvořené hvězdy jako dítě generátoru
        }
    }

    void Update () {
        // Prázdná metoda Update, není potřeba pro tento skript
    }
}
