using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEinvoiceType
{
    partial class HisEinvoiceTypeUpdate : BusinessBase
    {
		private List<HIS_EINVOICE_TYPE> beforeUpdateHisEinvoiceTypes = new List<HIS_EINVOICE_TYPE>();
		
        internal HisEinvoiceTypeUpdate()
            : base()
        {

        }

        internal HisEinvoiceTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EINVOICE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEinvoiceTypeCheck checker = new HisEinvoiceTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EINVOICE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.EINVOICE_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisEinvoiceTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEinvoiceType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEinvoiceType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisEinvoiceTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_EINVOICE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEinvoiceTypeCheck checker = new HisEinvoiceTypeCheck(param);
                List<HIS_EINVOICE_TYPE> listRaw = new List<HIS_EINVOICE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EINVOICE_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisEinvoiceTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEinvoiceType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEinvoiceType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisEinvoiceTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEinvoiceTypes))
            {
                if (!DAOWorker.HisEinvoiceTypeDAO.UpdateList(this.beforeUpdateHisEinvoiceTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisEinvoiceType that bai, can kiem tra lai." + LogUtil.TraceData("HisEinvoiceTypes", this.beforeUpdateHisEinvoiceTypes));
                }
				this.beforeUpdateHisEinvoiceTypes = null;
            }
        }
    }
}
