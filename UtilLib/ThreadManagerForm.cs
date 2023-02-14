using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace EMU.Util
{
    public partial class ThreadManagerForm : Form
    {
        private int threadCount = 0;
        private List<TreeNode> backTreeList = null;
        private List<TreeNode> frontTreeList = null;

        public ThreadManagerForm()
        {
            InitializeComponent();
            backTreeList = new List<TreeNode>();
            frontTreeList = new List<TreeNode>();
        }

        private void ThreadManagerForm_Load(object sender, EventArgs e)
        {
            List<ThreadEventArgs> threadEventArgs = ThreadManager.GetThreadEventArgs();
            threadCount = threadEventArgs.Count;
            foreach (ThreadEventArgs item in threadEventArgs)
            {
                AddTreeView(item);
            }

            ThreadManager.EventArgsAddingAction = ThreadEventArgsAdding;
            ThreadManager.EventArgsRemovingAction = ThreadEventArgsRemoving;
        }

        private void ThreadManagerForm_Shown(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lb_num1.Text = ThreadManager.GetBackTaskCount().ToString();
            lb_num2.Text = ThreadManager.GetTaskRunCount().ToString();

            RefreshTreeView(frontTreeList);
            RefreshTreeView(backTreeList);
        }

        private void Tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            object obj = node.Tag;
            if (obj != null)
            {
                string name = node.Name;
                SetTreeNode(name, obj, node);
            }
        }

        private void AddBackTreeView(TreeNode tv)
        {
            backTreeList.Add(tv);
            tv_left.Nodes.Add(tv);
        }

        private void AddFrontTreeView(TreeNode tv)
        {
            frontTreeList.Add(tv);
            tv_right.Nodes.Add(tv);
        }

        private void AddTreeView(ThreadEventArgs args)
        {
            TreeNode tv = new TreeNode() { Name = args.ThreadName, Text = args.ThreadName };
            tv.Checked = args.IsRunThread;
            if (args.IsBackThread)
            {
                AddBackTreeView(tv);
            }
            else
            {
                AddFrontTreeView(tv);
            }
            tv.Tag = args;
            InitTreeView(tv, args);
        }

        private void InitTreeView(TreeNode tv, ThreadEventArgs args)
        {
            foreach (KeyValuePair<string, object> item in args.VariableList)
            {
                InitTreeNode(item.Key, item.Value, tv);
            }
        }

        private void InitTreeNode(string name, object obj, TreeNode node)
        {
            TreeNode treeNode = null;
            int i = node.Nodes.IndexOfKey(name);
            if (i < 0)
            {
                treeNode = new TreeNode();
                treeNode.Name = name;
                treeNode.Tag = obj;
                node.Nodes.Add(treeNode);
            }
            else
            {
                treeNode = node.Nodes[i];
            }
            SetTreeNode(name, obj, treeNode);
        }

        private void RefreshTreeView(List<TreeNode> treeViews)
        {
            foreach (TreeNode item in treeViews)
            {
                if (item.Tag != null && item.Tag is ThreadEventArgs)
                {
                    item.Checked = (item.Tag as ThreadEventArgs).IsRunThread;
                }
            }
        }

        private void RemovingTreeView(TreeView tv, TreeNode node, List<TreeNode> list)
        {
            list.Remove(node);
            tv.Nodes.Remove(node);
        }

        private void SetTreeNode(string name, object obj, TreeNode treeNode)
        {
            if (obj == null)
            {
                treeNode.Text = name + ": NULL";
                return;
            }
            Type type = obj.GetType();
            if (type.IsValueType || type.Equals(typeof(string)))
            {
                treeNode.Text = name + ": " + obj.ToString();
            }
            else if (type.IsArray)
            {
                Array array = obj as Array;
                treeNode.Text = name + ": ";
                foreach (var item in array)
                {
                    treeNode.Text += item.ToString() + ", ";
                }
                treeNode.Text = treeNode.Text.Substring(0, treeNode.Text.Length - 1);
            }
            else if (type.Equals(typeof(ThreadEventArgs)))
            {
                treeNode.Text = name;
                ThreadEventArgs threadEventArgs = obj as ThreadEventArgs;
                foreach (KeyValuePair<string, object> item in threadEventArgs.VariableList)
                {
                    InitTreeNode(item.Key, item.Value, treeNode);
                }
            }
            else
            {
                treeNode.Text = name;
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo item in propertyInfos)
                {
                    if (item.CanRead)
                    {
                        InitTreeNode(item.Name, item.GetValue(obj), treeNode);
                    }
                }
            }
        }

        private void SetValueChange(string name, object obj, ThreadEventArgs args)
        {
            TreeNode treeNode = null;
            if (args.IsBackThread)
            {
                treeNode = backTreeList.Find(t => t.Name == args.ThreadName);
            }
            else
            {
                treeNode = frontTreeList.Find(t => t.Name == args.ThreadName);
            }
            if (treeNode != null)
            {
                InitTreeNode(name, obj, treeNode);
            }
            else
            {
                AddTreeView(args);
            }
        }

        private void ThreadEventArgsAdding(ThreadEventArgs args)
        {
            AddTreeView(args);
            args.SetValueChange = SetValueChange;
        }

        private void ThreadEventArgsRemoving(ThreadEventArgs args)
        {
            TreeNode node = backTreeList.Find(t => t.Tag == args);
            if (node != null)
            {
                RemovingTreeView(tv_left, node, backTreeList);
            }
            else
            {
                node = frontTreeList.Find(t => t.Tag == args);
                if (node != null)
                {
                    RemovingTreeView(tv_right, node, frontTreeList);
                }
            }
        }
    }
}
