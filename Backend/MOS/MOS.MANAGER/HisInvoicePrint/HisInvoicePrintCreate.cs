using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInvoicePrint
{
    partial class HisInvoicePrintCreate : BusinessBase
    {
		private List<HIS_INVOICE_PRINT> recentHisInvoicePrints = new List<HIS_INVOICE_PRINT>();
		
        internal HisInvoicePrintCreate()
            : base()
        {

        }

        internal HisInvoicePrintCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_INVOICE_PRINT data)
        {
            bool result = false;
            try
            {
                bool valid = true;

                data.LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                data.USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                data.PRINT_TIME = Inventec.Common.DateTime.Get.Now().Value;

                HisInvoicePrintCheck checker = new HisInvoicePrintCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisInvoicePrintDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInvoicePrint_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisInvoicePrint that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisInvoicePrints.Add(data);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisInvoicePrints))
            {
                if (!new HisInvoicePrintTruncate(param).TruncateList(this.recentHisInvoicePrints))
                {
                    LogSystem.Warn("Rollback du lieu HisInvoicePrint that bai, can kiem tra lai." + LogUtil.TraceData("recentHisInvoicePrints", this.recentHisInvoicePrints));
                }
            }
        }
    }
}
