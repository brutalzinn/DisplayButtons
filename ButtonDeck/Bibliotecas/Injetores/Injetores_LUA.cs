using ButtonDeck.Backend.Objects.Implementation.DeckActions.General;
using ButtonDeck.Forms;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
namespace ButtonDeck.Bibliotecas.Injetores
{
     class Global_lua{
    public string script_to_form { get; set; } = "";
        public dynamic form;
        public string name_space { get; set; } = "";



    public static string name_img { get; set; } = "";
    public string DeckActionCategory_string { get; set; } = "Deck";

}
    [MoonSharpUserData]
    class Formvoid
    {
    
        public static void setButtonImg(string nameimg)
        {

            if (Directory.Exists(Application.StartupPath + @"\Data\imgs") == false)
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Data\imgs");
            }
            string path = Application.StartupPath + @"\Data\imgs\" + nameimg;
            if (File.Exists(path))
            {

                Global_lua.name_img = nameimg;
                MainForm.Instance.UpdatePluginImg();
            }



        }



    }


    [MoonSharpUserData]

    public class formcontrol
    {



        private static Dictionary<string, string> users = new Dictionary<string, string>();
        public static string dictionary_name { get; set; } = "";


        public static void Set(string key, string value)
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

        public static string Get(string key)
        {
            string result = null;

            if (users.ContainsKey(key))
            {
                result = users[key];
            }

            return result;
        }

        public static void AddControlToUser(string key, string value)
        {
            if (FolderAddAction.Instance.ToControls.ContainsKey(key))
            {
                FolderAddAction.Instance.ToControls[key] = value;
            }
            else
            {
                FolderAddAction.Instance.ToControls.Add(key, value);
            }
        }
        public static void setFormControl(string name, string value, string type, int x, int y, int tam_x, int tam_y)
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

                    //       FolderAddAction.form.Controls.Add(txt);
                    AddControlToUser(name, txt.Text);
                    Global_lua.form.Controls.Add(txt);

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

                    //       FolderAddAction.form.Controls.Add(txt);
                    Global_lua.form.Controls.Add(ritchtext);
                    AddControlToUser(name, value);


                    break;
                case "label":
                    Label labeled = new Label();
                    labeled.Text = value;
                    labeled.Name = name;
                    //       FolderAddAction.form.Controls.Add(txt);
                    labeled.Width = tam_x;
                    labeled.Height = tam_y;
                    labeled.Location = new System.Drawing.Point(x, y);
                    AddControlToUser(labeled.Name, labeled.Text);
                    Global_lua.form.Controls.Add(labeled);

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
                    //       FolderAddAction.form.Controls.Add(txt);

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
        public static string GetControlToUser(string key)
        {
            string result = null;
            try
            {



                if (FolderAddAction.Instance.ToControls.ContainsKey(key))
                {
                    result = FolderAddAction.Instance.ToControls[key];
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine("NULL");
            }
            return result;
        }
        public static string getFormControl(string name, string type)
        {


            string result = "";
            foreach (Control c in Global_lua.form.Controls)
            {
                if (c.Name == name)
                {
                    result = c.Text;

                }
            }
            return result;

        }

    }
}
