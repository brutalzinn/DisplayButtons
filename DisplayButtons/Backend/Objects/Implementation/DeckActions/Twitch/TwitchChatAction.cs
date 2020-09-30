using DisplayButtons.Backend.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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


     

        public string username;
        public bool isTop;

   
        public void ChatHelper()
        {
         
            dynamic form = Activator.CreateInstance(FindType("DisplayButtons.Forms.TwitchChat.configs.TwitchChatConfig")) as Form;



            form.username_textbox.Text = username;
            form.checkbox_topmost.Checked = isTop;
            if (form.ShowDialog() == DialogResult.OK) {

                username = form.username_textbox.Text;
                isTop = form.checkbox_topmost.Checked;
            } else {
                form.Close();
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
        dynamic form = Activator.CreateInstance(FindType("DisplayButtons.Forms.TwitchChat.TwitchChatFast")) as Form;

            form.initilizeWeb(username);
            if (form.ShowDialog() == DialogResult.OK)
            {

                form.Close();
            }
            else
            {
                form.Close();
            }
        }
       
        public override void OnButtonUp(DeckDevice deckDevice)
        {
          ;
        }
    }
}
