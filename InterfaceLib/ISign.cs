using System;

namespace EMU.Interface
{
    public delegate void SetSign(string txt, bool isStart = true);
    public interface ISign
    {
        /// <summary>
        /// 设置标记事件
        /// </summary>
        event SetSign SetSign;
        /// <summary>
        /// 设置报警标记事件
        /// </summary>
        event Action<int, int> SetAlarm;
        /// <summary>
        /// 清理标记事件
        /// </summary>
        event Action ClearSign;
        /// <summary>
        /// 结束标记事件
        /// </summary>
        event Action EndSign;
    }
}
