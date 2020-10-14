﻿using Backend.Objects;
using McMaster.NETCore.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Utils
{
    public class DeckActionHelper
    {
        public DeckActionHelper(AbstractDeckAction deckAction)
        {
            DeckAction = deckAction;
        }

        public AbstractDeckAction DeckAction { get; set; }
        public string ToExecute { get; set; }
        public string dllpath { get; set; }
        public PluginLoader plugin { get; set; }
        public string ToScript { get; set; }
        public string ToName{ get; set; }
    }
}