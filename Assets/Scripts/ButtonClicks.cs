using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonClicks : MonoBehaviour
{

    public Button button1, forceReproduceButton, fertilityUpgrade1, fertilityUpgrade2, litterUpgrade;
    public TMPro.TextMeshProUGUI button1Text, forceReproduceText, fertilityUpgrade1Text, fertilityUpgrade2Text, litterUpgradeText, forceReproducePanelText, fertilityUpgrade1PanelText, fertilityUpgrade2PanelText, litterUpgradePanelText;

    public PeopleCounter peopleCounter;

    Coroutine autoPpl;
    Coroutine autoPpl2;
    int Button1ClickedAmnt = 0;

    int fertilityUpgrade1Level = 0;
    int fertilityUpgrade2Level = 0;
    double fertilityUpgrade2Cost = 1000;
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
        if(fertilityUpgrade1Level < 15)
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
        if(fertilityUpgrade1Level == 15)
        {
            fertilityUpgrade1Text.text = $"Upgrade Fertility\nCost: MAX";
            fertilityUpgrade1PanelText.text = $"This upgrade divides the time it takes for humans to automatically make babies. Lvl: 50";
            fertilityUpgrade1.onClick.RemoveListener(FertilityUpgrade1Clicked);
        }
    }

    void FertilityUpgrade2Clicked()
    {
        if(fertilityUpgrade2Level < 35)
        {
            if(fertilityUpgrade2Level == 0)
            {
                fertilityUpgrade2Cost = 1000;
            } else
            {
                fertilityUpgrade2Cost = (double)Mathf.Pow(10, fertilityUpgrade2Level+3)/2;
            }

            if(peopleCounter.peopleCount >= fertilityUpgrade2Cost +2)
            {
                fertilityUpgrade2Level += 1;
                peopleCounter.AddToCount(-fertilityUpgrade2Cost);
                fertilityUpgrade2Cost = (double)Mathf.Pow(10, fertilityUpgrade2Level+3)/2;

                if(fertilityUpgrade2Level==1){autoPpl2 = StartCoroutine(peopleCounter.RepeatAdd((double)(1*Mathf.Pow(2, litterUpgradeLevel)), 2.5f));}
                StopCoroutine(autoPpl2);
                autoPpl2 = StartCoroutine(peopleCounter.RepeatAdd((double)(1*Mathf.Pow(2, litterUpgradeLevel)), 1.5f/fertilityUpgrade2Level));
            }
            fertilityUpgrade2Text.text = $"Upgrade Fertility... Again\nCost: {fertilityUpgrade2Cost} people";
            fertilityUpgrade2PanelText.text = $"This upgrade makes humans copulate even faster! Every level divides the time it takes to make babies. Lvl: {fertilityUpgrade2Level}";
        }
        if(fertilityUpgrade2Level == 35)
        {
            fertilityUpgrade2Text.text = $"Upgrade Fertility... Again\nCost: MAX";
            fertilityUpgrade2PanelText.text = $"This upgrade makes humans copulate even faster! Every level divides the time it takes to make babies. Lvl: 50";
            fertilityUpgrade2.onClick.RemoveListener(FertilityUpgrade2Clicked);
        }
    }

    void LitterUpgradeClicked()
    {
        if(litterUpgradeLevel == 0)
        {
            litterUpgradeCost = 250;
        } else
        {
            litterUpgradeCost = (double)Mathf.Pow(250*litterUpgradeLevel, 2)/5;
        }

        if(peopleCounter.peopleCount >= litterUpgradeCost +2)
        {
            litterUpgradeLevel += 1;
            peopleCounter.AddToCount(-litterUpgradeCost);
            litterUpgradeCost = (double)Mathf.Pow(250*litterUpgradeLevel, 2)/5;

            StopCoroutine(autoPpl);
            autoPpl = StartCoroutine(peopleCounter.RepeatAdd((double)(1*Mathf.Pow(2, litterUpgradeLevel)), 5/(fertilityUpgrade1Level+1)));

            if(autoPpl2 != null) 
            {
                StopCoroutine(autoPpl2);
                autoPpl2 = StartCoroutine(peopleCounter.RepeatAdd((double)(1*Mathf.Pow(2, litterUpgradeLevel)), 1.5f/fertilityUpgrade2Level));
            }
        }
        litterUpgradeText.text = $"Double Litter Size\nCost: {litterUpgradeCost} people";
        litterUpgradePanelText.text = $"This upgrade doubles the amount of babies made per level. Grows exponentially. Lvl: {litterUpgradeLevel}";

        forceReproduceText.text = $"Force Reproduction\n(+{(double)(1*Mathf.Pow(2, litterUpgradeLevel))} people)";
        forceReproducePanelText.text = $"This button will add {(double)(1*Mathf.Pow(2, litterUpgradeLevel))} person to the planet every time you press it.";
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