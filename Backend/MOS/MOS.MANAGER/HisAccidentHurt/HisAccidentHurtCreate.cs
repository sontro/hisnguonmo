using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentHurt
{
    partial class HisAccidentHurtCreate : BusinessBase
    {
		private HIS_ACCIDENT_HURT recentHisAccidentHurtDTO = new HIS_ACCIDENT_HURT();
		
        internal HisAccidentHurtCreate()
            : base()
        {

        }

        internal HisAccidentHurtCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ACCIDENT_HURT data)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = null;
                bool valid = true;
                HisAccidentHurtCheck checker = new HisAccidentHurtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && this.HasWorkPlaceInfo(data.EXECUTE_ROOM_ID.Value, ref workPlace);
                if (valid)
                {
                    string year = DateTime.Now.Year.ToString().Substring(2);

                    data.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    data.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    data.EXECUTE_ROOM_ID = workPlace.RoomId;
                    data.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;
                    data.SEEDING_ISSUED_CODE = year;

					if (!DAOWorker.HisAccidentHurtDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentHurt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAccidentHurt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAccidentHurtDTO = data;
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
            if (this.recentHisAccidentHurtDTO != null)
            {
                if (!DAOWorker.HisAccidentHurtDAO.Truncate(this.recentHisAccidentHurtDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentHurt that bai, can kiem tra lai." + LogUtil.TraceData("HisAccidentHurt", this.recentHisAccidentHurtDTO));
                }
                this.recentHisAccidentHurtDTO = null;
            }
        }
    }
}
