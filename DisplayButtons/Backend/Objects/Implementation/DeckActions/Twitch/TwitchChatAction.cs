using DisplayButtons.Backend.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayButtons.Backend.Objects.Implementation.DeckActions.OBS
{
    public class TwitchChatAction : AbstractDeckAction
    {
      


   

        [ActionPropertyInclude]
        [ActionPropertyDescription("DESCRIPTIONSCENEITEM")]
        public String Chat { get; set; } = "";

   
        public void ChatHelper()
        {
         
            dynamic form = Activator.CreateInstance(FindType("DisplayButtons.Forms.TwitchChat.TwitchChatForm")) as Form;
          
          
           

            if (form.ShowDialog() == DialogResult.OK) {
                form.Close();
            } else {
            
            }
        }

        public override AbstractDeckAction CloneAction()
        {
            return new TwitchChatAction();
        }

        public override DeckActionCategory GetActionCategory()
        {
            return DeckActionCategory.Twitch;
        }

        public override string GetActionName()
        {
            return Texts.rm.GetString("DECKOBSVISIBILITYSCENE", Texts.cultereinfo);
        }

        public override void OnButtonDown(DeckDevice deckDevice)
        {
        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {
          ;
        }
    }
}
