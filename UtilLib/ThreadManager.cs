using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EMU.Util
{
    public delegate void BackThreadFunction(int startIndex, ThreadEventArgs threadEventArgs);
    public delegate void ThreadFunction(ThreadEventArgs threadEventArgs);
    public class ThreadManager
    {
        private static object backLock = new object();
        private static object runLock = new object();
        private static int runKey = 0;
        private static List<BackThreadFunction> backActions;
        private static List<ThreadEventArgs> eventArgs;
        private static Dictionary<int, ThreadEventArgs> backEventArgs;
        private static Dictionary<int, ThreadFunction> runActions;
        public static Action<ThreadEventArgs> EventArgsAddingAction { get; set; }
        public static Action<ThreadEventArgs> EventArgsRemovingAction { get; set; }
        static ThreadManager()
        {
            backActions = new List<BackThreadFunction>();
            eventArgs = new List<ThreadEventArgs>();
            backEventArgs = new Dictionary<int, ThreadEventArgs>();
            runActions = new Dictionary<int, ThreadFunction>();
            Task.Run(() =>
            {
                int i = 0;
                while (true)
                {
                    Thread.Sleep(500);
                    lock (backLock)
                    {
                        for (int j = 0; j < backActions.Count; j++)
                        {
                            ThreadEventArgs args = null;
                            if (backEventArgs.ContainsKey(j))
                            {
                                args = backEventArgs[j];
                            }
                            else
                            {
                                args = new ThreadEventArgs();
                                backEventArgs.Add(j, args);
                                EventArgsAddingAction?.Invoke(args);
                                eventArgs.Add(args);
                            }
                            int index = j;
                            Task.Run(() =>
                            {
                                args.IsRunThread = true;
                                backActions[index](i, args);
                                args.IsRunThread = false;
                            });
                        }
                    }
                    i++;
                    if (i > 10000)
                    {
                        i = 0;
                    }
                }
            });
        }
        public static void TaskRun(ThreadFunction action)
        {
            lock (runLock)
            {
                do
                {
                    runKey++;
                } while (runActions.ContainsKey(runKey));
                runActions.Add(runKey, action);
            }
            Task.Run(() =>
            {
                ThreadEventArgs args = new ThreadEventArgs();
                args.IsBackThread = false;
                EventArgsAddingAction?.Invoke(args);
                eventArgs.Add(args);
                args.IsRunThread = true;
                action(args);
                args.IsRunThread = false;
                EventArgsRemovingAction?.Invoke(args);
                eventArgs.Remove(args);
            });
        }
        public static int GetTaskRunCount()
        {
            return runActions.Count;
        }
        public static void BackTask(BackThreadFunction action)
        {
            lock (backLock)
            {
                backActions.Add(action); 
            }
        }
        public static int GetBackTaskCount()
        {
            return backActions.Count;
        }
        public static List<ThreadEventArgs> GetThreadEventArgs()
        {
            return eventArgs;
        }
    }
    public class ThreadEventArgs
    {
        public bool IsRunThread { get; internal set; } = false;
        public bool IsBackThread { get; internal set; } = true;
        /// <summary>
        /// 线程名称
        /// </summary>
        public string ThreadName { get; set; } = "thread_0";
        private Dictionary<string, object> variableList = new Dictionary<string, object>();
        public Dictionary<string, object> VariableList { get { return variableList; } }
        /// <summary>
        /// 添加跟踪的变量
        /// </summary>
        public void AddVariable(string name, object value)
        {
            if (VariableList.ContainsKey(name))
            {
                VariableList[name] = value;
            }
            else
            {
                VariableList.Add(name, value);
            }
        }
    }
}
