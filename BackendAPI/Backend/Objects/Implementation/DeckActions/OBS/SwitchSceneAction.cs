﻿using BackendAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackendAPI.Objects.Implementation.DeckActions.OBS
{
    public class SwitchSceneAction : AbstractDeckAction
    {
        [ActionPropertyInclude]
        [ActionPropertyDescription("DESCRIPTIONSELECTEDSCENE")]
        public String SceneName { get; set; } = "";

        public void SceneNameHelper()
        {
            var oldScene = new String(SceneName.ToCharArray());
            dynamic form = Activator.CreateInstance(FindType("Forms.ActionHelperForms.OBS.OBSSceneChangeHelper")) as Form;
            var execAction = CloneAction() as SwitchSceneAction;
            execAction.SceneName = SceneName;
            form.ModifiableAction = execAction;

            if (form.ShowDialog() == DialogResult.OK) {
                SceneName = form.ModifiableAction.SceneName;
            } else {
                SceneName = oldScene;
            }
        }

        public override AbstractDeckAction CloneAction()
        {
            return new SwitchSceneAction();
        }

        public override DeckActionCategory GetActionCategory()
        {
            return DeckActionCategory.OBS;
        }

        public override string GetActionName()
        {
            return Texts.rm.GetString("DECKOBSSWITCHSCENE", Texts.cultereinfo); 
        }

        public override void OnButtonDown(DeckDevice deckDevice)
        {
        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {
            if (OBSUtils.IsConnected && !string.IsNullOrEmpty(SceneName)) {
                OBSUtils.SwitchScene(SceneName);
            }
        }
    }
}
