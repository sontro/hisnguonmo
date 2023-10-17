using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Dashboard.UCFilter
{
    public partial class UCFilterByDay : UserControl
    {
        public UCFilterByDay()
        {
            InitializeComponent();
        }

        public bool ValidateUC()
        {
            bool result = true;
            try
            {
                if (dtFrom.EditValue == null || dtTo.EditValue == null || cboBuocNhay.EditValue == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (DateTime.Compare(dtFrom.DateTime.Date, dtTo.DateTime.Date) > 0)
                {
                    MessageBox.Show("Thời gian từ lớn thời gian đến", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
    }
}
