using UnityEngine;

public class Hitbox_script : MonoBehaviour
{
    private void OnMouseDown()
    {
        //find the parent pimple script
        Pimple_script pimple =
            GetComponentInParent<Pimple_script>();

        //pimple is clicked
        pimple.HitPimple();
    }
}