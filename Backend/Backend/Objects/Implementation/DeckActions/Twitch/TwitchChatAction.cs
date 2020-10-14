using Backend.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backend.Objects.Implementation.DeckActions.OBS
{
    public class TwitchChatAction : AbstractDeckAction
    {
      


   

        [ActionPropertyInclude]
        [ActionPropertyDescription("DESCRIPTIONSCENEITEM")]
        public String Chat { get; set; } = "";


     

        public string username;
        public bool isTop;
        public int opacity = 100;

   
        public void ChatHelper()
        {
         
            dynamic form = Activator.CreateInstance(FindType("Forms.TwitchChat.configs.TwitchChatConfig")) as Form;


            form.opacity_trackbar.Value = opacity;
            form.username_textbox.Text = username;
            form.checkbox_topmost.Checked = isTop;
            if (form.ShowDialog() == DialogResult.OK) {

                username = form.username_textbox.Text;
                isTop = form.checkbox_topmost.Checked;
                opacity = form.opacity_trackbar.Value;
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
           

             var thread = new Thread(ThreadSTA);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }
       public void ThreadSTA()
        {
            dynamic form = Activator.CreateInstance(FindType("Forms.TwitchChat.TwitchChatFast")) as Form;
            form.Opacity = opacity / (double)100;
            form.TopMost = isTop;
            form.initilizeWeb(username);
            if (form.ShowDialog() == DialogResult.OK)
            {

                form.Close();
            }
           
        }
        public override void OnButtonUp(DeckDevice deckDevice)
        {
          
        }
    }
}
