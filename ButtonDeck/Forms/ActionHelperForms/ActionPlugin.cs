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
        private string _toExecuteArguments;
        private string _toExecuteFileName;
        private string _toExecuteAsk;
        private FolderAddAction _modifiableAction;

        public FolderAddAction ModifiableAction
        {
            get { return _modifiableAction; }
            set
            {
                _modifiableAction = value;


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
        private void UpdateFinal(FolderAddAction act)
        {
            act.ToExecute = (_toExecuteFileName + "#" + _toExecuteArguments + "#" + _toExecuteAsk);

        }
    
        private void ActionPlugin_Load(object sender, EventArgs e)
        {

        }
    }
}
