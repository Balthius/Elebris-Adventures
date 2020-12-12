using UnityEngine;
using TMPro;
namespace Assets.Scripts.UI
{
    public class TextPopup : MonoBehaviour
    {
        TextMeshPro tmPro;
        private TextBundle _bundle;
       
        public void SetText(TextBundle bundle)
        {
            _bundle = bundle;
        }

        private void Awake()
        {
            tmPro = GetComponent<TextMeshPro>();
        }

        private void Update()
        {
            transform.position += new Vector3(0, _bundle.model.riseValue, 0 * Time.deltaTime);
        }

    }

}
