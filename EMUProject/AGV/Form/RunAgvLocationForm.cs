using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Project.AGV
{
    public partial class RunAgvLocationForm : Form
    {
        public List<PointLocation> Locations { get; set; }
        public List<SortPointLocation> SortLocations { get; set; }

        public RunAgvLocationForm()
        {
            InitializeComponent();
        }

        private void RunAgvLocationForm_Load(object sender, EventArgs e)
        {
            if (Locations != null)
            {
                foreach (PointLocation item in Locations)
                {
                    lb_left.Items.Add(item.Name);
                }
            }
            if (SortLocations != null)
            {
                foreach (SortPointLocation item in SortLocations)
                {
                    lb_right.Items.Add(item.Name);
                    lb_right.Items.Add("等待: " + item.Internal + "毫秒");
                }
            }
            else
            {
                SortLocations = new List<SortPointLocation>();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                int i = lb_left.SelectedIndex;
                if (i > -1 && i < Locations.Count)
                {
                    int sleep = int.Parse(tb_internal.Text);
                    SortLocations.Add(new SortPointLocation(Locations[i]) { Internal = sleep });
                    lb_right.Items.Add(Locations[i].Name);
                    lb_right.Items.Add("等待: " + sleep + "毫秒");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            int i = lb_right.SelectedIndex / 2;
            if (i > -1 && i < SortLocations.Count)
            {
                SortLocations.RemoveAt(i);
                lb_right.Items.RemoveAt(i * 2);
                lb_right.Items.RemoveAt(i * 2);
            }
        }
    }

    public class SortPointLocation : PointLocation
    {
        public int Internal { get; set; }
        public SortPointLocation() { }
        public SortPointLocation(PointLocation location)
        {
            Name = location.Name;
            Point = location.Point;
            Turn = location.Turn;
        }
    }
}
