using DisplayButtons.Backend.Utils;
using System.Windows.Forms;

namespace DisplayButtons.Backend.Objects
{
    public class ProfileDeckHelper
    {

        public class GlobalPerfilBox

        {
            public string Text { get; set; }
            public Profile Value { get; set; }

            public override string ToString()
            {
                return Text;
            }

        }
     
            
       
    

    }
   public static class StaticHelper{

 public static void SelectItemByValue(this ComboBox cbo, object value)
        {
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                var prop = cbo.Items[i].GetType().GetProperty(cbo.ValueMember);
                if (prop != null && prop.GetValue(cbo.Items[i], null) == value)
                {
                    cbo.SelectedIndex = i;
                    break;
                }
            }
        }




}



}
