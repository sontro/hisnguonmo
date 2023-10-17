using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.Plugins.AssignPrescriptionCLS.AssignPrescription;
using HIS.Desktop.Plugins.AssignPrescriptionCLS.Resources;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using System;

namespace HIS.Desktop.Plugins.AssignPrescriptionCLS.Add.MaterialType
{
    class MaterialTypeRowAddBehavior : AddAbstract, IAdd
    {
        internal MaterialTypeRowAddBehavior(CommonParam param,
            frmAssignPrescription frmAssignPrescription,
            ValidAddRow validAddRow,
            HIS.Desktop.Plugins.AssignPrescriptionCLS.MediMatyCreateWorker.ChoosePatientTypeDefaultlService choosePatientTypeDefaultlService,
            HIS.Desktop.Plugins.AssignPrescriptionCLS.MediMatyCreateWorker.ChoosePatientTypeDefaultlServiceOther choosePatientTypeDefaultlServiceOther,
            CalulateUseTimeTo calulateUseTimeTo,
            ExistsAssianInDay existsAssianInDay,
            object dataRow)
            : base(param,
             frmAssignPrescription,
             validAddRow,
             choosePatientTypeDefaultlService,
             choosePatientTypeDefaultlServiceOther,
             calulateUseTimeTo,
             existsAssianInDay,
             dataRow)
        {
            this.Id = frmAssignPrescription.currentMedicineTypeADOForEdit.ID;
            this.DataType = HIS.Desktop.LocalStorage.BackendData.ADO.MedicineMaterialTypeComboADO.VATTU_DM;
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

        }

        bool IAdd.Run()
        {
            bool success = false;
            medicineTypeSDO__Category__SameMediAcin = null;
            try
            {
                if (this.CheckValidPre()
                    //&& this.ValidKhaDungThuocTrongNhaThuoc()
                    )
                {
                    //Nếu thuốc đã kê không đủ khả dụng trong kho, người dùng chọn lấy thuốc ngoài kho thay thế
                    //==> Lấy các thuốc ngoài kho + các thông tin số lượng, đường dùng, cách dùng, hướng dẫn sử dụng,.. đã chọn => tự động bổ sung vào danh sách thuốc đã chọn luôn
                    if (medicineTypeSDO__Category__SameMediAcin != null)
                    {
                        this.UpdateMediMatyByMedicineTypeCategory(medicineTypeSDO__Category__SameMediAcin);
                    }
                    //Nếu thuốc còn khả dụng trong kho
                    //==> Set các thông tin đối tượng mặc định, đường dùng, thời gian dùng, validate,...
                    else
                    {
                        this.CreateADO();
                    }

                    this.UpdateUseTimeInDataRow(this.medicineTypeSDO);
                    this.SetValidAssianInDayError();
                    this.SetValidAmountError();
                    if (medicineTypeSDO.ErrorMessageMedicineUseForm == ResourceMessage.BenhNhanDoiTuongTTBhytBatBuocPhaiNhapDuongDung
                           && medicineTypeSDO.ErrorTypeMedicineUseForm == ErrorType.Warning)
                    {
                        MessageManager.Show(ResourceMessage.BenhNhanDoiTuongTTBhytBatBuocPhaiNhapDuongDung);
                        frmAssignPrescription.cboMedicineUseForm.Focus();
                        frmAssignPrescription.cboMedicineUseForm.ShowPopup();
                        success = false;
                    }
                    else
                    {
                        medicineTypeSDO.PrimaryKey = medicineTypeSDO.SERVICE_ID + "__" + Inventec.Common.DateTime.Get.Now() + "__" + Guid.NewGuid().ToString();
                        this.SaveDataAndRefesh(medicineTypeSDO);
                        success = true;
                    }
                }
                else
                {
                    this.medicineTypeSDO = null;
                }
            }
            catch (Exception ex)
            {
                success = false;
                medicineTypeSDO = null;
                MessageManager.Show(Param, success);
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return success;
        }
    }
}
