using DisplayButtons.Backend.Networking;
using DisplayButtons.Backend.Networking.Implementation;
using DisplayButtons.Backend.Objects;
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
        public static void RefreshButton(int slot, DeckDevice CurrentDevice,DeckItemMisc deckmisc, bool sendToDevice = true)
        {
            // Buttons_Unfocus(this, EventArgs.Empty);

            IDeckFolder folder = MainForm.Instance.CurrentDevice?.CurrentProfile.Currentfolder;
            ImageModernButton control1 = MainForm.Instance.GetButtonControl(slot);
            //Label control_label = GetLabelControl(slot);
            // Label title_control = Controls.Find("titleLabel" + slot, true).FirstOrDefault() as Label;

            control1.NormalImage = null;
            control1.Tag = null;
            control1.Text = "";
            control1.ClearText();
            ClearSingleItemToDevice(CurrentDevice, slot);

            if (folder == null) control1.Invoke(new Action(control1.Refresh));

            if (folder == null) return;
            for (int i = 0; i < folder.GetDeckItems().Count; i++)
            {
                IDeckItem item = null;
                item = folder.GetDeckItems()[i];

                if (folder.GetItemIndex(item) != slot) continue;
                ImageModernButton control = MainForm.Instance.Controls.Find("modernButton" + folder.GetItemIndex(item), true).FirstOrDefault() as ImageModernButton;



                if (item != null)
                {


                    control.NormalImage = deckmisc?.GetItemImage()?.Bitmap;
               
                    control.TextButton = new TextLabel(deckmisc);
                    control.Tag = item;
                    control.Invoke(new Action(control.Refresh));
                    CurrentDevice.CheckCurrentFolder();
                    if (sendToDevice)
                    {
                        SendSingleItemToDevice(CurrentDevice, slot, deckmisc);

                    }


                }

            }

            // 
        }





    }
}
