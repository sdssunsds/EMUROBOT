namespace EMU.Interface
{
    /// <summary>
    /// 相机异常处理委托
    /// </summary>
    public delegate void CameraError(params object[] objs);
    /// <summary>
    /// 相机状态改变处理委托
    /// </summary>
    public delegate void CameraStateChanged(params object[] objs);
    /// <summary>
    /// 相机图片回调处理委托
    /// </summary>
    public delegate void ImageReady(params object[] objs);
    public interface ICameraControl
    {
        /// <summary>
        /// 相机初始化
        /// </summary>
        void Initialise();
        /// <summary>
        /// 打开相机
        /// </summary>
        void Open();
        /// <summary>
        /// 关闭相机
        /// </summary>
        void Close();
        /// <summary>
        /// 单帧拍图
        /// </summary>
        void OneShot();
        /// <summary>
        /// 连续拍图
        /// </summary>
        void ContinuousShot();
        /// <summary>
        /// 停止连续拍图
        /// </summary>
        void Stop();
        /// <summary>
        /// 设置相关事件
        /// </summary>
        /// <param name="cameraError">相机异常事件</param>
        /// <param name="cameraStateChanged">相机状态变化事件</param>
        /// <param name="imageReady">相机拍图回调事件</param>
        void SetEvent(CameraError cameraError, CameraStateChanged cameraStateChanged, ImageReady imageReady);
    }
}
