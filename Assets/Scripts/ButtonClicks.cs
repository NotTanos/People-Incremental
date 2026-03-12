using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonClicks : MonoBehaviour
{

    public Button button1, forceReproduceButton, fertilityUpgrade1, fertilityUpgrade2, litterUpgrade;
    public TMPro.TextMeshProUGUI button1Text, forceReproduceText, fertilityUpgrade1Text, fertilityUpgrade2Text, litterUpgradeText, fertilityUpgrade1PanelText, litterUpgradePanelText;

    public PeopleCounter peopleCounter;

    Coroutine autoPpl;
    int Button1ClickedAmnt = 0;

    int fertilityUpgrade1Level = 0;
    // int fertilityUpgrade2Level = 0;
    int litterUpgradeLevel = 0;
    double litterUpgradeCost = 250;


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
            litterUpgrade.gameObject.SetActive(true);

            autoPpl = StartCoroutine(peopleCounter.RepeatAdd(1, 5.0f));
        }
    }

    // I think this one is pretty self explanatory...
    void ForceReproduceClicked()
    {
        peopleCounter.AddToCount((double)(1*Mathf.Pow(2, litterUpgradeLevel)));
    }

    void FertilityUpgrade1Clicked()
    {
        double fertilityUpgrade1Cost = (double)Mathf.Pow(100, fertilityUpgrade1Level+1);

        if(peopleCounter.peopleCount >= fertilityUpgrade1Cost +2)
        {
            fertilityUpgrade1Level += 1;
            peopleCounter.AddToCount(-fertilityUpgrade1Cost);
            fertilityUpgrade1Cost = (double)Mathf.Pow(100, fertilityUpgrade1Level+1);

            StopCoroutine(autoPpl);
            autoPpl = StartCoroutine(peopleCounter.RepeatAdd((double)(1*Mathf.Pow(2, litterUpgradeLevel)), 5/(fertilityUpgrade1Level+1)));
        }
        if(fertilityUpgrade1Level == 1)
        {
            fertilityUpgrade2.gameObject.SetActive(true);
        }
        fertilityUpgrade1Text.text = $"Upgrade Fertility\nCost: {fertilityUpgrade1Cost} people";
        fertilityUpgrade1PanelText.text = $"This upgrade divides the time it takes for humans to automatically make babies. Lvl: {fertilityUpgrade1Level}";
    }

    void FertilityUpgrade2Clicked()
    {

    }

    void LitterUpgradeClicked()
    {
        if(litterUpgradeLevel == 0)
        {
            litterUpgradeCost = 250;
        } else
        {
            litterUpgradeCost = (double)Mathf.Pow(250*litterUpgradeLevel, 2);
        }

        if(peopleCounter.peopleCount >= litterUpgradeCost +2)
        {
            litterUpgradeLevel += 1;
            peopleCounter.AddToCount(-litterUpgradeCost);
            litterUpgradeCost = (double)Mathf.Pow(250*litterUpgradeLevel, 2);

            StopCoroutine(autoPpl);
            autoPpl = StartCoroutine(peopleCounter.RepeatAdd((double)(1*Mathf.Pow(2, litterUpgradeLevel)), 5/(fertilityUpgrade1Level+1)));
        }
        litterUpgradeText.text = $"Double Litter Size\nCost: {litterUpgradeCost} people";
        litterUpgradePanelText.text = $"This upgrade doubles the amount of babies made per level. Grows exponentially. Lvl: {litterUpgradeLevel}";

        forceReproduceText.text = $"Force Reproduction\n(+{(double)(1*Mathf.Pow(2, litterUpgradeLevel))} people)";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button1.onClick.AddListener(StartButtonClicked);
        forceReproduceButton.onClick.AddListener(ForceReproduceClicked);
        fertilityUpgrade1.onClick.AddListener(FertilityUpgrade1Clicked);
        fertilityUpgrade2.onClick.AddListener(FertilityUpgrade2Clicked);
        litterUpgrade.onClick.AddListener(LitterUpgradeClicked);

        forceReproduceButton.gameObject.SetActive(false);
        fertilityUpgrade1.gameObject.SetActive(false);
        fertilityUpgrade2.gameObject.SetActive(false);
        litterUpgrade.gameObject.SetActive(false);
    }

    // Update is called once per frame
    // void Update()
    // {

    // }
}
