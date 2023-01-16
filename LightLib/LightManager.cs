using EMU.Interface;
using EMU.Parameter;

namespace Light
{
    public class LightManager : ILightControl
    {
        private ILightControl lightControl = null;

        private LightManager()
        {
            lightControl = new CircularLight(EMU.Parameter.Properties.Settings.Default.Light);
            lightControl.SetLightBrightness(LightName.FrontRobotLight, EMU.Parameter.Properties.Settings.Default.LightFrontHigh);
            lightControl.SetLightBrightness(LightName.BackRobotLight, EMU.Parameter.Properties.Settings.Default.LightBackHigh);
        }
        private static LightManager instance;
        public static LightManager Instance
        {
            get
            {
                return instance ?? (instance = new LightManager());
            }
        }

        public void PowerOn(LightName light)
        {
            lightControl.PowerOn(light);
        }

        public void PowerOff(LightName light)
        {
            lightControl.PowerOff(light);
        }

        public bool LightConnect(LightName light)
        {
            return lightControl.LightConnect(light);
        }

        public bool LightDisConnect(LightName light)
        {
            return lightControl.LightDisConnect(light);
        }

        public bool LightOn(LightName light)
        {
            return lightControl.LightOn(light);
        }

        public bool LightOff(LightName light)
        {
            return lightControl.LightOff(light);
        }

        public bool SetLightBrightness(LightName light, int brightness)
        {
            return lightControl.SetLightBrightness(light, brightness);
        }
    }
}
