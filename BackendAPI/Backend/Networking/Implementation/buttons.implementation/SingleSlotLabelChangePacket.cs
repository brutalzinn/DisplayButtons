using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendAPI.Networking.Attributes;
using BackendAPI.Networking.IO;
using BackendAPI.Objects;

namespace BackendAPI.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer)]
    public class SingleSlotLabelChangePacket : INetworkPacket
    {
        public SingleSlotLabelChangePacket()
        { }
        public SingleSlotLabelChangePacket(int slot, string font, int size, int pos, string text, string color)
        {
            Id = slot;
            Font = font;
            Size = size;
            Position = pos;
            Color = color;
            Text = text;
        }
        
            private int id;
            private string font;
            private int size;
            private string text;
            private int position;
            private string color;
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
            {
                get { return size; }
                set { size = value; }
            }
            public int Position
            {
                get { return position; }
                set { position = value; }
            }
        
        public DeckImage DeckImage { get; set; }
        public int ImageSlot { get; set; }

        public override object Clone()
        {
            return new SingleSlotLabelChangePacket();
        }

        public override void FromInputStream(DataInputStream reader)
        {
            //Client to Server 
        }

        public override long GetPacketNumber() => 14;

        public override void ToOutputStream(DataOutputStream writer)
        {
            //Server to Client
            if(String.IsNullOrEmpty(Text) == false)
            {


         
            writer.WriteInt(Id);
            //font
            writer.WriteUTF(Font);
            //text

            writer.WriteUTF(Text);
            //size
            writer.WriteInt(Size);
            //position
            writer.WriteInt(Position);

            //color

            writer.WriteUTF(Color);
            }
        }
    }
}
