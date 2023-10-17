using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.HRM.Vietsens;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisServiceReq;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Util
{
    class HisTreatmentUploadHrm : BusinessBase
    {
        internal HisTreatmentUploadHrm()
            : base()
        {

        }

        internal HisTreatmentUploadHrm(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(long treatmentId)
        {
            bool result = false;
            try
            {
                if (HisHealthExamRankCFG.HRM_CONNECT_DATA == null || !HisHealthExamRankCFG.HRM_CONNECT_DATA.CheckValid())
                {
                    LogSystem.Info("Khong co thong tin cau hinh dia chi he thong HRM");
                    return true;
                }

                V_HIS_TREATMENT_FEE treatment = new HisTreatmentGet().GetFeeViewById(treatmentId);
                if (treatment == null)
                {
                    LogSystem.Error("TreatmentId invalid: " + treatmentId);
                }

                HIS_PATIENT patient = new HisPatientGet().GetById(treatment.PATIENT_ID);

                if (String.IsNullOrWhiteSpace(treatment.HRM_KSK_CODE) || String.IsNullOrWhiteSpace(patient.HRM_EMPLOYEE_CODE))
                {
                    LogSystem.Info("HRM_KSK_CODE or HRM_EMPLOYEE_CODE invalid");
                    return false;
                }

                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.TREATMENT_ID = treatment.ID;
                filter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH;
                filter.HAS_EXECUTE = true;
                filter.ORDER_DIRECTION = "DESC";
                filter.ORDER_FIELD = "INTRUCTION_TIME";
                List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(filter);

                if (!IsNotNullOrEmpty(serviceReqs))
                {
                    LogSystem.Info("Khong co du lieu yeu cau kham");
                    return false;
                }
                HIS_SERVICE_REQ req = serviceReqs.Where(o => o.IS_MAIN_EXAM == Constant.IS_TRUE).FirstOrDefault();
                if (req == null)
                {
                    req = serviceReqs.Where(o => o.HEALTH_EXAM_RANK_ID.HasValue).FirstOrDefault();
                }
                if (req == null || !req.HEALTH_EXAM_RANK_ID.HasValue)
                {
                    LogSystem.Info("Yeu cau kham khong co HEALTH_EXAM_RANK_ID");
                }

                List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);
                sereServs = sereServs != null ? sereServs.Where(o => o.AMOUNT > 0 && o.IS_NO_EXECUTE != Constant.IS_TRUE
                    && HisServiceTypeCFG.SUBCLINICAL_TYPE_IDs.Contains(o.TDL_SERVICE_TYPE_ID)).ToList() : null;
                List<HIS_SERE_SERV_TEIN> sereServTeins = null;
                List<HIS_SERE_SERV_EXT> sereServExts = null;
                if (IsNotNullOrEmpty(sereServs))
                {
                    sereServTeins = new HisSereServTeinGet().GetByTreatmentId(treatment.ID);
                    sereServExts = new HisSereServExtGet().GetByTreatmentId(treatment.ID);
                }

                KskData data = new KskData();
                data.medicalTestName = treatment.HRM_KSK_CODE ?? "";
                data.dataMedicalResult = new List<KskDetailData>();
                KskDetailData detail = new KskDetailData();
                detail.employeeCode = patient.HRM_EMPLOYEE_CODE ?? "";
                detail.medicalDate = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(treatment.IN_TIME);
                detail.medicalCost = Convert.ToInt64(treatment.TOTAL_PRICE);
                detail.medicalResult = req.SUBCLINICAL ?? "";
                detail.medicalRank = HisHealthExamRankCFG.DATA.FirstOrDefault(o => o.ID == req.HEALTH_EXAM_RANK_ID.Value).HEALTH_EXAM_RANK_CODE;
                detail.bloodGroup = patient.BLOOD_ABO_CODE ?? "";
                if (req.DHST_ID.HasValue)
                {
                    HIS_DHST dhst = new HisDhstGet().GetById(req.DHST_ID.Value);
                    if (dhst.WEIGHT.HasValue)
                        detail.weight = CommonUtil.ToString(dhst.WEIGHT.Value);
                    if (dhst.HEIGHT.HasValue)
                        detail.height = CommonUtil.ToString(dhst.HEIGHT.Value);
                }
                if (IsNotNullOrEmpty(sereServs))
                {
                    ProcessExaminationIndex(treatment, sereServs, sereServTeins, sereServExts, detail);
                }
                data.dataMedicalResult.Add(detail);



                HrmConsumer consumer = new HrmConsumer(HisHealthExamRankCFG.HRM_CONNECT_DATA.Address, HisHealthExamRankCFG.HRM_CONNECT_DATA.GrantType, HisHealthExamRankCFG.HRM_CONNECT_DATA.ClientId, HisHealthExamRankCFG.HRM_CONNECT_DATA.ClientSecret, HisHealthExamRankCFG.HRM_CONNECT_DATA.Loginname, HisHealthExamRankCFG.HRM_CONNECT_DATA.Password);

                result = consumer.PostKsk(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessExaminationIndex(V_HIS_TREATMENT_FEE treatment, List<HIS_SERE_SERV> sereServs, List<HIS_SERE_SERV_TEIN> sereServTeins, List<HIS_SERE_SERV_EXT> sereServExts, KskDetailData detail)
        {
            List<KskExaminationData> examinations = new List<KskExaminationData>();
            foreach (HIS_SERE_SERV ss in sereServs)
            {
                if (ss.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN)
                {
                    List<HIS_SERE_SERV_TEIN> ssTeins = sereServTeins != null ? sereServTeins.Where(o => o.SERE_SERV_ID == ss.ID).ToList() : null;
                    if (!IsNotNullOrEmpty(ssTeins))
                    {
                        LogSystem.Info("HisTreatmentUploadHrm. HIS_SERE_SERV_TEIN Is Null");
                        continue;
                    }

                    foreach (HIS_SERE_SERV_TEIN tein in ssTeins)
                    {
                        if (!tein.TEST_INDEX_ID.HasValue)
                        {
                            LogSystem.Info("HisTreatmentUploadHrm. TEST_INDEX_ID Is Null, Khong Gui Sang HRM");
                            continue;
                        }

                        V_HIS_TEST_INDEX tIndex = HisTestIndexCFG.DATA_VIEW.FirstOrDefault(o => o.ID == tein.TEST_INDEX_ID.Value);
                        if (tIndex != null)
                        {
                            KskExaminationData idx = new KskExaminationData();
                            idx.service_group_type = ss.TDL_SERVICE_CODE ?? "";
                            idx.service_group_type_name = ss.TDL_SERVICE_NAME ?? "";
                            idx.service_group_name = tIndex.TEST_INDEX_NAME ?? "";
                            idx.service_group = tIndex.TEST_INDEX_CODE ?? "";
                            string minVal = null;
                            string maxVal = null;
                            HisSereServTeinDecorator.Decorator(tIndex.ID, treatment.TDL_PATIENT_DOB, treatment.TDL_PATIENT_GENDER_ID, ref minVal, ref maxVal);
                            idx.service_min = minVal ?? "";
                            idx.service_max = maxVal ?? "";
                            idx.service_result = tein.VALUE ?? "";
                            idx.service_unit = tIndex.TEST_INDEX_UNIT_NAME ?? "";
                            examinations.Add(idx);
                        }
                        else
                        {
                            LogSystem.Info("HisTreatmentUploadHrm. V_HIS_TEST_INDEX Is Null, Khong Gui Sang HRM");
                        }
                    }
                }
                else
                {
                    HIS_SERE_SERV_EXT ssExt = sereServExts != null ? sereServExts.FirstOrDefault(o => o.SERE_SERV_ID == ss.ID) : null;
                    KskExaminationData cls = new KskExaminationData();
                    cls.service_group_name = ss.TDL_SERVICE_CODE ?? "";
                    cls.service_group = ss.TDL_SERVICE_NAME ?? "";
                    if (ssExt != null)
                    {
                        cls.service_result = ssExt.CONCLUDE ?? "";
                    }
                    HIS_SERVICE_UNIT unit = HisServiceUnitCFG.DATA.FirstOrDefault(o => o.ID == ss.TDL_SERVICE_UNIT_ID);
                    if (unit != null)
                    {
                        cls.service_unit = unit.SERVICE_UNIT_NAME ?? "";
                    }
                    examinations.Add(cls);
                }
            }
            if (IsNotNullOrEmpty(examinations))
            {
                detail.examination_index = examinations;
            }
        }
    }
}
