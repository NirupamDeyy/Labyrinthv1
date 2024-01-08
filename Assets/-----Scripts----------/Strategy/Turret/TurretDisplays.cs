using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class TurretDisplays : MonoBehaviour
{
    public List<Transform> turretParts;
    public Image healthBar;
    public float turretHealth = 100;

    private float maxTurretHealth = 100;
    private Material matImage;
    private List<Material> matTurretparts = new();
    private List<Color> matTurretPartscolors = new();
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
        turretHealth--;
        ChangeSkinColors(Color.red);
        ImageModifier(turretHealth);
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
            Debug.Log("tweening");
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
