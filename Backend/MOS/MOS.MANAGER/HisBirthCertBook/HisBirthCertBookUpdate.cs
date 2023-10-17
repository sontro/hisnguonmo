using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBirthCertBook
{
    partial class HisBirthCertBookUpdate : BusinessBase
    {
		private List<HIS_BIRTH_CERT_BOOK> beforeUpdateHisBirthCertBooks = new List<HIS_BIRTH_CERT_BOOK>();
		
        internal HisBirthCertBookUpdate()
            : base()
        {

        }

        internal HisBirthCertBookUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BIRTH_CERT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBirthCertBookCheck checker = new HisBirthCertBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BIRTH_CERT_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisBirthCertBookDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBirthCertBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBirthCertBook that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisBirthCertBooks.Add(raw);
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

        internal bool UpdateList(List<HIS_BIRTH_CERT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBirthCertBookCheck checker = new HisBirthCertBookCheck(param);
                List<HIS_BIRTH_CERT_BOOK> listRaw = new List<HIS_BIRTH_CERT_BOOK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisBirthCertBookDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBirthCertBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBirthCertBook that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisBirthCertBooks.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBirthCertBooks))
            {
                if (!DAOWorker.HisBirthCertBookDAO.UpdateList(this.beforeUpdateHisBirthCertBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisBirthCertBook that bai, can kiem tra lai." + LogUtil.TraceData("HisBirthCertBooks", this.beforeUpdateHisBirthCertBooks));
                }
				this.beforeUpdateHisBirthCertBooks = null;
            }
        }
    }
}
