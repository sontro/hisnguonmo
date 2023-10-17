using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAllergenic
{
    partial class HisAllergenicCreate : BusinessBase
    {
		private List<HIS_ALLERGENIC> recentHisAllergenics = new List<HIS_ALLERGENIC>();
		
        internal HisAllergenicCreate()
            : base()
        {

        }

        internal HisAllergenicCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ALLERGENIC data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAllergenicCheck checker = new HisAllergenicCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisAllergenicDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAllergenic_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAllergenic that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAllergenics.Add(data);
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
		
		internal bool CreateList(List<HIS_ALLERGENIC> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAllergenicCheck checker = new HisAllergenicCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAllergenicDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAllergenic_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAllergenic that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAllergenics.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisAllergenics))
            {
                if (!DAOWorker.HisAllergenicDAO.TruncateList(this.recentHisAllergenics))
                {
                    LogSystem.Warn("Rollback du lieu HisAllergenic that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAllergenics", this.recentHisAllergenics));
                }
				this.recentHisAllergenics = null;
            }
        }
    }
}
