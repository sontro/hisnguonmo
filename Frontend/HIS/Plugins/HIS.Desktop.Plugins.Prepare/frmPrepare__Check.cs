using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.BackendData.ADO;
using HIS.Desktop.Plugins.Prepare.ADO;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Common.Controls.EditorLoader;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Prepare
{
    public partial class frmPrepare : FormBase
    {
        private bool CheckValiGridPrepareMetyMaty()
        {
            bool result = true;
            try
            {
                result = result && this.CheckNotNullMPrepareMetyMaty();
                result = result && this.CheckExistDataPrepareMetyMaty();
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private bool CheckNotNullMPrepareMetyMaty()
        {
            bool result = true;
            try
            {
                List<PrepareMetyMatyADO> prepareMetyMatyADOs = gridControlPrepareMety.DataSource as List<PrepareMetyMatyADO>;
                List<PrepareMetyMatyADO> temps = prepareMetyMatyADOs != null ?
                    prepareMetyMatyADOs.Where(o => (o.REQ_AMOUNT??0) > 0 && o.METY_MATY_TYPE_ID > 0).ToList() : null;
                if (temps == null || temps.Count == 0 || prepareMetyMatyADOs.Count != temps.Count)
                {
                    MessageBox.Show("Không tìm thấy thuốc vật tư dự trù hoặc thiếu dữ liệu trường bắt buộc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;

        }

        private bool CheckExistDataPrepareMetyMaty()
        {
            bool result = true;
            try
            {
                List<PrepareMetyMatyADO> vaccinationMetyADOs = gridControlPrepareMety.DataSource as List<PrepareMetyMatyADO>;
                List<PrepareMetyMatyADO> temps = vaccinationMetyADOs != null ?
                    vaccinationMetyADOs.Where(o => o.METY_MATY_TYPE_ID > 0).ToList() : null;
                if (temps != null && temps.Count > 0)
                {
                    var Groups = temps.GroupBy(o => new { o.METY_MATY_TYPE_ID });
                    foreach (var g in Groups)
                    {
                        if (g.Count() > 1)
                        {
                            MessageBox.Show("Tồn tại dữ liệu trùng nhau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
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
