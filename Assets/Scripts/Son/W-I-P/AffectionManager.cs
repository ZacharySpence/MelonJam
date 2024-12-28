using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffectionManager : MonoBehaviour
{
    public static AffectionManager Instance;

    [SerializeField] public int affectionMeter;
    [SerializeField] Image affectionSprite;
    [SerializeField] Sprite happySprite, neutralSprite, angrySprite;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void ChangeAffection(int change)
    {
        affectionMeter += change;
        ChangeAffectionSprite();
        
    }

    void ChangeAffectionSprite()
    {
        affectionSprite.sprite = affectionMeter > 50 ? happySprite : 
            affectionMeter > 0 ? neutralSprite : angrySprite;
    }
}
