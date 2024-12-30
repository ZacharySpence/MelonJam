using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmotionValueManager : MonoBehaviour
{
    public static EmotionValueManager Instance;

    [SerializeField] public int happinessEV;
    [SerializeField] public int hotEV;
    [SerializeField] Image emotionSprite;
    [SerializeField] Sprite Sprite1, Sprite2, Sprite3, Sprite4, Sprite5;
    public AHEmotion currentAHEmotion;
    public HCEmotion currentHCEmotion;
    public TextMeshProUGUI txt;
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

    public void ChangeEmotionValues(int change, bool isHappinessEV)
    {
        if (isHappinessEV)
        {
            happinessEV += change;
        }
        else
        {
            hotEV += change;
        }
        ChangeEmotion();
        ChangeEmotionSprite();
        ChangeEmotionName();
        
    }

    void ChangeEmotion()
    {
        currentHCEmotion =  hotEV == 0? HCEmotion.Neutral : hotEV > 0 ? HCEmotion.Hot : HCEmotion.Cold;
        currentAHEmotion =  happinessEV > 0 ? AHEmotion.Happy : AHEmotion.Angry;

    }
    void ChangeEmotionName()
    {
        //Change the name on the phone based off hot/cold level
    }
    void ChangeEmotionSprite()
    {
        
        
        if (happinessEV > 2 & hotEV > 0)
        {
            emotionSprite.sprite = Sprite1;
            txt.text = "❤SWEETHEART❤";
        }
        else if (happinessEV >= 0 & hotEV >= 0)
        {
            emotionSprite.sprite = Sprite2;
            txt.text = "Cutie Pie";
        }
        else if (happinessEV >= 0 & hotEV < 0)
        {
            emotionSprite.sprite = Sprite3;
            txt.text = "Pookie";
        }
        else if (happinessEV < 0 & hotEV >= 0)
        {
            emotionSprite.sprite = Sprite4;
            txt.text = "Babe";
        }
        else if (happinessEV < 0 & hotEV < 0)
        {
            emotionSprite.sprite = Sprite5;
            txt.text = "Partner";
        }
    }
}

public enum AHEmotion
{
    Neutral,
    Angry,
    Happy,
    Any,
    Breakup

}
public enum HCEmotion
{
    Neutral,
    Hot,
    Cold
}
