using BackendAPI.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BackendAPI.Networking.TcpLib;
using BackendAPI.Sdk;
using BackendAPI.Objects;
using BackendProxy;
using BackendAPI.Events;

namespace WebShard
{

    public sealed class HelloWorldController
    {
      
        private readonly IHttpRequestContext _request;
        public class ModelButtonClick
        {
            public int  buttonid { get; set; }
            public int  actionid { get; set; }

            public ModelButtonClick()
            {

            }

            public ModelButtonClick(int _buttonid,int _actionid)
            {
                buttonid = _buttonid;
                actionid = _actionid;
            }
        }
    
        public ButtonShapes.ButtonAction PerformedAction { get; set; }
        public  HelloWorldController(IHttpRequestContext request)
        {
            _request = request;
          
        }
        public IResponse getButtonImage()
        {
            object result = null;
            Wrapper.events.On("deckitemsinglechangepacket", (e) => {
                // Cast event argrument to your event object
                var obj = (DeckItemSinglePacket)e;
      result = obj;

             
            });
            Wrapper.events.On("deckitemallitemchanged", (e) => {
                // Cast event argrument to your event object
                var obj = (DeckItemChangeAllImagePack)e;

                result = obj;

            });
            Wrapper.events.On("deckitemsingleclearpacket", (e) => {
                // Cast event argrument to your event object
                var obj = (DeckItemClearSinglePacket)e;

                result = obj;

            });
            Wrapper.events.On("deckitemclearallitempacket", (e) => {
                // Cast event argrument to your event object
                var obj = (DeckItemClearAllImagePack)e;

                result = obj;

            });
            return new JsonResponse(result);

        }
        public IResponse ButtonClick(ModelButtonClick model)
        {


            int actionID = model.actionid;
          
                PerformedAction = (ButtonShapes.ButtonAction)actionID;

                int SlotID = model.buttonid;
                Debug.WriteLine("ACTION: " + model.actionid + "Button id " + model.buttonid);
            // DevicePersistManager.GetDeckDeviceFromConnectionGuid(DevicePersistManager.GuidsFromConnections)
            DeckDevice device;
            if (Debugger.IsAttached && DevicePersistManager.IsDeviceTest)
            {   
                device = DevicePersistManager.DeviceTest;
             
            }
            else
            {

                device = DevicePersistManager.DeckDevicesFromConnection.FirstOrDefault().Value;

            }

device.OnButtonInteraction(PerformedAction, SlotID);

            return new RedirectResponse("/");
        }

        public IResponse Post(string model)
        {
        
            return new RedirectResponse("/");
        }
        public IResponse Get()
        {
            //   return new ContentResponse("Hello World!");
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
              //         Debug.WriteLine(path + @"\Content\Index.html");

            return new FileSystemResponse(path + @"\Content\Index.html", "text/html", "utf-8");
        }
        public IResponse JsonTest()
        {
            return new JsonResponse(new { Foo = 123, Bar = new[] { 1, 2, 3 } });
        }
    }
}