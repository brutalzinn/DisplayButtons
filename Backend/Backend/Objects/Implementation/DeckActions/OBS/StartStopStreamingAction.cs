using Backend.Properties;
using Backend.Utils;
using System.ComponentModel;

namespace Backend.Objects.Implementation.DeckActions.OBS
{
    public class StartStopStreamingAction : AbstractDeckAction, IDeckHelper
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

        int CurrentItem = 1;
        IDeckItem atual_item;
        public StreamingStateToggle ToggleStreamingAction { get; set; } = StreamingStateToggle.Stopped;
        [ActionPropertyInclude]
        [ActionPropertyDescription("DESCRIPTIONACTION")]
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
        public override bool setLayer(int _current, IDeckItem item)
        {

            if (_current != -1)
            {
                CurrentItem = _current;
                atual_item = item;

            }



            return true;
        }
        public override bool getLayer()
        {
            switch (StreamAction)
            {
                case StreamingState.Toggle:

                    return true;

                default:
                    return false;

            }
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
                                IDeckHelper.setVariable(false, atual_item, deckDevice);
                                break;
                            case StreamingStateToggle.Recording:
                                ToggleStreamingAction = StreamingStateToggle.Stopped;
                                OBSUtils.StopRecording();
                                IDeckHelper.setVariable(true, atual_item, deckDevice);
                                break;
                        }
                        break;
                }
            }
        }
    }
}
