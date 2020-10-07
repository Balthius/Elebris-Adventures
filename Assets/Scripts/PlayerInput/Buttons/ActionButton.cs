using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler
{

    public IUseable MyUseable { get; set; }

    public Button MyButton { get;private set; }
    // Use this for initialization
    void Start()
    {
        MyButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPress()
    {

    }
 
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

}
