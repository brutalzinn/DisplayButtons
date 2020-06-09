using ButtonDeck.Backend.Networking.Attributes;
using ButtonDeck.Backend.Networking.IO;
using ButtonDeck.Backend.Objects;
using Microsoft.VisualC.StlClr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonDeck.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer)]
    public class SlotLabelButtonChangeChunkPacket : INetworkPacket
    {
         class Labels
        {
            private int id;
            private string font;
            private int size;
            private string text;
            private int position;
            private int color;
            public Labels(int id, string font,int size, int position, string text,int color){
                this.id = id;
                this.font = font;
                this.text = text;
                this.size = size;
                this.position = position;
                this.color = color;

                }
            public int Color
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

       private static List<Labels> list_labels = new List<Labels>();
        public void AddToQueue(int slot, string text, string font,int size, int position, int color)
        {
           
            list_labels.Add (new Labels( slot, font, size, position,text, color));
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
                SendDeckLabel(writer, item.Id, item.Font,item.Size,item.Position,item.Text,item.Color) ;
            }

        }

        private void SendDeckLabel(DataOutputStream writer, int slot, string font,int size,int pos,string text,int color)
        {
           
                //Write the slot
                writer.WriteInt(slot);
                //font
                writer.WriteUTF(text);
            //text

            writer.WriteUTF(text);
                //size
                writer.WriteInt(size);
                //position
                writer.WriteInt(pos);

                //color

                writer.WriteInt(color);
           
        }

        public override object Clone()
        {
            return new SlotLabelButtonChangeChunkPacket();
        }
    }
}
