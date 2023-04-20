using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using TMPro;

// [ExecuteInEditMode]
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
    public int currentMonth = DateTime.Now.Month;
    public int currentYear = DateTime.Now.Year;
    public TextMeshProUGUI monthText;
    public TextMeshProUGUI yearText;
    public Button nextButton;
    // public Button prevButton;
    public GameObject entries;
    DateTime now;

    bool shouldUpdateRings = true;

    async void GetRings(int day, int month, int year, DayEntry entry)
    {
        HealthManager health = MainManager.Instance.healthManager;
        double moveRing = await health.GetMoveRingValueAsync(day, month, year);
        double exerciseRing = await health.GetExerciseRingValueAsync(day, month, year);
        double moveGoal = await health.GetMoveRingGoalAsync(day, month, year);
        Debug.Log($"On {month}/{day}/{year}, moveRing = {moveRing}, exerciseRing = {exerciseRing}, moveGoal = {moveGoal}");

        entry.moveRingValue = moveRing;
        entry.moveRingGoal = moveGoal;
        entry.exerciseRingValue = exerciseRing;
    }

    IEnumerator UpdateRingDataCoroutine(int day, int month, int year, DayEntry entry)
    {
        GetRings(day, month, year, entry);
        // isUpdateRingRunning--;
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentMonth = DateTime.Now.Month;
        currentYear = DateTime.Now.Year;
        monthText.text = MonthToText(currentMonth);
        yearText.text = $"{currentYear}";
        now = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        int days = DateTime.DaysInMonth(currentYear, currentMonth);
        DateTime referenceDay = new DateTime(currentYear, currentMonth, 1);
        int dayOfWeek = (int)referenceDay.DayOfWeek;
        // Debug.Log($"On {currentMonth}/{currentYear}, the first day starts at {referenceDay.DayOfWeek}");
        // Debug.Log($"This month starts on the day {dayOfWeek} and has {days} days");

        if (shouldUpdateRings)
        {
            for (int i = 0; i < 35; i++)
            {
                if (i < dayOfWeek || i >= dayOfWeek+days)
                {
                    entries.transform.GetChild(i).gameObject.GetComponent<DayEntry>().hide = true;
                }
                else // valid day
                {
                    DayEntry day = entries.transform.GetChild(i).gameObject.GetComponent<DayEntry>();
                    day.hide = false;
                    int dayNum = i - dayOfWeek + 1;
                    day.dayValue = dayNum;

                    if (!Application.isEditor)
                    {
                        // HealthManager health = MainManager.Instance.healthManager;

                        // day.moveRingGoal = health.GetMoveGoalDate(dayNum, currentMonth, currentYear);
                        // day.moveRingValue = health.GetMoveRingDate(dayNum, currentMonth, currentYear);
                        // day.exerciseRingValue = health.GetExerciseRingDate(dayNum, currentMonth, currentYear);
                        
                        // isUpdateRingRunning++;
                        if (currentMonth == DateTime.Now.Month && currentYear == DateTime.Now.Year && dayNum > DateTime.Now.Day)
                        {
                            // if (currentMonth == DateTime.Now.Month && currentYear == DateTime.Now.Year && dayNum == DateTime.Now.Day)
                            // {
                            //     day.moveRingValue = MainManager.Instance.healthManager.moveRing;
                            //     day.moveRingGoal = MainManager.Instance.healthManager.moveGoal;
                            //     day.exerciseRingValue = MainManager.Instance.healthManager.exerciseRing;
                            // }
                            // else
                            // {
                            //     day.moveRingValue = 0;
                            //     day.exerciseRingValue = 0;
                            // }
                            day.moveRingValue = 0;
                            day.exerciseRingValue = 0;
                        }
                        else
                        {
                            if (currentMonth == DateTime.Now.Month && currentYear == DateTime.Now.Year && dayNum == DateTime.Now.Day)
                            {
                                day.moveRingValue = MainManager.Instance.healthManager.moveRing;
                                day.moveRingGoal = MainManager.Instance.healthManager.moveGoal;
                                day.exerciseRingValue = MainManager.Instance.healthManager.exerciseRing;
                            }
                            else
                            {
                                StartCoroutine(UpdateRingDataCoroutine(dayNum, currentMonth, currentYear, day));
                            }
                            
                        }
                    }
                }
            }
            shouldUpdateRings = false;
            // Debug.Log($"Updating {isUpdateRingRunning} days' rings");
        }
        



        if (now.Year == currentYear && now.Month == currentMonth)
        {
            nextButton.interactable = false;
        }
        else
        {
            nextButton.interactable = true;
        }

    }

    public void NextMonth()
    {
        if (now.Year == currentYear && now.Month == currentMonth)
        {
            Debug.Log("You should not be able to go to the next month!");
            return;
        }

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
        shouldUpdateRings = true;
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
        shouldUpdateRings = true;
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
