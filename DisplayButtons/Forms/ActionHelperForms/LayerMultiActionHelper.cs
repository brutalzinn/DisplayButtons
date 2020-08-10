using ButtonDeck.Backend.Objects;
using ButtonDeck.Backend.Objects.Implementation;
using ButtonDeck.Backend.Objects.Implementation.DeckActions.General;
using ButtonDeck.Backend.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ButtonDeck.Backend.Objects.AbstractDeckAction;


namespace ButtonDeck.Forms.ActionHelperForms
{
    public partial class  LayerMultiActionHelper : TemplateForm
    {
      
        private LayerMultiActionHelper _modifiableAction;
        private static string scripter_form;
        public static int global_index;
        public LayerMultiActionHelper ModifiableAction
        {
            get { return _modifiableAction; }
            set
            {
                _modifiableAction = value;
                //var exec = GetExecutable(value.ToExecute);
                //  textBox1.Text = exec;
                //  int tamanho_2 = value.ToExecute.Substring(exec.Length).Length;
                //   textBox2.Text = value.ToExecute.Substring(exec.Length);

            

                }





            }
        public static IEnumerable<AbstractDeckAction> items = ReflectiveEnumerator.GetEnumerableOfType<AbstractDeckAction>();
        public ArrayList list_multiple_actions { get; set; } = new ArrayList();

        public List<AbstractDeckAction> list_actions { get; set; } = new List<AbstractDeckAction>();

        public LayerMultiActionHelper()
        {
            InitializeComponent();

        }
        public string scripter
        {
            get { return scripter_form; }
            set
            {
                scripter_form = value;


            }
        }

        private void CloseWithResult(DialogResult result)
        {
          

            DialogResult = result;
            Close();
        }
        private void ModernButton2_Click(object sender, EventArgs e)
        {
       
            CloseWithResult(DialogResult.OK);
        }

