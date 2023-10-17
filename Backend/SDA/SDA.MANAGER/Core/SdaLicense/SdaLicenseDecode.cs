using Inventec.Core;
using SDA.MANAGER.Core.SdaLicense.Decode;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDA.MANAGER.Core.SdaLicense
{
    class SdaLicenseDecode : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaLicenseDecode(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(SdaLicenseSDO))
                {
                    ISdaLicenseDecode behavior = SdaLicenseDecodeBehaviorFactory.MakeISdaLicenseCreate(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = default(T);
            }
            return result;
        }
    }
}
