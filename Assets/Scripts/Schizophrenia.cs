using UnityEngine;

public class Schizophrenia : MonoBehaviour
{
    public GameObject[] fakeEnemyPrefabs; 
    public float spawnInterval = 1f; 
    public float despawnTime = 4f; 

    private float nextSpawnTime; 
    private Transform playerPosition;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
        playerPosition = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnFakeEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnFakeEnemy()
    {
        Vector2 position = (Vector2)playerPosition.position + Random.insideUnitCircle * 10f;
        GameObject fakeEnemy = Instantiate(fakeEnemyPrefabs[Random.Range(0,fakeEnemyPrefabs.Length)], position, Quaternion.identity);
        if(fakeEnemy != null)
            Destroy(fakeEnemy, despawnTime);
    }
}
