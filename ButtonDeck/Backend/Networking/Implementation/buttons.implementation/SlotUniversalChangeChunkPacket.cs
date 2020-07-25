using ButtonDeck.Backend.Networking.Attributes;
using ButtonDeck.Backend.Networking.IO;
using ButtonDeck.Backend.Objects;
using Microsoft.VisualC.StlClr;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonDeck.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer | PacketArchitecture.ServerToClient)]
    public class SlotUniversalChangeChunkPacket : INetworkPacket
    {
        public class Labels
        {
            private int id;
            private string font;
            private int size;
            private string text;
            private int position;
            private string color;
            private DeckImage image;
            public Labels(int id, string font, int size, int position, string text, string color, DeckImage img) {
                if (String.IsNullOrEmpty(text))
                {

                    text = "-";
                }
                this.id = id;
                this.font = font;
                this.text = text;
                this.size = size;
                this.position = position;
                this.color = color;
                this.image = img;

            }
            public DeckImage Image
            {

                get { return image; }
                set { image = value; }

            }
            public string Color
            {

                get { return color; }
                set { color = value; }

            }
            public string Text
            {
                get { return text; }
                set { text = value; }
            }
            public int Id
            {

                get { return id; }
                set { id = value; }
            }

            public string Font
            {

                get { return font; }
                set { font = value; }


            }

            public int Size
            { get { return size; }
                set { size = value; }
            }
            public int Position
            {
                get { return position; }
                set { position = value; }
            }
            // Should also override == and != operators.
        }

        List<Labels> list_labels = new List<Labels>();
        public void AddToQueue(int slot, string text, string font,int size, int position, string color, DeckImage image)
        {
         
            list_labels.Add (new Labels( slot, font, size, position,text, color, image));
        }
        public void ClearPacket()
        {

            list_labels.Clear();

        }
        public override void FromInputStream(DataInputStream reader)
        {

         
           

        }

        public override long GetPacketNumber() => 12;

        public override void ToOutputStream(DataOutputStream writer)
        {
            //The number of images to send
            writer.WriteInt(list_labels.Count);
            foreach (var item in list_labels)
            {
                SendDeckLabel(writer, item.Id, item.Font,item.Size,item.Position,item.Text,item.Color, item.Image) ;
           
            }

        }

        private void SendDeckLabel(DataOutputStream writer, int slot, string font,int size,int pos,string text,string color , DeckImage img)
        {
           
            //Write the slot
            if (img != null)
            {

                writer.WriteInt(slot);

                writer.WriteInt(img.InternalBitmap.Length);
                writer.Write(img.InternalBitmap);

                //font
                writer.WriteUTF(font);
                //text

                writer.WriteUTF(text);
                //size
                writer.WriteInt(size);
                //position
                writer.WriteInt(pos);

                //color

                writer.WriteUTF(color);
            }
        }

        public override object Clone()
        {
            return new SlotUniversalChangeChunkPacket();
        }
    }
}
