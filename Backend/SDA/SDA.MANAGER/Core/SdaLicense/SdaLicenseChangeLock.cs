using SDA.MANAGER.Core.SdaLicense.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLicense
{
    partial class SdaLicenseChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaLicenseChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaLicense.Contains(entity.GetType()))
                {
                    ISdaLicenseChangeLock behavior = SdaLicenseChangeLockBehaviorFactory.MakeISdaLicenseChangeLock(param, entity);
                    result = behavior != null ? behavior.Run() : false;
                }
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
