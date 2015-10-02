using UnityEngine;
using System.Collections;

public class GunMan : MonoBehaviour
{
    private float shootTime;

    public void Die()
    {
        Destroy(gameObject);
    }
}
