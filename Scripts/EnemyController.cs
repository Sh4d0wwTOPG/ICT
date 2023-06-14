using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    float speed;
    public int score;
    public int life;
    public GameObject explosion;
    GameObject scoreTextUI;
    private Animator anim;

    void Start () {
        speed = 2f;
        scoreTextUI = GameObject.FindGameObjectWithTag("ScoreUITag");
        anim = GetComponent<Animator>();
    }

    void Update () {
        Vector2 position = transform.position;

        // Pohyb nepřátelského objektu dolů se zadanou rychlostí
        position = new Vector2 (position.x, position.y - speed * Time.deltaTime);
        transform.position = position;

        // Získání minimálního bodu zobrazeného v okně kamery
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // Zničení nepřátelského objektu, pokud překročí dolní okraj kamerového pohledu
        if (transform.position.y < min.y) {
            Destroy (gameObject);
        }
    }

    // Detekce kolize s objektem
    void OnTriggerEnter2D(Collider2D _collider) {
        // Pokud dojde ke kolizi s objektem označeným jako "PlayerBulletTag"
        if (_collider.tag == "PlayerBulletTag") {
            life--;

            // Pokud dojde k životům nepřátelského objektu, zničí se a přičte se skóre
            if (life == 0) {
                Destroy (gameObject);
                scoreTextUI.GetComponent<GameScore> ().Score += score;

                // Vytvoření a umístění exploze na pozici zničeného objektu
                GameObject expo = (GameObject)Instantiate (explosion);
                expo.transform.position = transform.position;
            }
        }
    }

    // Metoda pro výběr animace
    public void animationChooser(string method) {
        switch(method) {
            case "shooting":
                anim.SetBool ("shooting", true);
                break;
            case "unshooting":
                anim.SetBool ("shooting", false);
                break;
            default:
                break;
        }
    }
}
