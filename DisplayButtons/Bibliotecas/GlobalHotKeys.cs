

using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Bibliotecas.Helpers;
using Backend.GlobalHotkeys;
using WebSocketSharp;

namespace DisplayButtons
{
    public class DeckHotkeys
    {



        public static void UpdateAllFoldersHotkeys(DeckDevice device)
        {

            DeckHelpers.ListFolders(device.CurrentProfile.Mainfolder as DynamicDeckFolder).ForEach(e =>
            {
                if (!e.UniqueID.IsNullOrEmpty())
                {
                    refreshFolder(e);
                }

            });

        }                
                
      


    }
}
