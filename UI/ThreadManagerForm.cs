using EMU.Util;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace EMU.UI
{
    public partial class ThreadManagerForm : Form
    {
        private List<TreeView> backTreeList = null;
        private List<TreeView> frontTreeList = null;

        public ThreadManagerForm()
        {
            InitializeComponent();
            backTreeList = new List<TreeView>();
            frontTreeList = new List<TreeView>();
        }

        private void ThreadManagerForm_Load(object sender, EventArgs e)
        {
            List<ThreadEventArgs> threadEventArgs = ThreadManager.GetThreadEventArgs();
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
                string name = node.Text.Split(':')[0];
                SetTreeNode(name, obj, node);
            }
        }

        private void AddBackTreeView(TreeView tv)
        {
            backTreeList.Add(tv);
            AddTreeView(tv, flp_back);
        }

        private void AddFrontTreeView(TreeView tv)
        {
            frontTreeList.Add(tv);
            AddTreeView(tv, flp_front);
        }

        private void AddTreeView(ThreadEventArgs args)
        {
            TreeView tv = new TreeView();
            tv.Tag = args;
            InitTreeView(tv, args);
            tv.NodeMouseClick += Tv_NodeMouseClick;
            if (args.IsBackThread)
            {
                AddBackTreeView(tv);
            }
            else
            {
                AddFrontTreeView(tv);
            }
        }

        private void AddTreeView(TreeView tv, FlowLayoutPanel flp)
        {
            flp.Controls.Add(tv);
        }

        private void InitTreeView(TreeView tv, ThreadEventArgs args)
        {
            TreeNode root = new TreeNode() { Text = args.ThreadName };
            root.Checked = args.IsRunThread;
            tv.Nodes.Add(root);

            for (int i = 0; i < args.VariableList.Count; i++)
            {
                string name = "";
                if (i < args.VariableNames.Count)
                {
                    name = args.VariableNames[i];
                }
                else
                {
                    name = "未知";
                }
                InitTreeNode(name, args.VariableList[i], root);
            }
        }

        private void InitTreeNode(string name, object obj, TreeNode node)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Tag = obj;
            SetTreeNode(name, obj, treeNode);
            node.Nodes.Add(treeNode);
        }

        private void RefreshTreeView(List<TreeView> treeViews)
        {
            foreach (TreeView item in treeViews)
            {
                if (item.Tag != null && item.Tag is ThreadEventArgs)
                {
                    item.Nodes[0].Checked = (item.Tag as ThreadEventArgs).IsRunThread;
                }
            }
        }

        private void RemovingTreeView(int i, FlowLayoutPanel flp, List<TreeView> tvList)
        {
            TreeView tv = tvList[i];
            flp.Controls.Remove(tv);
            tvList.RemoveAt(i);
            tv.Dispose();
        }

        private void SetTreeNode(string name, object obj, TreeNode treeNode)
        {
            Type type = obj.GetType();
            if (type.IsValueType)
            {
                treeNode.Text = name + ": " + obj.ToString();
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

        private void ThreadEventArgsAdding(ThreadEventArgs args)
        {
            AddTreeView(args);
        }

        private void ThreadEventArgsRemoving(ThreadEventArgs args)
        {
            int i = backTreeList.FindIndex(t => t.Tag == args);
            if (i > -1)
            {
                RemovingTreeView(i, flp_back, backTreeList);
            }
            else
            {
                i = frontTreeList.FindIndex(t => t.Tag == args);
                if (i > -1)
                {
                    RemovingTreeView(i, flp_front, frontTreeList);
                }
            }
        }
    }
}
