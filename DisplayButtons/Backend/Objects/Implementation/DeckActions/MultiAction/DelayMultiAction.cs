using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DisplayButtons.Backend.Objects.Implementation.DeckActions.MultiAction
{



    public class DelayMultiAction : AbstractDeckAction
    {



        public  int delay_timer { get; set; }

        public override string GetActionName() => "Delay";
        public override AbstractDeckAction CloneAction()
        {
            return new DelayMultiAction();
        }
        public void ToExecuteHelper()
        {
            // var originalToExec = new String(ToExecute.ToCharArray());
            dynamic form = Activator.CreateInstance(FindType("DisplayButtons.Forms.ActionHelperForms.ToolDelayActionHelper")) as Form;

            form.delay_timer = delay_timer;
            if (form.ShowDialog() == DialogResult.OK)
            {
                //   ToExecute = form.ModifiableAction.ToExecute;
                delay_timer = form.delay_timer;
            }
            else
            {

                //   form.list_actions = list_actions;
            }
        }
        public override bool IsTool()
        {
            return true;
        }
        public override DeckActionCategory GetActionCategory() => DeckActionCategory.General;


        public override async void OnButtonDown(DeckDevice deckDevice)
        {
            Thread.Sleep(delay_timer);



        }
        async Task PausaComTaskDelay()
        {
          //  await Task.Delay(delay_timer);
        }
        public override void OnButtonUp(DeckDevice deckDevice)
        {
        }

    }
}
