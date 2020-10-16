using BackendAPI.Objects;
using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace OnlyDllPlugin
{

    [SoapInclude(typeof(OnlyDllPlugin.Class1))]


    public class Class1 : AbstractDeckAction
    {
        public string testing { get; set; }
        public override AbstractDeckAction CloneAction()
        {
            return new Class1();
        }

        public override DeckActionCategory GetActionCategory()
        {
            return DeckActionCategory.Deck;
        }

        public override string GetActionName()
        {
            return "Teste de dll";
        }
        public override bool IsPlugin()
        {
            return true;
        }
        public override void OnButtonDown(DeckDevice deckDevice)
        {
            Debug.WriteLine($"Button down pela AbtractDeck por {deckDevice.DeviceName}");

        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {
            Debug.WriteLine($"Button up pela AbtractDeck por {deckDevice.DeviceName}");
        }
    }
}
