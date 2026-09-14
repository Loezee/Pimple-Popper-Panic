using UnityEngine;

public class Hitbox_script : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Find the Pimple script on the parent
        Pimple_script pimple =
            GetComponentInParent<Pimple_script>();

        // Tell the pimple it was clicked
        pimple.HitPimple();
    }
}