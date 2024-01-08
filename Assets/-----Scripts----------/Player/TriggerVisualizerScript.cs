using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVisualizerScript : MonoBehaviour
{
    [SerializeField] private Transform triggerParent;
    public List<Transform> triggers = new();

    [SerializeField]
    private Color activatedColor, unActivatedColor;

    void Start()
    {
        foreach (Transform x in triggerParent)
        {
            triggers.Add(x);
        }
    }
    
    public void Triggered(bool isTriggered)
    {
        foreach (Transform x in triggers)
        {
            if (isTriggered)
            {
                Renderer rend = x.GetComponent<Renderer>();
                Material mat = rend.material;
                mat.SetColor("_BaseColor", activatedColor);
            }
            else
            {
                Renderer rend = x.GetComponent<Renderer>();
                Material mat = rend.material;
                mat.SetColor("_BaseColor", unActivatedColor);
            }
        }
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
