using TMPro;
using DG;
using UnityEngine;

public class GunSwitchControl : MonoBehaviour
{
    public Transform[] crosshairs;
    public Transform[] weaponImage;
    public GameObject[] guns; // Array of gun GameObjects
    public int currentGunIndex = 0; // Index of the current gun
    public bool canShootGun;
    public CoinsTracker coinsTracker;

    public TMP_Text coinsText;
    public TMP_Text totalCoinsText;

    private void Start()
    {
        canShootGun = true;
        UpdateCoinText(0);
    }
    public void UpdateCoinText(int coin)
    {
        
        coinsText.text = coin.ToString();
    }
    void Update()
    {
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheelInput > 0f) // Scrolling up
        {
            
            SwitchGun(1); // Switch to the next gun
            
        }
        else if (scrollWheelInput < 0f) // Scrolling down
        {
           

            SwitchGun(-1); // Switch to the previous gun
            
        }
    }

    public int gunIndex = 0;
    void SwitchGun(int direction)
    {
        //guns[currentGunIndex].SetActive(false); // Disable current gun

        currentGunIndex += direction;

        if (currentGunIndex < gunIndex)
        {
            canShootGun = false;
        }
        else if (currentGunIndex >= gunIndex)
        {
            canShootGun = true; // Wrap to the first gun if at the end
        }
        SwitchImage();
        gunIndex = currentGunIndex;
        
        //guns[currentGunIndex].SetActive(true); // Enable new current gun
    }

    void SwitchImage()
    {
        for(int i = 0; i < crosshairs.Length; i++)
        {
            crosshairs[i].gameObject.SetActive(false);
            weaponImage[i].gameObject.SetActive(false);
        }
        if(canShootGun)
        {
            crosshairs[0].gameObject.SetActive(true);
            weaponImage[0].gameObject.SetActive(true);
        }
        else
        {
            crosshairs[1].gameObject.SetActive(true);
            weaponImage[1].gameObject.SetActive(true);
        }
        

        

 

       
    }
}

