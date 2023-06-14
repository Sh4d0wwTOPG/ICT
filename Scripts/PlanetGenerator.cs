using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetGenerator : MonoBehaviour {
    public GameObject[] planets;    // Pole s prefabrikovanými objekty planet
    Queue<GameObject> availablePlanets = new Queue<GameObject>();    // Fronta dostupných planet

    void Start () {
        // Přidání planet do fronty dostupných planet
        availablePlanets.Enqueue(planets[0]);
        availablePlanets.Enqueue(planets[1]);
        availablePlanets.Enqueue(planets[2]);

        // Spuštění opakovaného volání metody MovePlanet s interval 20 sekund
        InvokeRepeating("MovePlanet", 0, 20f);
    }

    void Update () {
        // Prázdná metoda Update, není potřeba pro tento skript
    }

    void MovePlanet() {
        EnqueuePlanets();

        // Pokud není žádná dostupná planeta, nedělej nic
        if (availablePlanets.Count == 0)
            return;

        // Vytáhnutí planety z fronty a nastavení jejího příznaku isMoving na true, čímž se spustí její pohyb
        GameObject aPlanet = availablePlanets.Dequeue();
        aPlanet.GetComponent<Planet>().isMoving = true;
    }

    void EnqueuePlanets() {
        // Procházení všech planet v poli
        foreach (GameObject aPlanet in planets) {
            // Pokud je planeta pod nulovou pozicí a není ve stavu pohybu, resetuje se její pozice a přidá se zpět do fronty dostupných planet
            if ((aPlanet.transform.position.y < 0) && !(aPlanet.GetComponent<Planet>().isMoving)) {
                aPlanet.GetComponent<Planet>().ResetPosition();
                availablePlanets.Enqueue(aPlanet);
            }
        }
    }
}
