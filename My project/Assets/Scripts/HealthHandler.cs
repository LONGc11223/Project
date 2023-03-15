using System;
using System.Collections;
using System.Collections.Generic;
using InputSamples.Gestures;
using UnityEngine.UI;
using UnityEngine;
using Managers;
using TMPro;

public class HealthHandler : MonoBehaviour
{
    [SerializeField]
    private GestureController gestureController;
    public GameObject dayPanel;
    public GameObject calendarPanel;
    int currentPage = 0;

    public bool debug;
    public Image moveRing;
    public Image exerciseRing;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI debugText;

    double moveRingValue;
    double moveRingGoal;
    double exerciseRingValue;
    bool moveRewardToday;
    bool exerciseRewardToday;
    HealthManager.RingValues rings;
    
    // Start is called before the first frame update
    void Start()
    {
        moveRingValue = 450;
        moveRingGoal = 500;
        exerciseRingValue = 15;
        if (!debug)
        {
            moveRingValue = MainManager.Instance.healthManager.GetMoveRing();
            moveRingGoal = MainManager.Instance.healthManager.GetMoveGoal();
            exerciseRingValue = MainManager.Instance.healthManager.GetExerciseRing();
            StartCoroutine(MainManager.Instance.healthManager.GetRingData(11, 3, 2023, OnMoveRingReceived, OnExerciseRingReceived));
        }
        // Debug.Log(Screen.width);
        // calendarPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width,0);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Screen.width);
        if (!debug)
        {
            moveRingValue = MainManager.Instance.healthManager.GetMoveRing();
            moveRingGoal = MainManager.Instance.healthManager.GetMoveGoal();
            exerciseRingValue = MainManager.Instance.healthManager.GetExerciseRing();
            // StartCoroutine(MainManager.Instance.healthManager.GetRingData(11, 3, 2023, OnMoveRingReceived, OnExerciseRingReceived));
        }
        if (moveRingGoal < 250)
        {
            moveRingGoal = 250;
        }
        progressText.text = $"{Convert.ToInt32(moveRingValue / (double)moveRingGoal)}%";
        moveRing.fillAmount = (float)(moveRingValue / moveRingGoal);
        exerciseRing.fillAmount = (float)(exerciseRingValue / 30);

        debugText.text = $"Move - {moveRingValue}\nExercise - {exerciseRingValue}\nGoal - {moveRingGoal}\n\nMarch 11, 2023:\n{rings.moveRingValue}\n{rings.exerciseRingValue}";

        if (!exerciseRewardToday && exerciseRingValue >= 30.0)
        {
            exerciseRewardToday = true;
            MainManager.Instance.databaseManager.AddFunds(100);
        }

        if (!moveRewardToday && (moveRingValue / moveRingGoal) >= 1.0)
        {
            moveRewardToday = true;
            MainManager.Instance.databaseManager.AddFunds(50);
        }
    }

    void OnMoveRingReceived(double moveRingValue)
    {
        Debug.Log("Move ring value: " + moveRingValue);
        rings.moveRingValue = moveRingValue;
    }

    void OnExerciseRingReceived(double exerciseRingValue)
    {
        Debug.Log("Move ring value: " + exerciseRingValue);
        rings.exerciseRingValue = exerciseRingValue;
    }

    private void OnEnable()
    {
        // gestureController.PotentiallySwiped += OnDragged;
        gestureController.Swiped += OnSwiped;
        // gestureController.Pressed += OnPressed;
    }

    protected virtual void OnDisable()
    {
        // gestureController.PotentiallySwiped -= OnDragged;
        gestureController.Swiped -= OnSwiped;
        // gestureController.Pressed -= OnPressed;
    }

    private void OnSwiped(SwipeInput input)
    {
        if (input.SwipeDirection.x < -0.75f)
        {
            NextPage();
        } 
        else if (input.SwipeDirection.x > 0.75f)
        {
            PrevPage();
        }
    }

    public void NextPage()
    {
        if (currentPage == 0)
        {
            dayPanel.SetActive(false);
            calendarPanel.SetActive(true);
            currentPage++;
        }
    }

    // IEnumerator NextPageAnim(RectTransform page, Vector2 newPosition, float duration)
    // {
    //     float counter = 0;
    //     while (counter < duration)
    //     {
    //         counter += Time.deltaTime;
    //         Vector2 currentPos = page.position;

    //         float time = Vector2.Distance(currentPos, newPosition) / (duration - counter) * Time.deltaTime;
    //         page.position = Vector2.MoveTowards(currentPos, newPosition, time);
    //         yield return null;
    //     }
    // }

    public void PrevPage()
    {
        if (currentPage == 1)
        {
            dayPanel.SetActive(true);
            calendarPanel.SetActive(false);
            currentPage--;
        }
    }

}
