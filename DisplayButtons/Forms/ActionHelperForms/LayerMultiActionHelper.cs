using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Backend.Objects.Implementation.DeckActions.General;
using DisplayButtons.Backend.Utils;
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
using static DisplayButtons.Backend.Objects.AbstractDeckAction;
using static DisplayButtons.Bibliotecas.DeckEvents.FactoryForms;

namespace DisplayButtons.Forms.ActionHelperForms
{
    public partial class LayerMultiActionHelper : TemplateForm
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

           this.Text =  Texts.rm.GetString("LAYERMULTIACTIONTITLE", Texts.cultereinfo);
           label1.Text  = Texts.rm.GetString("LAYERMULTIACTIONACTIONSTITLE", Texts.cultereinfo);
            label2.Text = Texts.rm.GetString("LAYERMULTIACTIONLISTACTIONSTITLE", Texts.cultereinfo);
            label4.Text = Texts.rm.GetString("LAYERMULTIACTIONTOOLSTITLE", Texts.cultereinfo);
            imageModernButton4.Text = Texts.rm.GetString("LAYERMULTIACTIONCONFIGBUTTON", Texts.cultereinfo);
            DelButton.Text = Texts.rm.GetString("LAYERMULTIACTIONDELBUTTON", Texts.cultereinfo);
            DellAllButton.Text = Texts.rm.GetString("LAYERMULTIACTIONDELALLBUTTON", Texts.cultereinfo);
            modernButton2.Text = Texts.rm.GetString("EVENTSYSTEMSAVEBUTTON", Texts.cultereinfo);
            modernButton3.Text = Texts.rm.GetString("EVENTSYSTEMCANCELBUTTON", Texts.cultereinfo);
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
            list_actions.Clear();
            foreach (GlobalAbstractDeckAction item in listBox2.Items)
            {
                list_actions.Add(item.Value);
               
            
            }
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


            foreach (AbstractDeckAction item in list_actions)
            {
                GlobalAbstractDeckAction teste = new GlobalAbstractDeckAction();
                teste.Text = item.GetActionName();
                teste.Value = item;
                listBox2.Items.Add(teste);
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
                        item.MouseDown += (s, ee) =>
                        {
                            if (item.Tag is AbstractDeckAction act)
                            {

                                // listBox2.Items.Add(i2.GetActionName());
                                GlobalAbstractDeckAction listbox = new GlobalAbstractDeckAction();
                                listbox.Text = i2.GetActionName();
                                listbox.Value = i2;
                                listBox2.Items.Add(listbox);
                                //listBox2.Refresh();
                                //   item.DoDragDrop(new DeckActionHelper(act), DragDropEffects.Copy);

                            }
                        };
                        toAdd.Add(item);
                    }
                }
            }
            toAdd.AsEnumerable().Reverse().All(m =>
            {
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
                var enumItems = items.Where(i => i.GetActionCategory() == enumItem && i.IsTool() == false && i.IsPlugin() == false);
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
                        item.MouseDown += (s, ee) =>
                        {
                            if (item.Tag is AbstractDeckAction act)
                            {

                                //  listBox2.Items.Add(i2.GetActionName()) ;
                                GlobalAbstractDeckAction listbox = new GlobalAbstractDeckAction();
                                listbox.Text = i2.GetActionName();
                                listbox.Value = i2;
                                listBox2.Items.Add(listbox);

                                //listBox2.Refresh();
                                //   item.DoDragDrop(new DeckActionHelper(act), DragDropEffects.Copy);

                            }
                        };
                        toAdd.Add(item);
                    }
                }
            }
            toAdd.AsEnumerable().Reverse().All(m =>
            {
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



        }

        private void ImageModernButton4_Click(object sender, EventArgs e)
        {


            var c = ((GlobalAbstractDeckAction)listBox2.SelectedItem).Value;

         

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

        private void AddToListBox()
        {

            foreach (var item in list_actions)
            {
                GlobalControl listbox = new GlobalControl();
                listbox.Text = item.GetActionName();
                listbox.Value = item;
                listBox2.Items.Add(listbox);

            }
        }




        private void DownButton_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (listBox2.SelectedIndex == -1)
                {
                    MessageBox.Show(Texts.rm.GetString("LAYERMULTIACTIONSELECTITEMERROR", Texts.cultereinfo), Texts.rm.GetString("LAYERMULTIACTIONTITLE", Texts.cultereinfo), MessageBoxButtons.OK);
                }
                else
                {
                    int newIndex = listBox2.SelectedIndex + 1;

                    if (newIndex < 0)

                        return;

                    object selectItem = listBox2.SelectedItem;

                    listBox2.Items.Remove(selectItem);

                    listBox2.Items.Insert(newIndex, selectItem);
                    listBox2.SetSelected(newIndex, true);
                }
            }
            catch (Exception)
            {

            }
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
           
            try
            {
 if (listBox2.SelectedIndex == -1)
            {
                MessageBox.Show(Texts.rm.GetString("LAYERMULTIACTIONSELECTITEMERROR", Texts.cultereinfo), Texts.rm.GetString("LAYERMULTIACTIONTITLE", Texts.cultereinfo), MessageBoxButtons.OK);
                }
                else
                {

               
                int newIndex = listBox2.SelectedIndex - 1;

                if (newIndex < 0)
                
                    return;
                
                object selectItem = listBox2.SelectedItem;

                listBox2.Items.Remove(selectItem);

                listBox2.Items.Insert(newIndex, selectItem);
                listBox2.SetSelected(newIndex, true);
                }
            }
            catch (Exception)
            {

            }
        }

        private void DelButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(Texts.rm.GetString("LAYERMULTIACTIONDELLCONFIRM", Texts.cultereinfo), Texts.rm.GetString("LAYERMULTIACTIONTITLE", Texts.cultereinfo), MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                object selectItem = listBox2.SelectedItem;
                listBox2.Items.Remove(selectItem);
                //do something
            }
           
        }

        private void DelButtonAll_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(Texts.rm.GetString("LAYERMULTIACTIONDELALLCONFIRM", Texts.cultereinfo), Texts.rm.GetString("LAYERMULTIACTIONTITLE", Texts.cultereinfo), MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                listBox2.Items.Clear();
                //do something
            }
      

        }
    }

}
   
    

