using ButtonDeck.Backend.Objects.Implementation.DeckActions.General;
using ButtonDeck.Backend.Objects.Implementation.DeckActions.General;
using ButtonDeck.Backend.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonDeck.Forms.ActionHelperForms
{
    public partial class ExecutableRunHelper : TemplateForm
    {

        public static string GetExecutable(string command)
        {
            string executable = string.Empty;
            string[] tokens = command.Split(' ');

            for (int i = tokens.Length; i >= 0; i--) {
                executable = string.Join(" ", tokens, 0, i);
                if (File.Exists(executable.Trim('"')))
                    break;
            }
            return executable;
        }


        private string _toExecuteArguments;
        private string _toExecuteFileName;
        private string _toExecuteAsk;
        private ExecutableRunAction _modifiableAction;

        public ExecutableRunAction ModifiableAction {
            get { return _modifiableAction; }
            set
            {
                _modifiableAction = value;
               //var exec = GetExecutable(value.ToExecute);
              //  textBox1.Text = exec;
              //  int tamanho_2 = value.ToExecute.Substring(exec.Length).Length;
             //   textBox2.Text = value.ToExecute.Substring(exec.Length);

                var array = value.ToExecute.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

                //string teste = exec.Split(' ')[1];  

                for (int i = 0; i < array.Length; i++)
                {

                   Debug.WriteLine("V:" + GetExecutable(array[0]));

   
                    textBox1.Text = array[0];
                  
                        textBox2.Text = array[1].Trim();

                   
                    

                    if(array[2] == "1")
                    {


                        checkBox1.Checked = true;
                    }else
                    {

                        checkBox1.Checked = false;
                    }
                  //  checkBox1.Checked= Convert.ToBoolean(Convert.ToInt32(array[2]));
               
                  Debug.WriteLine("T:" + i);

              }





                }
        }
        public ExecutableRunHelper()
        {
            InitializeComponent();
        }

        public string ToExecuteArguments {
            get { return _toExecuteArguments; }
            set {
                _toExecuteArguments = value;
                UpdateFinal(ModifiableAction);
            }
        }

        private void UpdateFinal(ExecutableRunAction act)
        {
            act.ToExecute = (_toExecuteFileName + "#" + _toExecuteArguments + "#" + toExecuteASK);
            
        }

        public string ToExecuteFileName {
            get { return _toExecuteFileName; }
            set {
                _toExecuteFileName = value;
                UpdateFinal(ModifiableAction);
            }
        }
        public string toExecuteASK
        {
            get { return _toExecuteAsk; }
            set
            {
                _toExecuteAsk = value;
                UpdateFinal(ModifiableAction);
            }
        }

        private void ModernButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Executable Files (*.exe)|*.exe"
            };
            if (dlg.ShowDialog() == DialogResult.OK) {
                textBox1.Text = dlg.FileName;
            }

        }

        private void ModernButton2_Click(object sender, EventArgs e)
        {
            _toExecuteFileName = textBox1.Text;
            if(textBox2.Text.Length > 0)
            {

                _toExecuteArguments = textBox2.Text;

            }
            else
            {
                _toExecuteArguments = " ";

            }

            if (checkBox1.CheckState == CheckState.Checked)
            {

toExecuteASK = "1";

            }else
            {

                toExecuteASK = "0";
            }
              //parei aqui. não tá salvando essa parte.
            CloseWithResult(DialogResult.OK);
        }

        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }

        private void ModernButton3_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.Cancel);
        }

        private void ExecutableRunHelper_Load(object sender, EventArgs e)
        {
            
        }

        private void appBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
