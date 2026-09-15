using System.Collections;
using UnityEngine;

public class Pimple_script : MonoBehaviour
{
    public GameObject hitbox;

    //starting and ending pimple sizes
    public Vector3 startingScale = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 finalScale = new Vector3(1f, 1f, 1f);

    //pimple pink to white colors
    public Color growingColor = new Color(1f, 0.77f, 0.83f); //pink
    public Color clickableColor = Color.white;

    private GameManager_script gameManager;

    private float hitboxDelay;
    private float gracePeriod;

    private bool clicked = false;

    //renderer for the pimple
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

        //get the renderer from pimple
        pimpleRenderer = GetComponentInChildren<Renderer>();

        //small pimple
        transform.localScale = startingScale;

        //pink pimple
        pimpleRenderer.material.color = growingColor;

        //no hitbox yet in the beginning
        hitbox.SetActive(false);

        //pimple growing
        StartCoroutine(PimpleRoutine());
    }

    IEnumerator PimpleRoutine()
    {
        float timer = 0f;

        //pimple growing slowly
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

        //to make sure it grows to 1, 1, 1
        transform.localScale = finalScale;

        // pink to white
        pimpleRenderer.material.color = clickableColor;

        //hitbox on in the end
        hitbox.SetActive(true);

        //wait for grace period
        yield return new WaitForSeconds(gracePeriod);

        //no pop, then destroy the pimple
        if (!clicked)
        {
            Destroy(gameObject);
        }
    }

    public void HitPimple()
    {
        //prevents multiple clicks
        if (clicked)
            return;

        clicked = true;

        //5 points per pimple popped
        gameManager.AddPoints(5);

        //destroy the pimple and hitbox
        Destroy(gameObject);
    }
}