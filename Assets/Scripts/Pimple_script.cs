using System.Collections;
using UnityEngine;

public class Pimple_script : MonoBehaviour
{
    public GameObject hitbox;

    // Starting and ending sizes
    public Vector3 startingScale = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 finalScale = new Vector3(1f, 1f, 1f);

    // Pimple colors
    public Color growingColor = new Color(1f, 0.4f, 0.6f); // Pink
    public Color clickableColor = Color.white;

    private GameManager_script gameManager;

    private float hitboxDelay;
    private float gracePeriod;

    private bool clicked = false;

    // Renderer for the pimple
    private Renderer pimpleRenderer;

    public void Initialize(
        float delay,
        float grace,
        GameManager_script manager
    )
    {
        hitboxDelay = delay;
        gracePeriod = grace;
        gameManager = manager;

        // Get the renderer from the pimple
        pimpleRenderer = GetComponentInChildren<Renderer>();

        // Start small
        transform.localScale = startingScale;

        // Start pink
        pimpleRenderer.material.color = growingColor;

        // Hide hitbox
        hitbox.SetActive(false);

        // Start growth/timing
        StartCoroutine(PimpleRoutine());
    }

    IEnumerator PimpleRoutine()
    {
        float timer = 0f;

        // Gradually grow the pimple
        while (timer < hitboxDelay)
        {
            timer += Time.deltaTime;

            float progress = timer / hitboxDelay;

            transform.localScale = Vector3.Lerp(
                startingScale,
                finalScale,
                progress
            );

            yield return null;
        }

        // Make sure it reaches full size
        transform.localScale = finalScale;

        // Change from pink to white
        pimpleRenderer.material.color = clickableColor;

        // Turn on the hitbox
        hitbox.SetActive(true);

        // Wait for the grace period
        yield return new WaitForSeconds(gracePeriod);

        // If the player didn't click it, destroy it
        if (!clicked)
        {
            Destroy(gameObject);
        }
    }

    public void HitPimple()
    {
        // Prevent multiple clicks
        if (clicked)
            return;

        clicked = true;

        // Give 5 points
        gameManager.AddPoints(5);

        // Destroy pimple and hitbox
        Destroy(gameObject);
    }
}