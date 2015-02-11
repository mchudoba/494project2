using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour
{
	/* -----Variables----- */

	public RedEnemy redEnemyPrefab;
	public GreenEnemy greenEnemyPrefab;
	public float redEnemySpawnTime;
	public float greenEnemySpawnTime;
	public Vector2 minSpawn;
	public Vector2 maxSpawn;

	/* -----Unity methods----- */

	void Start()
	{
		InvokeRepeating("SpawnRedEnemy", 1f, redEnemySpawnTime);
		InvokeRepeating("SpawnGreenEnemy", 16f, greenEnemySpawnTime);
	}

	/* -----Custom methods----- */

	void SpawnRedEnemy()
	{
		Instantiate(redEnemyPrefab, RandomSpawnLocation(), Quaternion.identity);
	}

	void SpawnGreenEnemy()
	{
		Instantiate(greenEnemyPrefab, RandomSpawnLocation(), Quaternion.identity);
	}

	Vector3 RandomSpawnLocation()
	{
		Vector3 loc = new Vector3();
		loc.x = Random.Range(minSpawn.x, maxSpawn.x);
		loc.y = Random.Range(minSpawn.y, maxSpawn.y);
		loc.z = 0f;

		return loc;
	}
}
