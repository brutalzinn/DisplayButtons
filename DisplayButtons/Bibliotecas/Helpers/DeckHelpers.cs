using DisplayButtons.Backend.Networking;
using DisplayButtons.Backend.Networking.Implementation;
using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Bibliotecas.DeckText;
using DisplayButtons.Controls;
using DisplayButtons.Forms;
using DisplayButtons.Misc;
using DisplayButtons.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisplayButtons.Bibliotecas.Helpers
{
   public static class DeckHelpers
    {



        public static void ClearSingleItemToDevice(DeckDevice device, int slot)
        {
            var con = device.GetConnection();
            con.SendPacket(new SlotImageClearPacket(slot));

        }
        public static void SendSingleItemToDevice(DeckDevice device, int slot, DeckItemMisc item)
        {
            var con = device.GetConnection();
            if (con != null)
            {
                bool isFolder = false;

                var image = item.GetItemImage() ?? (new DeckImage(isFolder ? Resources.img_folder : Resources.img_item_default));
                var seri = image.BitmapSerialized;
                con.SendPacket(new SingleUniversalChangePacket(image)
                {
                    ImageSlot = slot,
                    CurrentItem = item

                });


            }

        }

        public static IDeckItem getDeckItem(int slot)
        {

            IDeckFolder folder = MainForm.Instance.CurrentDevice?.CurrentProfile.Currentfolder;
            ImageModernButton control1 = MainForm.Instance.GetButtonControl(slot);
            if (folder == null) return null;
            IDeckItem item = null;
            for (int i = 0; i < folder.GetDeckItems().Count; i++)
            {
                item = folder.GetDeckItems()[i];
                if (folder.GetItemIndex(item) != slot) continue;
            }


            return item;
        }
        public static void RefreshButton(IDeckItem item,int camada,  DeckItemMisc itemmisc, DeckDevice device)
        {
            // Buttons_Unfocus(this, EventArgs.Empty);
            MainForm.Instance.Invoke(new Action(() =>
            {
                IDeckFolder folder = device.CurrentProfile.Currentfolder;
            ImageModernButton control = MainForm.Instance.Controls.Find("modernButton" + folder.GetItemIndex(item), true).FirstOrDefault() as ImageModernButton;

            if (camada == 1)
            {
                control.Camada = 1;
                control.NormalImage = itemmisc?.GetItemImage()?.Bitmap;
            }else if(camada == 2)
            {
                control.Camada = 2;
                control.NormalLayerTwo = itemmisc?.GetItemImage()?.Bitmap ?? Resources.img_item_default; ;

            }
            control.TextButton = new TextLabel(itemmisc);
            control.Tag = item;
     
          control.Invoke(new Action(control.Refresh));
            device.CheckCurrentFolder();

            SendSingleItemToDevice(device, folder.GetItemIndex(item), itemmisc);
            }));


        }



        public class DeckEvent : Sharpy.IEvent
        {

            public DeckItemMisc DeckMisc;
            public IDeckItem DeckItem;
            public int Camada;

            public DeckEvent( IDeckItem item, int cam, DeckItemMisc deckmisc = null)
            {
                this.DeckMisc = deckmisc;
                this.DeckItem = item;
                this.Camada = cam;
            }
        }
        public class DeckFolderEvent : Sharpy.IEvent
        {

            public DynamicDeckFolder _folder;

            public DeckFolderEvent(DynamicDeckFolder folder)
            {
                this._folder = folder;   
            }
        }
    }


}
