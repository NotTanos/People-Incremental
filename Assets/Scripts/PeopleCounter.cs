using UnityEngine;
using System.Collections;
using TMPro;

public class PeopleCounter : MonoBehaviour

{
    public int peopleCount = 0;
    public TMPro.TextMeshProUGUI countText;

    public void UpdatePeopleText()
    {
        if(countText != null)
        {
            countText.text = "There are " + peopleCount.ToString() + " people on this planet.";
        }
    }

    public void AddToCount(int amount)
    {
        peopleCount += amount;  
        UpdatePeopleText();
    }

    public IEnumerator RepeatAdd(int amount, float repeatRate)
    {
        while(true)
        {
            AddToCount(amount);
            yield return new WaitForSeconds(repeatRate);
        }
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