using HIS.Desktop.Plugins.AssignPrescriptionYHCT.Add.MaterialType;
using HIS.Desktop.Plugins.AssignPrescriptionYHCT.Add.MedicineType;
using HIS.Desktop.Plugins.AssignPrescriptionYHCT.Add.MedicineTypeOther;
using HIS.Desktop.Plugins.AssignPrescriptionYHCT.Add.MediStockD1SDO;
using HIS.Desktop.Plugins.AssignPrescriptionYHCT.ADO;
using HIS.Desktop.Plugins.AssignPrescriptionYHCT.AssignPrescription;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.AssignPrescriptionYHCT.Add
{
    public class AddFactory
    {
        internal static IAdd MakeIAdd(CommonParam param,
            frmAssignPrescription frmAssignPrescription,
            ValidAddRow validAddRow,
            GetPatientTypeBySeTy getPatientTypeBySeTy,
            CalulateUseTimeTo calulateUseTimeTo,
            ExistsAssianInDay existsAssianInDay,
            object dataRow,
            int datatype)
        {
            IAdd result = null;
            try
            {
                switch (datatype)
                {
                    case HIS.Desktop.LocalStorage.BackendData.ADO.MedicineMaterialTypeComboADO.THUOC:
                        result = new MediStockD1SDORowAddBehavior(param, frmAssignPrescription, validAddRow, getPatientTypeBySeTy, calulateUseTimeTo, existsAssianInDay, dataRow);
                        break;
                    case HIS.Desktop.LocalStorage.BackendData.ADO.MedicineMaterialTypeComboADO.VATTU:
                        result = new MediStockD1SDORowAddBehavior(param, frmAssignPrescription, validAddRow, getPatientTypeBySeTy, calulateUseTimeTo, existsAssianInDay, dataRow);
                        break;
                    case HIS.Desktop.LocalStorage.BackendData.ADO.MedicineMaterialTypeComboADO.THUOC_DM:
                        result = new MedicineTypeRowAddBehavior(param, frmAssignPrescription, validAddRow, getPatientTypeBySeTy, calulateUseTimeTo, existsAssianInDay, dataRow);
                        break;
                    case HIS.Desktop.LocalStorage.BackendData.ADO.MedicineMaterialTypeComboADO.VATTU_DM:
                        result = new MaterialTypeRowAddBehavior(param, frmAssignPrescription, validAddRow, getPatientTypeBySeTy, calulateUseTimeTo, existsAssianInDay, dataRow);
                        break;
                    case HIS.Desktop.LocalStorage.BackendData.ADO.MedicineMaterialTypeComboADO.THUOC_TUTUC:
                        result = new MedicineTypeOtherRowAddBehavior(param, frmAssignPrescription, validAddRow, getPatientTypeBySeTy, calulateUseTimeTo, existsAssianInDay, dataRow);
                        break;
                    default:
                        break;
                }

                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + dataRow.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataRow), dataRow)
                    //+ "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => addType), addType)
                   + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param), ex);
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
