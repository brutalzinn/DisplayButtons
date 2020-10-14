
using Properties;
using Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Backend.Objects.Implementation.DeckActions.OBS
{
    public class StartStopRecordingAction : AbstractDeckAction, IDeckHelper
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
        int CurrentItem = 1;
        IDeckItem atual_item;
        [ActionPropertyInclude]
        [ActionPropertyDescription("DESCRIPTIONACTION")]
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
            switch (RecordAction)
            {
                case RecordingState.Toggle:
               
                    return true;

                default:
                    return false;

            }
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
                              IDeckHelper.setVariable(false, atual_item, deckDevice);
                                break;
                            case RecordStateToggle.Recording:
                                ToggleAction = RecordStateToggle.Stopped;
                               
                                OBSUtils.StopRecording();
                                IDeckHelper.setVariable(true, atual_item, deckDevice);
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
