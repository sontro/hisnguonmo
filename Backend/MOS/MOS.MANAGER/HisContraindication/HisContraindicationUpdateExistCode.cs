using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisContraindication
{
    partial class HisContraindicationUpdate : BusinessBase
    {
		private List<HIS_CONTRAINDICATION> beforeUpdateHisContraindications = new List<HIS_CONTRAINDICATION>();
		
        internal HisContraindicationUpdate()
            : base()
        {

        }

        internal HisContraindicationUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CONTRAINDICATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisContraindicationCheck checker = new HisContraindicationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CONTRAINDICATION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.CONTRAINDICATION_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisContraindicationDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisContraindication_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisContraindication that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisContraindications.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_CONTRAINDICATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisContraindicationCheck checker = new HisContraindicationCheck(param);
                List<HIS_CONTRAINDICATION> listRaw = new List<HIS_CONTRAINDICATION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.CONTRAINDICATION_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisContraindicationDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisContraindication_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisContraindication that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisContraindications.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisContraindications))
            {
                if (!DAOWorker.HisContraindicationDAO.UpdateList(this.beforeUpdateHisContraindications))
                {
                    LogSystem.Warn("Rollback du lieu HisContraindication that bai, can kiem tra lai." + LogUtil.TraceData("HisContraindications", this.beforeUpdateHisContraindications));
                }
				this.beforeUpdateHisContraindications = null;
            }
        }
    }
}
