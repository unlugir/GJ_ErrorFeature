using UnityEngine;
using UnityEngine.EventSystems;

namespace ErrorSpace
{
    //i am just worried about clear all listeners staff somewhere in the code, so here we code at 2am
    public class ButtonSFX : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] SFXObject _sfx;
        public void OnPointerClick(PointerEventData eventData)
        {
            SFXManager.Main.Play(_sfx);
        }
    }
}
