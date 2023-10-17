using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaConfigAppUser
{
    class SdaConfigAppUserCopy : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaConfigAppUserCopy(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (TypeCollection.SdaConfigAppUser.Contains(entity.GetType()))
                {
                    Copy.ISdaConfigAppUserCopy behavior = Copy.SdaConfigAppUserCopyFactory.MakeISarUserReportTypeCopy(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = default(T);
            }
            return result;
        }
    }
}
