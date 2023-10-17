using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.Plugins.ExpMestBCSCreate.Run;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestBCSCreate.ExpMestBCSCreate
{
    class ExpMestBCSCreateBehavior : Tool<IDesktopToolContext>, IExpMestBCSCreate
    {
        object[] entity;

        internal ExpMestBCSCreateBehavior()
            : base()
        {

        }

        internal ExpMestBCSCreateBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IExpMestBCSCreate.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                    }
                }
                if (moduleData != null)
                {
                    var _mediStocks = BackendDataWorker.Get<V_HIS_MEDI_STOCK>().FirstOrDefault(p => p.ROOM_ID == moduleData.RoomId && p.ROOM_TYPE_ID == moduleData.RoomTypeId && p.IS_CABINET == 1);
                    if (_mediStocks != null)
                    {
                        return new UCExpMestBCSCreate(moduleData, _mediStocks);
                    }
                    else
                    {
                        return new UCExpMestBCSCreate(moduleData, true);
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
