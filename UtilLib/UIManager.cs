using System;

namespace EMU.Util
{
    public class UIManager
    {
        public static event Action<string> ClearUIEvent;
        public static void ClearEvent(string uiName)
        {
            ClearUIEvent?.Invoke(uiName);
        }
    }
}
