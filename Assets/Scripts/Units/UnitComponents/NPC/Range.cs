using Assets.Scripts.Units;
using UnityEngine;

public class Range : MonoBehaviour
{

    private CpuInputController parent;

    private void Start()
    {
        parent = GetComponentInParent<CpuInputController>();
        //Debug.Log(parent);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //simple target aquisition system
        if (collision.tag == "Hero")
        {
            parent.Target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == parent.Target)
        {
            parent.Target = null;
        }
    }
}
