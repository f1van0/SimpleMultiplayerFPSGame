using UnityEngine;

namespace JoyWay.UI
{
    public class HideableUI : MonoBehaviour
    {
        public GameObject _hideableObject;
        
        public void Show()
        {
            _hideableObject.SetActive(true);
        }
        
        public void Hide()
        {
            _hideableObject.SetActive(false);
        }
    }
}