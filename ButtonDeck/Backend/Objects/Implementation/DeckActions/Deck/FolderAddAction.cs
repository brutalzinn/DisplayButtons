using ButtonDeck.Backend.Utils;
using ButtonDeck.Forms;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ButtonDeck.Backend.Objects.Implementation.DeckActions.General
{
    [DataContract]

    public class FolderAddAction : AbstractDeckAction
    {





<<<<<<< HEAD:ButtonDeck/Backend/Objects/Implementation/DeckActions/Deck/PluginListGenerator.cs
        public static  string script  = "";
=======



        //     public static string script { get; set; } = "";
        public static string script { get; set; } = "";
>>>>>>> parent of e2fe468... coisas críticas:ButtonDeck/Backend/Objects/Implementation/DeckActions/Deck/FolderAddAction.cs
        public string script_to_form { get; set; } = "";

        public static string name_space { get; set; } = "";



        public static string name_img { get; set; } = "";
        public static string DeckActionCategory_string { get; set; } = "Deck";
<<<<<<< HEAD:ButtonDeck/Backend/Objects/Implementation/DeckActions/Deck/PluginListGenerator.cs
   
  
        public static string name { get; set; } = "";

=======
        [ActionPropertyInclude]
        [ActionPropertyDescription("name")]
        public static string name { get; set; } = "teste";
        [ActionPropertyInclude]
>>>>>>> parent of e2fe468... coisas críticas:ButtonDeck/Backend/Objects/Implementation/DeckActions/Deck/FolderAddAction.cs
        [ActionPropertyDescription("To Execute")]
        public string ToExecute { get; set; } = "";

        [ActionPropertyInclude]

        public SerializableDictionary<string, string> ToControls { get; set; } = new SerializableDictionary<string, string>();

        public static string dictionary_name { get; set; } = "";
       public static dynamic form;
        [XmlIgnore]
        private static FolderAddAction instance;

        public static FolderAddAction Instance
        {
            get
            {

                return instance;
            }
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
                    
                    name_img = nameimg;
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
            public static void setFormControl(string name, string value, string type, int x ,int y, int tam_x, int tam_y)
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
                        txt.Location = new System.Drawing.Point (x,y);

                        //       FolderAddAction.form.Controls.Add(txt);
                        AddControlToUser(name,txt.Text);
                      form.Controls.Add(txt);

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
                        form.Controls.Add(ritchtext);
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
                        form.Controls.Add(labeled);
                     
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
                        file_table_layout.Controls.Add(file_button, 1 , 0);

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
                foreach (Control c in form.Controls)
                {
                    if (c.Name == name)
                    {
                        result = c.Text;

                    }
                }
                return result;

            }
          
        }
        public override bool IsPlugin()
        {
            ScribeBot.Scripter.Execute(script,true);

            ScribeBot.Scripter.Environment.Globals["formdesignvoid"] = typeof(Formvoid);

           // ScribeBot.Scripter.Environment.Globals["name_space"] = name_space;
            return true;
        }
    
   
        public void ToExecuteHelper()
        {
            instance = this;
            ScribeBot.Scripter.Execute(script, true);

            //          ScribeBot.Scripter.Environment.Globals["list"] = typeof(LIST);
            var originalToExec = new String(ToExecute.ToCharArray());
             form = Activator.CreateInstance(FindType("ButtonDeck.Forms.ActionHelperForms.ActionPlugin")) as Form;
           
            ScribeBot.Scripter.Environment.Globals["formdesign"] = typeof(formcontrol);
            object functiontable = ScribeBot.Scripter.Environment.Globals["form_menu"];
            ScribeBot.Scripter.Environment.Call(functiontable);
            //  var execAction = CloneAction() as FolderAddAction;

            //    execAction.ToExecute = ToExecute;

            //   form.ModifiableAction = execAction;

            form.scripter = script;
            if (form.ShowDialog() == DialogResult.OK)
            {

                ScribeBot.Scripter.Execute(script, true);

                object functioncall = ScribeBot.Scripter.Environment.Globals["menu_ok"];
                ScribeBot.Scripter.Environment.Call(functioncall);
                // ToExecute = form.ModifiableAction.toExecute;  
                // oExecute = form.ModifiableAction;
            }
            else
        {
                ToExecute = originalToExec;
            }
        }
        public override DeckImage GetDefaultItemImage()
        {
        // string image_name = ScribeBot.Scripter.Environment.Globals["set_image"];
         //   DynValue obj = UserData.Create(name_space);
            //  ScribeBot.Scripter.Execute(script, false);
  if(name_img == "")
            {


                return base.GetDefaultItemImage();
            }
            else
            {
             //   ScribeBot.Scripter.Execute(script, true);
                var bitmap = new Bitmap(Application.StartupPath + @"\Data\imgs\" + name_img);
            return new DeckImage(bitmap);

            }
            
        }
        public override AbstractDeckAction CloneAction()
        {
            //  Debug.WriteLine("CHEGOU A CHAMAR O NAMESPACE" + name_space);
            // DynValue obj = UserData.Create(name_space);
            //  ScribeBot.Scripter.Execute(script, false);
            //  ScribeBot.Scripter.Environment.Globals.Set("name_space", obj);
   

            return new FolderAddAction();
        }
      

        public override DeckActionCategory GetActionCategory()
        {

            DeckActionCategory animal = (DeckActionCategory)Enum.Parse(typeof(DeckActionCategory), DeckActionCategory_string);
            return animal;

        }

        public override string GetActionName()
        {

            return name;
        }

        public override void OnButtonDown(DeckDevice deckDevice)
        {
 Debug.WriteLine("CONTINENTE");
    //        ScribeBot.Scripter.Environment.Globals["list"] = typeof(LIST);


            ScribeBot.Scripter.Execute(script,true);
           object functiontable = ScribeBot.Scripter.Environment.Globals["ButtonDown"];
          ScribeBot.Scripter.Environment.Call(functiontable);
            //  ScribeBot.Scripter.Environment.Call(DynValue.NewString("buttondown"));
            
          
        }
      
        public override void OnButtonUp(DeckDevice deckDevice)
        {
        //    ScribeBot.Scripter.Environment.Globals["list"] = typeof(LIST);

            //    ScribeBot.Scripter.Execute(script);
            //    DynValue luaFactFunction = ScribeBot.Scripter.Environment.Globals.Get("ButtonDown");
            ScribeBot.Scripter.Execute(script, true);
            object functiontable = ScribeBot.Scripter.Environment.Globals["ButtonUP"];
            ScribeBot.Scripter.Environment.Call(functiontable);
            //    DynValue res = ScribeBot.Scripter.Environment.Call(luaFactFunction);
            //   ScribeBot.Scripter.Execute(res.tos);
        }
    }
}