        private void ModernButton3_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.Cancel);
        }

        private void LayerMultiActionHelper_Load(object sender, EventArgs e)
        {
 GenerateActions(listBox3);
           // listBox2.DrawItem += new DrawItemEventHandler(ListBox2_DrawItem);
            //    listb.OwnerDraw = true;
       GenerateSidebar(listBox1);

            foreach (var item in list_actions)
            {

                listBox2.Items.Add(item.GetActionName());
    }
           
            

        }
        private void GenerateActions(Control parent)
        
            {
                Padding categoryPadding = new Padding(5, 0, 0, 0);
                Font categoryFont = new Font(parent.Font.FontFamily, 13, FontStyle.Bold);
                Padding itemPadding = new Padding(25, 0, 0, 0);
                Font itemFont = new Font(parent.Font.FontFamily, 12);

                var items = ReflectiveEnumerator.GetEnumerableOfType<AbstractDeckAction>();

                List<Control> toAdd = new List<Control>();




                foreach (DeckActionCategory enumItem in Enum.GetValues(typeof(DeckActionCategory)))
                {
                    var enumItems = items.Where(i => i.GetActionCategory() == enumItem && i.IsTool() == true);
                    if (enumItems.Any())
                    {
                        toAdd.Add(new Label()
                        {
                            Padding = categoryPadding,
                            TextAlign = ContentAlignment.MiddleLeft,
                            Font = categoryFont,
                            Dock = DockStyle.Top,
                            Text = enumItem.ToString(),
                            Tag = "header",
                            Height = TextRenderer.MeasureText(enumItem.ToString(), categoryFont).Height
                        });


                        foreach (var i2 in enumItems)
                        {
                            Label item = new Label()
                            {
                                Padding = itemPadding,
                                TextAlign = ContentAlignment.MiddleLeft,
                                Font = itemFont,
                                Dock = DockStyle.Top,
                                Text = i2.GetActionName(),
                                Height = TextRenderer.MeasureText(i2.GetActionName(), itemFont).Height,
                                Tag = i2,

                            };
                            //    Debug.WriteLine("TAG VINDO: " + i2);
                            item.MouseDown += (s, ee) => {
                                if (item.Tag is AbstractDeckAction act)
                                {

                                    listBox2.Items.Add(i2.GetActionName());
                                    list_actions.Add(i2);


                                    //listBox2.Refresh();
                                    //   item.DoDragDrop(new DeckActionHelper(act), DragDropEffects.Copy);

                                }
                            };
                            toAdd.Add(item);
                        }
                    }
                }
                toAdd.AsEnumerable().Reverse().All(m => {
                    parent.Controls.Add(m);
                    return true;
                });



            }
            private void GenerateSidebar(Control parent)
        {
            Padding categoryPadding = new Padding(5, 0, 0, 0);
            Font categoryFont = new Font(parent.Font.FontFamily, 13, FontStyle.Bold);
            Padding itemPadding = new Padding(25, 0, 0, 0);
            Font itemFont = new Font(parent.Font.FontFamily, 12);

            var items = ReflectiveEnumerator.GetEnumerableOfType<AbstractDeckAction>();

            List<Control> toAdd = new List<Control>();




            foreach (DeckActionCategory enumItem in Enum.GetValues(typeof(DeckActionCategory)))
            {
                var enumItems = items.Where(i => i.GetActionCategory() == enumItem && i.IsTool() == false && i.IsPlugin() == false || i.IsPlugin() == true);
                if (enumItems.Any())
                {
                    toAdd.Add(new Label()
                    {
                        Padding = categoryPadding,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Font = categoryFont,
                        Dock = DockStyle.Top,
                        Text = enumItem.ToString(),
                        Tag = "header",
                        Height = TextRenderer.MeasureText(enumItem.ToString(), categoryFont).Height
                    });


                    foreach (var i2 in enumItems)
                    {
                        Label item = new Label()
                        {
                            Padding = itemPadding,
                            TextAlign = ContentAlignment.MiddleLeft,
                            Font = itemFont,
                            Dock = DockStyle.Top,
                            Text = i2.GetActionName(),
                            Height = TextRenderer.MeasureText(i2.GetActionName(), itemFont).Height,
                            Tag = i2,

                        };
                        //    Debug.WriteLine("TAG VINDO: " + i2);
                        item.MouseDown += (s, ee) => {
                            if (item.Tag is AbstractDeckAction act)
                            {
                    
                                listBox2.Items.Add(i2.GetActionName()) ;
                                list_actions.Add(i2);
   

                                //listBox2.Refresh();
                             //   item.DoDragDrop(new DeckActionHelper(act), DragDropEffects.Copy);

                            }
                        };
                        toAdd.Add(item);
                    }
                }
            }
            toAdd.AsEnumerable().Reverse().All(m => {
                parent.Controls.Add(m);
                return true;
            });
        }


        
        private void ListBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
          
      
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
          

            // Find the string in ListBox2.
            if(listBox2.SelectedItem != null)
            {

 global_index = listBox2.SelectedIndex;
                label3.Text = global_index.ToString();
          //  Debug.WriteLine("INDEX COLORED: " + global_index);

            }
        
           
        }

        private void ImageModernButton4_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("INDEX SELECIONADO: " + global_index);
         
            var c = list_actions.ElementAt(global_index); // 4th
        
          
          
                MethodInfo helperMethod = c.GetType().GetMethod("ToExecuteHelper");
                if (helperMethod != null)
                {

                    helperMethod.Invoke(c, new object[] { });

                }
          


            


              //  LoadProperties(c);
        
        }

        private void ImageModernButton3_Click(object sender, EventArgs e)
        {

        }
    

        private void Button1_Click(object sender, EventArgs e)
        {
            //   var c = list_actions.ElementAt(global_index); // 4th


            int newIndex = global_index - 1;

            object selectItem = listBox2.SelectedItem;
            var selectItem_list = list_actions.ElementAt(global_index);
            listBox2.Items.Remove(selectItem);
            list_actions.RemoveAt(global_index);
            listBox2.Items.Insert(newIndex, selectItem);
            list_actions.Insert(newIndex, selectItem_list);


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int newIndex = global_index + 1;

            object selectItem = listBox2.SelectedItem;
            var selectItem_list = list_actions.ElementAt(global_index);
            listBox2.Items.Remove(selectItem);
            list_actions.RemoveAt(global_index);
            listBox2.Items.Insert(newIndex, selectItem);
            list_actions.Insert(newIndex, selectItem_list);
        }
    }
    }
    

