using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class GunMan : MonoBehaviour
{
    private long shootTime; // millisecondes
    private Stopwatch stopwatch;
    private long aboutShootTime;

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

    public void AboutFire(Stopwatch stopwatch)
    {        
        this.stopwatch = stopwatch;
        aboutShootTime = stopwatch.ElapsedMilliseconds;

        StartCoroutine("Fire");
    }

    public long ShootTime
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

    IEnumerator Fire()
    {
        while (stopwatch.ElapsedMilliseconds - aboutShootTime < shootTime)
        {
            yield return 1;   
        }

        GameController.Instrance.GunManFire();
    }
}
