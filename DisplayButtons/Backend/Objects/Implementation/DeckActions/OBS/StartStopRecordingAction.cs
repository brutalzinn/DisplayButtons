
using DisplayButtons.Properties;
using DisplayButtons.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DisplayButtons.Backend.Objects.Implementation.DeckActions.OBS
{
    public class StartStopRecordingAction : AbstractDeckAction
    {
        static bool firstTime;
        public enum RecordingState
        {
            [Description("OBSDESCRIPTIONSTART")]
            Start,
            [Description("OBSDESCRIPTIONSTOP")]
            Stop,
            [Description("OBSDESCRIPTIONTOGGLE")]
            Toggle
        }
        public enum RecordStateToggle
        {
            Recording,
            Stopped


        }
        [ActionPropertyInclude]
        [ActionPropertyDescription("Action")]
        [ActionPropertyUpdateImageOnChanged]
        public RecordingState RecordAction { get; set; }
        public RecordStateToggle ToggleAction { get; set; } = RecordStateToggle.Stopped;
        public override AbstractDeckAction CloneAction()
        {
            return new StartStopRecordingAction();
        }

        public override DeckActionCategory GetActionCategory()
        {
            return DeckActionCategory.OBS;
        }

        public override string GetActionName()
        {
            if (!firstTime) {
                firstTime ^= true;
                return Texts.rm.GetString("DECKOBSRECORD", Texts.cultereinfo);
            }
            switch (RecordAction) {
                case RecordingState.Start:
                    return "Start Recording";
                case RecordingState.Stop:
                    return "Stop Recording";
            }
            return Texts.rm.GetString("DECKOBSRECORD", Texts.cultereinfo);
        }

        public override DeckImage GetDefaultItemImage()
        {
            switch (RecordAction) {
                case RecordingState.Start:
                    return new DeckImage(Resources.img_item_start_recording);
                case RecordingState.Stop:
                    return new DeckImage(Resources.img_item_stop_recording);
                default:
                    return base.GetDefaultItemImage();
            }
        }

        public override void OnButtonDown(DeckDevice deckDevice)
        {


 if (OBSUtils.IsConnected) {
                switch (RecordAction) {
                    case RecordingState.Start:
                        OBSUtils.StartRecording();
                        break;
                    case RecordingState.Stop:
                        OBSUtils.StopRecording();
                        break;

                    case RecordingState.Toggle:
                        switch (ToggleAction)
                        {
                            case RecordStateToggle.Stopped:
                                ToggleAction = RecordStateToggle.Recording;
                                OBSUtils.StartRecording();
                                break;
                            case RecordStateToggle.Recording:
                                ToggleAction = RecordStateToggle.Stopped;
                                OBSUtils.StopRecording();
                                break;
                        }



                        break;
                }
            }






        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {
           
        }
    }
}
