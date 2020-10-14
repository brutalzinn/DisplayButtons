
using DisplayButtons.Forms;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayButtons.Engine.Wrappers
{
    [MoonSharpUserData]
    class PluginWrapper
    {

        public static Dictionary<string, string> myLists =
        new Dictionary<string, string>();
        public void  setPlayerString(string key, string value)
        {
            myLists.Add(key, value);

        }
        public void setFormControl(string name, string value, int posX, int posY, string type)
        {

            switch (type)
            {
                case "textbox":
                    Forms.ActionHelperForms.ActionPlugin teste = new Forms.ActionHelperForms.ActionPlugin();
                    System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();

                    txt.Text = value;
                    teste.Controls.Add(txt);


                        break;



            }


        }
           
            public static string getPlayerString(string key, bool keyorpair)
        {
            try
            {

           
            foreach (KeyValuePair<string, string> keyparrr in myLists)
            {
              //  Console.WriteLine("Key: {0}, Value: {1}",
            
              if(keyorpair == true)
                {
  if(key == keyparrr.Key)
                {


                  return  keyparrr.Value;

                }


                }
                else
                {
                    if (key == keyparrr.Value)
                    {


                        return keyparrr.Key;

                    }


                }
            }
            }



            catch (Exception)
            {

                return "";
            }
            return "";
        }
    }
    [MoonSharpUserData]
    public class formcontrol
    {
        public string name_img { get; set; } = "";

        public void setButtonImg(string nameimg)
        {

            if (Directory.Exists(Application.StartupPath + @"\Data\imgs") == false)
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Data\imgs");
            }
            string path = Application.StartupPath + @"\Data\imgs\" + nameimg;
            if (File.Exists(path))
            {

                name_img = nameimg;
                MainForm.Instance.UpdatePluginImg();
            }



        }
        private Dictionary<string, string> users = new Dictionary<string, string>();
        public string dictionary_name { get; set; } = "";


        public void Set(string key, string value)
        {
            if (users.ContainsKey(key))
            {
                users[key] = value;
            }
            else
            {
                users.Add(key, value);
            }
        }

        public string Get(string key)
        {
            string result = null;

            if (users.ContainsKey(key))
            {
                result = users[key];
            }

            return result;
        }

        public void AddControlToUser(string key, string value)
        {
            if (PluginLuaGenerator.Instance.ToControls.ContainsKey(key))
            {
                PluginLuaGenerator.Instance.ToControls[key] = value;
            }
            else
            {
                PluginLuaGenerator.Instance.ToControls.Add(key, value);
            }
        }

        public void setFormControl(string name, string value, string type, int x, int y, int tam_x, int tam_y)
        {


            switch (type)
            {
                case "textbox":
                    TextBox txt = new TextBox();
                    if (!String.IsNullOrEmpty(value))
                    {
                        txt.Text = value;

                    }
                    txt.Name = name;
                    txt.Width = tam_x;
                    txt.Height = tam_y;
                    txt.Location = new System.Drawing.Point(x, y);

                    //       PluginLuaGenerator.form.Controls.Add(txt);
                    //  AddControlToUser(name,txt.Text);
                    PluginLuaGenerator.Instance.form.Controls.Add(txt);

                    break;
                case "ritchtextbox":
                    RichTextBox ritchtext = new RichTextBox();
                    if (!String.IsNullOrEmpty(value))
                    {
                        ritchtext.Text = value;

                    }

                    ritchtext.Name = name;
                    ritchtext.Width = tam_x;
                    ritchtext.Height = tam_y;
                    ritchtext.Location = new System.Drawing.Point(x, y);

                    //       PluginLuaGenerator.form.Controls.Add(txt);
                    PluginLuaGenerator.Instance.form.Controls.Add(ritchtext);
                    //   AddControlToUser(name, value);


                    break;
                case "label":
                    Label labeled = new Label();
                    labeled.Text = value;
                    labeled.Name = name;
                    //       PluginLuaGenerator.form.Controls.Add(txt);
                    labeled.Width = tam_x;
                    labeled.Height = tam_y;
                    labeled.Location = new System.Drawing.Point(x, y);
                    //     AddControlToUser(labeled.Name, labeled.Text);
                    PluginLuaGenerator.Instance.form.Controls.Add(labeled);

                    break;
                case "file":
                    TextBox file_text = new TextBox();
                    OpenFileDialog file_search = new OpenFileDialog();
                    Button file_button = new Button();
                    TableLayoutPanel file_table_layout = new TableLayoutPanel();
                    file_text.Name = name;
                    //   file_text.Dock = DockStyle.Left;
                    file_button.Name = name + "_button";
                    //     file_button.Anchor = AnchorStyles.Left;
                    //  file_button.Dock = DockStyle.Right;
                    file_table_layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 2f));
                    file_table_layout.RowStyles.Add(new RowStyle(SizeType.AutoSize, 2f));
                    file_button.Click += (s, ee) =>
                    {
                        file_search.ShowDialog();

                        file_text.Text = file_search.FileName;

                    };
                    //       PluginLuaGenerator.form.Controls.Add(txt);

                    file_text.Width = tam_x;
                    file_table_layout.Width = tam_x + 90;
                    //     file_text.Height = tam_y;
                    file_table_layout.Location = new System.Drawing.Point(x, y);
                    //     form.Controls.Add(file_search);

                    file_table_layout.Controls.Add(file_text, 0, 0);
                    file_table_layout.Controls.Add(file_button, 1, 0);

                    // form.Controls.Add(file_table_layout);

                    //ToControls.Add(file_table_layout);

                    break;


            }


        }
        public string GetControlToUser(string key)
        {
            string result = null;
            try
            {



                if (PluginLuaGenerator.Instance.ToControls.ContainsKey(key))
                {
                    result = PluginLuaGenerator.Instance.ToControls[key];
                }
                else
                {

                    result = "";
                }
            }
            catch (Exception )
            {

                //    result = "";
                Debug.WriteLine("NULL");
            }
            return result;
        }
        public string getFormControl(string name)
        {


            string result = "";
            foreach (Control c in PluginLuaGenerator.Instance.form.Controls)
            {
                if (c.Name == name)
                {
                    result = c.Text;

                }
                else
                {

                    result = "";
                }
            }
            return result;

        }

    }

}
