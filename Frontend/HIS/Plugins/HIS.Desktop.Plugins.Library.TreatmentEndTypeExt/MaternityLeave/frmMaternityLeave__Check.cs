using HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.Data;
using HIS.Desktop.Utility;
using Inventec.Common.Controls.EditorLoader;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.MaternityLeave
{
    public partial class frmMaternityLeave : FormBase
    {
        private bool Check()
        {
            bool result = true;
            try
            {
                List<MaternityLeaveData> maternityLeaveDatas = gridControlMaternityLeave.DataSource as List<MaternityLeaveData>;
                if (maternityLeaveDatas == null && maternityLeaveDatas.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin con!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                MaternityLeaveData maternityLeaveData = maternityLeaveDatas.FirstOrDefault(o => 
                    !o.BornTimeDt.HasValue 
                    && String.IsNullOrEmpty(o.FatherName) 
                    && !o.GenderId.HasValue 
                    && !o.Weight.HasValue);
                if (maternityLeaveData != null)
                {
                    MessageBox.Show("Tồn tại dòng dữ liệu trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
