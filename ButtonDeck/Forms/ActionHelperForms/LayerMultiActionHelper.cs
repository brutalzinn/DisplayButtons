using ButtonDeck.Backend.Objects;
using ButtonDeck.Backend.Objects.Implementation;
using ButtonDeck.Backend.Objects.Implementation.DeckActions.General;
using ButtonDeck.Backend.Utils;
using System;
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
    public partial class LayerMultiActionHelper : TemplateForm
    {
      
        private LayerMultiActionHelper _modifiableAction;
        private static string scripter_form;
        public static int global_index = 0;
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

            listBox2.DrawItem += new DrawItemEventHandler(ListBox2_DrawItem);
            //    listb.OwnerDraw = true;
            GenerateSidebar(listBox1);
            foreach( var item in list_actions)
            {
         Debug.WriteLine("PERCORRENDO ITEM AO LOAD " + item.GetActionName());
          listBox2.Items.Add(item.GetActionName());

            }

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
                var enumItems = items.Where(i => i.GetActionCategory() == enumItem && i.IsPlugin() == false || i.IsPlugin() == true);
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
                               



                                listBox2.Refresh();
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


        private void LoadProperties(DynamicDeckItem item)
        {


            var props = item.DeckAction.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(ActionPropertyIncludeAttribute)));
            foreach (var prop in props)
            {
              //  bool shouldUpdateIcon = Attribute.IsDefined(prop, typeof(ActionPropertyUpdateImageOnChangedAttribute));
                MethodInfo helperMethod = item.DeckAction.GetType().GetMethod(prop.Name + "Helper");
                if (helperMethod != null)
                {
              

                   helperMethod.Invoke(item.DeckAction, new object[] { });

                  
                }
             
            }

           
        }
        private void ListBox2_DrawItem(object sender, DrawItemEventArgs e)
        {

            if (e.Index < 0) return;
            //if the item state is selected them change the back color 
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          Color.Yellow);//Choose the color

            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Draw the current item text
            e.Graphics.DrawString(listBox2.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            label3.Text = listBox2.Items[e.Index].ToString();
            global_index = e.Index;
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ImageModernButton4_Click(object sender, EventArgs e)
        {
            var c = list_actions.ElementAt(global_index); // 4th
            var props = c.GetType().GetProperties().Where(
                   prop => Attribute.IsDefined(prop, typeof(ActionPropertyIncludeAttribute)));
            foreach (var prop in props)
            {
               
                MethodInfo helperMethod = c.GetType().GetMethod(prop.Name + "Helper");
                if (helperMethod != null)
                {

                    helperMethod.Invoke(c, new object[] { });

                }
          


            }


              //  LoadProperties(c);
        
        }
        }
    }
    

