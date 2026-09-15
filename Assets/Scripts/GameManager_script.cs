using UnityEngine;
using TMPro;

public class GameManager_script : MonoBehaviour
{
    public GameObject pimplePrefab;

    //for when each pimple appears
    public float[] spawnTimes;

    //pimple growing before hitbox appears
    public float[] hitboxDelays;

    //grace period before pimple is gone
    public float gracePeriod = 0.4f;

    //pimple spawn points are empty game objects
    public Transform[] spawnPoints;

    //no same spawn point twice in a row
    private int lastSpawnIndex = -1;

    public int score = 0;

    //text display for score
    public TMP_Text scoreText;

    private int nextSpawn = 0;

    private AudioSource audioSource;

    void Start()
    {
        //get audio source attached to game manager
        audioSource = GetComponent<AudioSource>();

        //display score
        UpdateScoreUI();

        //start the music
        audioSource.Play();
    }

    void Update()
    {
        //is it time to spawn next pimple?
        if (nextSpawn < spawnTimes.Length &&
            audioSource.time >= spawnTimes[nextSpawn])
        {
            SpawnPimple();

            nextSpawn++;
        }
    }

    void SpawnPimple()
    {
        //choose one random spawn point
        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, spawnPoints.Length);

        } while (
            randomIndex == lastSpawnIndex &&
            spawnPoints.Length > 1
        );

        //remember the spawn point
        lastSpawnIndex = randomIndex;

        Transform spawnPoint = spawnPoints[randomIndex];


        //clone the pimple prefab at that spawn point
        GameObject newPimple = Instantiate(
            pimplePrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );


        //refer to pimple script
        Pimple_script pimpleScript =
            newPimple.GetComponent<Pimple_script>();


        //give the pimple its timing
        pimpleScript.Initialize(
            hitboxDelays[nextSpawn],
            gracePeriod,
            this
        );
    }

    public void AddPoints(int amount)
    {
        score += amount;

        //update the score
        UpdateScoreUI();

        Debug.Log("Score: " + score);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}