using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.MultiDate
{
    public partial class UCMultiDate : UserControl
    {
        DateTime? data;
        public UCMultiDate()
        {
            InitializeComponent();
        }
        public UCMultiDate(DateTime? oldValue)
        {
            try
            {
                InitializeComponent();
                this.data = oldValue;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void UCMultiDate_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.data != null && this.data.Value != DateTime.MinValue)
                {
                    dtIntructionTime.EditValue = this.data;
                }
                else
                {
                    dtIntructionTime.EditValue = null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        internal DateTime? GetValue()
        {
            try
            {
                if (dtIntructionTime.DateTime != DateTime.MinValue)
                {
                    return dtIntructionTime.DateTime;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return null;
        }
    }
}
