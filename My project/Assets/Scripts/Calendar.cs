using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class Calendar : MonoBehaviour
{
    public enum Days {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
    }
    public Days startingDay;
    public int currentMonth = 1;
    public int currentYear = 2020;
    public TextMeshProUGUI monthText;
    public TextMeshProUGUI yearText;
    public GameObject entries;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int days = DateTime.DaysInMonth(currentYear, currentMonth);
        DateTime referenceDay = new DateTime(currentYear, currentMonth, 1);
        // Debug.Log(referenceDay.DayOfWeek);


    }

    public void NextMonth()
    {
        if (currentMonth == 12) 
        {
            currentMonth = 1;
            currentYear++;
        }
        else
        {
            currentMonth++;
        }
        monthText.text = MonthToText(currentMonth);
        yearText.text = $"{currentYear}";
    }

    public void LastMonth()
    {
        if (currentMonth == 1)
        {
            currentMonth = 12;
            currentYear--;
        }
        else
        {
            currentMonth--;
        }
        monthText.text = MonthToText(currentMonth);
        yearText.text = $"{currentYear}";
    }

    string MonthToText(int month)
    {
        switch(month)
        {
            case 1:
                return "January";
            case 2:
                return "February";
            case 3:
                return "March";
            case 4:
                return "April";
            case 5:
                return "May";
            case 6:
                return "June";
            case 7:
                return "July";
            case 8:
                return "August";
            case 9:
                return "September";
            case 10:
                return "October";
            case 11:
                return "November";
            case 12:
                return "December";
            
        }
        return "Month";
    }

    public void Reset()
    {

    }
}
