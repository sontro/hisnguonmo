using SAR.MANAGER.Core.SarPrint.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrint
{
    partial class SarPrintUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintUpdate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarPrint.Contains(entity.GetType()))
                {
                    ISarPrintUpdate behavior = SarPrintUpdateBehaviorFactory.MakeISarPrintUpdate(param, entity);
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
