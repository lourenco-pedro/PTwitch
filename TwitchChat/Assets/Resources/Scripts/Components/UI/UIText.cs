using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PoolSystem;

namespace TwitchChat_frntEnd
{
    public class UIText : MonoBehaviour, IMonoBehaviourPool
    {
        [SerializeField]
        private TMP_Text Text;

        public string text { get => Text.text; set => Text.text = value; }
        public Color color { get => Text.color; set => Text.color = value; }
        public float fontSize { get => Text.fontSize; set => Text.fontSize = value; }

        public MonoBehaviour MonoBehaviourReference { get => this; }
        public GameObject GameObjectReference { get => gameObject; }

#if UNITY_EDITOR
        void OnValidate()
        {
            Text = GetComponent<TMP_Text>();
        }
#endif
    }
}
