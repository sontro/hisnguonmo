using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.LibraryHein.Bhyt.HeinTreatmentType;
using MOS.LibraryHein.Common;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisTreatment;
using MOS.ServicePaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ
{
    internal partial class HisSereServUpdateHein : BusinessBase
    {
        /// <summary>
        /// Cap nhat thong tin + ti le chi tra cho cac dich vu thuoc loai con lai (ko phai KSK va BHYT)
        /// </summary>
        /// <param name="hisSereServs"></param>
        /// <returns></returns>
        private bool UpdateOther(List<HIS_SERE_SERV> hisSereServs, HIS_PATIENT_TYPE_ALTER lastPatientTypeAlter, HIS_TREATMENT treatment)
        {
            bool result = true;
            try
            {
                List<HIS_SERE_SERV> otherSereServs = hisSereServs
                    .Where(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                        && o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__KSK
                        && o.IS_EXPEND != MOS.UTILITY.Constant.IS_TRUE)
                    .ToList();

                if (IsNotNullOrEmpty(otherSereServs))
                {
                    otherSereServs.ForEach(o =>
                    {
                        o.HEIN_PRICE = null;
                        o.HEIN_RATIO = null;
                        o.HEIN_CARD_NUMBER = null;
                        o.JSON_PATIENT_TYPE_ALTER = null;
                    });

                    SereServPriceUtil.UpdateBedPrice(otherSereServs);

                    //Neu co cau hinh ap dung chinh sach tinh gia kham tuong tu nhu cua BHYT thi uu tien xu ly theo key nay truoc
                    //Luu y: rieng chinh sach "BN nhap vien chi tinh gia 1 cong kham" se ko ap dung giong BHYT, chi ap dung cac chinh sach sau:
                    //- Công khám thứ 2 trở đi, nếu khác chuyên khoa sẽ tính giá 30%, tổng giá tất cả các công khám không vượt quá 2 lần công khám đầu tiên.
                    //- Công khám thứ 2 nếu cùng chuyên khoa với công khám trước đấy thì sẽ tính 0đ
                    //- Nếu cùng ngày, BN đi khám buổi sáng, đến buổi chiều BN cũng đến khám (tạo hồ sơ điều trị khác), thì công khám buổi chiều vẫn được tính là khám lần 2 và tính theo công thức trên.
                    //- Công khám cấp cứu, nếu thời gian BN nhập viện quá 4h thì sẽ tính 0đ.
                    if (HisSereServCFG.APPLY_BHYT_EXAM_POLICY_FOR_NON_BHYT_OPTION == HisSereServCFG.ApplyBhytExamPolictyForNonBhytOption.EXCEPT_2TH_EXAM_IN_PATIENT_POLICY
                        || HisSereServCFG.APPLY_BHYT_EXAM_POLICY_FOR_NON_BHYT_OPTION == HisSereServCFG.ApplyBhytExamPolictyForNonBhytOption.APPLY_ALL)
                    {
                        HIS_TREATMENT_TYPE treatmentType = HisTreatmentTypeCFG.DATA.Where(o => o.ID == lastPatientTypeAlter.TREATMENT_TYPE_ID).FirstOrDefault();
                        this.UpdateExamPrice(hisSereServs, treatment, this.recentEmergencyDepartmentId, this.recentEmergencyDepartmentInTime, this.recentEmergencyDepartmentOutTime, treatmentType.HEIN_TREATMENT_TYPE_CODE);
                    }
                    //neu co cau hinh ko tinh tien cho dv kham thu 2 tro di doi voi BN vien phi
                    else if (HisSereServCFG.RATIO_FOR_2ND_EXAM_FOR_HOSPITAL_FEE.HasValue)
                    {
                        List<HIS_SERE_SERV> exams = null;
                        //Neu co cau hinh "luon tinh gia cua DV kham dau tien giong nhu kham chinh" 
                        //thi chi sap xep theo thoi gian y lenh de xac dinh kham chinh
                        //Neu khong thi sap xep theo truong "la kham chinh"
                        if (HisTreatmentCFG.IS_PRICING_FIRST_EXAM_AS_MAIN_EXAM)
                        {
                            exams = otherSereServs
                                .Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH)
                                .OrderBy(o => o.TDL_INTRUCTION_TIME).ThenBy(o => o.ID).ToList();
                        }
                        else
                        {
                            exams = otherSereServs
                                .Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH)
                                .OrderByDescending(o => o.TDL_IS_MAIN_EXAM)
                                .ThenBy(o => o.TDL_INTRUCTION_TIME)
                                .ThenBy(o => o.ID).ToList();
                        }

                        if (IsNotNullOrEmpty(exams))
                        {
                            exams[0].PRICE = exams[0].PRIMARY_PRICE.Value;
                            //tu dich vu thu 2 tro di, tinh gia theo ti le cau hinh
                            if (exams.Count > 1)
                            {
                                for (int i = 1; i < exams.Count; i++)
                                {
                                    exams[i].PRICE = exams[i].PRIMARY_PRICE.Value * HisSereServCFG.RATIO_FOR_2ND_EXAM_FOR_HOSPITAL_FEE.Value;
                                }
                            }
                        }
                    }
                    if (HisSereServCFG.IS_APPLY_ARISING_SURG_POLICY_FOR_NON_BHYT == HisSereServCFG.IsApplyArisingSurgPricePolicyForNonBhytOption.BHYT)
                    {
                        this.UpdateSurgPriceForNonBhyt(otherSereServs);
                    }
                    else if (HisSereServCFG.IS_APPLY_ARISING_SURG_POLICY_FOR_NON_BHYT == HisSereServCFG.IsApplyArisingSurgPricePolicyForNonBhytOption.ALL)
                    {
                        List<long> parent = otherSereServs.Where(o => o.PARENT_ID.HasValue).Select(o => o.PARENT_ID.Value).Distinct().ToList(); //C
                        List<HIS_SERE_SERV> ss = hisSereServs.Where(o => parent.Contains(o.ID) && o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList(); //từ hisSereServs lấy ra sere_serv có ID nằm trong danh sách C và có đối tượng thanh toán là BHYT
                        if (ss != null) otherSereServs.AddRange(ss); // nếu D có dữ liệu thì sẽ thêm vào otherSereServs
                        
                        this.UpdateSurgPriceForNonBhyt(otherSereServs);

                    }
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Cap nhat gia doi voi cac dich vu phau thuat/thu thuat
        /// </summary>
        /// <param name="hisSereServs"></param>
        private void UpdateSurgPriceForNonBhyt(List<HIS_SERE_SERV> hisSereServs)
        {
            foreach (HIS_SERE_SERV sereServ in hisSereServs)
            {
                //Neu dich vu nay la dich vu kem theo cua 1 dich vu khac thi tiep tuc xu ly
                if (sereServ.PARENT_ID.HasValue)
                {
                    //Lay dich vu chinh
                    HIS_SERE_SERV parent = hisSereServs.Where(o => o.ID == sereServ.PARENT_ID.Value).SingleOrDefault();

                    if (parent != null && parent.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT && parent.TDL_EXECUTE_ROOM_ID == sereServ.TDL_EXECUTE_ROOM_ID)
                    {
                        decimal price = sereServ.PRIMARY_PRICE.HasValue ? sereServ.PRIMARY_PRICE.Value : sereServ.ORIGINAL_PRICE;

                        //Neu dich vu dinh kem la dich vu thu thuat
                        if (sereServ.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT)
                        {
                            sereServ.PRICE = price * BhytConstant.ATTACH_MISU_RATIO;
                        }
                        //Neu dich vu dinh kem la dich vu phau thuat, thi kiem tra xem
                        //dich vu do co cung kip thuc hien hay khong
                        else if (sereServ.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT)
                        {
                            //Neu dich vu dinh kem cung kip thuc hien voi dich vu chinh
                            if (sereServ.EKIP_ID.HasValue && parent.EKIP_ID.HasValue
                                && sereServ.EKIP_ID.Value == parent.EKIP_ID.Value)
                            {
                                sereServ.PRICE = price * BhytConstant.ATTACH_SURG_RATIO;
                            }
                            //Neu dich vu dinh kem ko cung kip thuc hien voi dich vu chinh
                            else
                            {
                                sereServ.PRICE = price * BhytConstant.ATTACH_SURG_OTHER_EKIP_RATIO;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Cap nhat gia doi voi cac dich vu kham
        /// </summary>
        /// <param name="hisSereServs"></param>
        private void UpdateExamPrice(List<HIS_SERE_SERV> hisSereServs, HIS_TREATMENT treatment, long? recentDepartmentId, long? recentDepartmentInTime, long? recentDepartmentOutTime, string treatmentTypeCode)
        {
            List<HIS_SERE_SERV> otherTreatments = null;

            //neu co cau hinh tinh tien cong kham lan 2 cho cac ho so dieu tri khac nhau cua cung 1 BN
            if (HisHeinBhytCFG.CALC_2TH_EXAM_FOR_OTHER_TREATMENT)
            {
                otherTreatments = this.GetExamSereServOfOtherTreatment(hisSereServs, treatment);
            }

            //Lay danh sach cac dich vu kham
            List<HIS_SERE_SERV> examList = hisSereServs != null ? hisSereServs.Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH).ToList() : null;

            if (IsNotNullOrEmpty(otherTreatments))
            {
                examList.AddRange(otherTreatments);
            }

            if (IsNotNullOrEmpty(examList))
            {
                //Neu co cau hinh "luon tinh gia cua DV kham dau tien giong nhu kham chinh" 
                //thi chi sap xep theo thoi gian y lenh de xac dinh kham chinh
                //Neu khong thi sap xep theo truong "la kham chinh"
                if (HisTreatmentCFG.IS_PRICING_FIRST_EXAM_AS_MAIN_EXAM)
                {
                    examList = examList.OrderBy(o => o.TDL_INTRUCTION_TIME).ThenBy(o => o.ID).ToList();
                }
                else
                {
                    examList = examList.OrderByDescending(o => o.TDL_IS_MAIN_EXAM).ThenBy(o => o.TDL_INTRUCTION_TIME).ThenBy(o => o.ID).ToList();
                }

                if (treatmentTypeCode.Equals(HeinTreatmentTypeCode.TREAT)
                    && HisSereServCFG.CALC_1_EXAM_FOR_IN_PATIENT
                    && HisSereServCFG.APPLY_BHYT_EXAM_POLICY_FOR_NON_BHYT_OPTION == HisSereServCFG.ApplyBhytExamPolictyForNonBhytOption.APPLY_ALL)
                {
                    if (examList.Count >= 2)
                    {
                        //chi cap nhat voi cac sere_serv thuoc cung ho so dieu tri
                        if (treatment.ID == examList[0].TDL_TREATMENT_ID)
                        {
                            examList[0] = SereServPriceUtil.SetPrice(examList[0], examList[0].ORIGINAL_PRICE);
                        }

                        for (int i = 1; i < examList.Count; i++)
                        {
                            //chi cap nhat voi cac sere_serv thuoc cung ho so dieu tri
                            if (treatment.ID == examList[i].TDL_TREATMENT_ID)
                            {
                                examList[i] = this.SetZeroPrice(examList[i]);
                            }
                        }
                    }

                    //Doi voi BN dieu tri noi tru co cong kham chinh thuoc phong kham cap cuu, 
                    //co thoi gian dieu tri lon hon 4h nhung co gia cong kham > 0 thi cap nhat lai thanh 0đ
                    if (this.IsApplyEmergencyExamPolicy(treatmentTypeCode, treatment, examList[0], recentDepartmentId, recentDepartmentInTime, recentDepartmentOutTime))
                    {
                        examList[0] = this.SetZeroPrice(examList[0]);
                    }
                }
                else
                {

                    //Gioi han tong gia tien cua cac dich vu kham (=2 lan nguyen gia (gia bao gom VAT) cua dich vu kham dau tien)
                    decimal firstPrice = examList[0].PRIMARY_PRICE.HasValue ? examList[0].PRIMARY_PRICE.Value : examList[0].ORIGINAL_PRICE;
                    decimal limit = 2 * firstPrice * (1 + examList[0].VAT_RATIO); //nhan voi 1 + vat ==> tinh ra nguyen gia

                    //Tong so tien cua cac dich vu kham
                    decimal totalPrice = 0;
                    int beginCount = 0;

                    //duyet de bo sung speciality_code
                    for (int i = 0; i < examList.Count; i++)
                    {
                        string specialityCode = HisServiceCFG.DATA_VIEW
                            .Where(o => o.ID == examList[i].SERVICE_ID)
                            .FirstOrDefault().SPECIALITY_CODE;
                        examList[i].TDL_SPECIALITY_CODE = specialityCode;
                    }

                    //duyet de lay ra cac dich vu chinh (quy tac: moi speciality_code se co 1 dich vu chinh)
                    //(uu tien primary, neu cung primary thi uu tien thoi gian chi dinh som hon)
                    //var orderPrimaryList = examList.OrderBy(o => !o.IS_ADDITION.HasValue ? 0 : o.IS_ADDITION.Value).ThenBy(o => o.INTRUCTION_TIME).ThenBy(o => o.ID).ToList();
                    List<HIS_SERE_SERV> primaryList = new List<HIS_SERE_SERV>();

                    foreach (var s in examList)
                    {
                        if (string.IsNullOrWhiteSpace(s.TDL_SPECIALITY_CODE) || !primaryList.Exists(t => t.TDL_SPECIALITY_CODE == s.TDL_SPECIALITY_CODE))
                        {
                            primaryList.Add(s);
                        }
                    }

                    int ratioIndex = 0;

                    int begin = 0;
                    //Doi voi BN dieu tri noi tru co cong kham chinh thuoc phong kham cap cuu, 
                    //co thoi gian dieu tri lon hon 4h nhung co gia cong kham > 0 thi cap nhat lai thanh 0đ
                    if (this.IsApplyEmergencyExamPolicy(treatmentTypeCode, treatment, examList[0], recentDepartmentId, recentDepartmentInTime, recentDepartmentOutTime))
                    {
                        examList[0] = this.SetZeroPrice(examList[0]);
                        begin = 1;
                    }

                    if (examList.Count > begin)
                    {
                        for (int i = begin; i < examList.Count; i++)
                        {
                            //neu ko nam trong d/s dich vu kham chinh thi ko tinh tien --> cong kham thu 2 cung chuyen khoa voi 1 trong cac cong kham chinh
                            if (examList[i].TDL_SPECIALITY_CODE != null && !primaryList.Exists(t => t.ID == examList[i].ID))
                            {
                                //chi cap nhat voi cac sere_serv thuoc cung ho so dieu tri
                                if (treatment.ID == examList[i].TDL_TREATMENT_ID)
                                {
                                    examList[i] = this.SetZeroPrice(examList[i]);
                                }
                            }
                            else
                            {
                                decimal ratio = BhytConstant.MAP_INDEX_TO_PRICE_RATIO_EXAM.ContainsKey(ratioIndex + beginCount) ? BhytConstant.MAP_INDEX_TO_PRICE_RATIO_EXAM[ratioIndex + beginCount] : 0;

                                decimal price = examList[i].PRIMARY_PRICE.HasValue ? examList[i].PRIMARY_PRICE.Value : examList[i].ORIGINAL_PRICE;
                                decimal newPrice = ratio * price * (1 + examList[i].VAT_RATIO);

                                //Neu sau khi nhan ti le van chua vuot qua gia tri gioi han thi ti le nay se duoc ap dung
                                if (newPrice + totalPrice <= limit)
                                {
                                    //chi cap nhat voi cac sere_serv thuoc cung ho so dieu tri
                                    if (treatment.ID == examList[i].TDL_TREATMENT_ID)
                                    {
                                        examList[i] = this.SetExamPrice(examList[i], ratio * price);
                                    }
                                }
                                //Neu sau khi nhan ti le vuot qua gia tri gioi han thi thuc hien tinh lai
                                else
                                {
                                    newPrice = (limit - totalPrice) < 0 ? 0 : (limit - totalPrice);

                                    //chi cap nhat voi cac sere_serv thuoc cung ho so dieu tri
                                    if (treatment.ID == examList[i].TDL_TREATMENT_ID)
                                    {
                                        examList[i] = this.SetExamPrice(examList[i], newPrice / (examList[i].VAT_RATIO + 1));//chia cho 1 + VAT de lay ra don gia
                                    }
                                }
                                //Cap nhat lai tong so tien cua cac dich vu kham
                                totalPrice += this.GetPrice(examList[i]) * (1 + examList[i].VAT_RATIO);
                                ratioIndex++;
                            }
                        }
                    }
                }
            }
        }

        private HIS_SERE_SERV SetZeroPrice(HIS_SERE_SERV ss)
        {
            if (ss.HEIN_LIMIT_PRICE.HasValue)
            {
                ss.HEIN_LIMIT_PRICE = 0;
            }
            ss.PRICE = 0;
            ss.OVERTIME_PRICE = null;//de tranh truong hop OVERTIME_PRICE > PRICE
            return ss;
        }

        /// <summary>
        /// Cap nhat gia cho dich vu kham
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="newPrice">Gia moi</param>
        /// <param name="firstDifferPrice">Gia chenh lech cua dich vu kham dau tien</param>
        /// <returns></returns>
        private HIS_SERE_SERV SetExamPrice(HIS_SERE_SERV ss, decimal newPrice)
        {
            ss.PRICE = newPrice;
            ss.OVERTIME_PRICE = ss.OVERTIME_PRICE.HasValue && ss.OVERTIME_PRICE.Value > ss.PRICE ? ss.PRICE : ss.OVERTIME_PRICE;//de tranh truong hop OVERTIME_PRICE > PRICE
            return ss;
        }

        private decimal GetPrice(HIS_SERE_SERV ss)
        {
            return ss.HEIN_LIMIT_PRICE.HasValue ? ss.HEIN_LIMIT_PRICE.Value : ss.PRICE;
        }

        private bool IsApplyEmergencyExamPolicy(string treatmentTypeCode, HIS_TREATMENT treatment, HIS_SERE_SERV firstExam, long? recentDepartmentId, long? recentDepartmentInTime, long? recentDepartmentOutTime)
        {
            if (HisHeinBhytCFG.EMERGENCY_EXAM_POLICY_OPTION == HisHeinBhytCFG.EmergencyExamPolicyOption.BY_EXECUTE_ROOM)
            {
                if (treatment != null && treatment.CLINICAL_IN_TIME.HasValue && treatment.OUT_TIME.HasValue)
                {
                    DateTime? clinicalInTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(treatment.CLINICAL_IN_TIME.Value);
                    DateTime? outTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(treatment.OUT_TIME.Value);

                    //Tinh thoi gian dieu tri noi tru
                    double hours = (outTime.Value - clinicalInTime.Value).TotalSeconds / 3600;

                    //Kiem tra xem cong kham chinh cua BN co phai duoc xu ly tai phong cap cuu hay ko
                    V_HIS_EXECUTE_ROOM firstExamRoom = firstExam != null ? HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == firstExam.TDL_EXECUTE_ROOM_ID).FirstOrDefault() : null;
                    return firstExamRoom != null
                        && firstExamRoom.IS_EMERGENCY == Constant.IS_TRUE
                        && hours >= BhytConstant.CLINICAL_TIME_FOR_EMERGENCY
                        && treatmentTypeCode.Equals(HeinTreatmentTypeCode.TREAT);
                }
            }
            else if (HisHeinBhytCFG.EMERGENCY_EXAM_POLICY_OPTION == HisHeinBhytCFG.EmergencyExamPolicyOption.BY_EXECUTE_DEPARTMENT)
            {
                if (recentDepartmentId.HasValue && recentDepartmentInTime.HasValue && recentDepartmentOutTime.HasValue)
                {
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == recentDepartmentId.Value).FirstOrDefault();

                    DateTime? inTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(recentDepartmentInTime.Value);
                    DateTime? outTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(recentDepartmentOutTime.Value);

                    //Tinh thoi gian BN t khoa cap cuu
                    double hours = (outTime.Value - inTime.Value).TotalSeconds / 3600;

                    //Neu khoa la khoa cap cuu va BN nam o khoa cap cuu hon 4h thi cap nhat cac chi dinh kham cua khoa cap cuu ve 0d

                    return department != null && department.IS_EMERGENCY == Constant.IS_TRUE && hours >= BhytConstant.CLINICAL_TIME_FOR_EMERGENCY
                        && firstExam.TDL_EXECUTE_DEPARTMENT_ID == recentDepartmentId;
                }
            }
            return false;
        }

        /// <summary>
        /// Lay cac dv thuoc ho so dieu tri khac nhung cung BN va co thoi gian chi dinh trong ngay voi cac dv thuoc ho so dieu tri nay
        /// </summary>
        /// <param name="sereServs"></param>
        /// <param name="treatment"></param>
        /// <returns></returns>
        private List<HIS_SERE_SERV> GetExamSereServOfOtherTreatment(List<HIS_SERE_SERV> sereServs, HIS_TREATMENT treatment)
        {
            List<HIS_SERE_SERV> exams = sereServs.Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH).ToList();
            if (IsNotNullOrEmpty(exams))
            {
                long minInstructionTime = exams.Min(t => t.TDL_INTRUCTION_TIME);
                long maxInstructionTime = exams.Max(t => t.TDL_INTRUCTION_TIME);

                long fromTime = Inventec.Common.DateTime.Get.StartDay(minInstructionTime).Value;
                long toTime = Inventec.Common.DateTime.Get.EndDay(maxInstructionTime).Value;
                //Loc ra cac chi nhanh co cung ma KCB ban dau voi ho so hien tai
                HIS_BRANCH currentBranch = HisBranchCFG.DATA.Where(o => o.ID == treatment.BRANCH_ID).FirstOrDefault();
                List<long> sameHeinOrgCodeBranchIds = HisBranchCFG.DATA.Where(o => o.HEIN_MEDI_ORG_CODE == currentBranch.HEIN_MEDI_ORG_CODE).Select(o => o.ID).ToList();

                HisSereServFilterQuery filter = new HisSereServFilterQuery();
                filter.TDL_PATIENT_ID = treatment.PATIENT_ID;
                filter.TDL_INTRUCTION_TIME_FROM = fromTime;
                filter.TDL_INTRUCTION_TIME_TO = toTime;
                filter.TREATMENT_ID__NOT_EQUAL = treatment.ID;
                filter.TDL_SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH;
                filter.HAS_EXECUTE = true;
                filter.IS_EXPEND = false;
                filter.TDL_EXECUTE_BRANCH_IDs = sameHeinOrgCodeBranchIds;

                return new HisSereServGet().Get(filter);
            }
            return null;
        }

        /// <summary>
        /// Cap nhat gia cua dich vu PTTT
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="newPrice"></param>
        /// <returns></returns>
        private HIS_SERE_SERV SetSurgPrice(HIS_SERE_SERV ss, decimal newPrice)
        {
            ss.PRICE = newPrice;
            return ss;
        }
    }
}
