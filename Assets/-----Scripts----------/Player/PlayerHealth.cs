using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class PlayerHealth : MonoBehaviour
{
    public ActionUIcontrol actionUIcontrol;
    public ImageFaderScript imageFader;
    public List<Image> healthBlocks;
    public Transform HealthBarParent;
    [Range(0, 10)]
    public int playerHealth = 0;
    public int playerHealthMax = 10;
    public Color warningHealthColor = Color.yellow;
    public Color lastHealthColor = Color.red;
    private bool wait;


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
           // Debug.Log("Increasing health");
            healthBlocks[playerHealth].gameObject.SetActive(true);
            playerHealth++;
        }
        else if( playerHealth >= 0 )
        {
            playerHealth--;
            healthBlocks[playerHealth].gameObject.SetActive(false);
            
            if (playerHealth > 3)
            {
                ChangeImageColor(lastHealthColor, Color.white);
            }
            else if (playerHealth <= 3 && playerHealth >= 2)
            {
                foreach (Image image in healthBlocks)
                { image.color = Color.yellow; }
                InvokeRepeating("BlinkImageColor", 0f, 1f);
            }
            else if(playerHealth == 1)
            {
                foreach (Image image in healthBlocks)
                { image.color = Color.red; }
                InvokeRepeating("BlinkImageColor", 0f, 1f);
            }
            else if(playerHealth <= 0)
            {
                //gameOver
                Debug.Log("you dead");
                imageFader.FadeImageMethod(2, false);
                Invoke("HealthZero", 2);
            }
        }
    }

    private void HealthZero()
    {
        actionUIcontrol.PauseWithoutResumeButton();
        actionUIcontrol.ShowDeathText();
    }


    private void ChangeImageColor(Color color, Color resetColor)
    {
        foreach(Image image in healthBlocks)
        {
            image.DOColor(color, 0.01f).OnComplete(() => image.DOColor(resetColor, 1));
            
        }
    }

    private void BlinkImageColor()
    {
        foreach (Image image in healthBlocks)
        {
            image.DOFade(0f, .5f).SetEase(Ease.OutQuad).OnComplete(() => image.DOFade(1, .5f).SetEase(Ease.OutQuad));
        }
        
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
