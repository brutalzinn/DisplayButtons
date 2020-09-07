using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms
{
    public partial class About : TemplateForm
    {
        public About()
        {
            InitializeComponent();
            label1.Text = Texts.rm.GetString("APPLICATIONNAME", Texts.cultereinfo);
            label2.Text = Texts.rm.GetString("ABOUTINFOAPPVERSIONLABEL", Texts.cultereinfo) + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = Texts.rm.GetString("ABOUTNAMETEXT", Texts.cultereinfo);
            LibraryHelper instance = new LibraryHelper();
            instance.prepareLibraryList();
            foreach (var item in instance.toAdd)
            {

                richTextBox1.AppendText(item.Name + " " + item.Github_repo + Environment.NewLine);
            }

        }

        private void About_Load(object sender, EventArgs e)
        {

        }
    }
}
