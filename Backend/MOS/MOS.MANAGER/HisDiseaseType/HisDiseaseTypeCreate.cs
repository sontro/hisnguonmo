using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDiseaseType
{
    partial class HisDiseaseTypeCreate : BusinessBase
    {
		private List<HIS_DISEASE_TYPE> recentHisDiseaseTypes = new List<HIS_DISEASE_TYPE>();
		
        internal HisDiseaseTypeCreate()
            : base()
        {

        }

        internal HisDiseaseTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DISEASE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDiseaseTypeCheck checker = new HisDiseaseTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisDiseaseTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDiseaseType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDiseaseType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDiseaseTypes.Add(data);
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
		
		internal bool CreateList(List<HIS_DISEASE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDiseaseTypeCheck checker = new HisDiseaseTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisDiseaseTypeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDiseaseType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDiseaseType that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisDiseaseTypes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisDiseaseTypes))
            {
                if (!DAOWorker.HisDiseaseTypeDAO.TruncateList(this.recentHisDiseaseTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisDiseaseType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDiseaseTypes", this.recentHisDiseaseTypes));
                }
				this.recentHisDiseaseTypes = null;
            }
        }
    }
}
