using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook
{
    public partial class MaterialTabController : Form
    {
        public MaterialTabController()
        {
            InitializeComponent();
            circularPictureBox1.AllowDrop = true;
            txtType.SelectedIndex = 0;
            txtTipe.SelectedIndex = 0;
        }

        private void circularPictureBox1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog ofd =new OpenFileDialog();
            ofd.Filter = "JPG (*.jpg,*.jpeg)|*.jpg;*.jpeg";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = ofd.FileName;
                circularPictureBox1.ImageLocation = fileToOpen;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Registered");
        }

        private void circularPictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            foreach (var pic in ((string[])e.Data.GetData(DataFormats.FileDrop)))
            {
                Image img = Image.FromFile(pic);
                circularPictureBox1.Image = img;
            }
        }

        private void circularPictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {

        }
    }
}
