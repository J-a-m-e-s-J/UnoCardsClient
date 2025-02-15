using UnityEngine;

namespace UnoCardsClient.Statics
{
    public static class StaticFunctions
    {
        public static void Hide(RectTransform rectTransform)
        {
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y, -1);
        }

        public static void Show(RectTransform rectTransform)
        {
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y, 0);
        }
    }
}