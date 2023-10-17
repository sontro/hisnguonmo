using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Controls;
using Inventec.Core;
using MOS.Filter;
using Inventec.Common.Adapter;
using HIS.Desktop.ApiConsumer;

namespace HIS.Desktop.Plugins.EnterKskInfomantionVer2.Run
{
    public partial class frmEnterKskInfomantionVer2
    {
        private void FillDataPageKSKOther()
        {
            try
            {
                ResetControlKSKOther();
                FillDataKSKOther();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void FillDataKSKOther()
        {
            try
            {
                if (currentServiceReq != null)
                {
                    CommonParam param = new CommonParam();
                    HisKskOtherFilter filter = new HisKskOtherFilter();
                    filter.SERVICE_REQ_ID = currentServiceReq.ID;
                    var data = new BackendAdapter(param).Get<List<HIS_KSK_OTHER>>("api/HisKskOther/Get", ApiConsumers.MosConsumer, filter, param);
                    if (data != null && data.Count > 0)
                    {
                        this.currentKskOther = data.First();

                        chkKSKType1.Checked = currentKskOther.KSK_TYPE == 1;
                        chkKSKType2.Checked = currentKskOther.KSK_TYPE == 2;
                        txtConclude6.Text = currentKskOther.CONCLUDE;
                    }
                    else
                    {
                        ResetControlKSKOther();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ResetControlKSKOther()
        {
            try
            {
                chkKSKType1.Checked = false;
                chkKSKType2.Checked = false;
                txtConclude6.Text = "";
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private HIS_KSK_OTHER GetValueKSKOther()
        {
            HIS_KSK_OTHER obj = new HIS_KSK_OTHER();
            try
            {
                if (chkKSKType1.Checked)
                    obj.KSK_TYPE = 1;
                else if (chkKSKType2.Checked)
                    obj.KSK_TYPE = 2;
                obj.CONCLUDE = txtConclude6.Text;
                
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return obj;
        }
    }
}
