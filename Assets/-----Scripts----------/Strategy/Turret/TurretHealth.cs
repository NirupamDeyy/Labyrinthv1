using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class TurretHealth : MonoBehaviour
{
    public Sprite sleepingImage, shootingImage, deadImage;
    public Image stateImage;
    public Material materialLabel;
    public Material matLabel;
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
    public Material materialUIImage;
    public Material matImage;
    public List<Material> matTurretparts = new();
    public List<Color> matTurretPartscolors = new();
    public TurretStateManager turretStateManager;
  
    void Start()
    {
        Material matLabelInstance = new Material(materialLabel);
        matLabel = matLabelInstance;

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
        //matImage = healthBar.material;
        Material matInstance = new Material(materialUIImage);
        healthBar.material = matInstance;

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
                        
                    }
                        
                }
            }
        }
        if(turretHealth <= 1)
        {
            ChangeSkinColors(Color.black);
            ImageModifier(0);
           
        }
    }
    public void DidSomething()
    {
        if (turretStateManager.currentState == turretStateManager.sleepingState)
        {
            SwitchImage(State.sleeping);
        }
        else if (turretStateManager.currentState == turretStateManager.seekingAndShooting)
        {
            SwitchImage(State.shooting);
        }
    }

            void SwitchImage(State state)
    {
        switch(state)
        {
            case State.sleeping:
                matLabel.SetTexture("_Image", sleepingImage.texture);
                break;
            case State.shooting:
                matLabel.SetTexture("_Image", shootingImage.texture);
                break;
            case State.dead:
                matLabel.SetTexture("_Image", deadImage.texture);
                break;

            default:
                matLabel.SetTexture("_Image", sleepingImage.texture);
                break;
        }

        stateImage.material = matLabel;
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
            turretStateManager.enabled = false;
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
        if(turretHealth > 1)
        {
            for (int i = 0; i < matTurretparts.Count; i++)
            {
                matTurretparts[i].SetColor("_BaseColor", matTurretPartscolors[i]);
            }
            canDecreaseHealth = true;
        }
        
    }
}
