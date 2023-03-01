using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class DayEntry : MonoBehaviour
{
    public bool hide;
    public GameObject holder;
    [Header("Label")]
    public int dayValue;
    public TextMeshProUGUI dayText;

    [Header("Move Ring")]
    public Image moveRing;
    public double moveRingValue;
    public double moveRingGoal = 200;

    [Header("Exercise Ring")]
    public Image exerciseRing;
    public double exerciseRingValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hide)
        {
            holder.SetActive(false);
        }
        else
        {
            holder.SetActive(true);
        }
        dayText.text = $"{dayValue}";
        moveRing.fillAmount = (float)(moveRingValue / moveRingGoal);
        exerciseRing.fillAmount = (float)(exerciseRingValue / 30);
    }
}
