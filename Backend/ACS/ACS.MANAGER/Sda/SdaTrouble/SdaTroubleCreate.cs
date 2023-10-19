using ACS.MANAGER.Base;
using Inventec.Common.Logging;
using Inventec.Core;
using System;

namespace ACS.MANAGER.SdaTrouble
{
    class SdaTroubleCreate  : Inventec.Backend.MANAGER.BusinessBase
    {
        internal SdaTroubleCreate()
            : base()
        {

        }

        internal SdaTroubleCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(string message)
        {
            try
            {
                return TroubleCache.Add(message);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
