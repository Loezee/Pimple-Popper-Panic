using System.Collections;
using UnityEngine;

public class Pimple_script : MonoBehaviour
{
    public GameObject hitbox;

    // Starting and ending sizes
    public Vector3 startingScale = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 finalScale = new Vector3(1f, 1f, 1f);

    private GameManager_script gameManager;

    private float hitboxDelay;
    private float gracePeriod;

    private bool clicked = false;

    public void Initialize(
        float delay,
        float grace,
        GameManager_script manager
    )
    {
        hitboxDelay = delay;
        gracePeriod = grace;
        gameManager = manager;

        // Start the pimple very small
        transform.localScale = startingScale;

        // Make sure the hitbox is initially invisible/inactive
        hitbox.SetActive(false);

        // Start the growth and timing
        StartCoroutine(PimpleRoutine());
    }

    IEnumerator PimpleRoutine()
    {
        float timer = 0f;

        // Gradually grow the pimple
        while (timer < hitboxDelay)
        {
            timer += Time.deltaTime;

            // Calculate how far through the growth we are
            float progress = timer / hitboxDelay;

            // Gradually change the scale
            transform.localScale = Vector3.Lerp(
                startingScale,
                finalScale,
                progress
            );

            yield return null;
        }

        // Make absolutely sure the pimple reaches full size
        transform.localScale = finalScale;

        // Growth is finished, so activate the hitbox
        hitbox.SetActive(true);

        // Give the player their grace period
        yield return new WaitForSeconds(gracePeriod);

        // If they didn't click it...
        if (!clicked)
        {
            Destroy(gameObject);
        }
    }

    public void HitPimple()
    {
        // Prevent multiple clicks from giving multiple points
        if (clicked)
            return;

        clicked = true;

        // Give the player 5 points
        gameManager.AddPoints(5);

        // Destroy the pimple and hitbox
        Destroy(gameObject);
    }
}