using HIS.Desktop.ADO;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisTranPatiTemp
{
    class HisTranPatiTempBehavior : Tool<IDesktopToolContext>, IHisTranPatiTemp
    {
        object[] entity;

        Inventec.Desktop.Common.Modules.Module moduleData = null;
        HIS_TRAN_PATI_TEMP tranPatiTemp = null;
        internal HisTranPatiTempBehavior()
            : base()
        {

        }

        internal HisTranPatiTempBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IHisTranPatiTemp.Run()
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        if (item is HIS_TRAN_PATI_TEMP)
                            tranPatiTemp = (HIS_TRAN_PATI_TEMP)item;
                    }
                }
                if (moduleData != null && tranPatiTemp != null)
                {
                    return new frmHisTranPatiTemp(moduleData, tranPatiTemp);
                }
                else if (moduleData != null)
                {
                    return new frmHisTranPatiTemp(moduleData, null);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
