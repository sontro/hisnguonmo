using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaProvince
{
    partial class SdaProvinceBO : BusinessObjectBase
    {
        internal bool DeleteWithDelReference(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaProvinceDeleteWithDelReference(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal List<object> GetDynamic(object filter)
        {
            List<object> result = new List<object>();
            try
            {
                IDelegacyT delegacy = new SdaProvinceGetDynamic(param, filter);
                result = delegacy.Execute<List<object>>();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new List<object>();
            }
            return result;
        }
    }
}
