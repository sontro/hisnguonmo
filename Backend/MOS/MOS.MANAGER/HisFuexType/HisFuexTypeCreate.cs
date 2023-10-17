using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFuexType
{
    partial class HisFuexTypeCreate : BusinessBase
    {
		private List<HIS_FUEX_TYPE> recentHisFuexTypes = new List<HIS_FUEX_TYPE>();
		
        internal HisFuexTypeCreate()
            : base()
        {

        }

        internal HisFuexTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_FUEX_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFuexTypeCheck checker = new HisFuexTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisFuexTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFuexType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisFuexType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisFuexTypes.Add(data);
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
		
		internal bool CreateList(List<HIS_FUEX_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFuexTypeCheck checker = new HisFuexTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisFuexTypeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFuexType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisFuexType that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisFuexTypes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisFuexTypes))
            {
                if (!DAOWorker.HisFuexTypeDAO.TruncateList(this.recentHisFuexTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisFuexType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisFuexTypes", this.recentHisFuexTypes));
                }
				this.recentHisFuexTypes = null;
            }
        }
    }
}
