using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.OtherForms
{
    public partial class UCOtherForms : UserControl
    {
        Inventec.Desktop.Common.Modules.Module currentModule;
        long treatmentId;

        public UCOtherForms()
        {
            InitializeComponent();
        }

        public UCOtherForms(Inventec.Desktop.Common.Modules.Module currentModule, long treatmentId)
        {
            InitializeComponent();
            try
            {
                this.currentModule = currentModule;
                this.treatmentId = treatmentId;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
