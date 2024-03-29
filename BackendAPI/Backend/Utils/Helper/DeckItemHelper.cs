﻿using BackendAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAPI.Utils
{
    public class DeckItemMoveHelper
    {

        public DeckItemMoveHelper(IDeckItem deckItem, IDeckFolder oldFolder, int oldSlot)
        {
            DeckItem = deckItem;
            OldFolder = oldFolder;
            OldSlot = oldSlot;
        }

        public IDeckItem DeckItem { get; set; }
        public IDeckFolder OldFolder { get; set; }
        public int OldSlot { get; set; }
        public bool CopyOld { get; set; } = false;
    }
}
