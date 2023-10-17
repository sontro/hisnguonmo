using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserRoom
{
    class HisUserRoomUpdate : BusinessBase
    {
        internal HisUserRoomUpdate()
            : base()
        {

        }

        internal HisUserRoomUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_USER_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserRoomCheck checker = new HisUserRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    HisUserRoomFilterQuery filter = new HisUserRoomFilterQuery();
                    filter.LOGINNAME__EXACT = data.LOGINNAME;
                    filter.ROOM_ID = data.ROOM_ID;
                    filter.ID__NOT_EQUAL = data.ID;
                    List<HIS_USER_ROOM> existed = new HisUserRoomGet().Get(filter);
                    if (IsNotNullOrEmpty(existed))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDaTonTaiTrenHeThong);
                        throw new Exception("Du lieu da ton tai tren he thong." + LogUtil.TraceData("data", data));
                    }
                    result = DAOWorker.HisUserRoomDAO.Update(data);
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

        internal bool UpdateList(List<HIS_USER_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUserRoomCheck checker = new HisUserRoomCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisUserRoomDAO.UpdateList(listData);
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
