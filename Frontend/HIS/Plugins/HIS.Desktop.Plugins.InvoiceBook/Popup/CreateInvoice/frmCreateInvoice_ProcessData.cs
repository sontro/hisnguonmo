using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.ApiConsumer;
using Inventec.Common.Adapter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.SDO;

namespace HIS.Desktop.Plugins.InvoiceBook
{
    public partial class frmCreateInvoice
    {
        #region Get_Data

        internal List<HIS_BRANCH> BranchGetData()
        {
            List<HIS_BRANCH> result;
            try
            {
                var paramCommon = new CommonParam();
                result = new BackendAdapter(paramCommon).Get<List<HIS_BRANCH>>
                    (ApiConsumer.HisRequestUriStore.HIS_BRANCH_GET, ApiConsumers.MosConsumer, new HisBranchFilter(), paramCommon);
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
        internal List<HIS_PAY_FORM> PayFormsGetData()
        {
            List<HIS_PAY_FORM> result;
            try
            {
                var paramCommon = new CommonParam();
                result = new BackendAdapter(paramCommon).Get<List<HIS_PAY_FORM>>
                    (ApiConsumer.HisRequestUriStore.HIS_PAY_FORM_GET, ApiConsumers.MosConsumer, new HisPayFormFilter(), paramCommon);
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
        #endregion

        #region Update_Data

        #endregion

        #region Create_Data
        internal HIS_INVOICE InvoiceCreateData(HisInvoiceSDO invoiceSdo)
        {
            HIS_INVOICE result;
            try
            {
                var paramCommon = new CommonParam();
                result = new BackendAdapter(paramCommon).Post<HIS_INVOICE>
                    (ApiConsumer.HisRequestUriStore.HIS_INVOICE__CREATE, ApiConsumers.MosConsumer, invoiceSdo, paramCommon);
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        
        #endregion

        #region Delete_Data

        #endregion
    }
}
