using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEinvoiceType
{
    partial class HisEinvoiceTypeCreate : BusinessBase
    {
		private List<HIS_EINVOICE_TYPE> recentHisEinvoiceTypes = new List<HIS_EINVOICE_TYPE>();
		
        internal HisEinvoiceTypeCreate()
            : base()
        {

        }

        internal HisEinvoiceTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EINVOICE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEinvoiceTypeCheck checker = new HisEinvoiceTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EINVOICE_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisEinvoiceTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEinvoiceType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEinvoiceType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEinvoiceTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisEinvoiceTypes))
            {
                if (!DAOWorker.HisEinvoiceTypeDAO.TruncateList(this.recentHisEinvoiceTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisEinvoiceType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEinvoiceTypes", this.recentHisEinvoiceTypes));
                }
				this.recentHisEinvoiceTypes = null;
            }
        }
    }
}
