using SDA.MANAGER.Core.SdaSql.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSql
{
    partial class SdaSqlCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaSqlCreate(CommonParam param, object data)
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
                    ISdaSqlCreate behavior = SdaSqlCreateBehaviorFactory.MakeISdaSqlCreate(param, entity);
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
