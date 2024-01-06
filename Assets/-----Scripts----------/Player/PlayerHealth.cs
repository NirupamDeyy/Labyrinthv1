using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class PlayerHealth : MonoBehaviour
{
    public List<Image> healthBlocks;
    public Transform HealthBarParent;
    [Range(0, 10)]
    public int playerHealth = 0;
    public int playerHealthMax = 10;
    public Color warningHealthColor = Color.yellow;
    public Color lastHealthColor = Color.red;
    void Start()
    {
        foreach(Transform t in HealthBarParent)
        { healthBlocks.Add(t.GetComponent<Image>()); }
        //AddDeleteBlock(true);
        ResetHealth();
    }
    void ResetHealth()
    {
        AddDeleteBlock(true);
        if(playerHealth <= 9)
        {
            Invoke("ResetHealth", .5f);
        }
    }
    
    public void AddDeleteBlock(bool add)
    {
       

        if (add == true)
        {
            Debug.Log("Increasing health");
            healthBlocks[playerHealth].gameObject.SetActive(true);
            playerHealth++;
        }
        else
        {
            Debug.Log("Got damage");
            playerHealth--;
            healthBlocks[playerHealth].gameObject.SetActive(false);
            /*if (playerHealth == 2)
            {
                healthBlocks[playerHealth].color = warningHealthColor;
                healthBlocks[playerHealth - 1].color = warningHealthColor;
            }
            else if (playerHealth == 1)
            {
                healthBlocks[playerHealth].color = lastHealthColor;
            }*/
        }
        

        //Debug.Log(playerHealth);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Danger"))
        {
            Debug.Log(collision.gameObject.name);
            AddDeleteBlock(false);
        }
        else
        {
          //  Debug.Log("baal");
        }
    }
}
