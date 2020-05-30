using ButtonDeck.Backend.Objects;
using ButtonDeck.Backend.Objects.Implementation.DeckActions.General;
using ButtonDeck.Backend.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ButtonDeck.Backend.Objects.AbstractDeckAction;

namespace ButtonDeck.Forms.ActionHelperForms
{
    public partial class LayerMultiActionHelper : TemplateForm
    {
        public LayerMultiActionHelper()
        {
            InitializeComponent();
        }
        private LayerMultiActionHelper _modifiableAction;

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
        //    listb.OwnerDraw = true;
            GenerateSidebar(listBox1);
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
                                listBox2.Items.Add(item.Text) ;
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

      

        private void ListBox2_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {

            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            var item = listBox2.Items[e.Index];
            if (e.Index % 2 == 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Gold), e.Bounds);
            }
            e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
                e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }
    }
    }

