using ButtonDeck.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using ButtonDeck.Backend.Utils.Native;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static ButtonDeck.Backend.Objects.Implementation.DeckActions.General.KeyPressAction;

namespace ButtonDeck.Backend.Objects.Implementation
{
    [Serializable]
    public class DynamicDeckFolder : IDeckFolder
    {
        public DeckImage DeckImage { get; set; }
        SerializableDictionary<int, IDeckItem> items = new SerializableDictionary<int, IDeckItem>();

        public SerializableDictionary<int, IDeckItem> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }
   
        public override List<IDeckItem> GetDeckItems() => items.Values.ToList();
        public override List<IDeckFolder> GetSubFolders() => items.Values.OfType<IDeckFolder>().ToList();

        public override void Add(int slot, IDeckItem item) => items[slot] = item;

        public override DeckImage GetItemImage()
        {
            return DeckImage;
        }

        public override int GetItemIndex(IDeckItem item)
        {
            if (!items.Any(c => c.Value == item)) return -1;
            var key = items.First(c => c.Value == item);
            return key.Key;
        }

        [XmlIgnore]
        public IDeckFolder ParentFolder { get; set; }
        public override IDeckFolder GetParent()
        {
         
  
            return ParentFolder;
        }

        public override void SetParent(IDeckFolder folder)
        {
            ParentFolder = folder;
        }

        public override int Add(IDeckItem item)
        {
            int addToSlot = -1;
            for (int i = Globals.calc - 1; i >= 0; i--)
            {
                if (items.ContainsKey(i))
                {
                    addToSlot = i + 1;
                    break;
                }
            }
            if (addToSlot != -1)
            {
                Add(addToSlot, item);
            }
            return addToSlot;
        }

        public override void Remove(int slot)
        {
            items.Remove(slot);
        }

        public override DeckImage GetDeckImage()
        {
            return DeckImage;
        }
        [XmlIgnore]
        public string folder_name { get; set; }
        [XmlIgnore]
        public int folder_father { get; set; }
        [XmlIgnore]
        public int father
        {


            get
            { // serialize
                if (folder_father == 0) return 0;
                return folder_father;
            }
            set
            { // deserialize
                if (value == 0)
                {
                    folder_father = 0;
                }
                else
                {
                    folder_father = value;

                }

            }


        }


        [XmlElement("name")]
        public string name
        {


            get
            { // serialize
                if (folder_name == null) return null;
                return folder_name;
            }
            set
            { // deserialize
                if (value == null)
                {
                    folder_name = null;
                }
                else
                {
                    folder_name = value;

                }

            }


        }
        [Serializable]
      

            public class KeyInfoGlobal
        {
                public KeyInfoGlobal()
                {
                }
                public KeyInfoGlobal(Keys[] modifierKeys, Keys[] keys)
                {
                    ModifierKeys = modifierKeys;
                    Keys = keys;
                }

                public Keys[] ModifierKeys { get; set; } = new Keys[] { };
                public Keys[] Keys { get; set; } = new Keys[] { };
            }
       
        [ActionPropertyInclude]
      [ActionPropertyDescription("GlobalHotKeys")]
    
            public KeyInfoGlobal KeyGlobalValue { get; set; } = new KeyInfoGlobal();
            public void KeyGlobalValueHelper()
            {
                var keyInfo = new KeyInfoGlobal(KeyGlobalValue.ModifierKeys, KeyGlobalValue.Keys);
                dynamic form = Activator.CreateInstance(UsbMode.FindType("ButtonDeck.Forms.ActionHelperForms.FolderGlobalHotKey")) as Form;

                var execAction = new DynamicDeckFolder() as DynamicDeckFolder;
                execAction.KeyGlobalValue = KeyGlobalValue;
                form.ModifiableAction = execAction;

                if (form.ShowDialog() == DialogResult.OK)
                {
                KeyGlobalValue = form.ModifiableAction.KeyGlobalValue;
                }
                else
                {
                KeyGlobalValue = keyInfo;
                }
            }
        }

    
  
    
    }
