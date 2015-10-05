using UnityEngine;
using System.Collections;

public class GunMan : MonoBehaviour
{
    private float shootTime;

    void Start()
    {
        transform.position = Vector3.left * 2;
    }

    void Update()
    {
        if (transform.position != Vector3.zero)
        {
            transform.position = Vector3.Lerp(transform.position, Vector3.zero, Time.deltaTime * 5);
        }
    }

    public void Die()
    {
        CancelInvoke();
        Destroy(gameObject);
    }

    public void AboutFire()
    {        
        Invoke("Fire", shootTime);
    }

    public float ShootTime
    {
        get
        {
            return shootTime;
        }

        set
        {
            shootTime = value;
        }
    }

    private void Fire()
    {
        GameController.Instrance.GunManFire();
    }
}
