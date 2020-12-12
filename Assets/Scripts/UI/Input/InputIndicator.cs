using UnityEngine;
using UnityEngine.UI;

public class InputIndicator : MonoBehaviour
{
    private Image bgImage;
    private Image actionImage;
    bool currentState;

    private Color originalColor;
    private void OnEnable()
    {
        bgImage = GetComponent<Image>();
        originalColor = bgImage.color;
        Image[] images = gameObject.GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            if (image != bgImage)
            {
                actionImage = image;
                break;
            }

        }
    }

    public void SetImage(Sprite actionImage)
    {
        this.actionImage.sprite = actionImage;
    }
    //replace with event call?
    public void AlterImageState(bool state)
    {
        if (currentState == state) return; //don't waste time updating values that havent changed
        currentState = state;
        if (currentState)
        {
            bgImage.color = Color.white;
            //change color and size slightly
        }
        else
        {
            bgImage.color = originalColor;
            //revert
        }
    }
}
