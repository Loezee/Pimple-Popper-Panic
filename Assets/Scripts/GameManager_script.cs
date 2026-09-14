using UnityEngine;

public class GameManager_script : MonoBehaviour
{
   public GameObject pimplePrefab;

    //timing for pimple
    public float[] spawnTimes;

    //pimple spawner center
    public Transform spawnArea;
    //pimple spawner radius
    public float spawnRadius = 5f;

    private int nextSpawn = 0;

    private AudioSource audioSource;

    void Start()
    {
        //start with music at the same time
        //for pimple spawning timing
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        if (nextSpawn < spawnTimes.Length &&
            audioSource.time >= spawnTimes[nextSpawn])
        {
            SpawnSphere();
            nextSpawn++;
        }
    }

//spawn randomly inside of radius
    void SpawnSphere()
{
    Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;

    Vector3 spawnPosition = new Vector3(
        spawnArea.position.x + randomCircle.x,
        spawnArea.position.y + randomCircle.y,
        spawnArea.position.z
    );

    Instantiate(
        spherePrefab,
        spawnPosition,
        Quaternion.identity
    );
}
}