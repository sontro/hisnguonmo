using HIS.Desktop.Plugins.ImpMestCreate.ADO;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.ImpMestCreate.Save
{
    class SaveFactory
    {
        internal static ISaveInit MakeIServiceRequestRegister(
            CommonParam param,
            List<VHisServiceADO> serviceADOs,
            UCImpMestCreate ucImpMestCreate,
            Dictionary<string, V_HIS_BID_MEDICINE_TYPE> dicbidmedicine,
            Dictionary<string, V_HIS_BID_MATERIAL_TYPE> dicbidmaterial,
            long roomId,
            ResultImpMestADO resultADO)
        {
            ISaveInit result = null;
            try
            {
                long impMestTypeId = Inventec.Common.TypeConvert.Parse.ToInt64((ucImpMestCreate.cboImpMestType.EditValue ?? "0").ToString());
                if (impMestTypeId == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DK)
                {
                    result = new SaveInitBehavior(param, serviceADOs, ucImpMestCreate, dicbidmedicine, dicbidmaterial, roomId, resultADO);
                }
                else if (impMestTypeId == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__KK)
                {
                    result = new SaveInveBehavior(param, serviceADOs, ucImpMestCreate, dicbidmedicine, dicbidmaterial, roomId, resultADO);
                }
                else if (impMestTypeId == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__KHAC)
                {
                    result = new SaveOtherBehavior(param, serviceADOs, ucImpMestCreate, dicbidmedicine, dicbidmaterial, roomId, resultADO);
                }
                else if (impMestTypeId == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC)
                {
                    result = new SaveManuBehavior(param, serviceADOs, ucImpMestCreate, dicbidmedicine, dicbidmaterial, roomId, resultADO);
                }

                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + serviceADOs.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => serviceADOs), serviceADOs), ex);
                result = null;
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
