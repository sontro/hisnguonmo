using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExecuteRoom
{
    class HisExecuteRoomCreate : BusinessBase
    {
        internal HisExecuteRoomCreate()
            : base()
        {

        }

        internal HisExecuteRoomCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HisExecuteRoomSDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    data.HisRoom.ROOM_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__XL;
                    if (new HisRoomCreate(param).Create(data.HisRoom))
                    {
                        data.HisExecuteRoom.ROOM_ID = data.HisRoom.ID;
                        result = this.Create(data.HisExecuteRoom);
                        if (!result)
                        {
                            if (!new HisRoomTruncate(param).Truncate(data.HisRoom))
                            {
                                LogSystem.Warn("Rollback du lieu his_room that bai. Can kiem tra lai. " + LogUtil.TraceData(LogUtil.GetMemberName(() => data.HisRoom), data.HisRoom));
                            }
                        }
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

        private bool Create(HIS_EXECUTE_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExecuteRoomCheck checker = new HisExecuteRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EXECUTE_ROOM_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisExecuteRoomDAO.Create(data);
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

        internal bool CreateList(List<HisExecuteRoomSDO> lisData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExecuteRoomCheck checker = new HisExecuteRoomCheck(param);
                HisRoomCheck roomChecker = new HisRoomCheck(param);
                if (IsNotNullOrEmpty(lisData))
                {
                    lisData.ForEach(o=>o.HisRoom.ROOM_TYPE_ID= IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__XL);
                    foreach (HisExecuteRoomSDO data in lisData)
                    {
                        valid = valid && IsNotNull(data);
                        valid = valid && IsNotNull(data.HisExecuteRoom);
                        valid = valid && IsNotNull(data.HisRoom);
                        valid = valid && roomChecker.VerifyRequireField(data.HisRoom);
                        if (valid)
                        {
                            data.HisExecuteRoom.HIS_ROOM = data.HisRoom;
                            valid = valid && checker.VerifyRequireField(data.HisExecuteRoom);
                            valid = valid && checker.ExistsCode(data.HisExecuteRoom.EXECUTE_ROOM_CODE, null);
                        }
                    }
                    if (valid)
                    {
                        result = DAOWorker.HisExecuteRoomDAO.CreateList(lisData.Select(s => s.HisExecuteRoom).ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
