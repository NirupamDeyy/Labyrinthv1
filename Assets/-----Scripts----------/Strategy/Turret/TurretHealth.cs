using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class TurretHealth : MonoBehaviour
{
    public Sprite sleepingImage, shootingImage, deadImage;
    public Image stateImage;
    private enum State
    {
        sleeping,
        shooting,
        dead
    }

    public List<Transform> turretParts;
    public Image healthBar;
    public float turretHealth = 100;

    private float maxTurretHealth = 100;
    public Material matImage;
    public List<Material> matTurretparts = new();
    public List<Color> matTurretPartscolors = new();
    public TurretStateManager turretStateManager;
  
    void Start()
    {
        Material mat;
        Renderer rend;
        foreach (Transform t in turretParts)
        {
            rend = t.GetComponent<Renderer>();
            mat = rend.material;
            if (mat != null)
            {
                matTurretparts.Add(mat);
            }
            else
            {
                Debug.Log("material not found");
            }
        }
        Color color;
        foreach (Material material in matTurretparts)
        {
            if (material != null)
            {
                color = material.color;
                matTurretPartscolors.Add(color);
            }
        }
        turretHealth = 100;
        maxTurretHealth = 100;
        matImage = healthBar.material;
    }
    bool canDecreaseHealth;
    public void DecreaseTurretHealth()
    {
        if(turretHealth > 0)
        {
            if (turretStateManager.currentState == turretStateManager.sleepingState)
            {
                canDecreaseHealth = false;
                
                if(turretHealth > 1)
                {
                    SwitchImage(State.sleeping);
                    ChangeSkinColors(Color.white);
                    turretStateManager.dosomethig();
                    Invoke("ResetSkinColors", 2);
                }
                    
            }
            else if (turretStateManager.currentState == turretStateManager.seekingAndShooting)
            {
                if (canDecreaseHealth)
                {
                    turretHealth--;
                    if (turretHealth > 1)
                    {
                        ChangeSkinColors(Color.red);
                        ImageModifier(turretHealth);
                        SwitchImage(State.shooting);
                    }
                        
                }
            }
        }
        if(turretHealth < 1)
        {
            ChangeSkinColors(Color.black);
            ImageModifier(0);
        }
    }

    void SwitchImage(State state)
    {
        switch(state)
        {
            case State.sleeping:
                stateImage.sprite = sleepingImage;
                break;
            case State.shooting:
                stateImage.sprite = shootingImage;
                break;
            case State.dead:
                stateImage.sprite = deadImage;
                break;

            default:
                stateImage.sprite = sleepingImage;
                break;
        }
    }
    private bool canTween =  true;
    Color healthColor;
    private void ImageModifier(float health)
    {
        healthBar.fillAmount = health / maxTurretHealth;
        matImage.SetColor("_EmissionColor", Color.red * Mathf.LinearToGammaSpace(10f));
        
        if (health > 50)
        {
            healthColor = Color.white;
        }
        else if(health > 1 && health < 50)
        {
            healthColor = Color.yellow;
        }
        else if(health <=1)
        {
            SwitchImage(State.dead);
            ChangeSkinColors(Color.black);
        }
        if (canTween)
        {
           
            canTween = false;
            healthBar.transform.DOPunchScale(new Vector3(0, 1, 0), 0.1f, 1, 1).OnComplete(() =>
            ChangeImageColor(healthColor));
        }
    }

    private void ChangeImageColor(Color color)
    {
        matImage.SetColor("_BaseColor", color);
        matImage.SetColor("_EmissionColor", color * Mathf.LinearToGammaSpace(10f));
        ResetSkinColors();
        canTween = true;
    }

    private void ChangeSkinColors(Color color)
    {
        foreach(Material mat in matTurretparts)
        {
            mat.SetColor("_BaseColor", color);
        }
    }

    private void ResetSkinColors()
    {
        for(int i = 0; i < matTurretparts.Count; i++)
        {
            matTurretparts[i].SetColor("_BaseColor", matTurretPartscolors[i]);  
        }
        canDecreaseHealth = true;
    }
}
