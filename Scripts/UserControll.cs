using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserControll : MonoBehaviour {
    public GameObject gameManager;    // Manažer hry
    public float speed;
    public GameObject bullets;    // Prefab pro střelu
    public GameObject bulletPosition1;
    public GameObject bulletPosition2;
    public GameObject explosion;    // Prefab pro explozi
    private const float firstX = 0.01f;
    private const float firstY = -2.53f;
    private Animator anim;
    float fireRate = 0.25f;
    private float nextFire = 0.15f;

    public Text lives;    // UI pro životy
    const int MaxLives = 3;
    int currentLives;    // Aktuální počet životů

    public void Init() {
        currentLives = MaxLives;
        lives.text = currentLives.ToString();    // Zobrazí počet životů na UI
        gameObject.transform.position = new Vector2(firstX, firstY);
        gameObject.SetActive(true);    // Aktivuje herní objekt hráče
    }

    void Start () {
        anim = GetComponent<Animator> ();
    }

    void Update () {
        // Stisknutí klávesy C způsobí výstřel
        if (Input.GetKey(KeyCode.C) && Time.time > nextFire) {
            animationChooser("shooting");
            gameObject.GetComponent<AudioSource>().Play();    // Zvukový efekt
            GameObject bullets01 = (GameObject)Instantiate(bullets);
            bullets01.transform.position = bulletPosition1.transform.position;

            GameObject bullets02 = (GameObject)Instantiate(bullets);
            bullets02.transform.position = bulletPosition2.transform.position;
            nextFire = Time.time + fireRate;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)) {
            speed = 2f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            speed = 4f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            speed = 6f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            speed = 8f;
        }
        
        float x = Input.GetAxisRaw("Horizontal");    // Hodnota -1 pro vlevo, 0 pro žádný vstup, 1 pro vpravo
        float y = Input.GetAxisRaw("Vertical");      // Hodnota -1 pro dolů, 0 pro žádný vstup, 1 pro nahoru

        Vector2 direction = new Vector2(x, y).normalized;    // Převod na vektor použitelný v Unity
        Move(direction);
    }

    void Move(Vector2 dir) {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));    // Levý dolní roh obrazovky
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));    // Pravý horní roh obrazovky

        max.x = max.x - 0.15f;    // Odečítá polovinu šířky hráče
        min.x = min.x + 0.2f;    // Přidává polovinu šířky hráče
        max.y = max.y - 0.15f;    // Odečítá polovinu výšky hráče
        min.y = min.y + 0.2f;    // Přidává polovinu výšky hráče

        Vector2 pos = transform.position;    // Aktuální pozice hráče

        pos += dir * speed * Time.deltaTime;    // Výpočet nové pozice

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);    // Omezení na obrazovku
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;    // Aktualizace pozice hráče
    }

    void OnTriggerEnter2D(Collider2D _collider) {
        // Detekce kolize s nepřátelskou lodí nebo střelou
        if (_collider.tag == "EnemyShipTag" || _collider.tag == "EnemyBulletTag") {
            currentLives--;
            lives.text = currentLives.ToString();
            GameObject explosionObj = (GameObject)Instantiate(explosion);
            explosionObj.transform.position = transform.position;
            if (currentLives == 0) {
                gameManager.GetComponent<GameManager>().SetGameState(GameManager.GameStates.GameOver);
                gameObject.SetActive(false);    // Skryje herní objekt hráče
            }
            else {
                gameObject.SetActive(false);
                Invoke("airplaneRebirth", 2f);    // Po 2 sekundách obnoví hráče
            }
        }
    }

    public void airplaneRebirth() {
        gameObject.transform.position = new Vector2(firstX, firstY);
        gameObject.SetActive(true);
    }

    public void animationChooser(string action) {
        switch (action) {
            case "shooting":
                anim.SetBool("Shooting", true);
                break;
            case "unshooting":
                anim.SetBool("Shooting", false);
                break;
            default:
                break;
        }
    }
}
