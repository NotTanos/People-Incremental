using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class PeopleCounter : MonoBehaviour

{
    public double peopleCount = 0;
    public TMPro.TextMeshProUGUI countText;

    public void UpdatePeopleText()
    {
        if(countText != null)
        {
            countText.text = $"There are {peopleCount} people on this planet.";
        }
        if(peopleCount == double.PositiveInfinity)
        {
            Invoke(nameof(GameEnd), 3.0f);
        }
    }

    public void AddToCount(double amount)
    {
        peopleCount += amount;  
        UpdatePeopleText();
    }

    public IEnumerator RepeatAdd(double amount, float repeatRate)
    {
        while(true)
        {
            AddToCount(amount);
            yield return new WaitForSeconds(repeatRate);
        }
    }

    void GameEnd()
    {
        SceneManager.LoadScene("Yay");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdatePeopleText();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}