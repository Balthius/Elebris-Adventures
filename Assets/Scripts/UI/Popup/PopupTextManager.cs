using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PopupTextManager : MonoBehaviour
    {
        public GameObject uiText;
        public ConstantPopupManager manager;
        private void Awake()
        {
            manager.OnValueChanged += CreateText;
        }
        public void CreateText(Transform transform, TextBundle bundle)
        {
            Instantiate(uiText, transform);
            uiText.GetComponent<TextPopup>().SetText(bundle);

        }
    }
}
