using UnityEngine;

public class Hitbox_script : MonoBehaviour
{
     private void OnMouseDown()
    {
        Destroy(transform.parent.gameObject);
    }
}
