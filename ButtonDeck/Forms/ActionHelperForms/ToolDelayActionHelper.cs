using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonDeck.Forms.ActionHelperForms
{
    public partial class ToolDelayActionHelper : TemplateForm
    {
        private ToolDelayActionHelper _modifiableAction;
        public ToolDelayActionHelper ModifiableAction
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
        public int delay_timer { get; set; }
        public ToolDelayActionHelper()
        {
            InitializeComponent();
        }
        private void CloseWithResult(DialogResult result)
        {


            DialogResult = result;
            Close();
        }
        private void ModernButton2_Click(object sender, EventArgs e)
        {
            delay_timer = Convert.ToInt32(textBox1.Text);
            CloseWithResult(DialogResult.OK);

        }

        private void ModernButton3_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.Cancel);

        }

        private void ToolDelayActionHelper_Load(object sender, EventArgs e)
        {
            textBox1.Text = delay_timer.ToString();
           // delay_timer = Convert.ToInt32(textBox1.Text);
        }
    }
}
