﻿using BackendAPI.Bibliotecas;
using BackendAPI.Networking.Attributes;
using BackendAPI.Networking.IO;
using BackendAPI.Objects;
using BackendAPI.Objects.Implementation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BackendAPI.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer | PacketArchitecture.ServerToClient)]
    public class SlotUniversalChangeChunkPacket : INetworkPacket
    {



        IDictionary<int, DeckItemMisc> toSend = new Dictionary<int, DeckItemMisc>();
        public void AddToQueue(int slot, DeckItemMisc item)
        {

       
            toSend.Add(slot, item);
        }
        public void ClearPacket()
        {

           

        }
        public override void FromInputStream(DataInputStream reader)
        {

        }

        public override long GetPacketNumber() => 12;

        public override void ToOutputStream(DataOutputStream writer)
        {
      
            writer.WriteInt(toSend.Count);
         //   writer.WriteUTF(jsonString);

           foreach (var item in toSend)
           {
              SendDeckLabel(writer, item.Key, item.Value) ;

             }

        }

        private void SendDeckLabel(DataOutputStream writer, int slot, DeckItemMisc item)
        {

            //Write the slot
            if (item.GetItemImage() != null)
            {
                Json headerContent = new Json();

     
                writer.WriteInt(slot);
                //Byte array lenght
                writer.WriteInt(item.GetItemImage().InternalBitmap.Length);
                writer.Write(item.GetItemImage().InternalBitmap);

                headerContent.Font = " ";
                headerContent.Size = item.Decksize;
                headerContent.Position = item.Deckposition;
                headerContent.Text = item.Deckname;
                headerContent.Color = item.Deckcolor;

                headerContent.Stroke_color = item.Stroke_color;
                headerContent.Stroke_dx = item.Stroke_dxtext;
                headerContent.Stroke_radius = item.Stroke_radius;
                headerContent.Stroke_dy = item.Stroke_Dy;
                headerContent.IsStroke = item.IsStroke;
                headerContent.Isboldtext = item.Isboldtext;
                headerContent.Isnormaltext = item.Isnormaltext;
                headerContent.Isitalictext = item.Isitalictext;
                headerContent.Ishinttext = item.Ishinttext;
                string jsonString = JsonConvert.SerializeObject(headerContent, Formatting.None);

                writer.WriteUTF(jsonString);

                ////text

                //writer.WriteUTF(text);
                ////size
                //writer.WriteInt(size);
                ////position
                //writer.WriteInt(pos);

                ////color

                //writer.WriteUTF(color);
            }
        }

        public override object Clone()
        {
            return new SlotUniversalChangeChunkPacket();
        }
    }
}
