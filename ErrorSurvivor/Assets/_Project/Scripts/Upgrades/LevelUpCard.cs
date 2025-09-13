using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ErrorSpace
{
    public class LevelUpCard : MonoBehaviour
    {
        public UnityEvent OnClick => button.onClick;

        [SerializeField] private TMP_Text uName;
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;

        public void Set(Sprite sprite, string description, string name)
        {
            uName.text = name;
            this.text.text = description;
            this.icon.sprite = sprite;
            gameObject.SetActive(true);
        }
    }
}
