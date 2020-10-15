

using BackendAPI;
using BackendAPI.Objects.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayButtons.Forms.ActionHelperForms
{
    public partial class FolderGlobalHotKey : TemplateForm
    {

        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }

        public DynamicDeckFolder ModifiableAction {
            get { return _modifiableAction; }
            set { _modifiableAction = value;
                modifierKeys = value.KeyGlobalValue.ModifierKeys.ToList();
                nonModifierKeys = value.KeyGlobalValue.Keys.ToList();
                UpdateTextBox();
            }
        }
        public bool ListenToKeys { get; set; } = true;
        public FolderGlobalHotKey()
        {
            InitializeComponent();
            textBox1.Click += (sender, e) => {
                if (!ListenToKeys) {
                    ListenToKeys = true;
                    nonModifierKeys = new List<Keys>();
                    modifierKeys = new List<Keys>();
                    UpdateTextBox();
                }
            };
        }

        List<Keys> modifierKeys;
        List<Keys> nonModifierKeys;
        private DynamicDeckFolder _modifiableAction;

        private void FolderGlobalHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (!ListenToKeys) return;
            List<Keys> modifiers = new List<Keys>();
            List<Keys> nonModifiers = new List<Keys>();
            foreach (Keys r in Enum.GetValues(typeof(Keys))) {
                if (e.Modifiers != Keys.None && e.Modifiers.HasFlag(r))
                    modifiers.Add(r);
                else if (e.KeyCode == (r))
                    nonModifiers.Add(r);
            }
            nonModifierKeys = nonModifiers;
            modifierKeys = modifiers;
            UpdateTextBox();
        }

        private void UpdateTextBox()
        {
            if (modifierKeys != null && nonModifierKeys != null) {
                string value1 = string.Join(" + ", modifierKeys.Where(c => c != Keys.None).Select(c => c.ToString()).OrderBy(c => c));
                string value2 = string.Join(" + ", nonModifierKeys.Where(c => !(c == Keys.ShiftKey || c == Keys.ControlKey || c == Keys.Menu)));
                textBox1.Text = string.IsNullOrEmpty(value1) ? value2 : string.IsNullOrEmpty(value2) ? value1 : (string.Join(" + ", value1, value2));
            }
        }

        private void FolderGlobalHotKey_KeyUp(object sender, KeyEventArgs e)
        {
            /*foreach (Keys r in Enum.GetValues(typeof(Keys))) {
                if (e.Modifiers.HasFlag(r))
                    modifierKeys.Remove(r);
                else
                    nonModifierKeys.Remove(r);
            }
            UpdateTextBox();*/
            ListenToKeys = false;
        }

        private void ModernButton2_Click(object sender, EventArgs e)
        {
            _modifiableAction.KeyGlobalValue = new DynamicDeckFolder.KeyInfoGlobal(modifierKeys.Where(c=>c != Keys.None).ToArray(), nonModifierKeys.Where(c => c != Keys.None).ToArray());
            CloseWithResult(DialogResult.OK);
        }

        private void ModernButton3_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.Cancel);
        }

        private void FolderGlobalHotKey_Load(object sender, EventArgs e)
        {
            appBar1.Text = Texts.rm.GetString("KEYSELECTOR", Texts.cultereinfo);
            label1.Text = Texts.rm.GetString("KEYSELECTOR_KEY", Texts.cultereinfo);
        }
    }
}
