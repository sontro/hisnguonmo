using SDA.MANAGER.Core.SdaLicense.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLicense
{
    partial class SdaLicenseUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaLicenseUpdate(CommonParam param, object data)
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
                    ISdaLicenseUpdate behavior = SdaLicenseUpdateBehaviorFactory.MakeISdaLicenseUpdate(param, entity);
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
