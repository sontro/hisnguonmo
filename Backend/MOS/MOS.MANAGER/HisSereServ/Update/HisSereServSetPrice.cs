using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.LibraryHein.Bhyt.HeinLevel;
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
using MOS.ServicePaty;
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

        internal HisSereServSetPrice(CommonParam param, HIS_TREATMENT treatment, List<long> medicineIds, List<long> materialIds)
            : base(param)
        {
            this.treatment = treatment;
            this.medicinePaties = new HisMedicinePatyGet().GetByMedicineIds(medicineIds);
            this.materialPaties = new HisMaterialPatyGet().GetByMaterialIds(materialIds);
            this.medicines = new HisMedicineGet().GetByIds(medicineIds);
            this.materials = new HisMaterialGet().GetByIds(materialIds);
        }

        internal bool AddPrice(HIS_SERE_SERV data, List<HIS_SERE_SERV> allSereServsOfTreatments, long instructionTime, long executeBranchId, long requestRoomId, long requestDepartmentId, long executeRoomId)
        {
            bool result = true;
            try
            {
                //Lay thong tin dich vu
                V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == data.SERVICE_ID).FirstOrDefault();
                //Bo sung thong tin gia
                this.SetPrice(data, allSereServsOfTreatments, instructionTime, executeBranchId, requestRoomId, requestDepartmentId, executeRoomId, hisService);
                //Bo sung thong tin ve gia lien quan den BHYT (luu y: can goi ham nay sau, 
                //vi co su dung lai gia tri price da duoc set o ham phia tren)
                new HisSereServPriceUtil(param).SetBhytPrice(data, this.treatment, instructionTime, hisService);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool AddPrice(HIS_SERE_SERV data, long instructionTime, long executeBranchId, long requestRoomId, long requestDepartmentId, long executeRoomId)
        {
            //tam thoi truyen null vao allSereServsOfTreatments --> ko xu ly nghiep vu lay gia theo lan chi dinh
            return this.AddPrice(data, null, instructionTime, executeBranchId, requestRoomId, requestDepartmentId, executeRoomId);
        }

        /// <summary>
        /// Set gia cho thuoc/vat tu/mau
        /// </summary>
        /// <param name="data"></param>
        /// <param name="instructionTime"></param>
        /// <returns></returns>
        internal void AddPriceForNonService(HIS_SERE_SERV data, long instructionTime, string icdCode, string icdSubCode)
        {
            //Lay thong tin dich vu tuong ung voi medicine, material
            V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == data.SERVICE_ID).FirstOrDefault();
            new HisSereServPriceUtil(param).SetBhytPrice(data, this.treatment, instructionTime, hisService, icdCode, icdSubCode);
        }

        private void SetPrice(HIS_SERE_SERV data, List<HIS_SERE_SERV> allSereServsOfTreatments, long instructionTime, long executeBranchId, long requestRoomId, long requestDepartmentId, long executeRoomId, V_HIS_SERVICE hisService)
        {
            //Gia thuoc/vat tu duoc lay theo gia nhap luc chi dinh
            if ((HisExpMestCFG.IS_BLOOD_EXP_PRICE_OPTION && !data.MEDICINE_ID.HasValue && !data.MATERIAL_ID.HasValue) // Khi cau hinh duoc bat thi gia "Mau" thi se lay theo chinh sach gia
                || (!data.MEDICINE_ID.HasValue && !data.MATERIAL_ID.HasValue && !data.BLOOD_ID.HasValue)
                || (data.MEDICINE_ID.HasValue && data.TDL_IS_VACCINE == Constant.IS_TRUE && HisSereServCFG.IS_VACCINE_EXP_PRICE_OPTION)
                )
            {
                if ((data.USER_PRICE.HasValue || data.IS_USER_PACKAGE_PRICE == Constant.IS_TRUE)
                    && (data.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || data.PRIMARY_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuChiDinhGiaKhongChoPhepSuDungDoiTuongThanhToanBhyt, data.TDL_SERVICE_NAME);
                    throw new Exception();
                }

                V_HIS_SERVICE_PATY servicePaty = this.GetServicePaty(data, allSereServsOfTreatments, executeBranchId, instructionTime, data.PATIENT_TYPE_ID, data.SERVICE_ID, requestRoomId, requestDepartmentId, executeRoomId, data.PACKAGE_ID, data.SERVICE_CONDITION_ID,data.TDL_RATION_TIME_ID);

                HIS_DEPARTMENT requestDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == requestDepartmentId).FirstOrDefault();

                //Neu co cau hinh bat buoc su dung doi tuong thanh toan khac
                //Khi do, gia se lay theo gia duoc cau hinh, con hein_limit se lay theo doi tuong ma nguoi dung duoc chi dinh
                //==> he thong se can cu theo truong nay de tinh gia chenh lech

                if (data.PRIMARY_PATIENT_TYPE_ID == data.PATIENT_TYPE_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DoiTuongPhuThuKhongDuocTrungVoiDoiTuongThanhToan);
                    throw new Exception();
                }

                // Kiem tra truong "ĐTTT áp dụng" (APPLIED_PATIENT_TYPE_IDS) trong service
                bool isNullAppliedPatientTypeIds = string.IsNullOrWhiteSpace(hisService.APPLIED_PATIENT_TYPE_IDS);
                List<long> appliedPatientTypeIds = null;
                if (!isNullAppliedPatientTypeIds)
                {
                    var listStr = hisService.APPLIED_PATIENT_TYPE_IDS.Split(',').ToList();
                    listStr = IsNotNullOrEmpty(listStr) ? listStr.Where( o => !string.IsNullOrWhiteSpace(o)).ToList() : null;
                    appliedPatientTypeIds = IsNotNullOrEmpty(listStr) ? listStr.Select(Int64.Parse).ToList() : null;
                }

                // Kiem tra truong "ĐTCT áp dụng" (APPLIED_PATIENT_CLASSIFY_IDS) trong service
                bool isNullAppliedPatientClassifyIds = string.IsNullOrWhiteSpace(hisService.APPLIED_PATIENT_CLASSIFY_IDS);
                List<long> appliedPatientClassifyIds = null;
                if (!isNullAppliedPatientClassifyIds)
                {
                    var listStr = hisService.APPLIED_PATIENT_CLASSIFY_IDS.Split(',').ToList();
                    listStr = IsNotNullOrEmpty(listStr) ? listStr.Where(o => !string.IsNullOrWhiteSpace(o)).ToList() : null;
                    appliedPatientClassifyIds = IsNotNullOrEmpty(listStr) ? listStr.Select(Int64.Parse).ToList() : null;
                }

                if (hisService.BILL_PATIENT_TYPE_ID.HasValue
                    && hisService.IS_NOT_CHANGE_BILL_PATY == Constant.IS_TRUE
                    && data.PATIENT_TYPE_ID != hisService.BILL_PATIENT_TYPE_ID.Value
                    && data.PRIMARY_PATIENT_TYPE_ID != hisService.BILL_PATIENT_TYPE_ID.Value
                    && (isNullAppliedPatientTypeIds || (IsNotNullOrEmpty(appliedPatientTypeIds) && appliedPatientTypeIds.Contains(data.PATIENT_TYPE_ID)))
                    && (isNullAppliedPatientClassifyIds || (IsNotNullOrEmpty(appliedPatientClassifyIds) && appliedPatientClassifyIds.Contains(treatment.TDL_PATIENT_CLASSIFY_ID ?? -1)))
                    )
                {
                    HIS_PATIENT_TYPE pt = HisPatientTypeCFG.DATA.Where(o => o.ID == hisService.BILL_PATIENT_TYPE_ID.Value).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_BatBuocSuDungDoiTuong, hisService.SERVICE_NAME, hisService.SERVICE_CODE, pt.PATIENT_TYPE_NAME);
                    throw new Exception();
                }

                //Neu ko su dung gia goi do nguoi dung nhap thi update lai theo gia goi dc khai bao trong dich vu
                if (data.PACKAGE_ID.HasValue && data.IS_USER_PACKAGE_PRICE != Constant.IS_TRUE
                    && HisPackageCFG.NOT_FIXED_SERVICE_PACKAGE_IDS != null
                    && HisPackageCFG.NOT_FIXED_SERVICE_PACKAGE_IDS.Contains(data.PACKAGE_ID.Value))
                {
                    data.PACKAGE_PRICE = hisService.PACKAGE_PRICE;
                }

                V_HIS_SERVICE_PATY primaryServicePaty = data.PRIMARY_PATIENT_TYPE_ID.HasValue ? this.GetServicePaty(data, allSereServsOfTreatments, executeBranchId, instructionTime, data.PRIMARY_PATIENT_TYPE_ID.Value, data.SERVICE_ID, requestRoomId, requestDepartmentId, executeRoomId, data.PACKAGE_ID, data.SERVICE_CONDITION_ID, data.TDL_RATION_TIME_ID) : null;

                if (primaryServicePaty != null)
                {
                    if (primaryServicePaty.PRICE <= servicePaty.PRICE)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_GiaDoiTuongPhuThuCanLonHonGiaDoiTuongThanhToan);
                        throw new Exception();
                    }

                    //Neu co gia do nguoi dung nhap luc chi dinh thi lay gia nguoi dung nhap
                    if (data.USER_PRICE.HasValue)
                    {
                        data.PRICE = data.USER_PRICE.Value;
                        data.VAT_RATIO = 0;
                        data.OVERTIME_PRICE = null;
                        data.ORIGINAL_PRICE = servicePaty.PRICE;
                        data.HEIN_LIMIT_PRICE = servicePaty.PRICE;
                        data.LIMIT_PRICE = servicePaty.PRICE;
                        data.PRIMARY_PRICE = data.USER_PRICE.Value;
                    }
                    else
                    {
                        data.PRICE = this.GetCommunePrice(primaryServicePaty.PRICE, requestDepartment.BRANCH_ID, hisService.SERVICE_TYPE_ID);
                        data.VAT_RATIO = primaryServicePaty.VAT_RATIO;
                        data.OVERTIME_PRICE = primaryServicePaty.OVERTIME_PRICE;
                        data.ORIGINAL_PRICE = servicePaty.PRICE;
                        data.HEIN_LIMIT_PRICE = servicePaty.PRICE;
                        data.LIMIT_PRICE = servicePaty.PRICE;
                        data.ACTUAL_PRICE = servicePaty.ACTUAL_PRICE;
                        data.PRIMARY_PRICE = primaryServicePaty.PRICE;
                    }
                }
                else
                {
                    //Neu co gia do nguoi dung nhap luc chi dinh thi lay gia nguoi dung nhap
                    if (data.USER_PRICE.HasValue)
                    {
                        data.PRICE = data.USER_PRICE.Value;
                        data.VAT_RATIO = 0;
                        data.OVERTIME_PRICE = null;
                        data.ORIGINAL_PRICE = data.USER_PRICE.Value;
                        data.HEIN_LIMIT_PRICE = null;
                        data.LIMIT_PRICE = null;
                        data.PRIMARY_PRICE = data.USER_PRICE.Value;
                    }
                    else
                    {
                        //Bo sung phan tinh lai gia doi voi tuyen xa
                        data.PRICE = this.GetCommunePrice(servicePaty.PRICE, requestDepartment.BRANCH_ID, hisService.SERVICE_TYPE_ID);
                        data.PRIMARY_PRICE = servicePaty.PRICE;
                        data.VAT_RATIO = servicePaty.VAT_RATIO;
                        data.ORIGINAL_PRICE = servicePaty.PRICE;
                        data.ACTUAL_PRICE = servicePaty.ACTUAL_PRICE;
                        data.OVERTIME_PRICE = servicePaty.OVERTIME_PRICE;
                        data.LIMIT_PRICE = null;
                    }
                }
            }
            else if (data.MEDICINE_ID.HasValue)
            {
                HIS_MEDICINE medicine = this.medicines != null ? this.medicines.Where(o => o.ID == data.MEDICINE_ID.Value).FirstOrDefault() : null;

                if (medicine != null)
                {
                    if (data.TDL_IS_VACCINE != Constant.IS_TRUE || !HisSereServCFG.IS_VACCINE_EXP_PRICE_OPTION)
                    {
                        if (medicine.IS_SALE_EQUAL_IMP_PRICE == Constant.IS_TRUE)
                        {
                            data.PRIMARY_PRICE = medicine.IMP_PRICE;
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

                                string expiredDate = medicine.EXPIRED_DATE.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToDateStringSeparateString(medicine.EXPIRED_DATE.Value) : "";
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_LoThuocDaKeKhongTonTaiChinhSachGiaTuongUngVoiDoiTuongThanhToan, service.SERVICE_NAME, expiredDate, medicine.PACKAGE_NUMBER, patientType.PATIENT_TYPE_NAME);
                                throw new Exception();
                            }
                            data.PRIMARY_PRICE = paty.EXP_PRICE;
                            data.PRICE = paty.EXP_PRICE;
                            data.VAT_RATIO = paty.EXP_VAT_RATIO;
                            data.ORIGINAL_PRICE = paty.EXP_PRICE;
                        }
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
                        data.PRIMARY_PRICE = material.IMP_PRICE;
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
                            string expiredDate = material.EXPIRED_DATE.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToDateStringSeparateString(material.EXPIRED_DATE.Value) : "";

                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_LoVatTuDaKeKhongTonTaiChinhSachGiaTuongUngVoiDoiTuongThanhToan, service.SERVICE_NAME, expiredDate, material.PACKAGE_NUMBER, patientType.PATIENT_TYPE_NAME);

                            throw new Exception();
                        }
                        data.PRICE = paty.EXP_PRICE;
                        data.VAT_RATIO = paty.EXP_VAT_RATIO;
                        data.ORIGINAL_PRICE = paty.EXP_PRICE;
                        data.PRIMARY_PRICE = paty.EXP_PRICE;
                    }
                }
            }

            //truong hop ko thuc hien, hoac hao phi thi set null
            if (data.IS_EXPEND == Constant.IS_TRUE || data.IS_NO_EXECUTE == Constant.IS_TRUE)
            {
                data.OVERTIME_PRICE = null;
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
        private V_HIS_SERVICE_PATY GetServicePaty(HIS_SERE_SERV data, List<HIS_SERE_SERV> allSereServsOfTreatments, long executeBranchId, long instructionTime, long patientTypeId, long serviceId, long requestRoomId, long requestDepartmentId, long executeRoomId, long? packageId, long? serviceConditionId, long? rationTimeId)
        {
            //Lay thong tin chinh sach gia duoc ap dung cho sere_serv
            V_HIS_SERVICE_PATY appliedServicePaty = HisSereServPriceUtil.GetServicePaty(data, allSereServsOfTreatments, executeBranchId, instructionTime, this.treatment.IN_TIME, patientTypeId, serviceId, requestRoomId, requestDepartmentId, executeRoomId, packageId, serviceConditionId, treatment.TDL_PATIENT_CLASSIFY_ID, rationTimeId);

            if (appliedServicePaty == null)
            {
                HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == patientTypeId).FirstOrDefault();
                V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == serviceId).FirstOrDefault();
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, hisService.SERVICE_NAME, hisService.SERVICE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                throw new Exception();
            }
            return appliedServicePaty;
        }

        //Lay ra gia duoc ap dung cho tram y te xa
        private decimal GetCommunePrice(decimal price, long requestBranchId, long serviceTypeId)
        {
            HIS_BRANCH hisBranch = HisBranchCFG.DATA.Where(o => o.ID == requestBranchId).FirstOrDefault();
            if (hisBranch != null)
            {
                List<long> notUseServiceTypeIds = new List<long>() { 
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC, 
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT, 
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU, 
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH
                };

                //Chi ap dung voi DVKT (ko ap dung voi kham, thuoc, vat tu, mau)
                if (hisBranch.HEIN_LEVEL_CODE == HeinLevelCode.COMMUNE
                    && HisHeinBhytCFG.PAYMENT_RATIO__SERVICE__COMMUNE_LEVEL.HasValue
                    && HisHeinBhytCFG.PAYMENT_RATIO__SERVICE__COMMUNE_LEVEL.Value > 0 //do neu ko co key cau hinh nay thi gia tri la -1
                    && !notUseServiceTypeIds.Contains(serviceTypeId))
                {
                    return price * HisHeinBhytCFG.PAYMENT_RATIO__SERVICE__COMMUNE_LEVEL.Value;
                }
            }
            return price;
        }
    }
}
