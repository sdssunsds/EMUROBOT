using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EMU.RolePermissions
{
    public partial class JurisdictionControl : UserControl
    {
        private TreeNode selectedNode = null;
        public bool IsSave { get; set; } = true;

        public JurisdictionControl()
        {
            InitializeComponent();
        }

        public List<Jurisdiction> GetJurisdictions()
        {
            List<Jurisdiction> list = new List<Jurisdiction>();
            foreach (TreeNode item in treeView1.Nodes)
            {
                GetTreeNodeData(item, null, ref list);
            }
            return list;
        }

        private void JurisdictionControl_Load(object sender, EventArgs e)
        {
            List<Jurisdiction> jurisdictions = Global.DataBase.GetJurisdictionDatas();
            foreach (Jurisdiction item in jurisdictions)
            {
                BindTreeNodes(item);
            }
            treeView1.ExpandAll();
        }

        private void JurisdictionControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.S)
            {
                if (!IsSave)
                {
                    Global.DataBase.ClearJurisdiction();
                    Global.DataBase.SaveJurisdiction(GetJurisdictions());
                    IsSave = true;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (selectedNode != null)
            {
                Jurisdiction jurisdiction = selectedNode.Tag as Jurisdiction;
                jurisdiction.Name = textBox1.Text;
                selectedNode.Text = GetNodeText(jurisdiction);
                IsSave = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedNode != null)
            {
                Jurisdiction jurisdiction = selectedNode.Tag as Jurisdiction;
                if (radioButton1.Checked)
                {
                    jurisdiction.ControlRange = ControlRange.Enable;
                }
                else
                {
                    jurisdiction.ControlRange = ControlRange.Visible;
                }
                selectedNode.Text = GetNodeText(jurisdiction);
                IsSave = false;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBox1.TextChanged -= textBox1_TextChanged;
            radioButton1.CheckedChanged -= radioButton1_CheckedChanged;

            string[] vs = e.Node.Text.Split(' ');
            textBox1.Text = vs[0];
            ControlRange range = (ControlRange)Enum.Parse(typeof(ControlRange), vs[1]);
            switch (range)
            {
                case ControlRange.Enable:
                    radioButton1.Checked = true;
                    break;
                case ControlRange.Visible:
                    radioButton2.Checked = true;
                    break;
            }
            selectedNode = e.Node;

            textBox1.TextChanged += textBox1_TextChanged;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {

        }

        private void BindTreeNodes(Jurisdiction jurisdiction, TreeNode node = null)
        {
            if (jurisdiction.Parent == null)
            {
                TreeNode treeNode = new TreeNode() { Name = jurisdiction.ID.ToString(), Text = GetNodeText(jurisdiction), Tag = jurisdiction };
                if (node == null)
                {
                    treeView1.Nodes.Add(treeNode);
                }
                else
                {
                    node.Nodes.Add(treeNode);
                }
            }
            else
            {
                if (node == null)
                {
                    foreach (TreeNode item in treeView1.Nodes)
                    {
                        BindTreeNodes(jurisdiction, item);
                    }
                }
                else
                {
                    if (node.Name == jurisdiction.Parent.ID.ToString())
                    {
                        node.Nodes.Add(new TreeNode() { Name = jurisdiction.ID.ToString(), Text = GetNodeText(jurisdiction), Tag = jurisdiction });
                    }
                    else
                    {
                        foreach (TreeNode item in node.Nodes)
                        {
                            BindTreeNodes(jurisdiction, item);
                        }
                    }
                }
            }
        }

        private void GetTreeNodeData(TreeNode node, Jurisdiction parent, ref List<Jurisdiction> list)
        {
            if (node != null)
            {
                Jurisdiction jurisdiction = node.Tag as Jurisdiction;
                if (parent != null)
                {
                    jurisdiction.Parent = parent;
                }
                list.Add(jurisdiction);
                foreach (TreeNode item in node.Nodes)
                {
                    GetTreeNodeData(item, jurisdiction, ref list);
                }
            }
        }

        private string GetNodeText(Jurisdiction jurisdiction)
        {
            return jurisdiction.Name + " " + jurisdiction.ControlRange.ToString() + " " + jurisdiction.Path; ;
        }
    }
}
