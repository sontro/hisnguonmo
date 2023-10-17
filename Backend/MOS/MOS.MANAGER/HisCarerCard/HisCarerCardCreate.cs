using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCarerCard
{
    partial class HisCarerCardCreate : BusinessBase
    {
		private List<HIS_CARER_CARD> recentHisCarerCards = new List<HIS_CARER_CARD>();
		
        internal HisCarerCardCreate()
            : base()
        {

        }

        internal HisCarerCardCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CARER_CARD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCarerCardCheck checker = new HisCarerCardCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyDuplicate(data);
                if (valid)
                {
					if (!DAOWorker.HisCarerCardDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCarerCard_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCarerCard that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCarerCards.Add(data);
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
		
		internal bool CreateList(List<HIS_CARER_CARD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCarerCardCheck checker = new HisCarerCardCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.VerifyDuplicate(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisCarerCardDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCarerCard_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCarerCard that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisCarerCards.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisCarerCards))
            {
                if (!DAOWorker.HisCarerCardDAO.TruncateList(this.recentHisCarerCards))
                {
                    LogSystem.Warn("Rollback du lieu HisCarerCard that bai, can kiem tra lai." + LogUtil.TraceData("recentHisCarerCards", this.recentHisCarerCards));
                }
				this.recentHisCarerCards = null;
            }
        }
    }
}
