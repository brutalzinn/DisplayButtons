using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace DisplayButtons.Forms.loading
{
    public partial class loadingusercontrol : UserControl
    {
        public loadingusercontrol(Action worker)
        {
            InitializeComponent();
            if (worker == null)
                throw new ArgumentNullException();
            Worker = worker;
        }
        public Action Worker { get; set; }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Task.Factory.StartNew(Worker).ContinueWith(t => { this.Visible = false; }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        private void loadingusercontrol_Load(object sender, EventArgs e)
        {

        }
    }
}
