using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonClicks : MonoBehaviour
{

    public Button button1, forceReproduceButton;
    public TMPro.TextMeshProUGUI button1Text, forceReproduceText;

    public PeopleCounter peopleCounter;

    int Button1ClickedAmnt = 0;

    /* 
    This function is called when the button under the people count is clicked.
    It will turn turn itself off after the second click, revealing the force reproduce button.
    */
    void StartButtonClicked()
    {
        Button1ClickedAmnt += 1;
        if(Button1ClickedAmnt == 1)
        {
            peopleCounter.AddToCount(2);
            button1Text.text = "Make them reproduce!";
        }
        else if(Button1ClickedAmnt == 2)
        {
            button1.gameObject.SetActive(false);
            forceReproduceButton.gameObject.SetActive(true);

            StartCoroutine(peopleCounter.RepeatAdd(1, 5.0f));
        }
    }

    // I think this one is pretty self explanatory...
    void ForceReproduceClicked() 
    {
        peopleCounter.AddToCount(1);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        button1.onClick.AddListener(StartButtonClicked);
        forceReproduceButton.onClick.AddListener(ForceReproduceClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
