using HIS.Desktop.Plugins.AssignPrescriptionPK.ADO;
using HIS.Desktop.Plugins.AssignPrescriptionPK.AssignPrescription;
using HIS.Desktop.Plugins.AssignPrescriptionPK.Save.Create;
using HIS.Desktop.Plugins.AssignPrescriptionPK.Save.Update;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.Save
{
    class SaveFactory
    {
        internal static ISave MakeISave(CommonParam param,
            List<MediMatyTypeADO> mediMatyTypeADOs,
            frmAssignPrescription frmAssignPrescription,
            int actionType,
            bool isSaveAndPrint,
            long parentServiceReqId,
            long sereServParentId)
        {
            ISave result = null;
            try
            {
                if (actionType == HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionAdd)
                    result = new SaveCreateBehavior(param, mediMatyTypeADOs, frmAssignPrescription, actionType, isSaveAndPrint, parentServiceReqId, sereServParentId);
                else
                    result = new SaveUpdateBehavior(param, mediMatyTypeADOs, frmAssignPrescription, actionType, isSaveAndPrint, parentServiceReqId, sereServParentId);

                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + mediMatyTypeADOs.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => mediMatyTypeADOs), mediMatyTypeADOs)
                    + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => actionType), actionType)
                    + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => isSaveAndPrint), isSaveAndPrint)
                    + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => sereServParentId), sereServParentId)
                    + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => parentServiceReqId), parentServiceReqId), ex);
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
