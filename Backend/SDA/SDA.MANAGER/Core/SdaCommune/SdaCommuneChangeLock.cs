using SDA.MANAGER.Core.SdaCommune.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommune
{
    partial class SdaCommuneChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaCommuneChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaCommune.Contains(entity.GetType()))
                {
                    ISdaCommuneChangeLock behavior = SdaCommuneChangeLockBehaviorFactory.MakeISdaCommuneChangeLock(param, entity);
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
