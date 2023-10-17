using Inventec.Core;
using SDA.MANAGER.Core.SdaLicense.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDA.MANAGER.Core.SdaLicense
{
    class SdaLicenseUpdateT : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaLicenseUpdateT(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (TypeCollection.SdaLicense.Contains(typeof(T)))
                {
                    ISdaLicenseUpdate behavior = SdaLicenseUpdateBehaviorFactory.MakeISdaLicenseUpdate(param, entity);
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
