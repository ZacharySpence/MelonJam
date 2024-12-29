using UnityEngine;
using UnityEngine.UI;

public class EmotionValueManager : MonoBehaviour
{
    public static EmotionValueManager Instance;

    [SerializeField] public int happinessEV;
    [SerializeField] public int hotEV;
    [SerializeField] Image emotionSprite;
    [SerializeField] Sprite happySprite, neutralSprite, angrySprite, hotSprite,coldSprite;
    public AHEmotion currentAHEmotion;
    public HCEmotion currentHCEmotion;
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
        emotionSprite.sprite = happinessEV > 0 ? happySprite : angrySprite;
    }
}

public enum AHEmotion
{
    Neutral,
    Angry,
    Happy,
    Any

}
public enum HCEmotion
{
    Neutral,
    Hot,
    Cold
}
