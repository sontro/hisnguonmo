using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Core;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Common.Adapter;
using MOS.SDO;
using HIS.Desktop.ApiConsumer;
using DevExpress.Data;
using System.Collections;
using DevExpress.XtraGrid.Views.Base;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Utility;
using MOS.EFMODEL.DataModels;
using DevExpress.XtraEditors.Repository;

namespace HIS.Desktop.Plugins.BedRoomPartial
{
    public partial class UCBedRoomPartial : UserControlBase
    {
        //Phím tắt
        public void BangKe()
        {
            try
            {
                if (btnBangKe.Enabled == false)
                    return;
                btnBangKe_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public void ChiDinhDichVu()
        {
            try
            {
                if (btnChiDinhDichVu.Enabled == false)
                    return;
                btnChiDinhDichVu_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public void KeDonThuoc()
        {
            try
            {
                if (btnKeDonThuoc.Enabled == false)
                    return;
                btnKeDonThuoc_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public void KetThucDieuTri()
        {
            try
            {
                if (btnKetThucDieuTri.Enabled == false)
                    return;
                btnKetThucDieuTri_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
