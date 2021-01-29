using System;
using System.Diagnostics;
using System.Linq;
using BackendAPI.Objects;
using BackendAPI.Sdk;
using BackendAPI.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MyWinFormsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ButtonClickController : ControllerBase
    {
        public ButtonShapes.ButtonAction PerformedAction { get; set; }

        [HttpGet]
        public ActionResult Get(int id,int actionbutton = 1)
        {
            PerformedAction = (ButtonShapes.ButtonAction)actionbutton;

            Debug.WriteLine("ACTION: " + actionbutton + "Button id " + id);
            // DevicePersistManager.GetDeckDeviceFromConnectionGuid(DevicePersistManager.GuidsFromConnections)
            DeckDevice device = null;
            if (Debugger.IsAttached && DevicePersistManager.IsDeviceTest)
            {
                device = DevicePersistManager.DeviceTest;

            }
            else
            {

                device = DevicePersistManager.DeckDevicesFromConnection.FirstOrDefault().Value;

            }

            device.OnButtonInteraction(PerformedAction, id);

            //  return id;
            return Ok();
        }
    }
}