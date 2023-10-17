using SDA.MANAGER.Core.SdaSql.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSql
{
    partial class SdaSqlUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaSqlUpdate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaSql.Contains(entity.GetType()))
                {
                    ISdaSqlUpdate behavior = SdaSqlUpdateBehaviorFactory.MakeISdaSqlUpdate(param, entity);
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
