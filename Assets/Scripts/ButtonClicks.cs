using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonClicks : MonoBehaviour
{

    public Button button1, forceReproduceButton, fertilityUpgrade1;
    public TMPro.TextMeshProUGUI button1Text, forceReproduceText, fertilityUpgrade1Text;

    public PeopleCounter peopleCounter;

    Coroutine autoPpl;
    int Button1ClickedAmnt = 0;

    int fertilityUpgrade1Level = 1;
    

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
            fertilityUpgrade1.gameObject.SetActive(true);

            autoPpl = StartCoroutine(peopleCounter.RepeatAdd(1, 5.0f));
        }
    }

    // I think this one is pretty self explanatory...
    void ForceReproduceClicked() 
    {
        peopleCounter.AddToCount(1);
    }

    void FertilityUpgradeClicked()
    {
        double fertilityUpgrade1Cost = (double)Mathf.Pow(100, fertilityUpgrade1Level);

        if(peopleCounter.peopleCount >= fertilityUpgrade1Cost)
        {
            fertilityUpgrade1Level += 1;
            peopleCounter.AddToCount(-fertilityUpgrade1Cost);
            fertilityUpgrade1Cost = (double)Mathf.Pow(100, fertilityUpgrade1Level);

            StopCoroutine(autoPpl);
            autoPpl = StartCoroutine(peopleCounter.RepeatAdd(1, 5/fertilityUpgrade1Level));
        }
        fertilityUpgrade1Text.text = $"Upgrade Fertility | Cost: {fertilityUpgrade1Cost} people";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        button1.onClick.AddListener(StartButtonClicked);
        forceReproduceButton.onClick.AddListener(ForceReproduceClicked);
        fertilityUpgrade1.onClick.AddListener(FertilityUpgradeClicked);

        forceReproduceButton.gameObject.SetActive(false);
        fertilityUpgrade1.gameObject.SetActive(false);

    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
