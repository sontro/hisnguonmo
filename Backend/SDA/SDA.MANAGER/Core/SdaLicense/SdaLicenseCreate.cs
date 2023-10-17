using SDA.MANAGER.Core.SdaLicense.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLicense
{
    partial class SdaLicenseCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaLicenseCreate(CommonParam param, object data)
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
                    ISdaLicenseCreate behavior = SdaLicenseCreateBehaviorFactory.MakeISdaLicenseCreate(param, entity);
                    result = behavior != null ? (bool)behavior.Run() : false;
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
