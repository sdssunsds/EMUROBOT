using EMU.Parameter;

namespace EMU.Interface
{
    public interface ILightControl
    {
        /// <summary>
        /// 开电
        /// </summary>
        void PowerOn(LightName light);
        /// <summary>
        /// 关电
        /// </summary>
        void PowerOff(LightName light);
        /// <summary>
        /// 光源连接
        /// </summary>
        /// <param name="light">光源目标</param>
        bool LightConnect(LightName light);
        /// <summary>
        /// 光源断开连接
        /// </summary>
        /// <param name="light">光源目标</param>
        bool LightDisConnect(LightName light);
        /// <summary>
        /// 光源打开
        /// </summary>
        /// <param name="light">光源目标</param>
        bool LightOn(LightName light);
        /// <summary>
        /// 光源关闭
        /// </summary>
        /// <param name="light">光源目标</param>
        bool LightOff(LightName light);
        /// <summary>
        /// 设置光源亮度
        /// </summary>
        /// <param name="light">光源目标</param>
        /// <param name="brightness">亮度</param>
        bool SetLightBrightness(LightName light, int brightness);
    }
}
