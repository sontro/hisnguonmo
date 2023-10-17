using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDhst
{
    class HisDhstCreate : BusinessBase
    {
        private List<HIS_DHST> recentDatas = new List<HIS_DHST>();

        internal HisDhstCreate()
            : base()
        {

        }

        internal HisDhstCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DHST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDhstCheck checker = new HisDhstCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    long roomId = data.EXECUTE_ROOM_ID.HasValue ? data.EXECUTE_ROOM_ID.Value : 0;//ho tro his4 ko truyen len room_id
                    WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(roomId);

                    if (workPlace == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                        throw new Exception("Khong co thong tin workplace.");
                    }
                    data.EXECUTE_ROOM_ID = workPlace.RoomId;
                    data.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;

                    data.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    data.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    result = DAOWorker.HisDhstDAO.Create(data);
                    if (result)
                    {
                        this.recentDatas.Add(data);
                    }
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

        internal bool CreateList(List<HIS_DHST> lstData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDhstCheck checker = new HisDhstCheck(param);
                foreach (HIS_DHST data in lstData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisDhstDAO.CreateList(lstData);
                    if (result)
                    {
                        this.recentDatas.AddRange(lstData);
                    }
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
            if (IsNotNullOrEmpty(this.recentDatas))
            {
                if (!DAOWorker.HisDhstDAO.TruncateList(this.recentDatas))
                {
                    LogSystem.Warn("Rollback du lieu HisDhst that bai, can kiem tra lai." + LogUtil.TraceData("HisDhst", this.recentDatas));
                }
            }
        }
    }
}
