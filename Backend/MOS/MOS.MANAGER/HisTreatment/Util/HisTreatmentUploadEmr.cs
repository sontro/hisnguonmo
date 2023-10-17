using EMR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Util
{
    public class HisTreatmentUploadEmr : BusinessBase
    {
        private static string HAS_CONNECTION_EMR = "MOS.HAS_CONNECTION_EMR";
        private static string connect = "1";
        private string TokenCode = "";

        private static bool IsRunning;

        public HisTreatmentUploadEmr()
            : base()
        {
            this.TokenCode = ResourceTokenManager.GetTokenCode();
        }

        public HisTreatmentUploadEmr(CommonParam param)
            : base(param)
        {
            this.TokenCode = ResourceTokenManager.GetTokenCode();
        }

        internal void Run(HIS_TREATMENT treatment)
        {
            try
            {
                if (treatment != null)
                {
                    Thread threadUpdate = new Thread(new ParameterizedThreadStart(ThreadUpdateEmr));
                    try
                    {
                        threadUpdate.Priority = ThreadPriority.AboveNormal;
                        threadUpdate.Start(treatment);
                    }
                    catch (Exception ex)
                    {
                        threadUpdate.Abort();
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void Run(long treatmentId)
        {
            try
            {
                Run(treatmentId, true);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void Run(List<HIS_TREATMENT> listTreatment)
        {
            try
            {
                Run(listTreatment, true);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal bool Run(long treatmentId, bool isThread)
        {
            bool result = true;
            try
            {
                if (treatmentId > 0)
                {
                    if (isThread)
                    {
                        Thread threadUpdate = new Thread(new ParameterizedThreadStart(ThreadUpdateEmrById));
                        try
                        {
                            threadUpdate.Priority = ThreadPriority.AboveNormal;
                            threadUpdate.Start(treatmentId);
                        }
                        catch (Exception ex)
                        {
                            threadUpdate.Abort();
                            Inventec.Common.Logging.LogSystem.Error(ex);
                        }
                    }
                    else
                    {
                        ThreadUpdateEmrById(treatmentId);
                        result = !param.HasException && !(IsNotNullOrEmpty(param.BugCodes) || IsNotNullOrEmpty(param.Messages));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void Run(List<HIS_TREATMENT> listTreatment, bool isThread)
        {
            try
            {
                if (listTreatment != null && listTreatment.Count > 0)
                {
                    if (isThread)
                    {
                        Thread threadUpdate = new Thread(new ParameterizedThreadStart(ThreadUpdateEmrByList));
                        try
                        {
                            threadUpdate.Priority = ThreadPriority.AboveNormal;
                            threadUpdate.Start(listTreatment);
                        }
                        catch (Exception ex)
                        {
                            threadUpdate.Abort();
                            Inventec.Common.Logging.LogSystem.Error(ex);
                        }
                    }
                    else
                    {
                        ThreadUpdateEmrByList(listTreatment);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public void Resync()
        {
            try
            {
                if (IsRunning)
                {
                    LogSystem.Info("Tien trinh dang duoc chay khong cho phep khoi tao tien trinh khac");
                    return;
                }
                IsRunning = true;
                long inDateFrom = Convert.ToInt64(DateTime.Now.AddDays(-HisServiceReqCFG.INTEGRATE_SYSTEM_DAY_NUM_SYNC).ToString("yyyyMMdd") + "000000");
                long createTime = (Inventec.Common.DateTime.Get.Now() ?? 0) - 100;
                StringBuilder sb = new StringBuilder().Append("SELECT * FROM HIS_TREATMENT WHERE")
                   .Append(" IS_SYNC_EMR IS NULL OR IS_SYNC_EMR<>:param1 ")
                   .Append(" AND CREATE_TIME < :param2 ")
                   .Append("AND IN_DATE >= :param3");

                string sqlQuery = sb.ToString();
                var listTreatment = new MOS.DAO.Sql.SqlDAO().GetSql<HIS_TREATMENT>(sqlQuery, 1, createTime, inDateFrom);
                if (IsNotNullOrEmpty(listTreatment))
                {
                    Run(listTreatment, false);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            IsRunning = false;
        }

        private void ThreadUpdateEmrByList(object obj)
        {
            try
            {
                var config = Config.Loader.GetConfig(HAS_CONNECTION_EMR);
                if (config != null)
                {
                    string value = String.IsNullOrWhiteSpace(config.VALUE) ? config.DEFAULT_VALUE : config.VALUE;
                    if (value == connect)
                    {
                        if (obj is List<HIS_TREATMENT>)
                        {
                            List<HIS_TREATMENT> lt = obj as List<HIS_TREATMENT>;
                            foreach (var item in lt)
                            {
                                CreateOrUpdateEmrTreatment(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ThreadUpdateEmrById(object obj)
        {
            try
            {
                var config = Config.Loader.GetConfig(HAS_CONNECTION_EMR);
                if (config != null)
                {
                    string value = String.IsNullOrWhiteSpace(config.VALUE) ? config.DEFAULT_VALUE : config.VALUE;
                    if (value == connect)
                    {
                        if (obj is long)
                        {
                            var treatment = new HisTreatment.HisTreatmentGet().GetById((long)obj);
                            if (treatment != null)
                            {
                                CreateOrUpdateEmrTreatment(treatment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ThreadUpdateEmr(object treatment)
        {
            try
            {
                var config = Config.Loader.GetConfig(HAS_CONNECTION_EMR);
                if (config != null)
                {
                    string value = String.IsNullOrWhiteSpace(config.VALUE) ? config.DEFAULT_VALUE : config.VALUE;
                    if (value == connect)
                    {
                        if (treatment is HIS_TREATMENT)
                        {
                            HIS_TREATMENT t = treatment as HIS_TREATMENT;
                            CreateOrUpdateEmrTreatment(t);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void CreateOrUpdateEmrTreatment(HIS_TREATMENT data)
        {
            try
            {
                if (data != null)
                {
                    EMR_TREATMENT createData = ProcessDataEmrTreatment(data);

                    if (createData != null)
                    {
                        CommonParam param = new CommonParam();
                        if (!String.IsNullOrWhiteSpace(TokenCode))
                        {
                            var cosumer = ApiConsumerManager.ApiConsumerStore.EmrConsumer;
                            cosumer.SetTokenCode(TokenCode);

                            var apiresult = cosumer.Post<ApiResultObject<EMR_TREATMENT>>(EMR.URI.EmrTreatment.CREATE_OR_UPDATE, param, createData);
                            if (IsNotNull(apiresult) && IsNotNull(apiresult.Data))
                            {
                                Inventec.Common.Logging.LogSystem.Info("tao du lieu emr treatment thanh cong!");
                                string sql = "UPDATE HIS_TREATMENT SET IS_SYNC_EMR = 1 WHERE ID =" + data.ID;
                                if (!new MOS.DAO.Sql.SqlDAO().Execute(sql))
                                {
                                    Inventec.Common.Logging.LogSystem.Info("Update du lieu treatment that bai!");
                                }
                            }
                            else
                            {
                                if (IsNotNull(apiresult) && IsNotNull(apiresult.Param))
                                {
                                    this.param.BugCodes.AddRange(apiresult.Param.BugCodes);
                                    this.param.Messages.AddRange(apiresult.Param.Messages);
                                    this.param.HasException = apiresult.Param.HasException;
                                }

                                Inventec.Common.Logging.LogSystem.Info("tao du lieu emr treatment that bai!");
                                Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => apiresult), apiresult));
                            }
                        }
                        else
                        {
                            var apiresult = ApiConsumerManager.ApiConsumerStore.EmrConsumerWrapper.Post<EMR_TREATMENT>(true, EMR.URI.EmrTreatment.CREATE_OR_UPDATE, param, createData);
                            if (apiresult != null)
                            {
                                Inventec.Common.Logging.LogSystem.Info("tao du lieu emr treatment thanh cong!");
                                string sql = "UPDATE HIS_TREATMENT SET IS_SYNC_EMR = 1 WHERE ID =" + data.ID;
                                if (!new MOS.DAO.Sql.SqlDAO().Execute(sql))
                                {
                                    Inventec.Common.Logging.LogSystem.Info("Update du lieu treatment that bai!");
                                }
                            }
                            else
                            {
                                Inventec.Common.Logging.LogSystem.Info("tao du lieu emr treatment that bai!");
                                Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private EMR_TREATMENT ProcessDataEmrTreatment(HIS_TREATMENT data)
        {
            EMR_TREATMENT result = null;
            try
            {
                if (data != null)
                {
                    result = new EMR_TREATMENT();
                    HIS_BRANCH branch = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == data.BRANCH_ID);
                    if (branch != null)
                    {
                        result.BRANCH_CODE = branch.BRANCH_CODE;
                        result.MEDI_ORG_CODE = branch.HEIN_MEDI_ORG_CODE;
                    }

                    result.CLINICAL_IN_TIME = data.CLINICAL_IN_TIME;
                    var departmentTran = new HisDepartmentTranGet().GetLastByTreatmentId(data.ID);
                    if (departmentTran != null)
                    {
                        var department = Config.HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == departmentTran.DEPARTMENT_ID);
                        if (department != null)
                        {
                            result.CURRENT_DEPARTMENT_CODE = department.DEPARTMENT_CODE;
                            result.CURRENT_DEPARTMENT_NAME = department.DEPARTMENT_NAME;
                        }
                    }

                    var dataStore = Config.HisDataStoreCFG.DATA.FirstOrDefault(o => o.ID == data.DATA_STORE_ID);
                    if (dataStore != null)
                    {
                        result.DATA_STORE_CODE = dataStore.DATA_STORE_CODE;
                        result.DATA_STORE_NAME = dataStore.DATA_STORE_NAME;
                    }

                    result.DOB = data.TDL_PATIENT_DOB;
                    result.END_CODE = data.END_CODE;
                    result.FIRST_NAME = data.TDL_PATIENT_FIRST_NAME;
                    var gender = Config.HisGenderCFG.DATA.FirstOrDefault(o => o.ID == data.TDL_PATIENT_GENDER_ID);
                    if (gender != null)
                    {
                        result.GENDER_CODE = gender.GENDER_CODE;
                        result.GENDER_NAME = gender.GENDER_NAME;
                    }

                    var hisPatientTypeAlter = new HisPatientTypeAlterGet().GetViewLastByTreatmentId(data.ID);
                    if (hisPatientTypeAlter != null)
                    {
                        result.HEIN_CARD_NUMBER = hisPatientTypeAlter.HEIN_CARD_NUMBER;
                        result.PATIENT_TYPE_CODE = hisPatientTypeAlter.PATIENT_TYPE_CODE;
                        result.PATIENT_TYPE_NAME = hisPatientTypeAlter.PATIENT_TYPE_NAME;
                        result.TREATMENT_TYPE_CODE = hisPatientTypeAlter.TREATMENT_TYPE_CODE;
                        result.TREATMENT_TYPE_NAME = hisPatientTypeAlter.TREATMENT_TYPE_NAME;
                        if (hisPatientTypeAlter.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                        {
                            result.HEIN_TREATMENT_TYPE_CODE = MOS.LibraryHein.Bhyt.HeinTreatmentType.HeinTreatmentTypeCode.TREAT;
                        }
                        else if (hisPatientTypeAlter.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM || hisPatientTypeAlter.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU)
                        {
                            result.HEIN_TREATMENT_TYPE_CODE = MOS.LibraryHein.Bhyt.HeinTreatmentType.HeinTreatmentTypeCode.EXAM;
                        }
                    }
                    else
                    {
                        HIS_PATIENT_TYPE paty = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == data.TDL_PATIENT_TYPE_ID);
                        if (IsNotNull(paty))
                        {
                            result.PATIENT_TYPE_CODE = paty.PATIENT_TYPE_CODE;
                            result.PATIENT_TYPE_NAME = paty.PATIENT_TYPE_NAME;
                        }

                        HIS_TREATMENT_TYPE treatmentType = HisTreatmentTypeCFG.DATA.FirstOrDefault(o => o.ID == data.TDL_TREATMENT_TYPE_ID);
                        if (IsNotNull(treatmentType))
                        {
                            result.TREATMENT_TYPE_CODE = treatmentType.TREATMENT_TYPE_CODE;
                            result.TREATMENT_TYPE_NAME = treatmentType.TREATMENT_TYPE_NAME;
                        }
                    }

                    result.ICD_CODE = data.ICD_CODE;
                    result.ICD_NAME = data.ICD_NAME;
                    result.ICD_SUB_CODE = data.ICD_SUB_CODE;
                    result.ICD_TEXT = data.ICD_TEXT;
                    result.IN_CODE = data.IN_CODE;
                    result.IN_TIME = data.IN_TIME;
                    result.IS_EMERGENCY = data.IS_EMERGENCY;
                    result.IS_HAS_NOT_DAY_DOB = data.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                    result.LAST_NAME = data.TDL_PATIENT_LAST_NAME;
                    result.OUT_CODE = data.OUT_CODE;
                    result.OUT_TIME = data.OUT_TIME;
                    result.PATIENT_CODE = data.TDL_PATIENT_CODE;

                    result.STORE_CODE = data.STORE_CODE;
                    result.STORE_TIME = data.STORE_TIME;

                    result.TREATMENT_CODE = data.TREATMENT_CODE;

                    var treatmentEndType = Config.HisTreatmentEndTypeCFG.DATA.FirstOrDefault(o => o.ID == data.TREATMENT_END_TYPE_ID);
                    if (treatmentEndType != null)
                    {
                        result.TREATMENT_END_TYPE_CODE = treatmentEndType.TREATMENT_END_TYPE_CODE;
                        result.TREATMENT_END_TYPE_NAME = treatmentEndType.TREATMENT_END_TYPE_NAME;
                    }

                    var treatmentResult = Config.HisTreatmentResultCFG.DATA.FirstOrDefault(o => o.ID == data.TREATMENT_RESULT_ID);
                    if (treatmentResult != null)
                    {
                        result.TREATMENT_RESULT_CODE = treatmentResult.TREATMENT_RESULT_CODE;
                        result.TREATMENT_RESULT_NAME = treatmentResult.TREATMENT_RESULT_NAME;
                    }

                    var cards = new HisCardGet().GetLastByPatientId(data.PATIENT_ID);
                    if (cards != null)
                    {
                        result.CARD_CODE = cards.CARD_CODE;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
