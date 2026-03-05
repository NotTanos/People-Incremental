using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonClicks : MonoBehaviour
{

    public Button button1;
    public TMPro.TextMeshProUGUI button1Text;

    public PeopleCounter peopleCounter;

    int Button1ClickedAmnt = 0;
    void StartButtonClicked()
    {
        Button1ClickedAmnt += 1;
        if(Button1ClickedAmnt == 1)
        {
            peopleCounter.AddToCount(2);
            button1Text.text = "Make them reproduce!";
        }
        if(Button1ClickedAmnt == 2)
        {
            button1.gameObject.SetActive(false);
            StartCoroutine(peopleCounter.RepeatAdd(1, 5.0f));
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        button1.onClick.AddListener(StartButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
