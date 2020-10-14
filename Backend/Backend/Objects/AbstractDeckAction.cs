using DisplayButtons.Backend.Objects.Implementation.DeckActions;
using DisplayButtons.Backend.Objects.Implementation.DeckActions.OBS;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace DisplayButtons.Backend.Objects
{

    

    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ActionPropertyPluginsScriptEntryPoint : Attribute
    { }

    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ActionPropertyIncludeAttribute : Attribute
    { }

    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ActionPropertyUpdateImageOnChangedAttribute : Attribute
    {
    }

    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ActionPropertyDescriptionAttribute : Attribute
    {
        private readonly string description;

        public string Description
        {
            get
            {
                return Texts.rm.GetString(description, Texts.cultereinfo);
            }
        }

        public ActionPropertyDescriptionAttribute(string description)
        {
            this.description = description;
        }
    }
    // novos botões do multiple action 

    [System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct)
]
    public class MediaAttribute : System.Attribute
    {
        private string name;

        public MediaAttribute(string name)
        {
            this.name = name;

        }
    }
        public abstract class AbstractDeckAction 
    {
        public static Type FindType(string fullName)
        {
            return
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic)
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName.Equals(fullName));
        }

        public enum DeckActionCategory
        {
            General,
            AutoHotKey,
            Helpers,
            OBS,
            Misc,
            Deck,
            Twitch
            
        }

     
        public abstract DeckActionCategory GetActionCategory();
        public abstract string GetActionName();
        public virtual bool IsPlugin()
        {

            return false ;
        }
        public virtual bool IsTool()
        {

            return false;
        }
        public virtual void SetConfigs(string script_param) { }
       

        [Obsolete]
        public virtual bool OnButtonClick(DeckDevice deckDevice) {
            return false;
        }
        public abstract void OnButtonDown(DeckDevice deckDevice);
        public abstract void OnButtonUp(DeckDevice deckDevice);
        public abstract AbstractDeckAction CloneAction();
        public virtual DeckImage GetDefaultItemImage()
        {
            return null;
        }
        public virtual bool setLayer(int current = -1,IDeckItem item = null)
        {
            return false;
        }
        public virtual bool getLayer()
        {
            return false;
        }
        public virtual DeckImage GetLayerTwo()
        {
            return null;
        }

    }

    
    
}
