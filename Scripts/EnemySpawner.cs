using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemy;    
	float maxSpawnRateInSecond = 5f;

	
	void Start () {
	}
	
	
	void Update () {
	}

	
	void SpawnEnemy() {
		
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		
		GameObject enemy01 = (GameObject)Instantiate (enemy);
		enemy01.transform.position = new Vector2 (Random.Range(min.x, max.x), max.y);

		NextSpawnSheduler ();
	}

	
	void NextSpawnSheduler() {
		float spawnInNSec;
		if ( maxSpawnRateInSecond > 1f ) {
			
			spawnInNSec = Random.Range (1f, maxSpawnRateInSecond);
		} else {
			spawnInNSec = 1f;
		}
		Invoke ("SpawnEnemy", spawnInNSec);
	}

	
	void IncreseSpawnRate() {
		if ( maxSpawnRateInSecond > 1f ) {
			maxSpawnRateInSecond--;
		}

		if ( maxSpawnRateInSecond == 1f ) {
			CancelInvoke ("IncreseSpawnRate");
		}
	}

	
	public void StartEnemySpawn() {
		maxSpawnRateInSecond = 5f;    // 重設生怪時間
		Invoke ("SpawnEnemy", maxSpawnRateInSecond);
		// 每30秒減少怪物重造時間(PS:第一個是方法名，第二個是「第一次調用」要隔幾秒，第三個是「每隔幾秒調用一次」)
		InvokeRepeating("IncreseSpawnRate", 0f, 30f);
	}

	
	public void StopEnemySpawn() {
		CancelInvoke ("SpawnEnemy");
		CancelInvoke ("IncreseSpawnRate");
	}

}
