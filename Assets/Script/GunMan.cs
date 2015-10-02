using UnityEngine;
using System.Collections;

public class GunMan : MonoBehaviour
{
    private float shootTime = 2f;

    public void Die()
    {
        Destroy(gameObject);
    }

    public void AboutFire()
    {
        Invoke("Fire", shootTime);
    }

    private void Fire()
    {
        GameController.Instrance.GunManFire();
    }
}
