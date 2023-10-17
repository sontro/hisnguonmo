using HIS.Desktop.ADO;
using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DepositServiceKiosk.DepositServiceKiosk
{
    class DepositServiceKioskBehavior : Tool<IDesktopToolContext>, IDepositServiceKiosk
    {
        object[] entity;
        internal DepositServiceKioskBehavior()
            : base()
        {

        }

        internal DepositServiceKioskBehavior(CommonParam param, object[] data)
            : base()
        {
            this.entity = data;
        }

        object IDepositServiceKiosk.Run()
        {
            object result = null;
            try
            {
                HIS_CASHIER_ROOM cashierRoom = null;
                long treatmentId = 0;
                Inventec.Desktop.Common.Modules.Module Module = null;
                RefeshReference refeshReference = null;
                DelegateCloseForm_Uc delegateClose = null;

                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is HIS_CASHIER_ROOM)
                        {
                            cashierRoom = (HIS_CASHIER_ROOM)entity[i];
                        }
                        else if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            Module = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        else if (entity[i] is long)
                        {
                            treatmentId = (long)entity[i];
                        }
                        else if (entity[i] is RefeshReference)
                        {
                            refeshReference = (RefeshReference)entity[i];
                        }
                        else if (entity[i] is DelegateCloseForm_Uc)
                        {
                            delegateClose = (DelegateCloseForm_Uc)entity[i];
                        }
                    }
                }

                result = new frmDepositServiceKiosk(Module, treatmentId, cashierRoom, delegateClose);
                if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => cashierRoom), cashierRoom));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
