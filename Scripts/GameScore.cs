using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {
    Text scoreText;    // Reference na komponentu UI textu pro zobrazení skóre
    int userScore;     // Proměnná pro uchování skóre hráče

    public int Score {
        get {
            return this.userScore;
        }
        set {
            this.userScore = value;
            UpdateScoreTextUI();
        }
    }

    void Start () {
        // Získání komponenty Text UI tohoto objektu
        scoreText = GetComponent<Text>();
    }

    // Metoda pro aktualizaci zobrazení skóre
    public void UpdateScoreTextUI() {
        // Formátování skóre na textový řetězec s délkou 6 a předřazenými nulami (např. "000001")
        string scoreString = string.Format("{0:000000}", userScore);
        scoreText.text = scoreString; // Aktualizace zobrazeného textu skóre
    }

    void Update () {
        // Prázdná metoda Update, není potřeba pro tento skript
    }
}
