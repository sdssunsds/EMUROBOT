using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMU.RolePermissions
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string error = "";
            Global.LoginUser = Global.DataBase.Login(textBox1.Text, textBox2.Text, ref error);
            if (Global.LoginUser == null || !string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
