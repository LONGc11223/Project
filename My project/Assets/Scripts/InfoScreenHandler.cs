using System.Collections.Generic;
using InputSamples.Gestures;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using Managers;

public class InfoScreenHandler : MonoBehaviour
{
    [Header("Swipe Variables")]
    bool canSwipe = true;
    public float transitionDuration = 0.1f;
    [SerializeField]
    private GestureController gestureController;

    [Header("Page Variables")]
    public int offset = 0;
    public RectTransform pageContainer;
    public List<RectTransform> pages = new List<RectTransform>();
    List<Vector2> originalPagePositions = new List<Vector2>();

    public int currentPage;
    // public TextMeshProUGUI textDisplay;
    // public Image smallImage;
    // public Image largeImage;
    public GameObject pageMarker;
    
    // public List<Sprite> imageContents = new List<Sprite>();
    // [TextArea(5, 7)]
    // public List<string> textContents = new List<string>();

    void Awake() 
    {
        StartCoroutine(NotificationManager.RequestAuthorization());
    }

    void Start()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].anchoredPosition = new Vector2((Screen.width * i) + ((i != 0) ? offset * i : 0), 0);
            originalPagePositions.Add(pages[i].anchoredPosition);
        }
        UpdatePage();
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
            Debug.Log("Swiped left");
            NextPage();
        } 
        else if (input.SwipeDirection.x > 0.75f)
        {
            Debug.Log("Swiped right");
            PrevPage();
        }
    }

    void UpdatePage()
    {
        switch(currentPage)
        {
            case 0:
                pageMarker.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                pageMarker.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                pageMarker.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                break;
            case 1:
                pageMarker.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                pageMarker.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                pageMarker.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                break;
            case 2:
                pageMarker.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                pageMarker.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                pageMarker.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                break;
        }
    }

    public void NextPage()
    {
        if (currentPage < pages.Count - 1 && canSwipe) 
        {
            currentPage++;
            UpdatePage();
            StartCoroutine(ChangePageAnim(currentPage, 1, transitionDuration));
        }
        
    }

    IEnumerator ChangePageAnim(int currentPage, int direction,  float duration)
    {
        canSwipe = false;

        float counter = 0;

        while (counter < transitionDuration)
        {
            counter += Time.deltaTime;

            Debug.Log("Current Page: " + currentPage);
            Debug.Log((Screen.width + offset) * currentPage);

            if (direction == 0)
            {
                Vector2 newPosition = new Vector2(-(Screen.width + offset) * currentPage, 50f);
                pageContainer.anchoredPosition = Vector2.Lerp(pageContainer.anchoredPosition, newPosition, counter / transitionDuration);
            }
            else
            {
                Vector2 newPosition = new Vector2(-(Screen.width + offset) * currentPage, 50f);
                pageContainer.anchoredPosition = Vector2.Lerp(pageContainer.anchoredPosition, newPosition, counter / transitionDuration);
            }

            yield return null;
        }

        canSwipe = true;
    }

    public void PrevPage()
    {
        if (currentPage > 0 && canSwipe)
        {
            currentPage--;
            UpdatePage();
            StartCoroutine(ChangePageAnim(currentPage, 0, transitionDuration));
        }
        
    }

    public void PrevScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
    }
}
