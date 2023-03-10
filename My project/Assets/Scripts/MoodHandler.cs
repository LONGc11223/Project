using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodHandler : MonoBehaviour
{
    public Sprite happy;
    public Sprite sad;
    public Sprite neutral;
    public Sprite hungry;

    public Image image;

    public enum Mood { happy, sad, neutral, hungry }
    public Mood petMood;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (petMood)
        {
            case Mood.happy:
                // do code here
                image.sprite = happy;
                break;
            case Mood.sad:
                image.sprite = sad;
                break;
            case Mood.neutral:
                image.sprite = neutral;
                break;
            case Mood.hungry:
                image.sprite = hungry;
                break;
        }
    }
}
