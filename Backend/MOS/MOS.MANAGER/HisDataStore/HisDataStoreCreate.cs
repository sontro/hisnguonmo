using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDataStore
{
    class HisDataStoreCreate : BusinessBase
    {
        internal HisDataStoreCreate()
            : base()
        {

        }

        internal HisDataStoreCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HisDataStoreSDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    data.HisRoom.ROOM_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__LT;
                    if (new HisRoomCreate(param).Create(data.HisRoom))
                    {
                        data.HisDataStore.ROOM_ID = data.HisRoom.ID;
                        result = this.Create(data.HisDataStore);
                        if (!result)
                        {
                            if (!new HisRoomTruncate(param).Truncate(data.HisRoom))
                            {
                                LogSystem.Warn("Rollback du lieu his_room that bai. Can kiem tra lai. " + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data.HisRoom), data.HisRoom));
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

        private bool Create(HIS_DATA_STORE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDataStoreCheck checker = new HisDataStoreCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DATA_STORE_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisDataStoreDAO.Create(data);
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
