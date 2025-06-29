using UnityEngine;
using UnityEngine.UIElements;

namespace MiniUI.UIScreens
{
    public abstract class UIScreen : ScriptableObject
    {
        protected VisualElement RootElement { get; set; }

        public abstract void TryUpdateScreenData(VisualElement root);

        public void Show()
        {
            RootElement.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            RootElement.style.display = DisplayStyle.None;
        }
    }
}