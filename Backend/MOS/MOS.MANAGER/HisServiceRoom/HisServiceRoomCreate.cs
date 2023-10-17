using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRoom
{
    class HisServiceRoomCreate : BusinessBase
    {
        internal HisServiceRoomCreate()
            : base()
        {

        }

        internal HisServiceRoomCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceRoomCheck checker = new HisServiceRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    HisServiceRoomFilterQuery filter = new HisServiceRoomFilterQuery();
                    filter.ROOM_ID = data.ROOM_ID;
                    filter.SERVICE_ID = data.SERVICE_ID;
                    var exists = new HisServiceRoomGet().Get(filter);
                    if (IsNotNullOrEmpty(exists))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDaTonTaiTrenHeThong);
                        throw new Exception("Du lieu da ton tai tren he thong" + LogUtil.TraceData("data", data));
                    }
                    result = DAOWorker.HisServiceRoomDAO.Create(data);
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

        internal bool CreateList(List<HIS_SERVICE_ROOM> listData)
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
                }
                if (valid)
                {
                    result = DAOWorker.HisServiceRoomDAO.CreateList(listData);
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
