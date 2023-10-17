using SDA.SDO;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaEventLog
{
    partial class SdaEventLogBO : BusinessObjectBase
    {      
        internal bool CreateSDO(SdaEventLogSDO data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaEventLogCreate(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool CreateListSDO(List<SDA.SDO.SdaEventLogSDO> data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaEventLogCreate(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
