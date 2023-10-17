using SDA.MANAGER.Core.SdaCommuneMap.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommuneMap
{
    partial class SdaCommuneMapUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaCommuneMapUpdate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaCommuneMap.Contains(entity.GetType()))
                {
                    ISdaCommuneMapUpdate behavior = SdaCommuneMapUpdateBehaviorFactory.MakeISdaCommuneMapUpdate(param, entity);
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
