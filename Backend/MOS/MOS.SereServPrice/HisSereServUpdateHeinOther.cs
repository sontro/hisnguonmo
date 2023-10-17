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
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisTreatment;
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
        private bool UpdateOther(List<HIS_SERE_SERV> hisSereServs, HIS_PATIENT_TYPE_ALTER lastPatientTypeAlter)
        {
            bool result = true;
            try
            {
                List<HIS_SERE_SERV> otherSereServs = hisSereServs
                    .Where(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                        && o.IS_EXPEND != MOS.UTILITY.Constant.IS_TRUE).ToList();

                if (IsNotNullOrEmpty(otherSereServs))
                {
                    otherSereServs.ForEach(o =>
                    {
                        o.HEIN_PRICE = 0;
                        o.HEIN_RATIO = 0;
                        o.HEIN_CARD_NUMBER = null;
                        o.JSON_PATIENT_TYPE_ALTER = null;
                    });

                    SereServPriceUtil.UpdateBedPrice(otherSereServs);

                    //neu co cau hinh ko tinh tien cho dv kham thu 2 tro di doi voi BN vien phi
                    if (HisSereServCFG.RATIO_FOR_2ND_EXAM_FOR_HOSPITAL_FEE.HasValue)
                    {
                        //neu la benh nhan kham
                        if (lastPatientTypeAlter.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
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
                                exams[0].PRICE = exams[0].ORIGINAL_PRICE;
                                //tu dich vu thu 2 tro di, tinh gia theo ti le cau hinh
                                if (exams.Count > 1)
                                {
                                    for (int i = 1; i < exams.Count; i++)
                                    {
                                        exams[i].PRICE = exams[i].ORIGINAL_PRICE * HisSereServCFG.RATIO_FOR_2ND_EXAM_FOR_HOSPITAL_FEE.Value;
                                    }
                                }
                            }
                        }
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
    }
}
