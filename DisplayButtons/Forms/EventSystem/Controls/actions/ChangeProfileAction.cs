using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DisplayButtons.Bibliotecas.DeckEvents.Actions;
using DisplayButtons.Bibliotecas.DeckEvents;
using DisplayButtons.Backend.Utils;
using System.Linq;
using DisplayButtons.Backend.Objects;

namespace DisplayButtons.Forms.EventSystem.Controls.actions
{
    public partial class ChangeProfileAction : PanelControl
    {
        public ChangeProfile window;
        public ChangeProfileAction(ChangeProfile value)
        {
            InitializeComponent();
            if (value != null)
            {
                window = value;
                if (value.profile != null)
                {
                    ProfileStaticHelper.SelectItemByValue(comboBox1, value.profile);
                  //  comboBox1.SelectedItem = value.profile;
                }

            }
            loadProfiles();
        }
        public void loadProfiles()
        {
            foreach (var perfil in DevicePersistManager.PersistedDevices.ToList())
            {

                foreach (var list in perfil.profiles)
                {
                    ProfileVoidHelper.GlobalPerfilBox teste = new ProfileVoidHelper.GlobalPerfilBox();
                    teste.Text = list.Name;
                    teste.Value = list;
                    comboBox1.Items.Add(teste);


                }
            }

        }
        public ProfileVoidHelper.GlobalPerfilBox CurrentPerfil { get; set; }
        public override void SaveConfig()
        {

            if (window != null)
            {


                CurrentPerfil = (ProfileVoidHelper.GlobalPerfilBox)comboBox1.SelectedItem;
                window.profile = CurrentPerfil.Value;

                //     window.AppName = textBox1.Text;
            }

        }
        private void ChangeProfileAction_Load(object sender, EventArgs e)
        {

        }
        public override UserControl getControl { get => this; }
        public override AbstractAction getClassImplementAction { get => window; }
    }
}
