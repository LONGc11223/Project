using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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

    public void Reset()
    {

    }
}
