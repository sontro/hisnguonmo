using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashierRoom
{
    class HisCashierRoomCreate : BusinessBase
    {
        internal HisCashierRoomCreate()
            : base()
        {

        }

        internal HisCashierRoomCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HisCashierRoomSDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    data.HisRoom.ROOM_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__TN;
                    if (new HisRoomCreate(param).Create(data.HisRoom))
                    {
                        data.HisCashierRoom.ROOM_ID = data.HisRoom.ID;
                        result = this.Create(data.HisCashierRoom);
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

        private bool Create(HIS_CASHIER_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCashierRoomCheck checker = new HisCashierRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.CASHIER_ROOM_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisCashierRoomDAO.Create(data);
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
