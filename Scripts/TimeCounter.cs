using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour {
    Text timeUI;    // UI prvek pro časový počítadlo

    float startTime;    // Čas po stisknutí tlačítka "play"
    float elapsedTime;
    bool startCounter;

    float minutes;
    float seconds;

    void Start () {
        startCounter = false;
        timeUI = GetComponent<Text> ();    // Získání komponenty Text pro zobrazení času
    }

    public void startTimeCounter() {
        startTime = Time.time;    // Nastavení počátečního času na aktuální čas
        startCounter = true;
    }

    public void stopTimeCounter() {
        startCounter = false;    // Zastavení čítání času
    }

    void Update () {
        if (startCounter) {
            // Počítání času
            elapsedTime = Time.time - startTime;
            minutes = elapsedTime / 60;
            seconds = elapsedTime % 60;

            timeUI.text = string.Format ("{0:00}:{1:00}", minutes, seconds);    // Aktualizace textu časového UI
        }
    }
}
