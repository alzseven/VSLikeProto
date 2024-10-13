using UnityEngine;
using UnityEngine.UI;

namespace Content.UI
{
    public class UIItemLevelImage : MonoBehaviour
    {
        [SerializeField] private GameObject _fill;

        public void SetFill(bool isEnabled) => _fill.SetActive(isEnabled);
    }
}