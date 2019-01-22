using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AddressBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PersonPic.AllowDrop = true;
            //this.BackColor = Color.Black;
            //this.TransparencyKey = Color.Black;
        }
        List<Person> people = new List<Person>();

        private void PersonPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd =new OpenFileDialog();
            ofd.Filter = "JPG (*.jpg,*.jpeg)|*.jpg;*.jpeg";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var fileToOpen = ofd.FileName;
                MessageBox.Show(fileToOpen);
                PersonPic.ImageLocation = fileToOpen;
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            //MaterialTabController m = new MaterialTabController();
            //this.Hide();
            //m.Show();
            people[listPeople.SelectedItems[0].Index].Name = txtName.Text;
            people[listPeople.SelectedItems[0].Index].Phone = txtPhone.Text;
            people[listPeople.SelectedItems[0].Index].Email = txtEmail.Text;
            people[listPeople.SelectedItems[0].Index].Street = txtStreet.Text;
            people[listPeople.SelectedItems[0].Index].DateOfBirth = txtDob.Value;
            people[listPeople.SelectedItems[0].Index].AdditionalInfo = txtAdditionalNotes.Text;
            people[listPeople.SelectedItems[0].Index].Picture = PersonPic.ImageLocation;
            listPeople.SelectedItems[0].Text = txtName.Text;
            clear();
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            Person pp = new Person();
            p.Name = txtName.Text.Trim();
            p.Phone = txtPhone.Text.Trim();
            p.Email = txtEmail.Text.Trim();
            p.Street = txtStreet.Text.Trim();
            p.DateOfBirth = txtDob.Value;
            p.AdditionalInfo = txtAdditionalNotes.Text.Trim();
            p.Picture = PersonPic.ImageLocation;
            people.Add(p);
            listPeople.Items.Add(p.Name);
            clear();
        }

        private void clear()
        {
            txtName.Text = "";
            txtAdditionalNotes.Text = "";
            txtPhone.Text = "";
            txtDob.Value = DateTime.Now;
            txtEmail.Text = "";
            txtStreet.Text = "";
            PersonPic.Image = AddressBook.Properties.Resources.images;
            btnSaveChanges.Enabled = false;
            btnRemove.Enabled = false;
            btnAddContact.Enabled = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
           Remove();
           clear();
        }

        private void PersonPic_DragDrop(object sender, DragEventArgs e)
        {
            foreach (var pic in ((string[])e.Data.GetData(DataFormats.FileDrop)))
            {
                Image img = Image.FromFile(pic);
                PersonPic.Image = img;
            }
            //MessageBox.Show("Droped");
        }

        private void PersonPic_DragEnter(object sender, DragEventArgs e)
        {
           // MessageBox.Show("DragEnter");
            e.Effect = DragDropEffects.Copy;
        }

        public static Timer t;

        public void T_Tick(object sender, EventArgs e)
        {
            t.Stop();
            this.Hide();
            exit();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            t = new Timer();
            t.Interval = 2000;
            t.Start();
            this.Hide();
            SplashScreen s = new SplashScreen();
            s.Show();
            t.Tick += T_Tick;
        }

        void exit()
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            notifyIcon1.ShowBalloonTip(1000,"AddressBook","An Rj Porosh Creation Still Running in Background",ToolTipIcon.Info);

        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Show();
        }
        Person p = new Person();
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        XmlDocument xDoc =new XmlDocument();
        
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(path + "\\AddressBook - Porosh"))
            {
                Directory.CreateDirectory(path + "\\AddressBook - Porosh");
                File.Create(path + "\\AddressBook - Porosh\\settings.xml");
                XmlTextWriter xW =new XmlTextWriter(path + "\\AddressBook - Porosh\\settings.xml",Encoding.UTF8);
                xW.WriteStartElement("People");
                xDoc.CreateTextNode("People");
                xW.WriteEndElement();
                xW.Close();
            }

            if (!File.Exists(path + "\\AddressBook - Porosh\\settings.xml"))
            {
             
                XmlTextWriter xW =new XmlTextWriter(path + "\\AddressBook - Porosh\\settings.xml",Encoding.UTF8);
                xW.WriteStartElement("People");
                xDoc.CreateTextNode("People");
                xW.WriteEndElement();
                xW.Close();
            }
            xDoc.Load(path+ "\\AddressBook - Porosh\\settings.xml");
            XmlNodeList xNodes = xDoc.SelectNodes("/People/Person");
            //if (xDoc.ParentNode != null)
                //if (xNodes != null)
                    foreach (XmlNode xNode in xNodes)
                    {
                        Person p = new Person();
                        p.Name = xNode.SelectSingleNode("Name")?.InnerText;
                        p.Email = xNode.SelectSingleNode("Email")?.InnerText;
                        p.Phone = xNode.SelectSingleNode("Phone")?.InnerText;
                        p.Street = xNode.SelectSingleNode("Street")?.InnerText;
                        p.DateOfBirth =
                            DateTime.FromFileTime(Convert.ToInt64(xNode.SelectSingleNode("DateOfBirth")?.InnerText));
                        p.AdditionalInfo = xNode.SelectSingleNode("AdditionalInfo")?.InnerText;
                        p.Picture = xNode.SelectSingleNode("PersonPic")?.InnerText;
                        people.Add(p);
                        listPeople.Items.Add(p.Name);
                    }

            btnSaveChanges.Enabled = false;
            btnRemove.Enabled = false;
        }

        void Remove()
        {
                //if (listPeople.SelectedItems.Count<0)
                //    return;
                //else
                {
                    people.RemoveAt(listPeople.SelectedItems[0].Index);
                    listPeople.Items.Remove(listPeople.SelectedItems[0]);
                    //xDoc.ParentNode?.RemoveAll();
                    //xDoc.SelectSingleNode(listPeople.SelectedItems[0].Name);
                    clear();
                
                 }
            //catch (Exception e)
            //{
            //    MessageBox.Show("Test "+e);
            //}
        }
        private void listPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
            edit();
        }

        void edit()
        {
            if (listPeople.SelectedItems.Count <= 0)
                return;
            txtName.Text = people[listPeople.SelectedItems[0].Index].Name;
            txtPhone.Text = people[listPeople.SelectedItems[0].Index].Phone;
            txtEmail.Text = people[listPeople.SelectedItems[0].Index].Email;
            txtStreet.Text = people[listPeople.SelectedItems[0].Index].Street;
            txtDob.Value = people[listPeople.SelectedItems[0].Index].DateOfBirth;
            txtAdditionalNotes.Text = people[listPeople.SelectedItems[0].Index].AdditionalInfo;
            PersonPic.ImageLocation = people[listPeople.SelectedItems[0].Index].Picture;
            btnAddContact.Enabled = false;
            btnSaveChanges.Enabled = true;
            btnRemove.Enabled = true;
            //xDoc.ParentNode?.RemoveAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove();
            clear();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edit();
            clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            xDoc.ParentNode?.RemoveAll();
            XmlTextWriter xW =new XmlTextWriter(path + "\\AddressBook - Porosh\\settings.xml",Encoding.UTF8);
            xW.WriteStartElement("People");
            xDoc.CreateTextNode("People");
            xW.WriteEndElement();
            xW.Close();
            //xDoc.RemoveAll();
            xDoc.Load(path+"\\AddressBook - Porosh\\settings.xml");

            foreach (Person p in people)
            {
                XmlNode xTop = xDoc.CreateElement("Person");
                XmlNode xName = xDoc.CreateElement("Name");
                XmlNode xPhone = xDoc.CreateElement("Phone");
                XmlNode xEmail = xDoc.CreateElement("Email");
                XmlNode xStreet = xDoc.CreateElement("Street");
                XmlNode xDob = xDoc.CreateElement("DateOfBirth");
                XmlNode xAdditionalInfo = xDoc.CreateElement("AdditionalInfo");
                XmlNode xPic = xDoc.CreateElement("PersonPic");
                xName.InnerText = p.Name;
                xPhone.InnerText = p.Phone;
                xEmail.InnerText = p.Email;
                xStreet.InnerText = p.Street;
                xDob.InnerText = p.DateOfBirth.ToFileTime().ToString();
                xAdditionalInfo.InnerText = p.AdditionalInfo;
                xPic.InnerText = p.Picture;
                xTop.AppendChild(xName);
                xTop.AppendChild(xPhone);
                xTop.AppendChild(xEmail);
                xTop.AppendChild(xStreet);
                xTop.AppendChild(xDob);
                xTop.AppendChild(xPic);
                xTop.AppendChild(xAdditionalInfo);
                xDoc.DocumentElement?.AppendChild(xTop);
            }
            xDoc.Save(path + "\\AddressBook - Porosh\\settings.xml");
        }
    }
        //private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    t = new Timer();
        //    t.Interval = 2000;
        //    t.Start();
        //    t.Tick += T_Tick;  
        //}
        //public void T_Tick(object sender, EventArgs e)
        //{
        //    t.Stop();
        //    Application.Exit();
        //}

        //private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    t = new Timer();
        //    t.Interval = 2000;
        //    t.Start();
        //    t.Tick += T_Tick;  
        //}
    }

    public class Person
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Picture { get; set; }
    }
