using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskContract
{
    partial class HisKskContractCreate : BusinessBase
    {
		private HIS_KSK_CONTRACT recentHisKskContract;
		
        internal HisKskContractCreate()
            : base()
        {

        }

        internal HisKskContractCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(KsKContractSDO data, ref HIS_KSK_CONTRACT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HisKskContractCheck checker = new HisKskContractCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.KskContract.KSK_CONTRACT_CODE, null);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                if (valid)
                {
                    data.KskContract.DEPARTMENT_ID = workPlace.DepartmentId;
					if (!DAOWorker.HisKskContractDAO.Create(data.KskContract))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskContract_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskContract that bai." + LogUtil.TraceData("data", data.KskContract));
                    }
                    this.recentHisKskContract = data.KskContract;
                    resultData = data.KskContract;
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
            if (this.recentHisKskContract != null)
            {
                if (!new HisKskContractTruncate(param).Truncate(this.recentHisKskContract))
                {
                    LogSystem.Warn("Rollback du lieu HisKskContract that bai, can kiem tra lai." + LogUtil.TraceData("HisKskContract", this.recentHisKskContract));
                }
            }
        }
    }
}
