using EMU.Parameter;
using System;

namespace EMU.ApplicationData
{
    public class RgvGlobalInfo
    {
        private static RgvGlobalInfo instance;
        public static RgvGlobalInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RgvGlobalInfo();
                }
                return instance;
            }
        }

        public event Action PropertyChangedEvent;

        public int ModWorkCode { get; set; }
        public string ModWorkMsg { get; set; }

        public string ID { get; set; }

        private int rgvCurrentRunSpeed;
        /// <summary>
        /// Rgv当前运行速度
        /// </summary>
        public int RgvCurrentRunSpeed
        {
            get { return rgvCurrentRunSpeed; }
            set
            {
                rgvCurrentRunSpeed = value;
                PropertyChangedEvent?.Invoke();
            }
        }

        private int rgvCurrentRunDistacnce;
        /// <summary>
        /// Rgv当前运行距离
        /// </summary>
        public int RgvCurrentRunDistacnce
        {
            get { return rgvCurrentRunDistacnce; }
            set
            {
                rgvCurrentRunDistacnce = value;
                PropertyChangedEvent?.Invoke();
            }
        }

        private int rgvCurrentPowerStat;
        /// <summary>
        /// Rgv电池状态
        /// </summary>
        public int RgvCurrentPowerStat
        {
            get { return rgvCurrentPowerStat; }
            set
            {
                rgvCurrentPowerStat = value;
                PropertyChangedEvent?.Invoke();
            }
        }

        private int rgvCurrentPowerElectricity;
        /// <summary>
        /// Rgv当前电池电量
        /// </summary>
        public int RgvCurrentPowerElectricity
        {
            get { return rgvCurrentPowerElectricity; }
            set
            {
                rgvCurrentPowerElectricity = value;
                PropertyChangedEvent?.Invoke();
            }
        }

        private int rgvCurrentPowerCurrent;
        /// <summary>
        /// Rgv当前电池电流
        /// </summary>
        public int RgvCurrentPowerCurrent
        {
            get { return rgvCurrentPowerCurrent; }
            set
            {
                rgvCurrentPowerCurrent = value;
                PropertyChangedEvent?.Invoke();
            }
        }

        private int rgvCurrentPowerTempture;
        /// <summary>
        /// Rgv当前电池温度
        /// </summary>
        public int RgvCurrentPowerTempture
        {
            get { return rgvCurrentPowerTempture; }
            set
            {
                rgvCurrentPowerTempture = value;
                PropertyChangedEvent?.Invoke();
            }
        }

        private string rgvCurrentMode;
        /// <summary>
        /// Rgv当前模式：本地模式,远程模式
        /// </summary>
        public string RgvCurrentMode
        {
            get { return rgvCurrentMode; }
            set
            {
                rgvCurrentMode = value;
                PropertyChangedEvent?.Invoke();
            }
        }

        private string rgvCurrentStat;
        /// <summary>
        /// Rgv当前状态信息
        /// </summary>
        public string RgvCurrentStat
        {
            get { return rgvCurrentStat; }
            set
            {
                rgvCurrentStat = value;
                PropertyChangedEvent?.Invoke();
            }
        }

        /// <summary>
        /// Rgv命令设置状态
        /// </summary>
        public string RgvCurrentCmdSetStat { get; set; }

        /// <summary>
        /// Rgv参数设置状态
        /// </summary>
        public int RgvCurrentParaSetStat { get; set; }

        private int rgvIsAlarm = 0;
        /// <summary>
        /// Rgv小车异常状态：0-设备正常,1-设备异常
        /// </summary>
        public int RgvIsAlarm
        {
            get { return rgvIsAlarm; }
            set
            {
                rgvIsAlarm = value;
                PropertyChangedEvent?.Invoke();
            }
        }

        private int rgvTargetRunSpeed;
        /// <summary>
        /// Rgv目标运行速度
        /// </summary>
        public int RgvTargetRunSpeed
        {
            get { return rgvTargetRunSpeed; }
            set
            {
                rgvTargetRunSpeed = value;
                PropertyChangedEvent?.Invoke();
            }
        }

        private int rgvTargetRunDistance;
        /// <summary>
        /// Rgv目标运行距离
        /// </summary>
        public int RgvTargetRunDistance
        {
            get { return rgvTargetRunDistance; }
            set
            {
                rgvTargetRunDistance = value;
                PropertyChangedEvent?.Invoke();
            }
        }

        private int rgvTrackLength;
        /// <summary>
        /// Rgv轨道运行长度
        /// </summary>
        public int RgvTrackLength
        {
            get { return rgvTrackLength; }
            set
            {
                rgvTrackLength = value;
                PropertyChangedEvent?.Invoke();
            }
        }

        private EquipmentStatus rgvRunStatMonitor = EquipmentStatus.READY;
        /// <summary>
        /// Rgv运动指令执行状态监控
        /// </summary>
        public EquipmentStatus RgvRunStatMonitor
        {
            get { return rgvRunStatMonitor; }
            set
            {
                rgvRunStatMonitor = value;
                PropertyChangedEvent?.Invoke();
            }
        }
    }
}
