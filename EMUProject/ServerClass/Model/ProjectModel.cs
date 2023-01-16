using System;

namespace Project.ServerClass.Model
{
    public class ProjectModel
    {
        private string id = "";
        private DateTime projectDate;
        private ProjectState projectState;
        private string projectText;
        private string testAddress;
        private string testRode;
        private string testPoint;
        private DateTime? testStart;
        private DateTime? testEnd;
        private string mode;
        private string sn;
        private string head;
        private ProjectType testType;
        private int count;
        private string robot;
        private bool enable = true;

        /// <summary>
        /// 计划编号
        /// </summary>
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 计划时间
        /// </summary>
        public DateTime ProjectDate
        {
            get { return projectDate; }
            set
            {
                projectDate = value;
                if (string.IsNullOrEmpty(id))
                {
                    id = value.ToString("yyyyMMddHHmmss");
                }
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 计划状态
        /// </summary>
        public ProjectState ProjectState
        {
            get { return projectState; }
            set
            {
                projectState = value;
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 计划说明
        /// </summary>
        public string ProjectText
        {
            get { return projectText; }
            set
            {
                projectText = value;
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 检修库
        /// </summary>
        public string TestAddress
        {
            get { return testAddress; }
            set
            {
                testAddress = value;
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 检修车道
        /// </summary>
        public string TestRode
        {
            get { return testRode; }
            set
            {
                testRode = value;
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 检修位
        /// </summary>
        public string TestPoint
        {
            get { return testPoint; }
            set
            {
                testPoint = value;
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 检修时间
        /// </summary>
        public DateTime? TestStart
        {
            get { return testStart; }
            set
            {
                testStart = value;
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 检修结束时间
        /// </summary>
        public DateTime? TestEnd
        {
            get { return testEnd; }
            set
            {
                testEnd = value;
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 车型
        /// </summary>
        public string Mode
        {
            get { return mode; }
            set
            {
                mode = value;
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 车号
        /// </summary>
        public string Sn
        {
            get { return sn; }
            set
            {
                sn = value;
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 车头号
        /// </summary>
        public string Head
        {
            get { return head; }
            set
            {
                head = value;
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 检测类型
        /// </summary>
        public ProjectType TestType
        {
            get { return testType; }
            set
            {
                testType = value;
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 车厢数量
        /// </summary>
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 机器人编号
        /// </summary>
        public string Robot
        {
            get { return robot; }
            set
            {
                robot = value;
                SetPropertyEvent?.Invoke();
            }
        }
        /// <summary>
        /// 计划启用
        /// </summary>
        public bool Enable
        {
            get { return enable; }
            set
            {
                enable = value;
                if (!value)
                {
                    if (projectState == ProjectState.检修中 || projectState == ProjectState.计划中)
                    {
                        testEnd = DateTime.Now;
                        projectState = ProjectState.检修完成;
                    }
                }
                SetPropertyEvent?.Invoke();
            }
        }
        public event Action SetPropertyEvent;
    }

    public enum ProjectState
    {
        计划中, 检修中, 检修完成, 检修异常
    }

    public enum ProjectType
    {
        车底检测, 车侧检测, 车顶检测
    }
}
