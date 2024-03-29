﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackendAPI.Objects.Implementation.DeckActions.Deck
{
    public class LayerMultiAction : AbstractDeckAction
    {

        public string name { get; set; } = Texts.rm.GetString("DECKGENERALMULTIACTION", Texts.cultereinfo);

        [ActionPropertyInclude]
        public string ToExecute { get; set; } = "";
        public string script { get; set; } = "";



        [ActionPropertyInclude]
        public ArrayList list_multiple_actions { get; set; } = new ArrayList();
        public List<AbstractDeckAction> list_actions { get; set; } = new List<AbstractDeckAction>();

        public void ToExecuteHelper()
        {
           // var originalToExec = new String(ToExecute.ToCharArray());
            dynamic form = Activator.CreateInstance(FindType("Forms.ActionHelperForms.LayerMultiActionHelper")) as Form;
      
            form.list_actions = list_actions;
            if (form.ShowDialog() == DialogResult.OK)
            {
                //   ToExecute = form.ModifiableAction.ToExecute;
                list_actions = form.list_actions;
            }
            else
            {

             //   form.list_actions = list_actions;
            }
        }

        public override AbstractDeckAction CloneAction()
        {
            return new LayerMultiAction();
        }

        public override DeckActionCategory GetActionCategory() => DeckActionCategory.General;

        public override string GetActionName() => name;



        public override void OnButtonDown(DeckDevice deckDevice)
        {
            var t = new Thread(() => actionUp(deckDevice));

            t.Start();

        }
        public void actionUp(DeckDevice deckDevice)
        {


          foreach(var item in list_actions)
            {
                MethodInfo helperMethod = item.GetType().GetMethod("OnButtonDown");
                if (helperMethod != null)
                {
                    Debug.WriteLine("TEntando executar.." + item.GetActionName());
                    helperMethod.Invoke(item, new object[] { deckDevice });

                }
             

            }

        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {
        }
    }
}
