using ButtonDeck.Backend.Objects.Implementation.DeckActions.General;
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
    public partial class ActionPlugin : Form
    {
        public ActionPlugin()
        {
            InitializeComponent();
        }
    
        private string _toExecuteFileName;

        private PluginListGenerator _modifiableAction;
        private static string scripter_form;
        public PluginListGenerator ModifiableAction
        {
            get { return _modifiableAction; }
            set
            {
                _modifiableAction = value;


            }
        }
        public string scripter
        {
            get { return scripter_form; }
            set
            {
                scripter_form = value;


            }
        }
        public string ToExecuteFileName
        {
            get { return _toExecuteFileName; }
            set
            {
                _toExecuteFileName = value;
                UpdateFinal(ModifiableAction);
            }
        }
        private void UpdateFinal(PluginListGenerator act)
        {
         //   act.ToExecute = (_toExecuteFileName + "#" + _toExecuteArguments + "#" + _toExecuteAsk);

        }

        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }
        private void ActionPlugin_Load(object sender, EventArgs e)
        {

        }

        private void ModernButton2_Click(object sender, EventArgs e)
        {
    

            CloseWithResult(DialogResult.OK);
        }

        private void ModernButton3_Click(object sender, EventArgs e)
        {
            ScribeBot.Scripter.Execute(scripter_form, false);

            object functiontable = ScribeBot.Scripter.Environment.Globals["menu_cancel"];

            ScribeBot.Scripter.Environment.Call(functiontable);
            CloseWithResult(DialogResult.Cancel);

        }
    }
}
