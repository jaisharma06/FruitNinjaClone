using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour {

	public List<GameObject> fruitPrefabs;
	public Transform[] spawnPoints;

	public float minDelay = .1f;
	public float maxDelay = 1f;

    private Coroutine SpawnFruitsCoroutine;

    private ApplicationManager applicationManager;

	// Use this for initialization
	void Start () {
        //StartSpawning();
        applicationManager = ApplicationManager.instance;
	}

    public void StopSpawning()
    {
        if (SpawnFruitsCoroutine != null)
        {
            StopCoroutine(SpawnFruitsCoroutine);
        }
    }

    public void StartSpawning()
    {
        SpawnFruitsCoroutine = StartCoroutine(SpawnFruits());
    }

	public IEnumerator SpawnFruits ()
	{
		while (true)
		{
            var maxTime = applicationManager.applicationData.timer;
            var timeSinceStart = maxTime - applicationManager.timeLeft;

			float delay = Random.Range(minDelay, maxDelay);

            if (timeSinceStart < maxTime / 2 && applicationManager.playerScore < 45)
            {
                delay = Random.Range(maxDelay / 2, maxDelay);
            }

            yield return new WaitForSeconds(delay);

			int spawnIndex = Random.Range(0, spawnPoints.Length);
			Transform spawnPoint = spawnPoints[spawnIndex];
            var randomFruit = fruitPrefabs[Random.Range(0, fruitPrefabs.Count)];
			GameObject spawnedFruit = Instantiate(randomFruit, spawnPoint.position, spawnPoint.rotation);
			Destroy(spawnedFruit, 5f);
		}
	}
	
}
