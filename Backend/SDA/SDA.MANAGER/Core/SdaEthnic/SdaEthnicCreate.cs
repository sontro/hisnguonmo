using SDA.MANAGER.Core.SdaEthnic.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEthnic
{
    partial class SdaEthnicCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaEthnicCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaEthnic.Contains(entity.GetType()))
                {
                    ISdaEthnicCreate behavior = SdaEthnicCreateBehaviorFactory.MakeISdaEthnicCreate(param, entity);
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
