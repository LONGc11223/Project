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
    public RectTransform dayPage;
    public RectTransform calendarPage;
    // public GameObject dayPanel;
    // public GameObject calendarPanel;
    int currentPage = 0;

    public bool debug;
    public Image moveRing;
    public Image exerciseRing;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI debugText;

    public GameObject redConfetti;
    public GameObject greenConfetti;

    [SerializeField] double moveRingValue;
    [SerializeField] double moveRingGoal;
    [SerializeField] double exerciseRingValue;
    bool moveRewardToday;
    bool exerciseRewardToday;
    HealthManager.RingValues rings;
    bool canSwipe = true;

    Vector2 dailyOrigin;
    Vector2 calendarOrigin;
    Vector2 otherOrigin;
    DatabaseManager databaseManager;
    
    // Start is called before the first frame update
    void Start()
    {
        MainManager.Instance.notificationManager.PrepareNotification();
        moveRingValue = 450;
        moveRingGoal = 500;
        exerciseRingValue = 15;
        Debug.Log($"{Convert.ToInt32(moveRingValue / (double)moveRingGoal * 100)}%");
        databaseManager = MainManager.Instance.databaseManager;
        if (!Application.isEditor)
        {
            moveRingValue = MainManager.Instance.healthManager.GetMoveRing();
            moveRingGoal = MainManager.Instance.healthManager.GetMoveGoal();
            exerciseRingValue = MainManager.Instance.healthManager.GetExerciseRing();
            
            // StartCoroutine(MainManager.Instance.healthManager.GetRingData(11, 3, 2023, OnMoveRingReceived, OnExerciseRingReceived));
        }
        // Debug.Log(Screen.width);
        calendarPage.anchoredPosition = new Vector2(Screen.width,0);

        dailyOrigin = new Vector2(dayPage.anchoredPosition.x, dayPage.anchoredPosition.y);
        calendarOrigin = new Vector2(calendarPage.anchoredPosition.x, calendarPage.anchoredPosition.y);
        otherOrigin = new Vector2(-calendarPage.anchoredPosition.x, calendarPage.anchoredPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Screen.width);
        if (!Application.isEditor)
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
        progressText.text = $"{Convert.ToInt32(moveRingValue / (double)moveRingGoal * 100)}%";
        moveRing.fillAmount = (float)(moveRingValue / moveRingGoal);
        exerciseRing.fillAmount = (float)(exerciseRingValue / 30);

        debugText.text = $"Move - {moveRingValue}\nExercise - {exerciseRingValue}\nGoal - {moveRingGoal}";


        int lastMoveRewardDay = MainManager.Instance.databaseManager.lastMoveReward.Day;
        int lastExerciseRewardDay = MainManager.Instance.databaseManager.lastExerciseReward.Day;

        if (!exerciseRewardToday && databaseManager.lastExerciseReward.Date != DateTime.Today && exerciseRingValue >= 30.0)
        {
            exerciseRewardToday = true;
            greenConfetti.SetActive(false);
            greenConfetti.SetActive(true);
            MainManager.Instance.databaseManager.AddFunds(100, 1);
        }

        if (!moveRewardToday && databaseManager.lastMoveReward.Date != DateTime.Today && (moveRingValue / moveRingGoal) >= 1.0)
        {
            moveRewardToday = true;
            redConfetti.SetActive(false);
            redConfetti.SetActive(true);
            MainManager.Instance.databaseManager.AddFunds(100 + (Convert.ToInt32(moveRingValue / 100) * 50), 0);
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

    public void SendTestNotification()
    {
        MainManager.Instance.notificationManager.TestNotification();
    }

    public void NextPage()
    {
        if (currentPage == 0 && canSwipe)
        {
            // dayPanel.SetActive(false);
            // calendarPanel.SetActive(true);

            StartCoroutine(NextPageAnim(calendarPage, dailyOrigin, 0.1f));
            currentPage++;
        }
    }

    IEnumerator NextPageAnim(RectTransform page, Vector2 newPosition, float duration)
    {
        canSwipe = false;
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            Vector2 currentPos = page.anchoredPosition;
            Vector2 oldPos = dayPage.anchoredPosition;

            float time = Vector2.Distance(currentPos, newPosition) / (duration - counter) * Time.deltaTime;
            float oldTime = Vector2.Distance(oldPos, otherOrigin) / (duration - counter) * Time.deltaTime;
            
            page.anchoredPosition = Vector2.MoveTowards(currentPos, newPosition, time);
            dayPage.anchoredPosition = Vector2.MoveTowards(oldPos, otherOrigin, oldTime);
            yield return null;
        }
        canSwipe = true;

    }

    public void PrevPage()
    {
        if (currentPage == 1 && canSwipe)
        {
            // dayPanel.SetActive(true);
            // calendarPanel.SetActive(false);

            StartCoroutine(PrevPageAnim(dayPage, dailyOrigin, 0.1f));            
            currentPage--;
        }
    }

    IEnumerator PrevPageAnim(RectTransform page, Vector2 newPosition, float duration)
    {
        canSwipe = false;
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            Vector2 currentPos = page.anchoredPosition;
            Vector2 oldPos = calendarPage.anchoredPosition;

            float time = Vector2.Distance(currentPos, newPosition) / (duration - counter) * Time.deltaTime;
            float oldTime = Vector2.Distance(oldPos, calendarOrigin) / (duration - counter) * Time.deltaTime;
            
            page.anchoredPosition = Vector2.MoveTowards(currentPos, newPosition, time);
            calendarPage.anchoredPosition = Vector2.MoveTowards(oldPos, calendarOrigin, oldTime);
            yield return null;
        }
        canSwipe = true;
    }

}
