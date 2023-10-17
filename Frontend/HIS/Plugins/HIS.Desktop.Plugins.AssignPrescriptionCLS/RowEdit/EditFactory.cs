using HIS.Desktop.Plugins.AssignPrescriptionCLS.AssignPrescription;
using HIS.Desktop.Plugins.AssignPrescriptionCLS.Edit.MaterialType;
using HIS.Desktop.Plugins.AssignPrescriptionCLS.Edit.MedicineType;
using HIS.Desktop.Plugins.AssignPrescriptionCLS.Edit.MediStockD1SDO;
using Inventec.Core;
using System;

namespace HIS.Desktop.Plugins.AssignPrescriptionCLS.Edit
{
    public class EditFactory
    {
        internal static IEdit MakeIEdit(CommonParam param,
            frmAssignPrescription frmAssignPrescription,
            ValidAddRow validAddRow,
            HIS.Desktop.Plugins.AssignPrescriptionCLS.MediMatyCreateWorker.ChoosePatientTypeDefaultlService choosePatientTypeDefaultlService,
            HIS.Desktop.Plugins.AssignPrescriptionCLS.MediMatyCreateWorker.ChoosePatientTypeDefaultlServiceOther choosePatientTypeDefaultlServiceOther,
            CalulateUseTimeTo calulateUseTimeTo,
            ExistsAssianInDay existsAssianInDay,
            object dataRow,
            int datatype)
        {
            IEdit result = null;
            try
            {
                switch (datatype)
                {
                    case HIS.Desktop.LocalStorage.BackendData.ADO.MedicineMaterialTypeComboADO.THUOC:
                        result = new MediStockD1SDORowEditBehavior(param, frmAssignPrescription, validAddRow, choosePatientTypeDefaultlService, choosePatientTypeDefaultlServiceOther, calulateUseTimeTo, existsAssianInDay, dataRow);
                        break;
                    case HIS.Desktop.LocalStorage.BackendData.ADO.MedicineMaterialTypeComboADO.VATTU:
                        result = new MediStockD1SDORowEditBehavior(param, frmAssignPrescription, validAddRow, choosePatientTypeDefaultlService, choosePatientTypeDefaultlServiceOther, calulateUseTimeTo, existsAssianInDay, dataRow);
                        break;
                    case HIS.Desktop.LocalStorage.BackendData.ADO.MedicineMaterialTypeComboADO.THUOC_DM:
                        result = new MedicineTypeRowEditBehavior(param, frmAssignPrescription, validAddRow, choosePatientTypeDefaultlService, choosePatientTypeDefaultlServiceOther, calulateUseTimeTo, existsAssianInDay, dataRow);
                        break;
                    case HIS.Desktop.LocalStorage.BackendData.ADO.MedicineMaterialTypeComboADO.VATTU_DM:
                        result = new MaterialTypeRowEditBehavior(param, frmAssignPrescription, validAddRow, choosePatientTypeDefaultlService, choosePatientTypeDefaultlServiceOther, calulateUseTimeTo, existsAssianInDay, dataRow);
                        break;                   
                    default:
                        break;
                }

                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + dataRow.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataRow), dataRow)
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
