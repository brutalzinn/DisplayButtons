using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ButtonDeck.Backend.Networking.Attributes;
using ButtonDeck.Backend.Networking.IO;
using ButtonDeck.Backend.Objects;

namespace ButtonDeck.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer)]
    public class SingleSlotUniversalChangePacket : INetworkPacket
    {
        public SingleSlotUniversalChangePacket()
        { }
        public SingleSlotUniversalChangePacket(DeckImage deckImage,int slot, string font, int size, int pos, string text, string color)
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
            return new SingleSlotUniversalChangePacket();
        }

        public override void FromInputStream(DataInputStream reader)
        {
            //Client to Server 
        }

        public override long GetPacketNumber() => 14;

        public override void ToOutputStream(DataOutputStream writer)
        {

            writer.WriteBoolean(DeckImage != null);
            //Server to Client
            if (String.IsNullOrEmpty(Text) == false)
            {

                if (DeckImage != null)
                {
                    writer.WriteInt(ImageSlot);
                    //Byte array lenght
                    writer.WriteInt(DeckImage.InternalBitmap.Length);
                    writer.Write(DeckImage.InternalBitmap);
                }

            

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
