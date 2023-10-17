using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RadixChangeCabinet.RadixChangeCabinet
{
    class RadixChangeCabinetBehavior : Tool<IDesktopToolContext>, IRadixChangeCabinet
    {
        object[] entity;

        internal RadixChangeCabinetBehavior()
            : base()
        {

        }

        internal RadixChangeCabinetBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IRadixChangeCabinet.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                V_HIS_EXP_MEST_4 expMestId = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        else if (entity[i] is V_HIS_EXP_MEST_4)
                        {
                            expMestId = (V_HIS_EXP_MEST_4)entity[i];
                        }
                    }
                }
                if (moduleData != null)
                {
                    var _mediStocks = BackendDataWorker.Get<V_HIS_MEDI_STOCK>().FirstOrDefault(p =>
                        p.ROOM_ID == moduleData.RoomId
                        && p.ROOM_TYPE_ID == moduleData.RoomTypeId);
                    if (_mediStocks != null)
                    {
                        return new frmFormUC(moduleData, expMestId, _mediStocks);
                    }
                }
                else
                    return null;
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
