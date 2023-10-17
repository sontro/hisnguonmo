using SDA.MANAGER.Core.SdaLicense.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLicense
{
    partial class SdaLicenseDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaLicenseDelete(CommonParam param, object data)
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
                    ISdaLicenseDelete behavior = SdaLicenseDeleteBehaviorFactory.MakeISdaLicenseDelete(param, entity);
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
