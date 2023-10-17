using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSesePtttMethod
{
    partial class HisSesePtttMethodCreate : BusinessBase
    {
		private List<HIS_SESE_PTTT_METHOD> recentHisSesePtttMethods = new List<HIS_SESE_PTTT_METHOD>();
		
        internal HisSesePtttMethodCreate()
            : base()
        {

        }

        internal HisSesePtttMethodCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SESE_PTTT_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSesePtttMethodCheck checker = new HisSesePtttMethodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisSesePtttMethodDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSesePtttMethod_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSesePtttMethod that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSesePtttMethods.Add(data);
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
		
		internal bool CreateList(List<HIS_SESE_PTTT_METHOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSesePtttMethodCheck checker = new HisSesePtttMethodCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSesePtttMethodDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSesePtttMethod_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSesePtttMethod that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSesePtttMethods.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSesePtttMethods))
            {
                if (!DAOWorker.HisSesePtttMethodDAO.TruncateList(this.recentHisSesePtttMethods))
                {
                    LogSystem.Warn("Rollback du lieu HisSesePtttMethod that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSesePtttMethods", this.recentHisSesePtttMethods));
                }
				this.recentHisSesePtttMethods = null;
            }
        }
    }
}
