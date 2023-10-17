using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.LibraryHein.Common;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ
{
    internal partial class HisSereServUpdateHein : BusinessBase
    {
        /// <summary>
        /// Cap nhat thong tin cho cac sere_serv su dung BHYT
        /// </summary>
        /// <param name="sereServs"></param>
        /// <param name="treatmentTypeCode"></param>
        private void UpdateBhyt(List<HIS_SERE_SERV> sereServs, HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER patientTypeAlter)
        {
            string treatmentTypeCode = null;
            if (patientTypeAlter != null)
            {
                HIS_TREATMENT_TYPE t = HisTreatmentTypeCFG.DATA
                    .Where(o => o.ID == patientTypeAlter.TREATMENT_TYPE_ID).FirstOrDefault();
                treatmentTypeCode = t != null ? t.HEIN_TREATMENT_TYPE_CODE : null;
            }

            List<HIS_SERE_SERV> heinSereServs = sereServs
                .Where(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && o.IS_EXPEND != MOS.UTILITY.Constant.IS_TRUE)
                .ToList();
            if (IsNotNullOrEmpty(heinSereServs))
            {
                //Set lai don gia cua dich vu theo chinh sach cua BYT
                this.setPriceWithBhytPolicy.Update(heinSereServs, treatment, treatmentTypeCode, this.recentEmergencyDepartmentId, this.recentEmergencyDepartmentInTime, this.recentEmergencyDepartmentOutTime);

                //Ko xu ly voi dich vu hao phi hoac khac BHYT
            List<long> paidAllOtherPaySourceIds = HisOtherPaySourceCFG.DATA != null ? HisOtherPaySourceCFG.DATA.Where(o => o.IS_PAID_ALL == Constant.IS_TRUE).Select(o => o.ID).ToList() : null;

                //Tinh lai ti le BHYT chi tra
                //Bo cac dich vu ma co nguon khac chi tra toan bo ra khoi danh sach tinh BHYT
            List<HIS_SERE_SERV> notOtherSourcePaidAlls = heinSereServs
                .Where(o => (!o.OTHER_PAY_SOURCE_ID.HasValue || paidAllOtherPaySourceIds == null || !paidAllOtherPaySourceIds.Contains(o.OTHER_PAY_SOURCE_ID.Value))
                && (o.IS_OTHER_SOURCE_PAID != Constant.IS_TRUE || HisSereServCFG.OTHER_SOURCE_PAID_SERVICE_OPTION != HisSereServCFG.OtherSourcePaidServiceOption.PAID_ALL)).ToList();
            if (!this.UpdateRatioBhyt(treatment, treatmentTypeCode, notOtherSourcePaidAlls))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        /// <summary>
        /// Cap nhat thong tin ti le BHYT
        /// </summary>
        /// <returns></returns>
        private bool UpdateRatioBhyt(HIS_TREATMENT treatment, string treatmentTypeCode, List<HIS_SERE_SERV> hisSereServs)
        {
            bool result = true;
            try
            {
                List<BhytServiceRequestData> heinServiceData = new List<BhytServiceRequestData>();

                //Cac loai duoc tinh cho goi vat tu ktc
                List<long> heinServiceTypeVts = new List<long>(){
					IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TT,
					IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TL,
					IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TDM,
					};

                //Cac loai duoc tinh la thuoc
                List<long> heinServiceTypeThs = new List<long>(){
					IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_NDM,
					IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_TDM,
					IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_TL,
                    IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_UT,
					};

                //Cac loai dv duoc tinh ktc
                List<long> heinServiceTypeKtcs = new List<long>(){
					IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__DVKTC,
					IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__PTTT,
					IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__CDHA,
					IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TDCN,
                    IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TT
					};

                foreach (HIS_SERE_SERV data in hisSereServs)
                {
                    string ratioType = BhytRatioTypeMapConfig.GetRatioType(data.TDL_HEIN_SERVICE_TYPE_ID);
                    bool isStent = HisMaterialTypeCFG.IsStentByServiceId(data.SERVICE_ID);
                    bool isHighService = data.TDL_HEIN_SERVICE_TYPE_ID.HasValue && heinServiceTypeKtcs.Contains(data.TDL_HEIN_SERVICE_TYPE_ID.Value);
                    ServiceTypeEnum serviceType = ServiceTypeEnum.OTHER;
                    if (data.TDL_HEIN_SERVICE_TYPE_ID.HasValue && heinServiceTypeVts.Contains(data.TDL_HEIN_SERVICE_TYPE_ID.Value))
                    {
                        serviceType = ServiceTypeEnum.MATERIAL;
                    }
                    else if (data.TDL_HEIN_SERVICE_TYPE_ID.HasValue && heinServiceTypeThs.Contains(data.TDL_HEIN_SERVICE_TYPE_ID.Value))
                    {
                        serviceType = ServiceTypeEnum.MEDICINE;
                    }

                    decimal price = Math.Round(data.PRICE * (data.VAT_RATIO + 1), 4); //Lam tron de khop voi trong DB
                    decimal amount = data.AMOUNT_TEMP > 0 && data.AMOUNT == 0 ? data.AMOUNT_TEMP.Value : data.AMOUNT;

                    BhytServiceRequestData t = new BhytServiceRequestData(data.ID, data.PARENT_ID, price, data.HEIN_LIMIT_PRICE, amount, data.JSON_PATIENT_TYPE_ALTER, ratioType, isStent, data.STENT_ORDER, data.ORIGINAL_PRICE, data.TDL_INTRUCTION_TIME, isHighService, serviceType, data.TDL_REQUEST_ROOM_ID, data.TDL_EXECUTE_ROOM_ID);
                    heinServiceData.Add(t);
                }

                bool noConstraintRoomWithMaterialPackage = HisHeinBhytCFG.CALC_MATERIAL_PACKAGE_PRICE_OPTION == HisHeinBhytCFG.CalcMaterialPackagePriceOption.NO_CONSTRAINT_ROOM;

                HIS_BHYT_PARAM bhytParam = this.GetBhytConstant(treatment);

                bool isNoLimitSecondStentForSpecials = HisHeinBhytCFG.SECOND_STENT_RATIO_OPTION == HisHeinBhytCFG.SecondStentRatioOption.ALL_FOR_SPECIAL_CARD;

                BhytHeinProcessor heinProcessor = new BhytHeinProcessor(bhytParam.BASE_SALARY, bhytParam.MIN_TOTAL_BY_SALARY, bhytParam.MAX_TOTAL_PACKAGE_BY_SALARY, bhytParam.SECOND_STENT_PAID_RATIO, noConstraintRoomWithMaterialPackage, HisHeinBhytCFG.NO_LIMIT_HIGH_HEIN_SERVICE_PRICE__PREFIXs, HisHeinBhytCFG.NO_LIMIT_MEDICINE_MATERIAL_PRICE__PREFIXs, isNoLimitSecondStentForSpecials, this.GetAcceptedOrgCode());

                
                if (!heinProcessor.UpdateHeinInfo(treatmentTypeCode, heinServiceData))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                    throw new Exception("Cap nhat thong tin BHYT cho cac Sere_Serv that bai. Du lieu se bi rollback." + LogUtil.TraceData("heinServiceData", heinServiceData));
                }

                foreach (RequestServiceData heinService in heinServiceData)
                {
                    HIS_SERE_SERV hisSereServ = hisSereServs.Where(o => o.ID == heinService.Id).SingleOrDefault();
                    hisSereServ.HEIN_RATIO = heinService.HeinRatio;
                    hisSereServ.HEIN_PRICE = heinService.HeinPrice;
                    hisSereServ.PRICE = Math.Round(heinService.Price / (hisSereServ.VAT_RATIO + 1), 4);
                    hisSereServ.HEIN_LIMIT_PRICE = heinService.LimitPrice;
                    hisSereServ.PATIENT_PRICE_BHYT = heinService.PatientPrice;
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

        public HIS_BHYT_PARAM GetBhytConstant(HIS_TREATMENT treatment)
        {
            long time = treatment.OUT_TIME.HasValue ? treatment.OUT_TIME.Value : Inventec.Common.DateTime.Get.Now().Value;
            HIS_BHYT_PARAM bhytParam = HisBhytParamCFG.DATA
                .Where(o => (!o.FROM_TIME.HasValue || o.FROM_TIME.Value <= time) && (!o.TO_TIME.HasValue || o.TO_TIME.Value >= time))
                .OrderByDescending(o => o.PRIORITY)
                .FirstOrDefault();

            if (bhytParam == null)
            {
                string timeStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(time);
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_ChuaCauHinhThamSoBhytTuongUng, timeStr);
                throw new Exception("Ko co thong tin cau hinh HIS_BHYT_PARAM tuong ung");
            }
            return bhytParam;
        }

        /// <summary>
        /// Lay ra danh sach cac ma KCB ban dau duoc chap nhan la dung tuyen
        /// </summary>
        /// <returns></returns>
        public List<string> GetAcceptedOrgCode()
        {
            HIS_BRANCH currentBranch = new TokenManager().GetBranch();
            List<string> acceptedMediOrgCodes = new List<string>();
            if (currentBranch != null)
            {
                if (!string.IsNullOrWhiteSpace(currentBranch.ACCEPT_HEIN_MEDI_ORG_CODE))
                {
                    string[] acceptCodes = currentBranch.ACCEPT_HEIN_MEDI_ORG_CODE.Split(',');
                    if (acceptCodes != null && acceptCodes.Length > 0)
                    {
                        acceptedMediOrgCodes.AddRange(acceptCodes.ToList());
                    }
                }
                if (!string.IsNullOrWhiteSpace(currentBranch.SYS_MEDI_ORG_CODE))
                {
                    string[] sysCodes = currentBranch.SYS_MEDI_ORG_CODE.Split(',');
                    if (sysCodes != null && sysCodes.Length > 0)
                    {
                        acceptedMediOrgCodes.AddRange(sysCodes.ToList());
                    }
                }
            }
            return acceptedMediOrgCodes;
        }
    }
}
