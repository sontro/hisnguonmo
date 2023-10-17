using HIS.Desktop.Plugins.AssignPrescriptionKidney.AssignPrescription;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using System;

namespace HIS.Desktop.Plugins.AssignPrescriptionKidney.Edit.MaterialTypeTSD
{
    class MaterialTypeTSDRowEditBehavior : EditAbstract, IEdit
    {
        internal MaterialTypeTSDRowEditBehavior(CommonParam param,
            frmAssignPrescription frmAssignPrescription,
            ValidAddRow validAddRow,
            GetPatientTypeBySeTy getPatientTypeBySeTy,
            CalulateUseTimeTo calulateUseTimeTo,
            ExistsAssianInDay existsAssianInDay,
            object dataRow)
            : base(param,
             frmAssignPrescription,
             validAddRow,
             getPatientTypeBySeTy,
             calulateUseTimeTo,
             existsAssianInDay,
             dataRow)
        {
            this.DataType = HIS.Desktop.LocalStorage.BackendData.ADO.MedicineMaterialTypeComboADO.VATTU_TSD;
            this.Code = frmAssignPrescription.currentMedicineTypeADOForEdit.MEDICINE_TYPE_CODE;
            this.Name = frmAssignPrescription.currentMedicineTypeADOForEdit.MEDICINE_TYPE_NAME;
            this.ManuFacturerName = frmAssignPrescription.currentMedicineTypeADOForEdit.MANUFACTURER_NAME;
            this.ServiceUnitName = frmAssignPrescription.currentMedicineTypeADOForEdit.SERVICE_UNIT_NAME;
            this.NationalName = frmAssignPrescription.currentMedicineTypeADOForEdit.NATIONAL_NAME;
            this.ServiceId = frmAssignPrescription.currentMedicineTypeADOForEdit.SERVICE_ID;
            this.Concentra = frmAssignPrescription.currentMedicineTypeADOForEdit.CONCENTRA;
            this.HeinServiceTypeId = frmAssignPrescription.currentMedicineTypeADOForEdit.HEIN_SERVICE_TYPE_ID;
            this.ServiceTypeId = frmAssignPrescription.currentMedicineTypeADOForEdit.SERVICE_TYPE_ID;
            this.IsOutKtcFee = ((frmAssignPrescription.currentMedicineTypeADOForEdit.IS_OUT_PARENT_FEE ?? -1) == 1);
            this.MaxReuseCount = frmAssignPrescription.currentMedicineTypeADOForEdit.MAX_REUSE_COUNT;//TODO
            this.UseRemainCount = frmAssignPrescription.currentMedicineTypeADOForEdit.USE_REMAIN_COUNT;//TODO
            this.UseCount = frmAssignPrescription.currentMedicineTypeADOForEdit.USE_COUNT;//TODO
            this.SeriNumber = frmAssignPrescription.currentMedicineTypeADOForEdit.SERIAL_NUMBER;
            this.MediStockId = frmAssignPrescription.currentMedicineTypeADOForEdit.MEDI_STOCK_ID;
            this.MediStockCode = frmAssignPrescription.currentMedicineTypeADOForEdit.MEDI_STOCK_CODE;
            this.MediStockName = frmAssignPrescription.currentMedicineTypeADOForEdit.MEDI_STOCK_NAME;
            this.Amount = 1;
        }

        bool IEdit.Run()
        {
            bool success = false;
            try
            {
                if (this.ValidSerialNumber())
                {
                    this.CreateADO();
                    this.UpdatePatientTypeInDataRow(this.medicineTypeSDO);
                    this.SetValidPatientTypeError();

                    this.SaveDataAndRefesh();
                    frmAssignPrescription.ReloadDataAvaiableMediBeanInCombo();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                success = false;
                MessageManager.Show(Param, success);
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return success;
        }

        bool ValidSerialNumber()
        {
            if (String.IsNullOrEmpty(this.SeriNumber))
            {
                Param.Messages.Add("Thiếu trường dữ liệu bắt buộc");
                throw new ArgumentNullException("Edit materialTSD check SerialNumber valid fail => edit material fail");
            }

            return true;
        }
    }
}
