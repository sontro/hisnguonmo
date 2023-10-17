using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAllergyCard
{
    partial class HisAllergyCardCreate : BusinessBase
    {
		private List<HIS_ALLERGY_CARD> recentHisAllergyCards = new List<HIS_ALLERGY_CARD>();
		
        internal HisAllergyCardCreate()
            : base()
        {

        }

        internal HisAllergyCardCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ALLERGY_CARD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAllergyCardCheck checker = new HisAllergyCardCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisAllergyCardDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAllergyCard_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAllergyCard that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAllergyCards.Add(data);
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
		
		internal bool CreateList(List<HIS_ALLERGY_CARD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAllergyCardCheck checker = new HisAllergyCardCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAllergyCardDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAllergyCard_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAllergyCard that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAllergyCards.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisAllergyCards))
            {
                if (!DAOWorker.HisAllergyCardDAO.TruncateList(this.recentHisAllergyCards))
                {
                    LogSystem.Warn("Rollback du lieu HisAllergyCard that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAllergyCards", this.recentHisAllergyCards));
                }
				this.recentHisAllergyCards = null;
            }
        }
    }
}
