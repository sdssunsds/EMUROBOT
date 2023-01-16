using EMU.Interface;
using EMU.Parameter;
using System;

namespace Laser
{
    public class LaserManager : ILaserControl
    {
        private Lidar lidar = null;
        private RangingLaser rangingLaser = null;

        public Lidar Lidar { get { return lidar; } }
        public RangingLaser RangingLaser { get { return rangingLaser; } }

        private LaserManager() { }
        private static LaserManager instance;
        public static LaserManager Instance
        {
            get
            {
                return instance ?? (instance = new LaserManager());
            }
        }

        public event LaserData GetLaserData;
        public event LaserObject GetLaserObj;
        public event LaserObjectEx GetLaserObjEx;

        public bool LaserConnect(LaserName laser)
        {
            switch (laser)
            {
                case LaserName.LocationLaser:
                    break;
                case LaserName.RangingLaser:
                    rangingLaser = new RangingLaser(EMU.Parameter.Properties.Settings.Default.LaserPositioning);
                    rangingLaser.ReadValue = new Action<string>((string s) => { GetLaserData?.Invoke(s, LaserName.RangingLaser); });
                    return rangingLaser.IsConnect();
                case LaserName.Lidar:
                    lidar = new Lidar(EMU.Parameter.Properties.Settings.Default.Lidar);
                    lidar.ReadValue = new Action<string>((string s) => { GetLaserData?.Invoke(s, LaserName.Lidar); });
                    lidar.ReadList = new Action<object, object, object, object>((object obj1, object obj2, object obj3, object obj4) => { GetLaserObjEx?.Invoke(LaserName.Lidar, obj1, obj2, obj3, obj4); });
                    return lidar.IsConnect();
                case LaserName.LineLidar:

                    break;
            }
            return false;
        }

        public bool LaserDisConnect(LaserName laser)
        {
            switch (laser)
            {
                case LaserName.LocationLaser:
                    break;
                case LaserName.RangingLaser:
                    rangingLaser.Close();
                    return true;
                case LaserName.Lidar:
                    lidar.Close();
                    return true;
                case LaserName.LineLidar:
                    break;
            }
            return false;
        }
    }
}
