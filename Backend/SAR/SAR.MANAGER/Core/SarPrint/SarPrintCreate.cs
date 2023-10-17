using SAR.MANAGER.Core.SarPrint.Create;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrint
{
    partial class SarPrintCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintCreate(CommonParam param, object data)
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
                    ISarPrintCreate behavior = SarPrintCreateBehaviorFactory.MakeISarPrintCreate(param, entity);
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
