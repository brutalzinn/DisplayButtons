﻿using DisplayButtons.Backend.Utils;
using DisplayButtons.Properties;
using System.ComponentModel;

namespace DisplayButtons.Backend.Objects.Implementation.DeckActions.OBS
{
    public class StartStopStreamingAction : AbstractDeckAction
    {
        static bool firstTime;
        public enum StreamingState
        {
            [Description("OBSDESCRIPTIONSTART")]
            Start,
            [Description("OBSDESCRIPTIONSTOP")]
            Stop,
            [Description("OBSDESCRIPTIONTOGGLE")]
            Toggle
        }
        public enum StreamingStateToggle
        {
            Recording,
            Stopped


        }
        public StreamingStateToggle ToggleStreamingAction { get; set; } = StreamingStateToggle.Stopped;
        [ActionPropertyInclude]
        [ActionPropertyDescription("Action")]
        [ActionPropertyUpdateImageOnChanged]
        public StreamingState StreamAction { get; set; }
        public override AbstractDeckAction CloneAction()
        {
            return new StartStopStreamingAction();
        }

        public override DeckActionCategory GetActionCategory()
        {
            return DeckActionCategory.OBS;
        }

        public override string GetActionName()
        {
            if (!firstTime) {
                firstTime ^= true;
                return Texts.rm.GetString("DECKOBSSTREAMING", Texts.cultereinfo);
            }
            switch (StreamAction) {
                case StreamingState.Start:
                    return "Start Streaming";
                case StreamingState.Stop:
                    return "Stop Streaming";
            }
            return Texts.rm.GetString("DECKOBSSTREAMING", Texts.cultereinfo);
        }

        public override DeckImage GetDefaultItemImage()
        {
            switch (StreamAction) {
                case StreamingState.Start:
                    return new DeckImage(Resources.img_item_start_stream);
                case StreamingState.Stop:
                    return new DeckImage(Resources.img_item_stop_stream);
                default:
                    return base.GetDefaultItemImage();
            }
        }

        public override void OnButtonDown(DeckDevice deckDevice)
        {
        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {
            if (OBSUtils.IsConnected) {
                switch (StreamAction) {
                    case StreamingState.Start:
                        OBSUtils.StartStreaming();
                        break;
                    case StreamingState.Stop:
                        OBSUtils.StopStreaming();
                        break;

                    case StreamingState.Toggle:
                        switch (ToggleStreamingAction)
                        {
                            case StreamingStateToggle.Stopped:
                                ToggleStreamingAction = StreamingStateToggle.Recording;
                                OBSUtils.StartRecording();
                                break;
                            case StreamingStateToggle.Recording:
                                ToggleStreamingAction = StreamingStateToggle.Stopped;
                                OBSUtils.StopRecording();
                                break;
                        }
                        break;
                }
            }
        }
    }
}
