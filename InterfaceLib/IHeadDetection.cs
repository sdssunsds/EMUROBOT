namespace EMU.Interface
{
    public interface IHeadDetection
    {
        /// <summary>
        /// 获取车头位置
        /// </summary>
        /// <param name="task">主任务对象</param>
        /// <returns>返回车头位置</returns>
        int GetHeadPosition(IMainTask task);
    }
}
