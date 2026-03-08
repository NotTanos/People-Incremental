using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnHoverPanels : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image infoPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoPanel.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel.gameObject.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
