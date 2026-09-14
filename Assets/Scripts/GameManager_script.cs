using UnityEngine;

public class GameManager_script : MonoBehaviour
{
    public GameObject pimplePrefab;

    // When each pimple appears
    public float[] spawnTimes;

    // How long the pimple takes to grow
    // and how long before the hitbox appears
    public float[] hitboxDelays;

    // How long the player has to click
    // after the hitbox appears
    public float gracePeriod = 0.4f;

    // Pimple spawning center
    public Transform spawnArea;

    // Pimple spawning radius
    public float spawnRadius = 5f;

    // Player's score
    public int score = 0;

    private int nextSpawn = 0;

    private AudioSource audioSource;

    void Start()
    {
        // Get the music
        audioSource = GetComponent<AudioSource>();

        // Start the music
        audioSource.Play();
    }

    void Update()
    {
        // Check if it's time to spawn the next pimple
        if (nextSpawn < spawnTimes.Length &&
            audioSource.time >= spawnTimes[nextSpawn])
        {
            SpawnPimple();
            nextSpawn++;
        }
    }

    void SpawnPimple()
    {
        // Pick a random position inside the circular spawn area
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;

        Vector3 spawnPosition = new Vector3(
            spawnArea.position.x + randomCircle.x,
            spawnArea.position.y + randomCircle.y,
            spawnArea.position.z
        );

        // Create the pimple
        GameObject newPimple = Instantiate(
            pimplePrefab,
            spawnPosition,
            Quaternion.identity
        );

        // Get the pimple script
        Pimple_script pimpleScript =
            newPimple.GetComponent<Pimple_script>();

        // Give the pimple its individual timing information
        pimpleScript.Initialize(
            hitboxDelays[nextSpawn],
            gracePeriod,
            this
        );
    }

    public void AddPoints(int amount)
    {
        score += amount;

        Debug.Log("Score: " + score);
    }
}