﻿using BackendAPI.Objects.Implementation;
using BackendAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BackendAPI.Objects
{
    [XmlInclude(typeof(DynamicDeckFolder))]
    public abstract class IDeckFolder : IDeckItem
    {
        public abstract DeckImage GetDeckImage();
        public abstract List<IDeckFolder> GetSubFolders();
        public abstract List<IDeckItem> GetDeckItems();
        public abstract IDeckFolder GetParent();
        public abstract void SetParent(IDeckFolder folder);

        public abstract void Add(int slot, IDeckItem item);
        public abstract int Add(IDeckItem item, Profile profile);
        public abstract void Remove(int slot);
        public abstract int GetItemIndex(IDeckItem item);

    }
}
