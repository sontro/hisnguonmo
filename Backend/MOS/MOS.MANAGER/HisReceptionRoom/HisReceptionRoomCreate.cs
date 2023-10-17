using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisReceptionRoom
{
    class HisReceptionRoomCreate : BusinessBase
    {
        internal HisReceptionRoomCreate()
            : base()
        {

        }

        internal HisReceptionRoomCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HisReceptionRoomSDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    if (new HisRoomCreate(param).Create(data.HisRoom))
                    {
                        data.HisReceptionRoom.ROOM_ID = data.HisRoom.ID;
                        result = this.Create(data.HisReceptionRoom);
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

        private bool Create(HIS_RECEPTION_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisReceptionRoomCheck checker = new HisReceptionRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.RECEPTION_ROOM_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisReceptionRoomDAO.Create(data);
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
