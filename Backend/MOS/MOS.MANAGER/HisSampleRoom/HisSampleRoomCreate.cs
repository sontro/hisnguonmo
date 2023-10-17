using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSampleRoom
{
    class HisSampleRoomCreate : BusinessBase
    {
        internal HisSampleRoomCreate()
            : base()
        {

        }

        internal HisSampleRoomCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HisSampleRoomSDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    data.HisRoom.ROOM_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__BP;
                    if (new HisRoomCreate(param).Create(data.HisRoom))
                    {
                        data.HisSampleRoom.ROOM_ID = data.HisRoom.ID;
                        result = this.Create(data.HisSampleRoom);
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

        private bool Create(HIS_SAMPLE_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSampleRoomCheck checker = new HisSampleRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SAMPLE_ROOM_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisSampleRoomDAO.Create(data);
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
    }
}
