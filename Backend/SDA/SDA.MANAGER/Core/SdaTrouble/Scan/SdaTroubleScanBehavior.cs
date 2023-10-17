using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaTrouble.Scan
{
    class SdaTroubleScanBehavior : BeanObjectBase, ISdaTroubleScan
    {
        internal SdaTroubleScanBehavior(CommonParam param)
            : base(param)
        {
        }

        bool ISdaTroubleScan.Run()
        {
            bool result = false;
            try
            {

                List<string> listTrouble = TroubleCache.GetAndClear();
                if (!new SdaTroubleBO().CreateByMessage(listTrouble))
                {
                    Logging("Khong tao duoc trouble he thong." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listTrouble), listTrouble), LogType.Error);
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
