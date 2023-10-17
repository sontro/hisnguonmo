using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEyeSurgryDesc
{
    partial class HisEyeSurgryDescCreate : BusinessBase
    {
		private List<HIS_EYE_SURGRY_DESC> recentHisEyeSurgryDescs = new List<HIS_EYE_SURGRY_DESC>();
		
        internal HisEyeSurgryDescCreate()
            : base()
        {

        }

        internal HisEyeSurgryDescCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EYE_SURGRY_DESC data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEyeSurgryDescCheck checker = new HisEyeSurgryDescCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisEyeSurgryDescDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEyeSurgryDesc_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEyeSurgryDesc that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEyeSurgryDescs.Add(data);
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
		
		internal bool CreateList(List<HIS_EYE_SURGRY_DESC> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEyeSurgryDescCheck checker = new HisEyeSurgryDescCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisEyeSurgryDescDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEyeSurgryDesc_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEyeSurgryDesc that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisEyeSurgryDescs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisEyeSurgryDescs))
            {
                if (!DAOWorker.HisEyeSurgryDescDAO.TruncateList(this.recentHisEyeSurgryDescs))
                {
                    LogSystem.Warn("Rollback du lieu HisEyeSurgryDesc that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEyeSurgryDescs", this.recentHisEyeSurgryDescs));
                }
				this.recentHisEyeSurgryDescs = null;
            }
        }
    }
}
