using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCare
{
    partial class HisCareCreate : BusinessBase
    {
        private HIS_CARE recentHisCare;

        internal HisCareCreate()
            : base()
        {

        }

        internal HisCareCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CARE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareCheck checker = new HisCareCheck(param);
                if (data != null && String.IsNullOrWhiteSpace(data.EXECUTE_LOGINNAME))
                {
                    data.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    data.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                }
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ValidateData(data);
                if (valid)
                {
                    data.HIS_DHST = null;
                    if (data.EXECUTE_TIME.HasValue)
                    {
                        data.EXECUTE_DATE = data.EXECUTE_TIME.Value - (data.EXECUTE_TIME.Value % 1000000);
                    }
                    long roomId = data.EXECUTE_ROOM_ID.HasValue ? data.EXECUTE_ROOM_ID.Value : 0;//ho tro his4 ko truyen len room_id

                    WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(roomId);
                    if (workPlace == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                        throw new Exception("Khong co thong tin workplace.");
                    }
                    data.EXECUTE_ROOM_ID = workPlace.RoomId;
                    data.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;

                    List<HIS_DHST> lstDhst = data.HIS_DHST1 != null ? data.HIS_DHST1.ToList() : null;

                    if (IsNotNullOrEmpty(lstDhst))
                    {
                        lstDhst.ForEach(f =>
                        {
                            f.TREATMENT_ID = data.TREATMENT_ID;
                            f.EXECUTE_DEPARTMENT_ID = data.EXECUTE_DEPARTMENT_ID;
                            f.EXECUTE_LOGINNAME = data.EXECUTE_LOGINNAME;
                            f.EXECUTE_ROOM_ID = data.EXECUTE_ROOM_ID;
                            f.EXECUTE_USERNAME = data.EXECUTE_USERNAME;
                        });
                    }

                    if (!DAOWorker.HisCareDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCare_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCare that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCare = data;
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
            if (this.recentHisCare != null)
            {
                if (!DAOWorker.HisCareDAO.Truncate(this.recentHisCare))
                {
                    LogSystem.Warn("Rollback du lieu HisCare that bai, can kiem tra lai." + LogUtil.TraceData("HisCare", this.recentHisCare));
                }
            }
        }
    }
}
