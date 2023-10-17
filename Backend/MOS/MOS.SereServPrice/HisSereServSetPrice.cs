using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicinePaty;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisServicePaty;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.SereServPrice;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ
{
    /// <summary>
    /// Thong tin ve gia duoc them khi thuc hien tao moi sere_serv
    /// </summary>
    class HisSereServSetPrice : BusinessBase
    {
        private HisServicePatyGet hisServicePatyGet = new HisServicePatyGet();

        private HIS_TREATMENT treatment;
        private List<HIS_MEDICINE_PATY> medicinePaties;
        private List<HIS_MATERIAL_PATY> materialPaties;
        private List<HIS_MEDICINE> medicines;
        private List<HIS_MATERIAL> materials;

        internal HisSereServSetPrice(CommonParam param, HIS_TREATMENT treatment, List<long> medicineIds, List<long> materialIds) : base(param)
        {
            this.treatment = treatment;
            this.medicinePaties = new HisMedicinePatyGet().GetByMedicineIds(medicineIds);
            this.materialPaties = new HisMaterialPatyGet().GetByMaterialIds(materialIds);
            this.medicines = new HisMedicineGet().GetByIds(medicineIds);
            this.materials = new HisMaterialGet().GetByIds(materialIds);
        }

        internal bool AddPrice(HIS_SERE_SERV data, long instructionTime, long executeBranchId, long requestRoomId, long requestDepartmentId, long executeRoomId)
        {
            bool result = true;
            try
            {
                //Lay thong tin dich vu
                V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == data.SERVICE_ID).FirstOrDefault();
                //Bo sung thong tin gia
                this.SetPrice(data, instructionTime, executeBranchId, requestRoomId, requestDepartmentId, executeRoomId, hisService);
                //Bo sung thong tin ve gia lien quan den BHYT (luu y: can goi ham nay sau, vi co su dung lai gia tri price da duoc set o ham phia tren)
                new SereServPriceUtil(HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT, Constant.IS_TRUE, this.treatment.IN_TIME).SetBhytPrice(data, instructionTime, hisService);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Set gia cho thuoc/vat tu/mau
        /// </summary>
        /// <param name="data"></param>
        /// <param name="instructionTime"></param>
        /// <returns></returns>
        internal bool AddPriceForNonService(HIS_SERE_SERV data, long instructionTime)
        {
            bool result = true;
            try
            {
                //Lay thong tin dich vu tuong ung voi medicine, material
                V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == data.SERVICE_ID).FirstOrDefault();
                new SereServPriceUtil(HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT, Constant.IS_TRUE, this.treatment.IN_TIME).SetBhytPrice(data, instructionTime, hisService);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void SetPrice(HIS_SERE_SERV data, long instructionTime, long executeBranchId, long requestRoomId, long requestDepartmentId, long executeRoomId, V_HIS_SERVICE hisService)
        {
            //Gia thuoc/vat tu duoc lay theo gia nhap luc chi dinh
            if (!data.MEDICINE_ID.HasValue && !data.MATERIAL_ID.HasValue && !data.BLOOD_ID.HasValue)
            {
                V_HIS_SERVICE_PATY servicePaty = this.GetServicePaty(executeBranchId, instructionTime, data.PATIENT_TYPE_ID, data.SERVICE_ID, requestRoomId, requestDepartmentId, executeRoomId);

                //Neu co cau hinh bat buoc su dung doi tuong thanh toan khac
                //Khi do, gia se lay theo gia duoc cau hinh, con hein_limit se lay theo doi tuong ma nguoi dung duoc chi dinh
                //==> he thong se can cu theo truong nay de tinh gia chenh lech
                if (hisService.BILL_PATIENT_TYPE_ID.HasValue)
                {
                    V_HIS_SERVICE_PATY billServicePaty = this.GetServicePaty(executeBranchId, instructionTime, hisService.BILL_PATIENT_TYPE_ID.Value, data.SERVICE_ID, requestRoomId, requestDepartmentId, executeRoomId);
                    data.PRICE = billServicePaty.PRICE;
                    data.VAT_RATIO = billServicePaty.VAT_RATIO;
                    data.OVERTIME_PRICE = billServicePaty.OVERTIME_PRICE;
                    data.ORIGINAL_PRICE = servicePaty.PRICE;
                    data.HEIN_LIMIT_PRICE = servicePaty.PRICE;
                }
                else
                {
                    data.PRICE = servicePaty.PRICE;
                    data.VAT_RATIO = servicePaty.VAT_RATIO;
                    data.ORIGINAL_PRICE = servicePaty.PRICE;
                    data.OVERTIME_PRICE = servicePaty.OVERTIME_PRICE;
                }
            }
            else if (data.MEDICINE_ID.HasValue)
            {
                HIS_MEDICINE medicine = this.medicines != null ? this.medicines.Where(o => o.ID == data.MEDICINE_ID.Value).FirstOrDefault() : null;
                if (medicine != null)
                {
                    if (medicine.IS_SALE_EQUAL_IMP_PRICE == Constant.IS_TRUE)
                    {
                        data.PRICE = medicine.IMP_PRICE;
                        data.VAT_RATIO = medicine.IMP_VAT_RATIO;
                        data.ORIGINAL_PRICE = medicine.IMP_PRICE;
                    }
                    else
                    {
                        HIS_MEDICINE_PATY paty = IsNotNullOrEmpty(this.medicinePaties) ? this.medicinePaties
                            .Where(o => o.PATIENT_TYPE_ID == data.PATIENT_TYPE_ID
                                && o.MEDICINE_ID == data.MEDICINE_ID).FirstOrDefault() : null;
                        if (paty == null)
                        {
                            HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.Where(o => o.ID == data.PATIENT_TYPE_ID).FirstOrDefault();
                            V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == data.SERVICE_ID).FirstOrDefault();
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_LoThuocDaKeKhongTonTaiChinhSachGiaTuongUngVoiDoiTuongThanhToan, service.SERVICE_NAME, patientType.PATIENT_TYPE_NAME);
                            throw new Exception();
                        }
                        data.PRICE = paty.EXP_PRICE;
                        data.VAT_RATIO = paty.EXP_VAT_RATIO;
                        data.ORIGINAL_PRICE = paty.EXP_PRICE;
                    }
                }
            }
            else if (data.MATERIAL_ID.HasValue)
            {
                HIS_MATERIAL material = this.materials != null ? this.materials.Where(o => o.ID == data.MATERIAL_ID.Value).FirstOrDefault() : null;
                if (material != null)
                {
                    if (material.IS_SALE_EQUAL_IMP_PRICE == Constant.IS_TRUE)
                    {
                        data.PRICE = material.IMP_PRICE;
                        data.VAT_RATIO = material.IMP_VAT_RATIO;
                        data.ORIGINAL_PRICE = material.IMP_PRICE;
                    }
                    else
                    {
                        HIS_MATERIAL_PATY paty = IsNotNullOrEmpty(this.materialPaties) ? this.materialPaties
                            .Where(o => o.PATIENT_TYPE_ID == data.PATIENT_TYPE_ID
                                && o.MATERIAL_ID == data.MATERIAL_ID).FirstOrDefault() : null;
                        if (paty == null)
                        {
                            HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.Where(o => o.ID == data.PATIENT_TYPE_ID).FirstOrDefault();
                            V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == data.SERVICE_ID).FirstOrDefault();
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_LoVatTuDaKeKhongTonTaiChinhSachGiaTuongUngVoiDoiTuongThanhToan, service.SERVICE_NAME, patientType.PATIENT_TYPE_NAME);
                            throw new Exception();
                        }
                        data.PRICE = paty.EXP_PRICE;
                        data.VAT_RATIO = paty.EXP_VAT_RATIO;
                        data.ORIGINAL_PRICE = paty.EXP_PRICE;
                    }
                }
            }
        }

        /// <summary>
        /// Lay gia cua dich vu. Can cu:
        /// - Chinh sach gia tuong ung voi service tai thoi diem chi dinh (instruction_time va treatment_time)
        /// - Cac quy dinh cua bo y te lien quan den dich vu kham, dich vu phau thuat
        /// </summary>
        /// <param name="instructionTime"></param>
        /// <param name="treatmentTime"></param>
        /// <returns></returns>
        private V_HIS_SERVICE_PATY GetServicePaty(long executeBranchId, long instructionTime, long patientTypeId, long serviceId, long requestRoomId, long requestDepartmentId, long executeRoomId)
        {
            //Lay thong tin chinh sach gia duoc ap dung cho sere_serv
            V_HIS_SERVICE_PATY appliedServicePaty = hisServicePatyGet.GetApplied(executeBranchId, executeRoomId, requestRoomId, requestDepartmentId, HisServicePatyCFG.DATA, instructionTime, this.treatment.IN_TIME, serviceId, patientTypeId);
            if (appliedServicePaty == null)
            {
                HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == patientTypeId).FirstOrDefault();
                V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == serviceId).FirstOrDefault();
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, hisService.SERVICE_NAME, hisService.SERVICE_TYPE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                throw new Exception();
            }
            return appliedServicePaty;
        }
    }
}
