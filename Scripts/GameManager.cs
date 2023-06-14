using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject playButton;
    public GameObject airplane;
    public GameObject enemySpawner;
    public GameObject gameOverSprite;
    public GameObject userScore;
    public GameObject gameTimer;
    public GameObject gameTitle;

    // Výčet stavů hry
    public enum GameStates {
        Opening, GamePlay, GameOver
    }

    GameStates gameStates;

    void Start () {
    }

    // Metoda pro aktualizaci stavů hry
    void UpdateGameStates () {
        switch(gameStates) {
            case GameStates.GameOver:
                // Zastavení časovače hry
                gameTimer.GetComponent<TimeCounter>().stopTimeCounter();

                // Zastavení vytváření nepřátelských objektů
                enemySpawner.GetComponent<EnemySpawner>().StopEnemySpawn();

                // Zobrazení obrazovky s nápisem "Game Over"
                gameOverSprite.SetActive(true);

                // Po 8 sekundách nastavení stavu hry na "Opening"
                Invoke("SetGameStateToOpening", 8f);
                break;
            case GameStates.GamePlay:
                // Resetování skóre hráče na 0
                userScore.GetComponent<GameScore>().Score = 0;

                // Skrytí tlačítka Play a názvu hry
                playButton.SetActive(false);
                gameTitle.SetActive(false);

                // Inicializace ovládání hráčova letadla
                airplane.GetComponent<UserControll>().Init();

                // Spuštění vytváření nepřátelských objektů
                enemySpawner.GetComponent<EnemySpawner>().StartEnemySpawn();

                // Spuštění časovače hry
                gameTimer.GetComponent<TimeCounter>().startTimeCounter();
                break;
            case GameStates.Opening:
                // Skrytí obrazovky s nápisem "Game Over"
                gameOverSprite.SetActive(false);

                // Zobrazení tlačítka Play a názvu hry
                playButton.SetActive(true);
                gameTitle.SetActive(true);
                break;
        }
    }

    // Metoda pro nastavení stavu hry
    public void SetGameState(GameStates state) {
        gameStates = state;
        UpdateGameStates();
    }

    // Metoda pro spuštění hry
    public void StartGamePlay() {
        gameStates = GameStates.GamePlay;
        UpdateGameStates();
    }

    // Metoda pro nastavení stavu hry na "Opening"
    public void SetGameStateToOpening() {
        SetGameState(GameStates.Opening);
    }
}
