using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.RepayService.RepayService;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.RepayService.RepayService
{
    class RepayServiceBehavior : BusinessBase, IRepayService
    {
        object[] entity;
        internal RepayServiceBehavior(CommonParam param, object[] filter)
            : base()
        {
            try
            {
                entity = filter;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        object IRepayService.Run()
        {
            try
            {
                V_HIS_TREATMENT_FEE hisTreatment = null;
                MOS.SDO.HisTransactionRepaySDO repaySdo = null;
                SendResultToOtherForm sendResultToOtherForm = null;
                long? branchId = null;
                long cashierRoomId = 0;
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                List<V_HIS_SERE_SERV_5> ListSereServ = null;

                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }

                            if (entity[i] is RepayServiceADO)
                            {
                                hisTreatment = ((RepayServiceADO)entity[i]).hisTreatment;
                                repaySdo = ((RepayServiceADO)entity[i]).repaySdo;
                                branchId = ((RepayServiceADO)entity[i]).branchId;
                                cashierRoomId = ((RepayServiceADO)entity[i]).cashierRoomId;
                                ListSereServ = ((RepayServiceADO)entity[i]).ListSereServ;
                            }
                            if (entity[i] is SendResultToOtherForm)
                            {
                                sendResultToOtherForm = (SendResultToOtherForm)entity[i];
                            }
                        }
                    }
                }

                return new frmRepayService(hisTreatment, repaySdo, sendResultToOtherForm, branchId, cashierRoomId, moduleData, ListSereServ);

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
