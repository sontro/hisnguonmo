using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Packing
{
    class HisPackingCheck : BusinessBase
    {
        internal HisPackingCheck()
            : base()
        {

        }

        internal HisPackingCheck(CommonParam param)
            : base(param)
        {

        }
        internal bool CheckWorkPlace(HisPackingCreateSDO data)
        {
            bool valid = true;
            try
            {
                WorkPlaceSDO sdo = TokenManager.GetWorkPlace(data.RequestRoomId);
                if (sdo == null || !sdo.MediStockId.HasValue || sdo.MediStockId.Value != data.MediStockId)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    throw new Exception("Khong co thong tin phong lam viec. Hoac phong lam viec khong phai la Kho bao che" + LogUtil.TraceData("WorkPlace", sdo));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
