using DisplayButtons.Backend.Utils;
using DisplayButtons.Forms;
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

namespace DisplayButtons.Backend.Objects.Implementation.DeckActions.General
{

 
    [MoonSharpUserData]
   
    public class PluginLuaGenerator : AbstractDeckAction
    {



        [XmlIgnore]
        public string ToScript { get; set; } = "";


        [ActionPropertyPluginsScriptEntryPoint]

        public  string ScriptEntryPoint { get; set; } = "";
        [ActionPropertyPluginsScriptEntryPoint]
        
        public string ScriptNamePoint { get; set; } = "";
        public string script_to_form { get; set; } = "";
        public string name_img { get; set; } = "";
       public string DeckActionCategory_string { get; set; } = "Deck";
        public string name { get; set; } = "";
        [ActionPropertyInclude]
        [ActionPropertyDescription("To Execute")]
        public string ToExecute { get; set; } = "";

        [XmlElement("ToControls")]

        public SerializableDictionary<string, string> ToControls { get; set; } = new SerializableDictionary<string, string>();

        public  string dictionary_name { get; set; } = "";
        [XmlIgnore]
        public dynamic form;
        [XmlIgnore]
        private static PluginLuaGenerator instance;
        [XmlIgnore]
        public static PluginLuaGenerator Instance
        {
            get
            {

                return instance;
            }
        }


        [MoonSharpUserData]


        public class formcontrol
        {

            private PluginLuaGenerator instance;
            public formcontrol()
            {



            }
            public formcontrol(PluginLuaGenerator param)
            {

                instance = param;
            }
            public  string name_img { get; set; } = "";

            public  void setButtonImg(string nameimg)
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
            private  Dictionary<string, string> users = new Dictionary<string, string>();
            public  string dictionary_name { get; set; } = "";


            public  void Set(string key, string value)
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

            public  string Get(string key)
            {
                string result = null;

                if (users.ContainsKey(key))
                {
                    result = users[key];
                }

                return result;
            }

            public  void AddKeyvalue(string key, string value)
            {
                if (instance.ToControls.ContainsKey(key))
                {
                    instance.ToControls[key] = value;
                }
                else
                {
                    instance.ToControls.Add(key, value);
                }
            }

            public  void setFormControl(string name, string value, string type, int x, int y, int tam_x, int tam_y)
            {

              if(instance.form != null)
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
                            instance.form.Controls.Add(txt);

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
                            instance.form.Controls.Add(ritchtext);
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
                            instance.form.Controls.Add(labeled);

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
            public  string GetKey(string key)
            {
                string result = null;
                try
                {



                    if (instance.ToControls.ContainsKey(key))
                    {
                        result = instance.ToControls[key];
                    }
                    else
                    {

                        result = "";
                    }
                }
                catch (Exception e)
                {

                    //    result = "";
                    Debug.WriteLine("NULL");
                }
                return result;
            }
            public  string getFormControl(string name)
            {


                string result = "";
                foreach (Control c in instance.form.Controls)
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



        public override bool IsPlugin()
        {
           // ScribeBot.Scripter.Execute(script,true);


           // ScribeBot.Scripter.Environment.Globals["name_space"] = name_space;
            return true;
        }
   
   public override void SetConfigs(string script_param)
        {




            //  ToScript = script_param;

            //  ToScript = File.ReadAllText(path);
            //      ToScript = File.ReadAllText(ScriptEntryPoint);
            // ToScript = File.ReadAllText(path);


        }

        public void ToExecuteHelper()
        {

            //   var originalToExec = new String(ToScript.ToCharArray());
            form = Activator.CreateInstance(FindType("DisplayButtons.Forms.ActionHelperForms.ActionPlugin")) as Form;

            ScribeBot.Scripter.Environment.Globals["formdesign"] = new formcontrol(this);







            Debug.WriteLine("VINDO NYULDO:" + ToScript);


            ScribeBot.Scripter.Execute(ToScript, true);
            try
            {
                object formmenu_object = ScribeBot.Scripter.Environment.Globals["FormMenu"];
                ScribeBot.Scripter.Environment.Call(formmenu_object);
                if (form.ShowDialog() == DialogResult.OK)
                {

                    //  ScribeBot.Scripter.Execute(script, true);

                    object functioncall = ScribeBot.Scripter.Environment.Globals["MenuOk"];
                    ScribeBot.Scripter.Environment.Call(functioncall);
                    // ToExecute = form.ModifiableAction.toExecute;  
                    // oExecute = form.ModifiableAction;
                }
                else
                {



                    object function_cancel = ScribeBot.Scripter.Environment.Globals["MenuCancel"];

                    ScribeBot.Scripter.Environment.Call(function_cancel);
                    //    ToScript = originalToExec;
                }
            }
            catch(Exception ii){}
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
            //  var oldSceneName = new String(ToScript.ToCharArray());



            return new PluginLuaGenerator();
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

            //        ScribeBot.Scripter.Environment.Globals["list"] = typeof(LIST);

            ScribeBot.Scripter.Environment.Globals["formdesign"] = new formcontrol(this);

            ScribeBot.Scripter.Execute(ToScript, true);
            //  Debug.WriteLine(script);
            object functiontable = ScribeBot.Scripter.Environment.Globals["ButtonDown"];
          ScribeBot.Scripter.Environment.Call(functiontable);

           
     
           //  ScribeBot.Scripter.Environment.Call(DynValue.NewString("buttondown"));
         


        }
        public PluginLuaGenerator() { }

        // The following constructor has parameters for two of the three
        // properties.
        public PluginLuaGenerator(string name_param, string script_param)
        {

       //     ToScript = script_param;
        }
        public override void OnButtonUp(DeckDevice deckDevice)
        {
            //    ScribeBot.Scripter.Environment.Globals["list"] = typeof(LIST);
            ScribeBot.Scripter.Environment.Globals["formdesign"] = new formcontrol(this);

            ScribeBot.Scripter.Execute(ToScript, true);
            //    DynValue luaFactFunction = ScribeBot.Scripter.Environment.Globals.Get("ButtonDown");

            object functiontable = ScribeBot.Scripter.Environment.Globals["ButtonUP"];
          ScribeBot.Scripter.Environment.Call(functiontable);
            //    DynValue res = ScribeBot.Scripter.Environment.Call(luaFactFunction);
            //   ScribeBot.Scripter.Execute(res.tos);
 

        }
    }
}
