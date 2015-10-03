using UnityEngine;
using System.Collections;

public class GunManManager : MonoBehaviour
{
    public GunMan[] gunMan;

    private static GunManManager instance;

    private GunMan currentGunMan;

    public static GunManManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GunManManager>();
            }

            return instance;
        }
    }

    public void Init()
    {
        GameController.Instrance.BeginFire += OnBeginFire;
    }

    public void Dispose()
    {
        GameController.Instrance.BeginFire -= OnBeginFire;
    }

    void OnBeginFire (object sender, GameControllerEventArgs e)
    {
        if (currentGunMan != null)
        {
            currentGunMan.AboutFire();
        }
    }

    public void AddRandomGunMan()
    {
        int index = Random.Range(0, gunMan.Length);

        GunMan man = gunMan[index];

        currentGunMan = Instantiate(man.gameObject).GetComponent<GunMan>();
    }

    public void RemoveCurrentGunMan()
    {
        currentGunMan.Die();

        currentGunMan = null;  
    }
}
