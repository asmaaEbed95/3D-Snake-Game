using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public Transform[] SpawnPoints;
	public float spawnTime = 0.5f;
	public GameObject[] Coins;

	void Start()
	{
		InvokeRepeating ("SpawnCoins", spawnTime, spawnTime);
	}
	void Update ()
	{
	}

	void SpawnCoins()
	{
		Transform currentSpawnPoint = SpawnPoints [Random.Range (0, SpawnPoints.Length)].transform;
		Instantiate (Coins [Random.Range (0, Coins.Length)], currentSpawnPoint.position, currentSpawnPoint.rotation);
	}
}
