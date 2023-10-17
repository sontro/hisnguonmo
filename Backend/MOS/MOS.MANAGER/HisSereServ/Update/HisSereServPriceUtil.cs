using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceHein;
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
    public class HisSereServPriceUtil : BusinessBase
    {
        public HisSereServPriceUtil()
            : base()
        {
        }

        public HisSereServPriceUtil(CommonParam param)
            : base(param)
        {
        }

        public void SetBhytPrice(HIS_SERE_SERV data, HIS_TREATMENT treatment, long instructionTime, V_HIS_SERVICE hisService)
        {
            this.SetBhytPrice(data, treatment, instructionTime, hisService, null, null);
        }

        public void SetBhytPrice(HIS_SERE_SERV data, HIS_TREATMENT treatment, long instructionTime, V_HIS_SERVICE hisService, string icdCode, string icdSubCode)
        {
            if (data.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
            {
                if (!this.IsHasBhytInfo(hisService))
                {
                    throw new Exception();
                }

                decimal? heinLimitRatio = null;
                decimal? heinLimitPrice = null;

                this.GetHeinLimitPrice(hisService, instructionTime, treatment, data.SERVICE_CONDITION_ID, data.TDL_EXECUTE_BRANCH_ID, icdCode, icdSubCode, ref heinLimitPrice, ref heinLimitRatio);

                if (!this.IsNoHeinLimit(data) && (heinLimitPrice.HasValue || heinLimitRatio.HasValue))
                {
                    //can review lai code ==> bo sung nghiep vu thanh toan theo ti le
                    //Neu la dich vu BHYT thiet lap ti le tran (thuoc/vat tu thanh toan theo ti le)
                    if (heinLimitPrice.HasValue)
                    {
                        data.CONFIG_HEIN_LIMIT_PRICE = heinLimitPrice;
                    }

                    if (heinLimitPrice.HasValue && heinLimitPrice <= data.PRICE)
                    {
                        data.HEIN_LIMIT_PRICE = heinLimitPrice;
                        //neu co gia tran thi set lai bang gia tran
                        //data.ORIGINAL_PRICE = data.HEIN_LIMIT_PRICE.Value * (1 + data.VAT_RATIO);
                        data.ORIGINAL_PRICE = data.HEIN_LIMIT_PRICE.Value / (1 + data.VAT_RATIO);
                    }
                    else if (heinLimitRatio.HasValue && heinLimitRatio.Value < 1)
                    {
                        data.HEIN_LIMIT_PRICE = heinLimitRatio.Value * data.PRICE * (1 + data.VAT_RATIO);
                    }
                }
                else if (!data.PRIMARY_PATIENT_TYPE_ID.HasValue)
                {
                    data.HEIN_LIMIT_PRICE = null;
                }

                //Neu nguoi dung chon "ko tinh gia chenh lech" thi gan lai gia
                if (data.HEIN_LIMIT_PRICE.HasValue && data.IS_NO_HEIN_DIFFERENCE == Constant.IS_TRUE)
                {
                    data.PRICE = data.HEIN_LIMIT_PRICE.Value;
                }
            }
            else
            {
                data.HEIN_LIMIT_PRICE = null;
            }
        }

        private bool IsHasBhytInfo(V_HIS_SERVICE hisService)
        {
            if (!hisService.HEIN_SERVICE_TYPE_ID.HasValue)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuThieuThongTinNhomBhyt, hisService.SERVICE_CODE, hisService.SERVICE_NAME);
                return false;
            }
            if (hisService.SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC
                && hisService.SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT)
            {
                if (string.IsNullOrWhiteSpace(hisService.HEIN_SERVICE_BHYT_CODE) || string.IsNullOrWhiteSpace(hisService.HEIN_SERVICE_BHYT_NAME))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuThieuThongTinMaHoacTenBhyt, hisService.SERVICE_CODE, hisService.SERVICE_NAME);
                    return false;
                }
            }
            else if (hisService.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT)
            {
                List<long> bhytMaterials = new List<long>(){
                    IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TDM,
                    IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TL,
                    IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TT,
                    IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__MAU,
                    IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__CPM
                };

                if (!bhytMaterials.Contains(hisService.HEIN_SERVICE_TYPE_ID.Value))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_VatTuSaiNhomBhyt, hisService.SERVICE_CODE, hisService.SERVICE_NAME);
                    return false;
                }
            }
            else if (hisService.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC)
            {
                List<long> bhytMedicines = new List<long>(){
                    IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_TDM,
                    IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_TL,
                    IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_UT,
                    IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__MAU,
                    IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__CPM
                };

                if (string.IsNullOrWhiteSpace(hisService.ACTIVE_INGR_BHYT_CODE) || !bhytMedicines.Contains(hisService.HEIN_SERVICE_TYPE_ID.Value))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_ThuocThieuThongTinBhyt, hisService.SERVICE_CODE, hisService.SERVICE_NAME);
                    return false;
                }
            }
            return true;
        }

        private HIS_SERVICE_HEIN GetServiceHein(HIS_TREATMENT treatment, string icdCode, string icdSubCode, long serviceId, long branchId, long instructionTime)
        {
            HIS_SERVICE_HEIN result = null;
            if (HisServiceHeinCFG.DATA != null)
            {
                int age = Inventec.Common.DateTime.Calculation.Age(treatment.TDL_PATIENT_DOB);

                //lay ca icd trong treatment va trong service_req
                string treatmentIcdCodeStr = ";" + treatment.ICD_CODE + ";" + treatment.ICD_SUB_CODE + ";" + icdCode + ";" + icdSubCode + ";";

                List<HIS_SERVICE_HEIN> actives = HisServiceHeinCFG.DATA
                    .Where(o => o.SERVICE_ID == serviceId && o.BRANCH_ID == branchId && o.IS_ACTIVE == 1)
                    .Where(o => ((!o.FROM_TIME.HasValue || o.FROM_TIME.Value <= instructionTime)
                        && (!o.TO_TIME.HasValue || o.TO_TIME.Value >= instructionTime))
                        || ((!o.TREATMENT_FROM_TIME.HasValue || o.TREATMENT_FROM_TIME.Value <= treatment.IN_TIME)
                        && (!o.TREATMENT_TO_TIME.HasValue || o.TREATMENT_TO_TIME.Value >= treatment.IN_TIME)))
                    .Where(o => (!o.AGE_FROM.HasValue || o.AGE_FROM.Value <= age) && (!o.AGE_TO.HasValue || o.AGE_TO.Value >= age))
                    .ToList();

                if (IsNotNullOrEmpty(actives))
                {
                    List<HIS_SERVICE_HEIN> lst = new List<HIS_SERVICE_HEIN>();
                    foreach (HIS_SERVICE_HEIN s in actives)
                    {
                        List<string> icdCodes = !string.IsNullOrWhiteSpace(s.ICD_CODES) ? s.ICD_CODES.Split(';').ToList() : null;
                        if (icdCodes == null || icdCodes.Count == 0 || icdCodes.Exists(t => treatmentIcdCodeStr.Contains(";" + t + ";")))
                        {
                            lst.Add(s);
                        }
                    }
                    result = IsNotNullOrEmpty(lst) ? lst
                        .OrderByDescending(o => o.PRIORITY)
                        .ThenByDescending(o => o.ID)
                        .FirstOrDefault() : null;
                }
            }
            return result;
        }

        /// <summary>
        /// Lay gia tran BHYT cua dich vu/thuoc/vat tu
        /// Gia tran co the thiet lap truc tiep trong danh muc dich vu (his_service) hoac theo thiet lap dich vu dieu kien (his_service_condition)
        /// </summary>
        /// <param name="hisService"></param>
        /// <param name="instructionTime"></param>
        /// <param name="treatment"></param>
        /// <param name="serviceConditionId"></param>
        /// <param name="executeBranchId"></param>
        /// <param name="icdCode"></param>
        /// <param name="icdSubCode"></param>
        /// <param name="heinLimitPrice"></param>
        /// <param name="heinLimitRatio"></param>
        public void GetHeinLimitPrice(V_HIS_SERVICE hisService, long instructionTime, HIS_TREATMENT treatment, long? serviceConditionId, long executeBranchId, string icdCode, string icdSubCode, ref decimal? heinLimitPrice, ref decimal? heinLimitRatio)
        {
            //Neu co thong tin "dich vu dieu kien" thi lay gia tran theo thiet lap dich vu dieu kien
            if (serviceConditionId.HasValue)
            {
                HIS_SERVICE_CONDITION condition = HisServiceConditionCFG.DATA != null ? HisServiceConditionCFG.DATA.Where(o => o.ID == serviceConditionId.Value).FirstOrDefault() : null;
                if (condition == null || condition.SERVICE_ID != hisService.ID)
                {
                    LogSystem.Warn("Khong ton tai HIS_SERVICE_CONDITION hoac HIS_SERVICE_CONDITION ko tuong ung voi dich vu duoc chon. serviceConditionId:" + serviceConditionId.Value + " ServiceId: " + hisService.ID);
                }
                else
                {
                    heinLimitRatio = condition.HEIN_RATIO;
                    heinLimitPrice = condition.HEIN_PRICE;
                }
            }

            //neu dich vu khai bao ti le tran
            if (hisService.IS_SPECIFIC_HEIN_PRICE == Constant.IS_TRUE)
            {
                HIS_SERVICE_HEIN serviceHein = this.GetServiceHein(treatment, icdCode, icdSubCode, hisService.ID, executeBranchId, instructionTime);
                heinLimitRatio = serviceHein != null ? serviceHein.RATIO : null;
            }
            else if (hisService.HEIN_LIMIT_RATIO.HasValue || hisService.HEIN_LIMIT_RATIO_OLD.HasValue)
            {
                //neu gia ap dung theo ngay vao vien, thi cac benh nhan vao vien truoc ngay ap dung se lay gia cu
                if (hisService.HEIN_LIMIT_PRICE_IN_TIME.HasValue)
                {
                    heinLimitRatio = treatment.IN_TIME < hisService.HEIN_LIMIT_PRICE_IN_TIME.Value ? hisService.HEIN_LIMIT_RATIO_OLD : hisService.HEIN_LIMIT_RATIO;
                }
                //neu ap dung theo ngay chi dinh, thi cac chi dinh truoc ngay ap dung se tinh gia cu
                else if (hisService.HEIN_LIMIT_PRICE_INTR_TIME.HasValue)
                {
                    heinLimitRatio = instructionTime < hisService.HEIN_LIMIT_PRICE_INTR_TIME.Value ? hisService.HEIN_LIMIT_RATIO_OLD : hisService.HEIN_LIMIT_RATIO;
                }
                //neu ca 2 truong ko co gia tri thi luon lay theo gia moi
                else
                {
                    heinLimitRatio = hisService.HEIN_LIMIT_RATIO;
                }
            }
            //neu dich vu khai bao gia tran
            else if (hisService.HEIN_LIMIT_PRICE.HasValue || hisService.HEIN_LIMIT_PRICE_OLD.HasValue)
            {
                //neu gia ap dung theo ngay vao vien, thi cac benh nhan vao vien truoc ngay ap dung se lay gia cu
                if (hisService.HEIN_LIMIT_PRICE_IN_TIME.HasValue)
                {
                    heinLimitPrice = treatment.IN_TIME < hisService.HEIN_LIMIT_PRICE_IN_TIME.Value ? hisService.HEIN_LIMIT_PRICE_OLD : hisService.HEIN_LIMIT_PRICE;
                }
                //neu ap dung theo ngay chi dinh, thi cac chi dinh truoc ngay ap dung se tinh gia cu
                else if (hisService.HEIN_LIMIT_PRICE_INTR_TIME.HasValue)
                {
                    heinLimitPrice = instructionTime < hisService.HEIN_LIMIT_PRICE_INTR_TIME.Value ? hisService.HEIN_LIMIT_PRICE_OLD : hisService.HEIN_LIMIT_PRICE;
                }
                //neu ca 2 truong ko co gia tri thi luon lay theo gia moi
                else
                {
                    heinLimitPrice = hisService.HEIN_LIMIT_PRICE;
                }
            }
        }

        /// <summary>
        /// Kiem tra xem co can phai set gia tran ko.
        /// (co cau hinh ko lay gia tran thuoc/vat tu doi voi 1 so dau ma the BHYT)
        /// </summary>
        /// <param name="sereServ"></param>
        /// <returns></returns>
        private bool IsNoHeinLimit(HIS_SERE_SERV sereServ)
        {
            if (!string.IsNullOrWhiteSpace(sereServ.HEIN_CARD_NUMBER)
                && IsNotNullOrEmpty(HisHeinBhytCFG.NO_LIMIT_MEDICINE_MATERIAL_PRICE__PREFIXs)
                && (sereServ.MEDICINE_ID.HasValue || sereServ.MATERIAL_ID.HasValue)
                && HisServiceCFG.NO_HEIN_LIMIT_FOR_SPECIAL_CARD != null
                && HisServiceCFG.NO_HEIN_LIMIT_FOR_SPECIAL_CARD.Contains(sereServ.SERVICE_ID))
            {

                foreach (string prefix in HisHeinBhytCFG.NO_LIMIT_MEDICINE_MATERIAL_PRICE__PREFIXs)
                {
                    if (sereServ.HEIN_CARD_NUMBER.StartsWith(prefix))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Lay gia cua dich vu. Can cu:
        /// - Chinh sach gia tuong ung voi service tai thoi diem chi dinh (instruction_time va treatment_time)
        /// - Cac quy dinh cua bo y te lien quan den dich vu kham, dich vu phau thuat
        /// </summary>
        /// <param name="instructionTime"></param>
        /// <param name="treatmentTime"></param>
        /// <returns></returns>
        public static V_HIS_SERVICE_PATY GetServicePaty(HIS_SERE_SERV data, List<HIS_SERE_SERV> allSereServsOfTreatments, long executeBranchId, long instructionTime, long inTime, long patientTypeId, long serviceId, long requestRoomId, long requestDepartmentId, long executeRoomId, long? packageId, long? serviceConditionId, long? patientClassifyId, long? rationTimeId)
        {
            long? instructionNumber = null;
            long? instructionNumberByType = null;

            //Xu ly de lay thu tu lan chi dinh
            if (allSereServsOfTreatments != null)
            {
                HIS_SERE_SERV[] orderByServiceIds = allSereServsOfTreatments
                    .Where(t => t.SERVICE_ID == data.SERVICE_ID
                        && t.IS_NO_EXECUTE != Constant.IS_TRUE
                        && t.IS_DELETE != Constant.IS_TRUE)//ko lay dieu kien service_req_id != null de tranh truong hop sere_serv tao moi thi chua co service_req_id
                    .OrderBy(t => t.TDL_INTRUCTION_TIME)
                    .ThenBy(t => t.ID)
                    .ToArray();

                HIS_SERE_SERV[] orderByServiceTypeIds = allSereServsOfTreatments
                        .Where(t => t.TDL_SERVICE_TYPE_ID == data.TDL_SERVICE_TYPE_ID
                            && t.IS_NO_EXECUTE != Constant.IS_TRUE
                            && t.IS_EXPEND != Constant.IS_TRUE //voi nghiep vu nay ko lay hao phi
                            && t.IS_DELETE != Constant.IS_TRUE) //ko lay dieu kien service_req_id != null de tranh truong hop sere_serv tao moi thi chua co service_req_id
                        .OrderBy(t => t.TDL_INTRUCTION_TIME)
                        .ThenBy(t => t.ID)
                        .ToArray();

                instructionNumberByType = orderByServiceTypeIds != null && orderByServiceTypeIds.Length > 0 ? orderByServiceTypeIds.FindIndex(t => t.ID == data.ID) + 1 : 1;
                instructionNumber = orderByServiceIds != null && orderByServiceIds.Length > 0 ? orderByServiceIds.FindIndex(t => t.ID == data.ID) + 1 : 1;

                instructionNumberByType = instructionNumberByType == 0 ? 1 : instructionNumberByType;
                instructionNumber = instructionNumber == 0 ? 1 : instructionNumber;
            }

            //Trong truong hop goi ko check "ko co dinh dich vu trong goi" thi moi lay chinh sach gia theo package_id
            long? p = null;
            if (packageId.HasValue && (HisPackageCFG.NOT_FIXED_SERVICE_PACKAGE_IDS == null || !HisPackageCFG.NOT_FIXED_SERVICE_PACKAGE_IDS.Contains(packageId.Value)))
            {
                p = packageId;
            }

            //Lay thong tin chinh sach gia duoc ap dung cho sere_serv
            V_HIS_SERVICE_PATY appliedServicePaty = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, executeBranchId, executeRoomId, requestRoomId, requestDepartmentId, instructionTime, inTime, serviceId, patientTypeId, instructionNumber, instructionNumberByType, p, serviceConditionId, patientClassifyId, rationTimeId);

            return appliedServicePaty;
        }
    }
}
