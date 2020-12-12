using UnityEngine;

namespace Assets.Scripts.UI
{
    [CreateAssetMenu(menuName ="UI/TextModel")]
    public class TextModel : ScriptableObject
    {
        public Color color = Color.white;
        public int size = 12;
        public int riseValue = 5;
        public float duration = .5f;
    }

}
