using UnityEngine;

public class Hitbox_script : MonoBehaviour
{
    public GameObject sphere;

    private void OnMouseDown()
    {
        Destroy(sphere);
        Destroy(gameObject);
    }
}
