using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDeathCertBook
{
    partial class HisDeathCertBookUpdate : BusinessBase
    {
		private List<HIS_DEATH_CERT_BOOK> beforeUpdateHisDeathCertBooks = new List<HIS_DEATH_CERT_BOOK>();
		
        internal HisDeathCertBookUpdate()
            : base()
        {

        }

        internal HisDeathCertBookUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DEATH_CERT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDeathCertBookCheck checker = new HisDeathCertBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DEATH_CERT_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisDeathCertBookDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDeathCertBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDeathCertBook that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisDeathCertBooks.Add(raw);
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

        internal bool UpdateList(List<HIS_DEATH_CERT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDeathCertBookCheck checker = new HisDeathCertBookCheck(param);
                List<HIS_DEATH_CERT_BOOK> listRaw = new List<HIS_DEATH_CERT_BOOK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisDeathCertBookDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDeathCertBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDeathCertBook that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisDeathCertBooks.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDeathCertBooks))
            {
                if (!DAOWorker.HisDeathCertBookDAO.UpdateList(this.beforeUpdateHisDeathCertBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisDeathCertBook that bai, can kiem tra lai." + LogUtil.TraceData("HisDeathCertBooks", this.beforeUpdateHisDeathCertBooks));
                }
				this.beforeUpdateHisDeathCertBooks = null;
            }
        }
    }
}
