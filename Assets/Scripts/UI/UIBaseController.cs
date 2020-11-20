using UnityEngine;

public abstract class UIBaseController : MonoBehaviour
{

    protected UIMasterController canvas;

    public virtual void Start()
    {
        canvas.Subscribe(this);
    }
    private void OnEnable()
    {
        //use depenancy injection, Unity style
        canvas = GetComponentInParent<UIMasterController>();
    }

    public virtual void OnPlayerUpdated()
    {

    }


    //respond to updates from the canvas

}
