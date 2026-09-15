using UnityEngine;

public class GameManager_script : MonoBehaviour
{
    public GameObject pimplePrefab;

    // --------------------------------------------------
    // PIMPLE TIMING
    // --------------------------------------------------

    // When each pimple appears in the song
    public float[] spawnTimes;

    // How long each pimple grows before its hitbox appears
    public float[] hitboxDelays;

    // How long the player has to click after
    // the hitbox appears
    public float gracePeriod = 0.4f;


    // --------------------------------------------------
    // PIMPLE SPAWN POINTS
    // --------------------------------------------------

    // Custom points placed around the 3D model
    public Transform[] spawnPoints;

    // Keeps track of the last spawn point used
    // so the same point isn't chosen twice in a row
    private int lastSpawnIndex = -1;


    // --------------------------------------------------
    // SCORE
    // --------------------------------------------------

    public int score = 0;


    // --------------------------------------------------
    // OTHER VARIABLES
    // --------------------------------------------------

    private int nextSpawn = 0;

    private AudioSource audioSource;


    // --------------------------------------------------
    // START
    // --------------------------------------------------

    void Start()
    {
        // Get the Audio Source attached to the Game Manager
        audioSource = GetComponent<AudioSource>();

        // Start the music
        audioSource.Play();
    }


    // --------------------------------------------------
    // UPDATE
    // --------------------------------------------------

    void Update()
    {
        // Check if there is another pimple to spawn
        if (nextSpawn < spawnTimes.Length &&
            audioSource.time >= spawnTimes[nextSpawn])
        {
            SpawnPimple();

            nextSpawn++;
        }
    }


    // --------------------------------------------------
    // SPAWN PIMPLE
    // --------------------------------------------------

    void SpawnPimple()
    {
        // Choose a random spawn point
        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, spawnPoints.Length);

        } while (
            randomIndex == lastSpawnIndex &&
            spawnPoints.Length > 1
        );

        // Remember this spawn point
        lastSpawnIndex = randomIndex;

        // Get the chosen spawn point
        Transform spawnPoint = spawnPoints[randomIndex];


        // Create the pimple at the spawn point's
        // position and rotation
        GameObject newPimple = Instantiate(
            pimplePrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );


        // Get the Pimple_script attached to the new pimple
        Pimple_script pimpleScript =
            newPimple.GetComponent<Pimple_script>();


        // Give the pimple its timing information
        pimpleScript.Initialize(
            hitboxDelays[nextSpawn],
            gracePeriod,
            this
        );
    }


    // --------------------------------------------------
    // ADD POINTS
    // --------------------------------------------------

    public void AddPoints(int amount)
    {
        score += amount;

        Debug.Log("Score: " + score);
    }
}