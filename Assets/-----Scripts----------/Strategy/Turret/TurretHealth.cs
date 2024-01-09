using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class TurretHealth : MonoBehaviour
{
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

    public void DecreaseTurretHealth()
    {
        if(turretStateManager.currentState == turretStateManager.sleepingState)
        {
            ChangeSkinColors(Color.white);
            turretStateManager.dosomethig();
            Invoke("ResetSkinColors", 2);
        }
        else if(turretStateManager.currentState == turretStateManager.seekingAndShooting)
        {
            turretHealth--;
            ChangeSkinColors(Color.red);
            ImageModifier(turretHealth);
        }
        
        //Debug.Log(turretStateManager.currentState);
        
        
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
        else if( health < 50)
        {
            healthColor = Color.yellow;
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
    }
}
