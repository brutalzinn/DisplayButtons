using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayButtons.Backend.Objects.Implementation.DeckActions.General
{
    public class ExecutableRunAction : AbstractDeckAction
    {


      
        public string name { get; set; } = "To execute";


        [ActionPropertyInclude]
        [ActionPropertyDescription("To Execute")]
        public string ToExecute { get; set; } = "";

        public void ToExecuteHelper()
        {
            var originalToExec = new String(ToExecute.ToCharArray());
            dynamic form = Activator.CreateInstance(FindType("DisplayButtons.Forms.ActionHelperForms.ExecutableRunHelper")) as Form;
            var execAction = CloneAction() as ExecutableRunAction;
            execAction.ToExecute = ToExecute;
            
            form.ModifiableAction = execAction;

            if (form.ShowDialog() == DialogResult.OK) {
                ToExecute = form.ModifiableAction.ToExecute;
            } else {
                ToExecute = originalToExec;
            }
        }

        public override AbstractDeckAction CloneAction()
        {
            return new ExecutableRunAction();
        }

        public override DeckActionCategory GetActionCategory() => DeckActionCategory.General;

        public override string GetActionName() => name;

        public static string GetExecutable(string command)
        {
            string executable = string.Empty;
            string[] tokens = command.Split('#');

            for (int i = tokens.Length; i >= 0; i--) {
                executable = string.Join("#", tokens, 0, i);
                if (File.Exists(executable.Trim('"')))
                    break;
            }
            return executable;
        }

        public override void OnButtonDown(DeckDevice deckDevice)
        {


            var array = ToExecute.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            if(array[2] == "1")
            {


                string message = "Você deseja executar esse processo?";
                string caption = "Abrir processo";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    // Closes the parent form.
                    return;
                }


            }
            if (string.IsNullOrEmpty(ToExecute) || string.IsNullOrWhiteSpace(ToExecute)) return;
            string exec = GetExecutable(ToExecute);
            var proc = new ProcessStartInfo(exec, ToExecute.Substring(exec.Length).Trim())
            {
                UseShellExecute = true,
            };
            Process.Start(proc);
        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {
        }
    }
}
