using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt.HeinTreatmentType;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisEquipmentSet;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisServicePaty;
using MOS.MANAGER.HisTreatment;
using MOS.ServicePaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ
{
    class SetPriceWithBhytPolicy : BusinessBase
    {
        internal SetPriceWithBhytPolicy(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal SetPriceWithBhytPolicy()
            : base()
        {

        }

        /// <summary>
        /// Cap nhat lai gia cho cac dich vu BHYT theo chinh sach cua Bo Y Te
        /// </summary>
        /// <param name="hisSereServs"></param>
        internal void Update(List<HIS_SERE_SERV> hisSereServs, HIS_TREATMENT treatment, string treatmentTypeCode, long? recentDepartmentId, long? recentDepartmentInTime, long? recentDepartmentOutTime)
        {
            if (IsNotNullOrEmpty(hisSereServs))
            {
                this.UpdateExamPrice(hisSereServs, treatment, recentDepartmentId, recentDepartmentInTime, recentDepartmentOutTime, treatmentTypeCode);
                this.UpdateSurgPrice(hisSereServs);
                this.UpdateEquipmentSet(hisSereServs);
                SereServPriceUtil.UpdateBedPrice(hisSereServs);
            }
        }

        /// <summary>
        /// Cap nhat gia theo quy dinh về cách tính giá "bộ vật tư"
        /// </summary>
        /// <param name="hisSereServs"></param>
        private void UpdateEquipmentSet(List<HIS_SERE_SERV> hisSereServs)
        {
            List<HIS_SERE_SERV> equipmentSetSereServs = hisSereServs != null ? hisSereServs.Where(o => o.EQUIPMENT_SET_ID.HasValue).ToList() : null;
            if (IsNotNullOrEmpty(equipmentSetSereServs))
            {
                List<long> equipmentSetIds = equipmentSetSereServs.Select(o => o.EQUIPMENT_SET_ID.Value).ToList();
                List<HIS_EQUIPMENT_SET> equipmentSets = new HisEquipmentSetGet().GetByIds(equipmentSetIds);

                //Gom nhom theo cac "bộ vật tư" (được định danh bằng equipment_set_id và equipment_set_order)
                var groups = equipmentSetSereServs.GroupBy(o => new { o.EQUIPMENT_SET_ID, o.EQUIPMENT_SET_ORDER });
                foreach (var g in groups)
                {
                    HIS_EQUIPMENT_SET eq = equipmentSets.Where(o => o.ID == g.Key.EQUIPMENT_SET_ID).FirstOrDefault();
                    if (eq != null && eq.HEIN_SET_LIMIT_PRICE.HasValue)
                    {
                        List<HIS_SERE_SERV> ss = g.ToList()
                            .Where(o => !o.IS_NO_EXECUTE.HasValue && o.AMOUNT > 0 && !o.IS_EXPEND.HasValue)
                            .OrderBy(o => o.PRICE)//luu y: can sap xep de xu ly cac price nho truoc
                            .ToList();

                        if (!IsNotNullOrEmpty(ss))
                        {
                            continue;
                        }

                        //Tinh tong gia cua bo vat tu
                        //Luu y: ko dung truc tiep truong VIR_PRICE, do co the mot so truong dung de nội suy ra
                        //trường này đã bị thay đổi nhưng hệ thống chưa truy vấn lại DB --> chưa cập nhật lại vir_price
                        decimal totalPrice = ss.Sum(o => this.VirPrice(o));
                        //Lam tron 2 chu so sau phan thap phan
                        decimal ratio = Math.Round(eq.HEIN_SET_LIMIT_PRICE.Value / totalPrice, 2);
                        //Chi xu ly khi gia tran nho hon tong gia cua bo vat tu
                        if (ratio < 1)
                        {
                            int index = 0;
                            decimal tmpPrice = 0;
                            foreach (HIS_SERE_SERV s in ss)
                            {
                                index++;
                                if (index < ss.Count)
                                {
                                    s.HEIN_LIMIT_PRICE = Math.Round(ratio * this.VirPrice(s), 2);
                                    tmpPrice += s.HEIN_LIMIT_PRICE.Value * s.AMOUNT;
                                }
                                else
                                {
                                    s.HEIN_LIMIT_PRICE = (eq.HEIN_SET_LIMIT_PRICE.Value - tmpPrice) / s.AMOUNT;
                                }
                                s.ORIGINAL_PRICE = s.HEIN_LIMIT_PRICE.Value / (1 + s.VAT_RATIO);
                            }
                        }
                    }
                }
            }
        }

        //Cap nhat cac gia cua cac dich vu co gia lien quan den lan chi dinh
        private void UpdatePriceDependOnInstrucionNumber(List<HIS_SERE_SERV> hisSereServs, HIS_TREATMENT treatment)
        {
            if (IsNotNullOrEmpty(hisSereServs))
            {
                //Kiem tra xem co chinh sach gia nao lien quan den so lan chi dinh tuong ung voi danh sach sere_serv hien tai ko
                List<long> serviceIds = HisServicePatyCFG.DATA
                    .Where(o => o.IS_ACTIVE == Constant.IS_TRUE && o.INTRUCTION_NUMBER_FROM.HasValue && hisSereServs.Where(t => t.SERVICE_ID == o.SERVICE_ID && t.PATIENT_TYPE_ID == o.PATIENT_TYPE_ID).Any())
                    .Select(o => o.SERVICE_ID).Distinct().ToList();

                //Neu co thi moi thuc hien xu ly tiep
                if (IsNotNullOrEmpty(serviceIds))
                {
                    HisServicePatyGet servicePatyGet = new HisServicePatyGet();

                    foreach (long serviceId in serviceIds)
                    {
                        //Sap xep theo thoi gian chi dinh
                        List<HIS_SERE_SERV> services = hisSereServs
                            .Where(o => o.SERVICE_ID == serviceId)
                            .OrderBy(o => o.TDL_INTRUCTION_TIME)
                            .ThenBy(o => o.ID).ToList();

                        int instructionNumber = 1;
                        foreach (HIS_SERE_SERV s in services)
                        {
                            V_HIS_SERVICE_PATY servicePaty = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, s.TDL_EXECUTE_BRANCH_ID, s.TDL_EXECUTE_ROOM_ID, s.TDL_REQUEST_ROOM_ID, s.TDL_REQUEST_DEPARTMENT_ID, s.TDL_INTRUCTION_TIME, treatment.IN_TIME, s.SERVICE_ID, s.PATIENT_TYPE_ID, instructionNumber);

                            if (servicePaty == null)
                            {
                                V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == s.SERVICE_ID).FirstOrDefault();
                                HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.Where(o => o.ID == s.PATIENT_TYPE_ID).FirstOrDefault();
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, service.SERVICE_NAME, service.SERVICE_CODE, patientType.PATIENT_TYPE_NAME);
                                throw new Exception();
                            }

                            SereServPriceUtil.SetPrice(s, servicePaty.PRICE);
                            instructionNumber++;
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
                    && HisSereServCFG.CALC_1_EXAM_FOR_IN_PATIENT)
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
                    decimal limit = 2 * examList[0].ORIGINAL_PRICE * (1 + examList[0].VAT_RATIO); //nhan voi 1 + vat ==> tinh ra nguyen gia
                    //Lay gia chenh lech cua dich vu kham dau tien
                    decimal firstDifferPrice = examList[0].PRICE - (examList[0].HEIN_LIMIT_PRICE ?? examList[0].PRICE);

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
                                    if (HisHeinBhytCFG.SET_ZERO_TO_2TH_SAME_SPECIALITY_EXAM_PRICE)
                                    {
                                        examList[i] = this.SetZeroPrice(examList[i]);
                                    }
                                    else
                                    {
                                        examList[i] = this.SetExamPrice(examList[i], 0, firstDifferPrice, treatment.IN_TIME);
                                    }
                                }
                            }
                            else
                            {
                                decimal ratio = BhytConstant.MAP_INDEX_TO_PRICE_RATIO_EXAM.ContainsKey(ratioIndex + beginCount) ? BhytConstant.MAP_INDEX_TO_PRICE_RATIO_EXAM[ratioIndex + beginCount] : 0;
                                decimal newPrice = ratio * examList[i].ORIGINAL_PRICE * (1 + examList[i].VAT_RATIO);

                                //Neu sau khi nhan ti le van chua vuot qua gia tri gioi han thi ti le nay se duoc ap dung
                                if (newPrice + totalPrice <= limit)
                                {
                                    //chi cap nhat voi cac sere_serv thuoc cung ho so dieu tri
                                    if (treatment.ID == examList[i].TDL_TREATMENT_ID)
                                    {
                                        examList[i] = this.SetExamPrice(examList[i], ratio * examList[i].ORIGINAL_PRICE, firstDifferPrice, treatment.IN_TIME);
                                    }
                                }
                                //Neu sau khi nhan ti le vuot qua gia tri gioi han thi thuc hien tinh lai
                                else
                                {
                                    newPrice = (limit - totalPrice) < 0 ? 0 : (limit - totalPrice);

                                    //chi cap nhat voi cac sere_serv thuoc cung ho so dieu tri
                                    if (treatment.ID == examList[i].TDL_TREATMENT_ID)
                                    {
                                        examList[i] = this.SetExamPrice(examList[i], newPrice / (examList[i].VAT_RATIO + 1), firstDifferPrice, treatment.IN_TIME);//chia cho 1 + VAT de lay ra don gia
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

        /// <summary>
        /// Cap nhat gia doi voi cac dich vu phau thuat/thu thuat
        /// </summary>
        /// <param name="hisSereServs"></param>
        private void UpdateSurgPrice(List<HIS_SERE_SERV> hisSereServs)
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
                        //Neu dich vu dinh kem la dich vu thu thuat
                        if (sereServ.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT)
                        {
                            this.SetSurgPrice(sereServ, BhytConstant.ATTACH_MISU_RATIO);
                        }
                        //Neu dich vu dinh kem la dich vu phau thuat, thi kiem tra xem
                        //dich vu do co cung kip thuc hien hay khong
                        else if (sereServ.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT)
                        {
                            //Neu dich vu dinh kem cung kip thuc hien voi dich vu chinh
                            if (sereServ.EKIP_ID.HasValue && parent.EKIP_ID.HasValue
                                && sereServ.EKIP_ID.Value == parent.EKIP_ID.Value)
                            {
                                this.SetSurgPrice(sereServ, BhytConstant.ATTACH_SURG_RATIO);
                            }
                            //Neu dich vu dinh kem ko cung kip thuc hien voi dich vu chinh
                            else
                            {
                                this.SetSurgPrice(sereServ, BhytConstant.ATTACH_SURG_OTHER_EKIP_RATIO);
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
        private HIS_SERE_SERV SetExamPrice(HIS_SERE_SERV ss, decimal newPrice, decimal firstDifferPrice, long inTime)
        {
            if (ss.HEIN_LIMIT_PRICE.HasValue)
            {
                ss.HEIN_LIMIT_PRICE = newPrice;
                if (HisHeinBhytCFG.CALC_2TH_EXAM_DIFF_PRICE_OPTION == (int)HisHeinBhytCFG.Calc2thExamDiffPriceOption.NORMAL)
                {
                    //ko xu ly j
                }
                else if (HisHeinBhytCFG.CALC_2TH_EXAM_DIFF_PRICE_OPTION == (int)HisHeinBhytCFG.Calc2thExamDiffPriceOption.FIRST_DIFF_1)
                {
                    if (ss.PRIMARY_PRICE.HasValue)
                    {
                        ss.PRICE = (ss.PRIMARY_PRICE.Value - ss.ORIGINAL_PRICE) + ss.HEIN_LIMIT_PRICE.Value;
                    }
                    else //thiet ke cu chua co truong primary_price --> can lay lai du lieu theo chinh sach gia
                    {
                        V_HIS_SERVICE_PATY originalServicePaty = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, ss.TDL_EXECUTE_BRANCH_ID, ss.TDL_EXECUTE_ROOM_ID, ss.TDL_REQUEST_ROOM_ID, ss.TDL_REQUEST_DEPARTMENT_ID, ss.TDL_INTRUCTION_TIME, inTime, ss.SERVICE_ID, ss.PATIENT_TYPE_ID, null);
                        if (originalServicePaty != null)
                        {
                            ss.PRICE = (originalServicePaty.PRICE - ss.ORIGINAL_PRICE) + ss.HEIN_LIMIT_PRICE.Value;
                        }
                    }
                }
                else if (HisHeinBhytCFG.CALC_2TH_EXAM_DIFF_PRICE_OPTION == (int)HisHeinBhytCFG.Calc2thExamDiffPriceOption.FIRST_DIFF_2)
                {
                    ss.PRICE = firstDifferPrice + newPrice;//set lai gia theo gia chenh lech cua cong kham 1
                }
            }
            else
            {
                ss.PRICE = newPrice;
            }
            ss.OVERTIME_PRICE = ss.OVERTIME_PRICE.HasValue && ss.OVERTIME_PRICE.Value > ss.PRICE ? ss.PRICE : ss.OVERTIME_PRICE;//de tranh truong hop OVERTIME_PRICE > PRICE
            return ss;
        }

        /// <summary>
        /// Cap nhat gia cua dich vu PTTT
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="newPrice"></param>
        /// <returns></returns>
        private HIS_SERE_SERV SetSurgPrice(HIS_SERE_SERV ss, decimal ratio)
        {
            //Neu BN phai tra phan chi phi con lai thi ko thay doi "PRICE"
            if (HisHeinBhytCFG.CALC_ARISING_SURG_PRICE_OPTION == HisHeinBhytCFG.CalcArisingSurgPriceOption.PAY_REMAIN)
            {
                ss.HEIN_LIMIT_PRICE = ratio * ss.ORIGINAL_PRICE;
            }
            //Neu BN ko phai tra phan chi phi con lai ke ca trong truong hop co chi phi phu thu
            else if (HisHeinBhytCFG.CALC_ARISING_SURG_PRICE_OPTION == HisHeinBhytCFG.CalcArisingSurgPriceOption.NOT_PAY_REMAIN_ALL)
            {
                ss.PRICE = ss.PRIMARY_PRICE.HasValue ? ratio * ss.PRIMARY_PRICE.Value : ratio * ss.ORIGINAL_PRICE;
                ss.HEIN_LIMIT_PRICE = ss.HEIN_LIMIT_PRICE.HasValue ? (decimal?)ss.ORIGINAL_PRICE * ratio : null;
            }
            //Neu BN ko phai tra phan chi phi con lai thi thay doi "PRICE"
            else
            {
                if (ss.HEIN_LIMIT_PRICE.HasValue)
                {
                    ss.HEIN_LIMIT_PRICE = ss.ORIGINAL_PRICE * ratio;
                }
                else
                {
                    ss.PRICE = ss.ORIGINAL_PRICE * ratio;
                }
            }
            return ss;
        }

        private decimal GetPrice(HIS_SERE_SERV ss)
        {
            return ss.HEIN_LIMIT_PRICE.HasValue ? ss.HEIN_LIMIT_PRICE.Value : ss.PRICE;
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
                filter.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                filter.TDL_EXECUTE_BRANCH_IDs = sameHeinOrgCodeBranchIds;

                return new HisSereServGet().Get(filter);
            }
            return null;
        }

        public bool IsApplyEmergencyExamPolicy(string treatmentTypeCode, HIS_TREATMENT treatment, HIS_SERE_SERV firstExam, long? recentDepartmentId, long? recentDepartmentInTime, long? recentDepartmentOutTime)
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
        /// Tinh vir_price (theo cong thuc tuong ung trong CSDL)
        /// </summary>
        /// <param name="tmp"></param>
        /// <returns></returns>
        private decimal VirPrice(HIS_SERE_SERV tmp)
        {
            return (tmp.PRICE + (tmp.ADD_PRICE ?? 0)) * (1 + tmp.VAT_RATIO);
        }
    }
}