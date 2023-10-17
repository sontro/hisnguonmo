using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRoom
{
    class HisServiceRoomUpdate : BusinessBase
    {
        internal HisServiceRoomUpdate()
            : base()
        {

        }

        internal HisServiceRoomUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceRoomCheck checker = new HisServiceRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    HisServiceRoomFilterQuery filter = new HisServiceRoomFilterQuery();
                    filter.ROOM_ID = data.ROOM_ID;
                    filter.SERVICE_ID = data.SERVICE_ID;
                    filter.ID__NOT_EQUAL = data.ID;
                    var exists = new HisServiceRoomGet().Get(filter);
                    if (IsNotNullOrEmpty(exists))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDaTonTaiTrenHeThong);
                        throw new Exception("Du lieu da ton tai tren he thong" + LogUtil.TraceData("data", data));
                    }
                    result = DAOWorker.HisServiceRoomDAO.Update(data);
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

        internal bool UpdateList(List<HIS_SERVICE_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceRoomCheck checker = new HisServiceRoomCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisServiceRoomDAO.UpdateList(listData);
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
