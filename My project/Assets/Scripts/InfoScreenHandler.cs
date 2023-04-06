using System.Collections.Generic;
using InputSamples.Gestures;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InfoScreenHandler : MonoBehaviour
{
    [Header("Swipe Variables")]
    bool canSwipe;
    public float transitionDuration;
    [SerializeField]
    private GestureController gestureController;

    [Header("Page Variables")]
    public int offset = 0;
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
            NextPage();
        } 
        else if (input.SwipeDirection.x > 0.75f)
        {
            PrevPage();
        }
    }

    void UpdatePage()
    {
        // textDisplay.text = textContents[currentPage];
        // if (textDisplay.text == "")
        // {
        //     largeImage.gameObject.SetActive(true);
        //     largeImage.sprite = imageContents[currentPage];
        // }
        // else
        // {
        //     largeImage.gameObject.SetActive(false);
        //     smallImage.sprite = imageContents[currentPage];
        // }

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
        }
        
    }

    IEnumerator ChangePageAnim(int currentPage, int direction)
    {
        canSwipe = false;

        float counter = 0;

        while (counter < transitionDuration)
        {
            counter += Time.deltaTime;

            for (int i = 0; i < pages.Count; i++)
            {
                Vector2 currentPos = pages[i].anchoredPosition;
                // Vector2 destination = pages[currentPage].anchoredPosition;
                // pages[i].anchoredPosition = new Vector2((Screen.width * i) + ((i != 0) ? offset * i : 0), 0);
                // Screen.width = 10
                // 0,10,20 <-- currentPage == 0
                // -10,0,10 <-- currentPage == 1 
                // -20,-10,0 <-- currentPage = 2
                // nextPage
                // (Screen.width * (currentPage)) 

                Vector2 newPosition;

                if (direction == 0) // Prev Page
                {
                    newPosition = originalPagePositions[0];
                }
                else // Next Page
                {
                    newPosition = originalPagePositions[0];
                }

                

                float time = Vector2.Distance(currentPos, newPosition) / (duration - counter) * Time.deltaTime;
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
