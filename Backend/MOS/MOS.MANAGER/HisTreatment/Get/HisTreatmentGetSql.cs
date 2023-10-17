using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMaterialType;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.HisServiceReqMety;
using MOS.MANAGER.HisPtttGroup;
using MOS.SDO;
using MOS.TDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentGet : GetBase
    {
        internal List<HisTreatmentWithPatientTypeInfoSDO> GetTreatmentWithPatientTypeInfoSdo(HisTreatmentWithPatientTypeInfoFilter filter)
        {
            try
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                long instructionTime = filter.INTRUCTION_TIME.HasValue ? filter.INTRUCTION_TIME.Value : now;

                string query = "SELECT TREA.*, "
                    + " (SELECT E.TREATMENT_TYPE_CODE || '|' || C.PATIENT_TYPE_CODE || '|' || A.HEIN_MEDI_ORG_CODE || '|' || A.RIGHT_ROUTE_TYPE_CODE || '|' || A.HEIN_CARD_NUMBER || '|' || A.LEVEL_CODE || '|' || A.RIGHT_ROUTE_CODE || '|' || A.HEIN_CARD_FROM_TIME || '|' || A.HEIN_CARD_TO_TIME || '|' || A.ADDRESS || '|' || A.PRIMARY_PATIENT_TYPE_ID"
                    + " FROM HIS_PATIENT_TYPE_ALTER A  "
                    + " JOIN HIS_PATIENT_TYPE C ON A.PATIENT_TYPE_ID = C.ID "
                    + " JOIN HIS_TREATMENT_TYPE E ON E.ID = A.TREATMENT_TYPE_ID "
                    + " WHERE A.TREATMENT_ID = :param1 AND A.LOG_TIME <= :param2 "
                    + " ORDER BY A.LOG_TIME DESC, A.ID DESC FETCH FIRST ROWS ONLY) AS PATIENT_TYPE "
                    + " FROM HIS_TREATMENT TREA "
                    + " WHERE TREA.ID = :param3 ";

                List<HisTreatmentWithPatientTypeInfoSDO> result = DAOWorker.SqlDAO.GetSql<HisTreatmentWithPatientTypeInfoSDO>(query, filter.TREATMENT_ID, instructionTime, filter.TREATMENT_ID);
                if (IsNotNullOrEmpty(result))
                {
                    HisTreatmentWithPatientTypeInfoSDO data = result[0];

                    if (!string.IsNullOrWhiteSpace(data.PATIENT_TYPE))
                    {
                        string[] ptInfos = data.PATIENT_TYPE.Split('|');
                        data.SERVER_TIME = now;
                        data.TREATMENT_TYPE_CODE = ptInfos.Length > 0 ? ptInfos[0] : "";
                        data.PATIENT_TYPE_CODE = ptInfos.Length > 1 ? ptInfos[1] : "";
                        data.HEIN_MEDI_ORG_CODE = ptInfos.Length > 2 ? ptInfos[2] : "";
                        data.RIGHT_ROUTE_TYPE_CODE = ptInfos.Length > 3 ? ptInfos[3] : "";
                        data.HEIN_CARD_NUMBER = ptInfos.Length > 4 ? ptInfos[4] : "";
                        data.LEVEL_CODE = ptInfos.Length > 5 ? ptInfos[5] : "";
                        data.RIGHT_ROUTE_CODE = ptInfos.Length > 6 ? ptInfos[6] : "";
                        try
                        {
                            data.HEIN_CARD_FROM_TIME = ptInfos.Length > 7 && !string.IsNullOrWhiteSpace(ptInfos[7]) ? long.Parse(ptInfos[7]) : 0;
                        }
                        catch (Exception ex)
                        {
                            LogSystem.Error(ex);
                            LogSystem.Error("Ko parse duoc HEIN_CARD_FROM_TIME tu chuoi: " + ptInfos[7]);
                        }

                        try
                        {
                            data.HEIN_CARD_TO_TIME = ptInfos.Length > 8 && !string.IsNullOrWhiteSpace(ptInfos[8]) ? long.Parse(ptInfos[8]) : 0;
                        }
                        catch (Exception ex)
                        {
                            LogSystem.Error(ex);
                            LogSystem.Error("Ko parse duoc HEIN_CARD_TO_TIME tu chuoi: " + ptInfos[8]);
                        }
                        data.HEIN_CARD_ADDRESS = ptInfos.Length > 9 ? ptInfos[9] : "";
                        try
                        {
                            data.PRIMARY_PATIENT_TYPE_ID = ptInfos.Length > 10 && !string.IsNullOrWhiteSpace(ptInfos[10]) ? long.Parse(ptInfos[10]) : 0;
                        }
                        catch (Exception ex)
                        {
                            LogSystem.Error(ex);
                            LogSystem.Error("Ko parse duoc PRIMARY_PATIENT_TYPE_ID tu chuoi: " + ptInfos[10]);
                        }
                    }

                    //Neu ho so dieu tri chua co ICD va ho so la theo loai hen kham thi lay thong tin ICD cua ho so cu
                    if (IsNotNullOrEmpty(data.APPOINTMENT_CODE) || HisTreatmentCFG.IS_USING_LASTEST_ICD)
                    {
                        HIS_TREATMENT previous = null;
                        if (IsNotNullOrEmpty(data.APPOINTMENT_CODE))
                        {
                            previous = new HisTreatmentGet().GetByCode(data.APPOINTMENT_CODE);
                        }
                        else if (HisTreatmentCFG.IS_USING_LASTEST_ICD)
                        {
                            HisTreatmentFilterQuery treatmentFilter = new HisTreatmentFilterQuery();
                            treatmentFilter.PATIENT_ID = data.PATIENT_ID;
                            treatmentFilter.TREATMENT_END_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN;
                            treatmentFilter.IS_PAUSE = true;

                            var treatments = new HisTreatmentGet().GetByPatientId(data.PATIENT_ID);
                            if (IsNotNullOrEmpty(treatments))
                            {
                                previous = treatments
                                    .Where(o => o.ID != data.ID)
                                    .OrderByDescending(o => o.IN_TIME)
                                    .ThenByDescending(o => o.ID).FirstOrDefault();
                            }
                        }

                        if (previous != null)
                        {
                            data.PREVIOUS_ICD_CODE = previous.ICD_CODE;
                            data.PREVIOUS_ICD_SUB_CODE = previous.ICD_SUB_CODE;
                            data.PREVIOUS_ICD_NAME = previous.ICD_NAME;
                            data.PREVIOUS_ICD_TEXT = previous.ICD_TEXT;
                            data.PREVIOUS_APPOINTMENT_TIME = previous.APPOINTMENT_TIME;
                            data.PREVIOUS_DOCTOR_LOGINNAME = previous.DOCTOR_LOGINNAME;
                            data.PREVIOUS_DOCTOR_USERNAME = previous.DOCTOR_USERNAME;
                            data.PREVIOUS_END_LOGINNAME = previous.END_LOGINNAME;
                            data.PREVIOUS_END_USERNAME = previous.END_USERNAME;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HisTreatmentCounterAndPriceSDO GetTreatmentCounterAndPriceByTime(HisTreatmentCounterAndPriceFilter filter)
        {
            HisTreatmentCounterAndPriceSDO result = null;
            try
            {
                string betweenTime = String.Format(" BETWEEN {0} AND {1}", filter.FROM_TIME, filter.TO_TIME);

                //Số lượng bệnh nhân đến khám
                string queryTempExam = "(SELECT COUNT(1) FROM HIS_TREATMENT TREA "
                    + "WHERE TREA.IN_TIME " + betweenTime
                    + " AND EXISTS (SELECT 1 FROM HIS_PATIENT_TYPE_ALTER PTAL "
                    + "WHERE PTAL.TREATMENT_TYPE_ID = {0} "
                    + "AND PTAL.TREATMENT_ID = TREA.ID "
                    + " ORDER BY PTAL.LOG_TIME FETCH FIRST ROW ONLY)) AS COUNT_TREATMENT_EXAM";

                string queryExam = String.Format(queryTempExam, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM);

                //Số lượng bệnh nhân nhập viện
                string queryTempTreat = "(SELECT COUNT(1) FROM HIS_TREATMENT TREA "
                    + "WHERE TREA.IN_TIME " + betweenTime
                    + " AND (SELECT PTAL.LOG_TIME FROM HIS_PATIENT_TYPE_ALTER PTAL "
                    + "WHERE PTAL.TREATMENT_TYPE_ID IN ({0},{1}) "
                    + "AND PTAL.TREATMENT_ID = TREA.ID "
                    + " ORDER BY PTAL.LOG_TIME FETCH FIRST ROW ONLY) " + betweenTime + ") AS COUNT_TREATMENT_IN";

                string queryTreat = String.Format(queryTempTreat, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU);

                //Số lượng bệnh nhân chuyển viện
                string queryTempTran = "(SELECT COUNT(1) FROM HIS_TREATMENT TREA "
                    + "WHERE TREA.OUT_TIME " + betweenTime
                    + " AND TREA.TREATMENT_END_TYPE_ID = {0} "
                    + "AND TREA.IS_PAUSE = {1}) AS COUNT_TREATMENT_TRAN";

                string queryTran = String.Format(queryTempTran, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN, MOS.UTILITY.Constant.IS_TRUE);

                //Tổng thu
                string queryTempTotalPrice = "(SELECT SUM(SESE.VIR_TOTAL_PRICE) FROM HIS_SERE_SERV SESE "
                    + " JOIN HIS_SERVICE_REQ SERE ON SERE.ID = SESE.SERVICE_REQ_ID "
                    + "WHERE (SESE.IS_DELETE IS NULL OR SESE.IS_DELETE <> {0}) "
                    + "AND (SESE.IS_NO_EXECUTE IS NULL OR SESE.IS_NO_EXECUTE <> {1}) "
                    + "AND SERE.INTRUCTION_TIME " + betweenTime + ") AS TOTAL_PRICE";
                string queryTotalPrice = String.Format(queryTempTotalPrice, IMSys.DbConfig.HIS_RS.COMMON.IS_DELETE__TRUE, MOS.UTILITY.Constant.IS_TRUE);

                //Tổng BH
                string queryTempHeinPrice = "(SELECT SUM(SESE.VIR_TOTAL_HEIN_PRICE) FROM HIS_SERE_SERV SESE "
                    + "JOIN HIS_SERVICE_REQ SERE ON SERE.ID = SESE.SERVICE_REQ_ID "
                    + "WHERE (SESE.IS_DELETE IS NULL OR SESE.IS_DELETE <> {0}) "
                    + "AND (SESE.IS_NO_EXECUTE IS NULL OR SESE.IS_NO_EXECUTE <> {1}) "
                    + "AND SERE.INTRUCTION_TIME " + betweenTime + ") AS TOTAL_HEIN_PRICE ";
                string queryHeinPrice = String.Format(queryTempHeinPrice, IMSys.DbConfig.HIS_RS.COMMON.IS_DELETE__TRUE, MOS.UTILITY.Constant.IS_TRUE);

                //Tổng BN
                string queryTempPatientPrice = "(SELECT SUM(SESE.VIR_TOTAL_PATIENT_PRICE) FROM HIS_SERE_SERV SESE "
                    + "JOIN HIS_SERVICE_REQ SERE ON SERE.ID = SESE.SERVICE_REQ_ID "
                    + "WHERE (SESE.IS_DELETE IS NULL OR SESE.IS_DELETE <> {0}) "
                    + "AND (SESE.IS_NO_EXECUTE IS NULL OR SESE.IS_NO_EXECUTE <> {1}) "
                    + "AND SERE.INTRUCTION_TIME " + betweenTime + ") AS TOTAL_PATIENT_PRICE";
                string queryPatienrPrice = String.Format(queryTempPatientPrice, IMSys.DbConfig.HIS_RS.COMMON.IS_DELETE__TRUE, MOS.UTILITY.Constant.IS_TRUE);

                //BN Thanh toán
                string queryTempBill = "(SELECT SUM(TRAN.AMOUNT - BILL.KC_AMOUNT) FROM HIS_TRANSACTION TRAN "
                    + "JOIN HIS_BILL BILL ON BILL.TRANSACTION_ID = TRAN.ID "
                    + "WHERE TRAN.TRANSACTION_TIME " + betweenTime
                    + " AND (TRAN.IS_CANCEL IS NULL OR TRAN.IS_CANCEL <> {0})) AS TOTAL_BILL_AMOUNT";
                string queryBill = String.Format(queryTempBill, MOS.UTILITY.Constant.IS_TRUE);
                //BN Tạm ứng
                string queryTempDeposit = "(SELECT SUM(AMOUNT) FROM HIS_TRANSACTION "
                    + "WHERE (IS_CANCEL IS NULL OR IS_CANCEL <> {0}) "
                    + "AND TRANSACTION_TYPE_ID = {1} "
                    + "AND TRANSACTION_TIME " + betweenTime + ") AS TOTAL_DEPOSIT_AMOUNT";
                string queryDeposit = String.Format(queryTempDeposit, MOS.UTILITY.Constant.IS_TRUE, IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU);

                //BN Hoàn ứng
                string queryTempRepay = "(SELECT SUM(AMOUNT) FROM HIS_TRANSACTION "
                    + "WHERE (IS_CANCEL IS NULL OR IS_CANCEL <> {0}) "
                    + "AND TRANSACTION_TYPE_ID = {1} "
                    + "AND TRANSACTION_TIME " + betweenTime + ") AS TOTAL_REPAY_AMOUNT";
                string queryRepay = String.Format(queryTempRepay, MOS.UTILITY.Constant.IS_TRUE, IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU);

                string query = "SELECT "
                + queryExam
                + " , " + queryTreat
                + " , " + queryTran
                + " , " + queryTotalPrice
                + " , " + queryHeinPrice
                + " , " + queryPatienrPrice
                + " , " + queryBill
                + " , " + queryDeposit
                + " , " + queryRepay
                + " FROM DUAL";

                result = DAOWorker.SqlDAO.GetSqlSingle<HisTreatmentCounterAndPriceSDO>(query);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        internal List<HisTreatmentCounterSDO> GetTreatmentCounter(List<List<string>> filter)
        {
            List<HisTreatmentCounterSDO> result = null;
            try
            {
                if (IsNotNullOrEmpty(filter))
                {
                    result = new List<HisTreatmentCounterSDO>();
                    foreach (var time in filter)
                    {
                        if (time.Count < 2) continue;
                        if (String.IsNullOrEmpty(time[0]) && String.IsNullOrEmpty(time[1])) continue;

                        long fromTime = ConvertTimeStringToTimeNumber(time[0]);
                        long toTime = ConvertTimeStringToTimeNumber(time[1]);
                        if (fromTime > 0 && toTime > 0)
                        {
                            var data = GetTreatmentCounter(fromTime, toTime);
                            if (data != null)
                            {
                                result.Add(data);
                            }
                            else
                            {
                                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HipoApiResult_KhongCoDuLieu, time[0], time[1]);
                            }
                        }
                    }
                    if (param.Messages == null || param.Messages.Count <= 0)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HipoApiResultSuccess);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        private HisTreatmentCounterSDO GetTreatmentCounter(long FROM_TIME, long TO_TIME)
        {
            HisTreatmentCounterSDO result = null;
            try
            {
                string betweenTime = String.Format(" BETWEEN {0} AND {1} ", FROM_TIME, TO_TIME);

                //Tổng bệnh nhân tiếp đón 
                string queryTreat = "(SELECT COUNT(*) FROM HIS_TREATMENT WHERE IN_TIME " + betweenTime + " ) AS bntd";

                //Bệnh nhân tiếp đón bảo hiểm
                string queryTempTreatBhyt = "(SELECT COUNT(*) FROM HIS_TREATMENT TREA WHERE TREA.IN_TIME " + betweenTime +
                    "AND (SELECT PTAL.LOG_TIME FROM HIS_PATIENT_TYPE_ALTER PTAL " +
                    "WHERE PTAL.PATIENT_TYPE_ID = {0} AND PTAL.TREATMENT_ID = TREA.ID " +
                    "ORDER BY PTAL.LOG_TIME DESC FETCH FIRST ROW ONLY) " + betweenTime + ") AS bntd_bh";

                string queryTreatBhyt = String.Format(queryTempTreatBhyt, MANAGER.Config.HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);

                //Tổng bệnh nhân khám thực tế
                string queryTempExam = "(SELECT COUNT(*) FROM HIS_TREATMENT TREA WHERE IN_TIME " + betweenTime +
                    "AND EXISTS (SELECT 1 FROM HIS_SERVICE_REQ SERE WHERE SERE.TREATMENT_ID = TREA.ID AND " +
                    "SERE.SERVICE_REQ_TYPE_ID = {0} AND SERE.START_TIME " + betweenTime + ")) AS bnktt";
                string queryExam = String.Format(queryTempExam, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH);

                //Bệnh nhân khám thực tế bảo hiểm
                string queryTempExamBhyt = "(SELECT COUNT(*) FROM HIS_TREATMENT TREA WHERE IN_TIME " + betweenTime +
                    "AND EXISTS (SELECT 1 FROM HIS_SERVICE_REQ SERE WHERE SERE.TREATMENT_ID = TREA.ID AND " +
                    "SERE.SERVICE_REQ_TYPE_ID = {0} AND SERE.START_TIME " + betweenTime + ") " +
                    "AND (SELECT PTAL.LOG_TIME FROM HIS_PATIENT_TYPE_ALTER PTAL " +
                    "WHERE PTAL.PATIENT_TYPE_ID = {1} AND PTAL.TREATMENT_ID = TREA.ID " +
                    "ORDER BY PTAL.LOG_TIME DESC FETCH FIRST ROW ONLY) " + betweenTime + ") AS bnktt_bh";
                string queryExamBhyt = String.Format(queryTempExamBhyt, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH, MANAGER.Config.HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);

                //Tổng bệnh nhân nhập viện nội trú
                string queryTempTreatIn = "(SELECT COUNT(*) FROM HIS_TREATMENT TREA "
                    + "WHERE TREA.IN_TIME " + betweenTime
                    + " AND (SELECT PTAL.LOG_TIME FROM HIS_PATIENT_TYPE_ALTER PTAL "
                    + "WHERE PTAL.TREATMENT_TYPE_ID = {0} "
                    + "AND PTAL.TREATMENT_ID = TREA.ID "
                    + " ORDER BY PTAL.LOG_TIME DESC FETCH FIRST ROW ONLY) " + betweenTime + ") AS bnnv";
                string queryTreatIn = String.Format(queryTempTreatIn, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU);

                //Bệnh nhân nhập viện nội trú bảo hiểm
                string queryTempTreatInBhyt = "(SELECT COUNT(*) FROM HIS_TREATMENT TREA "
                    + "WHERE TREA.IN_TIME " + betweenTime
                    + " AND (SELECT PTAL.LOG_TIME FROM HIS_PATIENT_TYPE_ALTER PTAL "
                    + "WHERE PTAL.TREATMENT_TYPE_ID = {0} AND PTAL.TREATMENT_ID = TREA.ID "
                    + " ORDER BY PTAL.LOG_TIME FETCH FIRST ROW ONLY) " + betweenTime +
                    "AND (SELECT PTAL.LOG_TIME FROM HIS_PATIENT_TYPE_ALTER PTAL " +
                    "WHERE PTAL.PATIENT_TYPE_ID = {1} AND PTAL.TREATMENT_ID = TREA.ID " +
                    "ORDER BY PTAL.LOG_TIME DESC FETCH FIRST ROW ONLY) " + betweenTime + ") AS bnnv_bh";
                string queryTreatInBhyt = String.Format(queryTempTreatInBhyt, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU, MANAGER.Config.HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);

                //Tổng bệnh nhân cấp toa cho về
                string queryTempTreatEnd = "(SELECT COUNT(*) FROM HIS_TREATMENT WHERE IN_TIME " + betweenTime +
                    "AND TREATMENT_END_TYPE_ID = {0}) AS {1}";

                string queryTreatEndPres = String.Format(queryTempTreatEnd, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CTCV, "bnct");

                //Bệnh nhân cấp toa cho về bảo hiểm
                string queryTempTreatEndBhyt = "(SELECT COUNT(*) FROM HIS_TREATMENT TREA WHERE TREA.IN_TIME " + betweenTime +
                    "AND TREA.TREATMENT_END_TYPE_ID = {0}" +
                    "AND (SELECT PTAL.LOG_TIME FROM HIS_PATIENT_TYPE_ALTER PTAL " +
                    "WHERE PTAL.PATIENT_TYPE_ID = {1} AND PTAL.TREATMENT_ID = TREA.ID " +
                    "ORDER BY PTAL.LOG_TIME DESC FETCH FIRST ROW ONLY) " + betweenTime + ") AS {2}";

                string queryTreatEndPresBhyt = String.Format(queryTempTreatEndBhyt, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CTCV, MANAGER.Config.HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT, "bnct_bh");

                //Tổng bệnh nhân chuyển viện
                string queryTreatEndTran = String.Format(queryTempTreatEnd, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN, "bncv");

                //Bệnh nhân chuyển viện bảo hiểm
                string queryTreatEndTranBhyt = String.Format(queryTempTreatEndBhyt, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN, MANAGER.Config.HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT, "bncv_bh");

                string query = "SELECT "
                + queryTreat
                + " , " + queryTreatBhyt
                + " , " + queryExam
                + " , " + queryExamBhyt
                + " , " + queryTreatIn
                + " , " + queryTreatInBhyt
                + " , " + queryTreatEndPres
                + " , " + queryTreatEndPresBhyt
                + " , " + queryTreatEndTran
                + " , " + queryTreatEndTranBhyt
                + " FROM DUAL";

                result = DAOWorker.SqlDAO.GetSqlSingle<HisTreatmentCounterSDO>(query);
                if (result == null)
                {
                    result = new HisTreatmentCounterSDO();
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HipoApiResult_KhongCoDuLieu,
                        Inventec.Common.DateTime.Convert.TimeNumberToDateString(FROM_TIME),
                        Inventec.Common.DateTime.Convert.TimeNumberToDateString(TO_TIME));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        internal List<HipoFinanceReportSDO> GetFinanceReport(List<List<string>> filter)
        {
            List<HipoFinanceReportSDO> result = null;
            try
            {
                if (IsNotNullOrEmpty(filter))
                {
                    result = new List<HipoFinanceReportSDO>();
                    foreach (var time in filter)
                    {
                        if (time.Count < 2) continue;
                        if (String.IsNullOrEmpty(time[0]) && String.IsNullOrEmpty(time[1])) continue;

                        long fromTime = ConvertTimeStringToTimeNumber(time[0]);
                        long toTime = ConvertTimeStringToTimeNumber(time[1]);
                        if (fromTime > 0 && toTime > 0)
                        {
                            var data = GetFinanceReport(fromTime, toTime);
                            if (data != null)
                            {
                                result.Add(data);
                            }
                            else
                            {
                                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HipoApiResult_KhongCoDuLieu, time[0], time[1]);
                            }
                        }
                    }
                    if (param.Messages == null || param.Messages.Count <= 0)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HipoApiResultSuccess);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        private HipoFinanceReportSDO GetFinanceReport(long FROM_TIME, long TO_TIME)
        {
            HipoFinanceReportSDO result = null;
            try
            {
                string betweenTime = String.Format(" BETWEEN {0} AND {1} ", FROM_TIME, TO_TIME);

                //Tổng thu
                string queryTempTotalPrice = "(SELECT NVL(SUM(SESE.VIR_TOTAL_PRICE),0) FROM HIS_SERE_SERV SESE "
                    + " JOIN HIS_SERVICE_REQ SERE ON SERE.ID = SESE.SERVICE_REQ_ID "
                    + "WHERE (SESE.IS_DELETE IS NULL OR SESE.IS_DELETE <> {0}) "
                    + "AND (SESE.IS_NO_EXECUTE IS NULL OR SESE.IS_NO_EXECUTE <> {1}) "
                    + "AND SERE.INTRUCTION_TIME " + betweenTime
                    + "AND EXISTS (SELECT 1 FROM HIS_SERE_SERV_BILL BILL WHERE BILL.SERE_SERV_ID = SESE.ID)) AS dt";
                string queryTotalPrice = String.Format(queryTempTotalPrice, IMSys.DbConfig.HIS_RS.COMMON.IS_DELETE__TRUE, MOS.UTILITY.Constant.IS_TRUE);

                //Tổng thu vp
                string queryTempTotalPriceVp = "(SELECT NVL(SUM(SESE.VIR_TOTAL_PRICE),0) FROM HIS_SERE_SERV SESE "
                    + " JOIN HIS_SERVICE_REQ SERE ON SERE.ID = SESE.SERVICE_REQ_ID "
                    + "WHERE (SESE.IS_DELETE IS NULL OR SESE.IS_DELETE <> {0}) "
                    + "AND (SESE.IS_NO_EXECUTE IS NULL OR SESE.IS_NO_EXECUTE <> {1}) "
                    + "AND SERE.INTRUCTION_TIME " + betweenTime
                    + "AND SESE.PATIENT_TYPE_ID = {2} "
                    + "AND EXISTS (SELECT 1 FROM HIS_SERE_SERV_BILL BILL WHERE BILL.SERE_SERV_ID = SESE.ID)) AS dt_vp";
                string queryTotalPriceVp = String.Format(queryTempTotalPriceVp, IMSys.DbConfig.HIS_RS.COMMON.IS_DELETE__TRUE, MOS.UTILITY.Constant.IS_TRUE, MANAGER.Config.HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE);

                //Tổng thu bhyt
                string queryTempTotalPriceBhyt = "(SELECT NVL(SUM(SESE.VIR_TOTAL_PRICE),0) FROM HIS_SERE_SERV SESE "
                    + " JOIN HIS_SERVICE_REQ SERE ON SERE.ID = SESE.SERVICE_REQ_ID "
                    + "WHERE (SESE.IS_DELETE IS NULL OR SESE.IS_DELETE <> {0}) "
                    + "AND (SESE.IS_NO_EXECUTE IS NULL OR SESE.IS_NO_EXECUTE <> {1}) "
                    + "AND SERE.INTRUCTION_TIME " + betweenTime
                    + "AND SESE.PATIENT_TYPE_ID = {2}"
                    + "AND EXISTS (SELECT 1 FROM HIS_SERE_SERV_BILL BILL WHERE BILL.SERE_SERV_ID = SESE.ID)) AS dt_bh";
                string queryTotalPriceBhyt = String.Format(queryTempTotalPriceBhyt, IMSys.DbConfig.HIS_RS.COMMON.IS_DELETE__TRUE, MOS.UTILITY.Constant.IS_TRUE, MANAGER.Config.HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);

                //Dịch vụ
                string queryTempService = "(SELECT NVL(SUM(SESE.VIR_TOTAL_PRICE),0) FROM HIS_SERE_SERV SESE "
                    + " JOIN HIS_SERVICE_REQ SERE ON SERE.ID = SESE.SERVICE_REQ_ID "
                    + "WHERE (SESE.IS_DELETE IS NULL OR SESE.IS_DELETE <> {0}) "
                    + "AND (SESE.IS_NO_EXECUTE IS NULL OR SESE.IS_NO_EXECUTE <> {1}) "
                    + "AND SESE.BLOOD_ID IS NULL AND SESE.MATERIAL_ID IS NULL AND SESE.MEDICINE_ID IS NULL "
                    + "AND SERE.INTRUCTION_TIME " + betweenTime
                    + "AND EXISTS (SELECT 1 FROM HIS_SERE_SERV_BILL BILL WHERE BILL.SERE_SERV_ID = SESE.ID)) AS dt_dv";
                string queryService = String.Format(queryTempService, MOS.UTILITY.Constant.IS_TRUE, IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU);

                //Tạm ứng
                string queryTempDeposit = "(SELECT NVL(SUM(AMOUNT),0) FROM HIS_TRANSACTION "
                    + "WHERE (IS_CANCEL IS NULL OR IS_CANCEL <> {0}) "
                    + "AND TRANSACTION_TYPE_ID = {1} "
                    + "AND TRANSACTION_TIME " + betweenTime + ") AS dt_tu";
                string queryDeposit = String.Format(queryTempDeposit, MOS.UTILITY.Constant.IS_TRUE, IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU);

                string query = "SELECT "
                + queryTotalPrice
                + " , " + queryTotalPriceVp
                + " , " + queryTotalPriceBhyt
                + " , " + queryTotalPrice
                + " , " + queryService
                + " , " + queryDeposit
                + " FROM DUAL";

                result = DAOWorker.SqlDAO.GetSqlSingle<HipoFinanceReportSDO>(query);
                if (result == null)
                {
                    result = new HipoFinanceReportSDO();
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HipoApiResult_KhongCoDuLieu,
                        Inventec.Common.DateTime.Convert.TimeNumberToDateString(FROM_TIME),
                        Inventec.Common.DateTime.Convert.TimeNumberToDateString(TO_TIME));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        private long ConvertTimeStringToTimeNumber(string time)
        {
            long result = 0;
            try
            {
                if (!String.IsNullOrWhiteSpace(time))
                {
                    string[] timeAr = time.Split('-', ' ', ':');
                    if (timeAr != null && timeAr.Count() > 0)
                    {
                        string t = "";
                        foreach (var item in timeAr)
                        {
                            t += item;
                        }

                        try
                        {
                            result = Convert.ToInt64(t.Trim());
                        }
                        catch (Exception ex)
                        {
                            Inventec.Common.Logging.LogSystem.Error(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = 0;
            }
            return result;
        }

        internal List<MissingInvoiceInfoMaterialSDO> GetMissingInvoiceInfoMaterialByTreatmentId(long treatmentId)
        {
            try
            {
                string query = "SELECT IMP.IMP_MEST_SUB_CODE, IMP.IMP_MEST_CODE, MT.MATERIAL_TYPE_NAME "
                                + " FROM HIS_SERE_SERV SS "
                                + " JOIN HIS_IMP_MEST_MATERIAL M ON SS.MATERIAL_ID = M.MATERIAL_ID "
                                + " JOIN HIS_IMP_MEST IMP ON IMP.ID = M.IMP_MEST_ID "
                                + " JOIN HIS_MATERIAL M ON M.ID = SS.MATERIAL_ID "
                                + " JOIN HIS_MATERIAL_TYPE MT ON MT.ID = M.MATERIAL_TYPE_ID "
                                + " WHERE SS.AMOUNT > 0 AND SS.IS_DELETE = 0 AND SS.SERVICE_REQ_ID IS NOT NULL "
                                + " AND SS.IS_NO_EXECUTE IS NULL AND SS.IS_EXPEND IS NULL "
                                + " AND IMP.IMP_MEST_TYPE_ID = :param1 "
                                + " AND SS.TDL_TREATMENT_ID = :param2 "
                                + " AND (IMP.DOCUMENT_NUMBER IS NULL OR IMP.DOCUMENT_DATE IS NULL)";


                return DAOWorker.SqlDAO.GetSql<MissingInvoiceInfoMaterialSDO>(query, IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC, treatmentId);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HisTreatmentRationNotApproveSDO> GetRationNotApprove(HisTreatmentRationNotApproveFilter filter)
        {
            List<HisTreatmentRationNotApproveSDO> result = null;
            try
            {
                if (filter.TREATMENT_ID.HasValue || !String.IsNullOrWhiteSpace(filter.TREATMENT_CODE__EXACT))
                {
                    string sql = "";
                    object sqlParam = null;
                    if (filter.TREATMENT_ID.HasValue)
                    {
                        sqlParam = filter.TREATMENT_ID.Value;
                        sql = "SELECT REQ.ID AS ServiceReqId, REQ.SERVICE_REQ_CODE AS ServiceReqCode, RA.RATION_SUM_CODE AS RationSumCode"
                        + " FROM HIS_SERVICE_REQ REQ"
                        + " LEFT JOIN HIS_RATION_SUM RA ON REQ.RATION_SUM_ID = RA.ID"
                        + " WHERE (REQ.IS_NO_EXECUTE IS NULL OR REQ.IS_NO_EXECUTE <> 1)"
                        + " AND (REQ.IS_DELETE IS NULL OR REQ.IS_DELETE <> 1)"
                        + " AND REQ.SERVICE_REQ_TYPE_ID = 17"
                        + " AND REQ.TREATMENT_ID = :param1"
                        + " AND (RA.RATION_SUM_STT_ID IS NULL OR RA.RATION_SUM_STT_ID <> 2)";
                    }
                    else
                    {
                        sqlParam = filter.TREATMENT_CODE__EXACT;
                        sql = "SELECT REQ.ID AS ServiceReqId, REQ.SERVICE_REQ_CODE AS ServiceReqCode, RA.RATION_SUM_CODE AS RationSumCode"
                        + " FROM HIS_SERVICE_REQ REQ"
                        + " LEFT JOIN HIS_RATION_SUM RA ON REQ.RATION_SUM_ID = RA.ID"
                        + " WHERE (REQ.IS_NO_EXECUTE IS NULL OR REQ.IS_NO_EXECUTE <> 1)"
                        + " AND (REQ.IS_DELETE IS NULL OR REQ.IS_DELETE <> 1)"
                        + " AND REQ.SERVICE_REQ_TYPE_ID = 17"
                        + " AND REQ.TDL_TREATMENT_CODE = :param1"
                        + " AND (RA.RATION_SUM_STT_ID IS NULL OR RA.RATION_SUM_STT_ID <> 2)";
                    }

                    result = DAOWorker.SqlDAO.GetSql<HisTreatmentRationNotApproveSDO>(sql, sqlParam);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        internal List<HisTreatmentMedicineTDO> GetMedicineForEmr(HisTreatmentMedicineForEmrFilter filter)
        {
            List<HisTreatmentMedicineTDO> result = null;
            try
            {
                if (!filter.TREATMENT_ID.HasValue && String.IsNullOrWhiteSpace(filter.TREATMENT_CODE__EXACT))
                {
                    LogSystem.Info("GetMedicineForEmr. Filter TREATMENT_ID and TREATMENT_CODE__EXACT is empty");
                    return null;
                }

                HIS_TREATMENT treatment = null;
                if (filter.TREATMENT_ID.HasValue)
                {
                    treatment = DAOWorker.SqlDAO.GetSqlSingle<HIS_TREATMENT>("SELECT * FROM HIS_TREATMENT WHERE ID = :param1", filter.TREATMENT_ID.Value);
                }
                else
                {
                    treatment = DAOWorker.SqlDAO.GetSqlSingle<HIS_TREATMENT>("SELECT * FROM HIS_TREATMENT WHERE TREATMENT_CODE = :param1", filter.TREATMENT_CODE__EXACT);
                }

                if (treatment == null)
                {

                    LogSystem.Info("GetMedicineForEmr. TreatmentId or TreatmentCode is invalid: " + filter.TREATMENT_CODE__EXACT + ", " + filter.TREATMENT_ID);
                    return null;
                }

                List<HisTreatmentMedicineTDO> tempTDOs = new List<HisTreatmentMedicineTDO>();

                //Thuoc trong kho
                if (!filter.IS_OUT_STOCK.HasValue || !filter.IS_OUT_STOCK.Value)
                {
                    HisExpMestMedicineView6FilterQuery medicineFilter = new HisExpMestMedicineView6FilterQuery();
                    medicineFilter.TDL_TREATMENT_ID = treatment.ID;
                    medicineFilter.TDL_INTRUCTION_DATE_FROM = filter.INSTRUCTION_DATE_FROM;
                    medicineFilter.TDL_INTRUCTION_DATE_TO = filter.INSTRUCTION_DATE_TO;
                    medicineFilter.TDL_INTRUCTION_DATE__EQUAL = filter.INSTRUCTION_DATE;
                    medicineFilter.IS_EXPORT = filter.IS_EXPORT;
                    medicineFilter.IS_NOT_TAKEN = false;

                    List<V_HIS_EXP_MEST_MEDICINE_6> expMestMedicines = new HisExpMestMedicineGet().GetView6(medicineFilter);

                    if (IsNotNullOrEmpty(expMestMedicines))
                    {
                        List<V_HIS_MEDICINE_TYPE> medicineTypes = new HisMedicineTypeGet().GetViewByIds(expMestMedicines.Select(s => s.TDL_MEDICINE_TYPE_ID ?? 0).Distinct().ToList());

                        // Lay ra tat ca cac thong tin chi dinh cha
                        List<long> ssParentIds = expMestMedicines.Where(o => o.SERE_SERV_PARENT_ID.HasValue).Select(s => s.SERE_SERV_PARENT_ID.Value).ToList();
                        List<V_HIS_SERE_SERV> ssParents = new List<V_HIS_SERE_SERV>();
                        if (IsNotNullOrEmpty(ssParentIds))
                        {
                            StringBuilder sqlBuilder = new StringBuilder();
                            sqlBuilder.Append("SELECT * FROM V_HIS_SERE_SERV WHERE %IN_CLAUSE%");
                            string query = this.AddInClause(ssParentIds, sqlBuilder.ToString(), "ID");
                            List<V_HIS_SERE_SERV> ss = DAOWorker.SqlDAO.GetSql<V_HIS_SERE_SERV>(query);
                            ssParents.AddRange(ss);
                        }

                        foreach (V_HIS_EXP_MEST_MEDICINE_6 exp in expMestMedicines)
                        {
                            HisTreatmentMedicineTDO tdo = new HisTreatmentMedicineTDO();
                            tdo.Amount = exp.AMOUNT;
                            tdo.Concentra = exp.CONCENTRA;
                            tdo.ExpireDate = exp.EXPIRED_DATE;
                            tdo.ExpPrice = exp.PRICE;
                            tdo.ExpVatRatio = exp.VAT_RATIO;
                            tdo.IsExport = (exp.IS_EXPORT == Constant.IS_TRUE);
                            tdo.IsOutStock = false;
                            tdo.MedicineId = exp.MEDICINE_ID;
                            tdo.MedicineTypeCode = exp.MEDICINE_TYPE_CODE;
                            tdo.MedicineTypeName = exp.MEDICINE_TYPE_NAME;

                            V_HIS_MEDICINE_TYPE type = medicineTypes != null ? medicineTypes.FirstOrDefault(o => o.ID == exp.MEDICINE_TYPE_ID) : null;
                            if (type != null)
                            {
                                tdo.MedicineUseFormCode = type.MEDICINE_USE_FORM_CODE;
                                tdo.MedicineUseFormName = type.MEDICINE_USE_FORM_NAME;
                            }

                            tdo.PackageNumber = exp.PACKAGE_NUMBER;
                            tdo.RequestLoginname = exp.REQ_LOGINNAME;
                            tdo.RequestUsername = exp.REQ_USERNAME;
                            tdo.ServiceUnitCode = exp.SERVICE_UNIT_CODE;
                            tdo.ServiceUnitName = exp.SERVICE_UNIT_NAME;
                            tdo.Speed = exp.SPEED;
                            tdo.TreatmentCode = treatment.TREATMENT_CODE;
                            tdo.InstructionDate = exp.TDL_INTRUCTION_DATE;
                            tdo.InstructionTime = exp.TDL_INTRUCTION_TIME;
                            tdo.SereServParentId = exp.SERE_SERV_PARENT_ID;
                            tdo.Tutorial = exp.TUTORIAL;
                            tdo.EXP_MEST_STT_ID = exp.EXP_MEST_STT_ID;
                            // Lay thong tin dich vu cha
                            V_HIS_SERE_SERV ssParent = IsNotNullOrEmpty(ssParents) ? ssParents.FirstOrDefault(o => o.ID == exp.SERE_SERV_PARENT_ID) : null;
                            V_HIS_SERVICE service = IsNotNull(ssParent) ? HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == ssParent.SERVICE_ID) : null;
                            tdo.ServiceId = service != null ? (long?)service.ID : null;
                            tdo.ServiceName = service != null ? service.SERVICE_NAME : null;

                            tempTDOs.Add(tdo);
                        }
                    }
                }


                //Thuoc ngoai kho
                if (!filter.IS_OUT_STOCK.HasValue || filter.IS_OUT_STOCK.Value)
                {
                    List<V_HIS_SERVICE_REQ_METY> lstMetyReq = null;
                    List<V_HIS_EXP_MEST_MEDICINE_6> outMedicines = null;
                    HisServiceReqMetyViewFilterQuery reqMetyFilter = new HisServiceReqMetyViewFilterQuery();

                    reqMetyFilter.TREATMENT_ID = treatment.ID;
                    reqMetyFilter.INTRUCTION_DATE_FROM = filter.INSTRUCTION_DATE_FROM;
                    reqMetyFilter.INTRUCTION_DATE_TO = filter.INSTRUCTION_DATE_TO;
                    reqMetyFilter.INTRUCTION_DATE__EQUAL = filter.INSTRUCTION_DATE;

                    lstMetyReq = new HisServiceReqMetyGet().GetView(reqMetyFilter);

                    if (lstMetyReq != null && lstMetyReq.Count > 0)
                    {
                        HisExpMestMedicineView6FilterQuery outMedicineFilter = new HisExpMestMedicineView6FilterQuery();
                        outMedicineFilter.PRESCRIPTION_IDs = lstMetyReq.Select(s => s.SERVICE_REQ_ID).Distinct().ToList();
                        outMedicineFilter.IS_NOT_TAKEN = false;
                        outMedicines = new HisExpMestMedicineGet().GetView6(outMedicineFilter);

                    }

                    if (IsNotNullOrEmpty(lstMetyReq))
                    {
                        List<V_HIS_MEDICINE_TYPE> medicineTypes = new HisMedicineTypeGet().GetViewByIds(lstMetyReq.Select(s => s.MEDICINE_TYPE_ID ?? 0).Distinct().ToList());
                        if (IsNotNullOrEmpty(outMedicines))
                        {
                            // Lay ra tat ca cac thong tin chi dinh cha
                            List<long> ssParentIds = outMedicines.Where(o => o.SERE_SERV_PARENT_ID.HasValue).Select(s => s.SERE_SERV_PARENT_ID.Value).ToList();
                            List<V_HIS_SERE_SERV> ssParents = new List<V_HIS_SERE_SERV>();
                            if (IsNotNullOrEmpty(ssParentIds))
                            {
                                StringBuilder sqlBuilder = new StringBuilder();
                                sqlBuilder.Append("SELECT * FROM V_HIS_SERE_SERV WHERE %IN_CLAUSE%");
                                string query = this.AddInClause(ssParentIds, sqlBuilder.ToString(), "ID");
                                List<V_HIS_SERE_SERV> ss = DAOWorker.SqlDAO.GetSql<V_HIS_SERE_SERV>(query);
                                ssParents.AddRange(ss);
                            }

                            foreach (V_HIS_EXP_MEST_MEDICINE_6 exp in outMedicines)
                            {
                                if (filter.IS_EXPORT.HasValue)
                                {
                                    if (filter.IS_EXPORT.Value && exp.IS_EXPORT != Constant.IS_TRUE)
                                    {
                                        continue;
                                    }
                                    else if (!filter.IS_EXPORT.Value && exp.IS_EXPORT == Constant.IS_TRUE)
                                    {
                                        continue;
                                    }
                                }
                                HisTreatmentMedicineTDO tdo = new HisTreatmentMedicineTDO();
                                tdo.Amount = exp.AMOUNT;
                                tdo.Concentra = exp.CONCENTRA;
                                tdo.ExpireDate = exp.EXPIRED_DATE;
                                tdo.ExpPrice = exp.PRICE;
                                tdo.ExpVatRatio = exp.VAT_RATIO;
                                tdo.IsExport = (exp.IS_EXPORT == Constant.IS_TRUE);
                                tdo.IsOutStock = true;
                                tdo.MedicineId = exp.MEDICINE_ID;
                                tdo.MedicineTypeCode = exp.MEDICINE_TYPE_CODE;
                                tdo.MedicineTypeName = exp.MEDICINE_TYPE_NAME;

                                V_HIS_MEDICINE_TYPE type = medicineTypes != null ? medicineTypes.FirstOrDefault(o => o.ID == exp.MEDICINE_TYPE_ID) : null;
                                if (type != null)
                                {
                                    tdo.MedicineUseFormCode = type.MEDICINE_USE_FORM_CODE;
                                    tdo.MedicineUseFormName = type.MEDICINE_USE_FORM_NAME;
                                }

                                tdo.PackageNumber = exp.PACKAGE_NUMBER;
                                tdo.RequestLoginname = exp.REQ_LOGINNAME;
                                tdo.RequestUsername = exp.REQ_USERNAME;
                                tdo.ServiceUnitCode = exp.SERVICE_UNIT_CODE;
                                tdo.ServiceUnitName = exp.SERVICE_UNIT_NAME;
                                tdo.Speed = exp.SPEED;
                                tdo.TreatmentCode = treatment.TREATMENT_CODE;
                                tdo.InstructionDate = exp.TDL_INTRUCTION_DATE;
                                tdo.InstructionTime = exp.TDL_INTRUCTION_TIME;
                                tdo.SereServParentId = exp.SERE_SERV_PARENT_ID;
                                tdo.Tutorial = exp.TUTORIAL;
                                tdo.EXP_MEST_STT_ID = exp.EXP_MEST_STT_ID;
                                // Lay thong tin dich vu cha
                                V_HIS_SERE_SERV ssParent = IsNotNullOrEmpty(ssParents) ? ssParents.FirstOrDefault(o => o.ID == exp.SERE_SERV_PARENT_ID) : null;
                                V_HIS_SERVICE service = IsNotNull(ssParent) ? HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == ssParent.SERVICE_ID) : null;
                                tdo.ServiceId = service != null ? (long?)service.ID : null;
                                tdo.ServiceName = service != null ? service.SERVICE_NAME : null;

                                tempTDOs.Add(tdo);
                            }
                        }

                        foreach (V_HIS_SERVICE_REQ_METY req in lstMetyReq)
                        {
                            V_HIS_EXP_MEST_MEDICINE_6 exp = null;
                            if (req.MEDICINE_TYPE_ID.HasValue && IsNotNullOrEmpty(outMedicines))
                            {
                                exp = outMedicines.FirstOrDefault(a => a.PRESCRIPTION_ID == req.SERVICE_REQ_ID && a.MEDICINE_TYPE_ID == req.MEDICINE_TYPE_ID);
                            }

                            if (filter.IS_EXPORT.HasValue)
                            {
                                if (filter.IS_EXPORT.Value && (exp == null || exp.IS_EXPORT != Constant.IS_TRUE))
                                {
                                    continue;
                                }
                                else if (!filter.IS_EXPORT.Value && (exp != null && exp.IS_EXPORT == Constant.IS_TRUE))
                                {
                                    continue;
                                }
                            }

                            if (exp == null)
                            {
                                HisTreatmentMedicineTDO tdo = new HisTreatmentMedicineTDO();
                                tdo.Amount = req.AMOUNT;
                                tdo.IsExport = false;
                                tdo.IsOutStock = true;
                                tdo.MedicineTypeName = req.MEDICINE_TYPE_NAME;

                                if (req.MEDICINE_TYPE_ID.HasValue)
                                {
                                    V_HIS_MEDICINE_TYPE type = medicineTypes != null ? medicineTypes.FirstOrDefault(o => o.ID == req.MEDICINE_TYPE_ID.Value) : null;
                                    if (type != null)
                                    {
                                        tdo.MedicineUseFormCode = type.MEDICINE_USE_FORM_CODE;
                                        tdo.MedicineUseFormName = type.MEDICINE_USE_FORM_NAME;
                                        tdo.Concentra = type.CONCENTRA;
                                        tdo.MedicineTypeCode = type.MEDICINE_TYPE_CODE;
                                        tdo.MedicineTypeName = type.MEDICINE_TYPE_NAME;
                                        tdo.ServiceUnitCode = type.SERVICE_UNIT_CODE;
                                        tdo.ServiceUnitName = type.SERVICE_UNIT_NAME;
                                    }
                                }

                                tdo.RequestLoginname = req.REQUEST_LOGINNAME;
                                tdo.RequestUsername = req.REQUEST_USERNAME;
                                tdo.Speed = req.SPEED;
                                tdo.TreatmentCode = treatment.TREATMENT_CODE;
                                tdo.InstructionDate = req.INTRUCTION_DATE;
                                tdo.InstructionTime = req.INTRUCTION_TIME;
                                tdo.Tutorial = req.TUTORIAL;
                                tempTDOs.Add(tdo);
                            }
                        }
                    }
                }

                if (IsNotNullOrEmpty(tempTDOs))
                {
                    var Groups = tempTDOs.GroupBy(g => new { g.Concentra, g.ExpireDate, g.ExpPrice, g.ExpVatRatio, g.InstructionTime, g.IsExport, g.IsOutStock, g.MedicineId, g.MedicineTypeCode, g.MedicineUseFormCode, g.PackageNumber, g.RequestLoginname, g.ServiceUnitCode, g.Speed, g.SereServParentId, g.ServiceId, g.Tutorial, g.EXP_MEST_STT_ID }).ToList();

                    result = (from r in Groups
                              select new HisTreatmentMedicineTDO()
                                  {
                                      Amount = r.Sum(s => s.Amount),
                                      Concentra = r.Key.Concentra,
                                      ExpireDate = r.Key.ExpireDate,
                                      ExpPrice = r.Key.ExpPrice,
                                      ExpVatRatio = r.Key.ExpVatRatio,
                                      InstructionDate = r.FirstOrDefault().InstructionDate,
                                      InstructionTime = r.Key.InstructionTime,
                                      IsExport = r.Key.IsExport,
                                      IsOutStock = r.Key.IsOutStock,
                                      MedicineId = r.Key.MedicineId,
                                      MedicineTypeCode = r.Key.MedicineTypeCode,
                                      MedicineTypeName = r.FirstOrDefault().MedicineTypeName,
                                      MedicineUseFormCode = r.Key.MedicineUseFormCode,
                                      MedicineUseFormName = r.FirstOrDefault().MedicineUseFormName,
                                      PackageNumber = r.Key.PackageNumber,
                                      RequestLoginname = r.Key.RequestLoginname,
                                      RequestUsername = r.FirstOrDefault().RequestUsername,
                                      ServiceUnitCode = r.Key.ServiceUnitCode,
                                      ServiceUnitName = r.FirstOrDefault().ServiceUnitName,
                                      Speed = r.Key.Speed,
                                      TreatmentCode = r.FirstOrDefault().TreatmentCode,
                                      SereServParentId = r.FirstOrDefault().SereServParentId,
                                      ServiceId = r.FirstOrDefault().ServiceId,
                                      ServiceName = r.FirstOrDefault().ServiceName,
                                      Tutorial = r.FirstOrDefault().Tutorial,
                                      EXP_MEST_STT_ID = r.FirstOrDefault().EXP_MEST_STT_ID
                                  }).OrderBy(o => o.InstructionTime).ToList();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        internal HisTreatmentClinicalDetailForEmrTDO GetClinicalDetailForEmr(string treatmentCode)
        {
            HisTreatmentClinicalDetailForEmrTDO result = null;
            try
            {
                if (String.IsNullOrWhiteSpace(treatmentCode))
                {
                    LogSystem.Info("GetClinicalDetailForEmr. Filter TREATMENT_CODE__EXACT is empty");
                    return null;
                }

                HIS_TREATMENT treatment = DAOWorker.SqlDAO.GetSqlSingle<HIS_TREATMENT>("SELECT * FROM HIS_TREATMENT WHERE TREATMENT_CODE = :param1", treatmentCode);

                if (treatment == null)
                {
                    LogSystem.Info("GetClinicalDetailForEmr. TreatmentCode is invalid: " + treatmentCode);
                    return null;
                }

                List<V_HIS_SERE_SERV_PTTT> SereServPttts = DAOWorker.SqlDAO.GetSql<V_HIS_SERE_SERV_PTTT>("SELECT * FROM V_HIS_SERE_SERV_PTTT WHERE TDL_TREATMENT_ID = :param1", treatment.ID);
                List<HIS_SERE_SERV_TEIN> SereServTeins = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV_TEIN>("SELECT ST.* FROM HIS_SERE_SERV_TEIN ST JOIN HIS_SERE_SERV SS ON ST.SERE_SERV_ID = SS.ID WHERE ST.TDL_TREATMENT_ID = :param1 AND SS.IS_DELETE = 0 AND SS.SERVICE_REQ_ID IS NOT NULL AND (SS.IS_NO_EXECUTE IS NULL OR SS.IS_NO_EXECUTE <> 1)", treatment.ID);
                List<HIS_SERE_SERV> SereServsAlls = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>("SELECT * FROM HIS_SERE_SERV WHERE TDL_TREATMENT_ID = :param1 AND (IS_DELETE IS NULL OR IS_DELETE <> 1) AND SERVICE_REQ_ID IS NOT NULL AND TDL_PATIENT_ID IS NOT NULL", treatment.ID);
                List<HIS_SERVICE_REQ> ServiceReqs = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ>("SELECT * FROM HIS_SERVICE_REQ WHERE TREATMENT_ID = :param1 AND (IS_DELETE IS NULL OR IS_DELETE <> 1)", treatment.ID);

                if ((IsNotNullOrEmpty(SereServPttts) || IsNotNullOrEmpty(SereServTeins)) && IsNotNullOrEmpty(SereServsAlls) && IsNotNullOrEmpty(ServiceReqs))
                {
                    result = new HisTreatmentClinicalDetailForEmrTDO();
                    if (IsNotNullOrEmpty(SereServPttts))
                    {
                        List<long> sereServIds = SereServPttts.Select(s => s.SERE_SERV_ID).Distinct().ToList();
                        string sqlExt = DAOWorker.SqlDAO.AddInClause(sereServIds, "SELECT * FROM HIS_SERE_SERV_EXT WHERE TDL_TREATMENT_ID = :param1 AND %IN_CLAUSE%", "SERE_SERV_ID");
                        List<HIS_SERE_SERV_EXT> SereServExts = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV_EXT>(sqlExt, treatment.ID);

                        List<V_HIS_EKIP_USER> EkipUsers = null;
                        List<long> ekipIds = SereServsAlls != null ? SereServsAlls.Where(o => o.EKIP_ID.HasValue).Select(s => s.EKIP_ID.Value).Distinct().ToList() : null;
                        if (IsNotNullOrEmpty(ekipIds))
                        {
                            string sqlEkip = DAOWorker.SqlDAO.AddInClause(ekipIds, "SELECT * FROM V_HIS_EKIP_USER WHERE %IN_CLAUSE%", "EKIP_ID");
                            EkipUsers = DAOWorker.SqlDAO.GetSql<V_HIS_EKIP_USER>(sqlEkip);
                        }

                        List<V_HIS_EKIP_PLAN_USER> EkipPlanUsers = null;
                        List<long> ekipPlanIds = DAOWorker.SqlDAO.GetSql<long>("SELECT EKIP_PLAN_ID FROM HIS_SERVICE_REQ WHERE TREATMENT_ID = :param1 AND EKIP_PLAN_ID IS NOT NULL", treatment.ID);//result.ServiceReqs != null ? result.ServiceReqs.Where(o => o.EKIP_PLAN_ID.HasValue).Select(s => s.EKIP_PLAN_ID.Value).Distinct().ToList() : null;
                        if (IsNotNullOrEmpty(ekipPlanIds))
                        {
                            string sqlEkipPlan = DAOWorker.SqlDAO.AddInClause(ekipPlanIds, "SELECT * FROM V_HIS_EKIP_PLAN_USER WHERE %IN_CLAUSE%", "EKIP_PLAN_ID");
                            EkipPlanUsers = DAOWorker.SqlDAO.GetSql<V_HIS_EKIP_PLAN_USER>(sqlEkipPlan);
                        }

                        result.PhauThuatThuThuat = ProgressPhauThuatThuThuat(SereServPttts, SereServsAlls, SereServExts, ServiceReqs, EkipUsers, EkipPlanUsers);
                    }

                    if (IsNotNullOrEmpty(SereServTeins))
                    {
                        result.ChiSoXetNghiem = ProgressChiSoXetNghiem(SereServTeins);
                    }
                }

                // Xu ly voi cac dich vu ptttt chua xu ly, dang xy ly
                List<HIS_SERE_SERV> ssHasNoPttt = null;
                if (IsNotNullOrEmpty(SereServPttts) && IsNotNullOrEmpty(SereServsAlls))
                {
                    ssHasNoPttt = SereServsAlls.Where(o => (o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT || o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT) && !SereServPttts.Select(s => s.SERE_SERV_ID).Contains(o.ID)).ToList();
                }
                else if (IsNotNullOrEmpty(SereServsAlls))
                {
                    ssHasNoPttt = SereServsAlls.Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT || o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT).ToList();
                }
                if (IsNotNullOrEmpty(ssHasNoPttt))
                {
                    if (result == null)
                    {
                        result = new HisTreatmentClinicalDetailForEmrTDO();
                    }
                    if (result.PhauThuatThuThuat == null)
                    {
                        result.PhauThuatThuThuat = new List<PhauThuatThuThuatEmrTDO>();
                    }
                    this.ProcessSSHasNoPttt(ssHasNoPttt, ServiceReqs, result);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        private void ProcessSSHasNoPttt(List<HIS_SERE_SERV> ssHasNoPttt, List<HIS_SERVICE_REQ> serviceReqs, HisTreatmentClinicalDetailForEmrTDO result)
        {
            List<long> sereServIds = ssHasNoPttt.Select(s => s.ID).Distinct().ToList();
            string sqlExt = DAOWorker.SqlDAO.AddInClause(sereServIds, "SELECT * FROM HIS_SERE_SERV_EXT WHERE %IN_CLAUSE%", "SERE_SERV_ID");
            List<HIS_SERE_SERV_EXT> allSSExt = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV_EXT>(sqlExt);

            foreach (HIS_SERE_SERV ss in ssHasNoPttt)
            {
                var tdo = new PhauThuatThuThuatEmrTDO();
                tdo.IDChiTietYLenh = ss.ID;
                tdo.TenPhauThuatThuThuat = ss.TDL_SERVICE_NAME;
                tdo.Loai = (int)ss.TDL_SERVICE_TYPE_ID;
                HIS_SERE_SERV_EXT ext = allSSExt != null ? allSSExt.FirstOrDefault(o => o.SERE_SERV_ID == ss.ID) : null;
                HIS_SERVICE_REQ req = serviceReqs != null ? serviceReqs.FirstOrDefault(o => o.ID == ss.SERVICE_REQ_ID) : null;

                List<ThuocVatTuEmrTDO> VatTuTieuHaoss = new List<ThuocVatTuEmrTDO>();
                string ServiceName = "";
                if (ssHasNoPttt != null && ssHasNoPttt.Count() > 0)
                {
                    //lấy vật tư
                    List<HIS_SERE_SERV> lstMaterial = ssHasNoPttt.Where(o => o.MATERIAL_ID != null && o.IS_NO_EXECUTE != 1 && o.PARENT_ID == ss.ID).ToList();

                    List<HIS_SERE_SERV> lstMaterials = new List<HIS_SERE_SERV>();

                    if (lstMaterial != null && lstMaterial.Count() > 0)
                    {
                        List<V_HIS_MATERIAL_TYPE> lstMaterialTypes = new List<V_HIS_MATERIAL_TYPE>();

                        HisMaterialTypeViewFilterQuery typeFilter = new HisMaterialTypeViewFilterQuery();
                        typeFilter.IS_ACTIVE = Constant.IS_TRUE;
                        List<V_HIS_MATERIAL_TYPE> materialTypes = new HisMaterialTypeGet().GetView(typeFilter);

                        foreach (var item in lstMaterial)
                        {
                            var lstConsumable = materialTypes.FirstOrDefault(o => o.IS_CONSUMABLE == 1 && o.SERVICE_ID == item.SERVICE_ID);
                            if (IsNotNull(lstConsumable))
                            {
                                lstMaterialTypes.Add(lstConsumable);
                                lstMaterials.Add(item);
                            }
                        }

                        if (lstMaterials != null && lstMaterials.Count > 0 && lstMaterialTypes != null && lstMaterialTypes.Count > 0)
                        {
                            lstMaterials = lstMaterials.OrderBy(o => o.SERVICE_ID).ToList();

                            foreach (var item in lstMaterials)
                            {
                                var lstMaterialType = lstMaterialTypes.FirstOrDefault(o => o.SERVICE_ID == item.SERVICE_ID);

                                VatTuTieuHaoss.Add(new ThuocVatTuEmrTDO()
                                {
                                    TenThuocVatTu = item.TDL_SERVICE_NAME,
                                    MaThuocVatTu = item.TDL_SERVICE_CODE,
                                    SoLuong = item.AMOUNT,
                                    DonVi = lstMaterialType != null ? lstMaterialType.SERVICE_UNIT_NAME : "",
                                    HangSanXuat = lstMaterialType != null ? lstMaterialType.MANUFACTURER_NAME : "",
                                });
                            }
                        }
                    }
                }
                if (ext != null)
                {
                    DateTime beginTime = (ext != null && ext.BEGIN_TIME.HasValue) ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(ext.BEGIN_TIME.Value).Value : DateTime.MinValue;
                    if (req != null && ss.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT && HisServiceReqCFG.SURG_EXECUTE_TAKE_INTRUCION_TIME_BY_SERVICE_REQ && ((ext != null && ext.BEGIN_TIME == null) || ext == null))
                    {
                        beginTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(req.INTRUCTION_TIME) ?? DateTime.Now;
                    }

                    DateTime datePttt = beginTime != DateTime.MinValue ? new DateTime(beginTime.Year, beginTime.Month, beginTime.Day) : DateTime.MinValue;
                    HIS_SERVICE_REQ serviceReq = ss != null ? serviceReqs.FirstOrDefault(o => o.ID == ss.SERVICE_REQ_ID) : null;
                    if (serviceReq != null && ss != null && ss.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT && HisServiceReqCFG.SURG_EXECUTE_TAKE_INTRUCION_TIME_BY_SERVICE_REQ && ((ext != null && ext.BEGIN_TIME == null) || ext == null))
                    {
                        beginTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.INTRUCTION_TIME) ?? DateTime.Now;
                    }
                    tdo.NgayPhauThuatThuThuat = datePttt;
                    tdo.NgayPhauThuatThuThuat_Gio = beginTime;

                    tdo.VatTuTieuHaos = (VatTuTieuHaoss != null && VatTuTieuHaoss.Count > 0) ? VatTuTieuHaoss : new List<ThuocVatTuEmrTDO>();
                    tdo.MoTa = ext.DESCRIPTION;
                    tdo.ThoiGianXyLy = MinuteNumberToFinish(ext.BEGIN_TIME, ext.END_TIME);
                    tdo.MaBacSyChiDinh = serviceReq != null ? serviceReq.REQUEST_LOGINNAME : "";
                    tdo.BacSyChiDinh = serviceReq != null ? serviceReq.REQUEST_USERNAME : "";
                    tdo.ThoiGianThuchien = ext.BEGIN_TIME;
                    tdo.ThoiGianKetThuc = ext.END_TIME;
                }

                if (req != null)
                {
                    tdo.ThoiGianYLenh = req.INTRUCTION_TIME;
                    tdo.MaYLenh = req.SERVICE_REQ_CODE;
                }
                result.PhauThuatThuThuat.Add(tdo);
            }
        }

        private List<ChiSoXetNghiemEmrTDO> ProgressChiSoXetNghiem(List<HIS_SERE_SERV_TEIN> SereServTeins)
        {
            List<ChiSoXetNghiemEmrTDO> result = new List<ChiSoXetNghiemEmrTDO>();
            try
            {
                List<HIS_SERE_SERV_TEIN> filterSereServTeins = new List<HIS_SERE_SERV_TEIN>();
                filterSereServTeins = SereServTeins.Where(o => o.VALUE != null && o.TEST_INDEX_ID != null).ToList();

                var SereServTein = filterSereServTeins.GroupBy(o => o.TEST_INDEX_ID).ToList();
                foreach (var g in SereServTein)
                {
                    var SereServTeinMax = g.OrderByDescending(o => o.MODIFY_TIME).FirstOrDefault();
                    var testIndex = HisTestIndexCFG.DATA_VIEW.FirstOrDefault(o => o.ID == SereServTeinMax.TEST_INDEX_ID);
                    if (testIndex != null)
                    {
                        ChiSoXetNghiemEmrTDO chiSoXetNghiem = new ChiSoXetNghiemEmrTDO();
                        chiSoXetNghiem.TestIndexCode = testIndex.TEST_INDEX_CODE;
                        chiSoXetNghiem.TestIndexName = testIndex.TEST_INDEX_NAME;
                        chiSoXetNghiem.Value = SereServTeinMax.VALUE;
                        chiSoXetNghiem.Description = !string.IsNullOrEmpty(SereServTeinMax.DESCRIPTION) ? SereServTeinMax.DESCRIPTION : "";
                        chiSoXetNghiem.ExecuteTime = SereServTeinMax.MODIFY_TIME != null ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(SereServTeinMax.MODIFY_TIME.Value) : null;
                        chiSoXetNghiem.IsBloodABO = testIndex.IS_BLOOD_ABO == 1 ? true : false;
                        chiSoXetNghiem.IsBloodRH = testIndex.IS_BLOOD_RH == 1 ? true : false;
                        chiSoXetNghiem.IsAbsAg = testIndex.IS_HBSAG == 1 ? true : false;
                        chiSoXetNghiem.IsHCV = testIndex.IS_HCV == 1 ? true : false;
                        chiSoXetNghiem.IsHIV = testIndex.IS_HIV == 1 ? true : false;
                        result.Add(chiSoXetNghiem);
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private List<PhauThuatThuThuatEmrTDO> ProgressPhauThuatThuThuat(List<V_HIS_SERE_SERV_PTTT> sereServPttts, List<HIS_SERE_SERV> sereServAlls, List<HIS_SERE_SERV_EXT> sereServExts, List<HIS_SERVICE_REQ> serviceReqs, List<V_HIS_EKIP_USER> hisEkipUsers, List<V_HIS_EKIP_PLAN_USER> hisEkipPlanUsers)
        {
            List<PhauThuatThuThuatEmrTDO> result = new List<PhauThuatThuThuatEmrTDO>();
            try
            {
                foreach (var sspttt in sereServPttts)
                {
                    HIS_SERE_SERV_EXT ssext = sereServExts != null && sereServExts.Count > 0 ? sereServExts.FirstOrDefault(o => o.SERE_SERV_ID == sspttt.SERE_SERV_ID) : null;

                    DateTime beginTime = (ssext != null && ssext.BEGIN_TIME.HasValue) ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(ssext.BEGIN_TIME.Value).Value : DateTime.MinValue;

                    HIS_SERE_SERV ss = sereServAlls != null ? sereServAlls.FirstOrDefault(o => o.ID == sspttt.SERE_SERV_ID) : null;
                    HIS_SERVICE_REQ serviceReq = ss != null ? serviceReqs.FirstOrDefault(o => o.ID == ss.SERVICE_REQ_ID) : null;
                    if (serviceReq != null && ss != null && ss.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT && HisServiceReqCFG.SURG_EXECUTE_TAKE_INTRUCION_TIME_BY_SERVICE_REQ && ((ssext != null && ssext.BEGIN_TIME == null) || ssext == null))
                    {
                        beginTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.INTRUCTION_TIME) ?? DateTime.Now;
                    }

                    DateTime datePttt = beginTime != DateTime.MinValue ? new DateTime(beginTime.Year, beginTime.Month, beginTime.Day) : DateTime.MinValue;
                    string bacSyPhauThuat = "", bacSyPhauThuatHoVaTen = "", bacSyGayMe = "", bacSyGayMeHoVaTen = "";
                    List<string> EkipThucHien = new List<string>();
                    if (ss != null && ss.EKIP_ID.HasValue)
                    {
                        var ekipUsers = hisEkipUsers != null ? hisEkipUsers.Where(o => o.EKIP_ID == ss.EKIP_ID).ToList() : null;
                        if (ekipUsers != null && ekipUsers.Count > 0)
                        {
                            V_HIS_EKIP_USER ekipUserBSPhauThuat = ekipUsers.FirstOrDefault(o => o.EXECUTE_ROLE_CODE == HisExecuteRoleCFG.EXECUTE_ROLE_CODE__MAIN);
                            if (IsNotNull(ekipUserBSPhauThuat))
                            {
                                bacSyPhauThuat = ekipUserBSPhauThuat.LOGINNAME;
                                bacSyPhauThuatHoVaTen = ekipUserBSPhauThuat.USERNAME;
                            }

                            V_HIS_EKIP_USER ekipUserBSGatMe = ekipUsers.FirstOrDefault(o => o.EXECUTE_ROLE_CODE == HisExecuteRoleCFG.EXECUTE_ROLE_CODE__ANESTHETIST);
                            if (IsNotNull(ekipUserBSGatMe))
                            {
                                bacSyGayMe = ekipUserBSGatMe.LOGINNAME;
                                bacSyGayMeHoVaTen = ekipUserBSGatMe.USERNAME;
                            }

                            EkipThucHien = ekipUsers.Select(o => o.USERNAME).ToList();
                        }
                    }

                    else if (serviceReq != null && serviceReq.EKIP_PLAN_ID.HasValue)
                    {
                        var ekipPlans = hisEkipPlanUsers != null ? hisEkipPlanUsers.Where(o => o.EKIP_PLAN_ID == serviceReq.EKIP_PLAN_ID.Value).ToList() : null;
                        if (ekipPlans != null && ekipPlans.Count > 0)
                        {
                            V_HIS_EKIP_PLAN_USER ekipUserPlanBSPhauThuat = ekipPlans.FirstOrDefault(o => o.EXECUTE_ROLE_CODE == HisExecuteRoleCFG.EXECUTE_ROLE_CODE__MAIN);
                            if (IsNotNull(ekipUserPlanBSPhauThuat))
                            {
                                bacSyPhauThuat = ekipUserPlanBSPhauThuat.LOGINNAME;
                                bacSyPhauThuatHoVaTen = ekipUserPlanBSPhauThuat.USERNAME;
                            }

                            V_HIS_EKIP_PLAN_USER ekipUserPlanBSGatMe = ekipPlans.FirstOrDefault(o => o.EXECUTE_ROLE_CODE == HisExecuteRoleCFG.EXECUTE_ROLE_CODE__ANESTHETIST);
                            if (IsNotNull(ekipUserPlanBSGatMe))
                            {
                                bacSyGayMe = ekipUserPlanBSGatMe.LOGINNAME;
                                bacSyGayMeHoVaTen = ekipUserPlanBSGatMe.USERNAME;
                            }
                        }
                    }

                    List<ThuocVatTuEmrTDO> VatTuTieuHaoss = new List<ThuocVatTuEmrTDO>();
                    string ServiceName = "";
                    if (sereServAlls != null && sereServAlls.Count() > 0)
                    {
                        //lấy vật tư
                        List<HIS_SERE_SERV> lstMaterial = sereServAlls.Where(o => o.MATERIAL_ID != null && o.IS_NO_EXECUTE != 1 && o.PARENT_ID == sspttt.SERE_SERV_ID).ToList();

                        List<HIS_SERE_SERV> lstMaterials = new List<HIS_SERE_SERV>();

                        if (lstMaterial != null && lstMaterial.Count() > 0)
                        {
                            List<V_HIS_MATERIAL_TYPE> lstMaterialTypes = new List<V_HIS_MATERIAL_TYPE>();

                            HisMaterialTypeViewFilterQuery typeFilter = new HisMaterialTypeViewFilterQuery();
                            typeFilter.IS_ACTIVE = Constant.IS_TRUE;
                            List<V_HIS_MATERIAL_TYPE> materialTypes = new HisMaterialTypeGet().GetView(typeFilter);

                            foreach (var item in lstMaterial)
                            {
                                var lstConsumable = materialTypes.FirstOrDefault(o => o.IS_CONSUMABLE == 1 && o.SERVICE_ID == item.SERVICE_ID);
                                if (IsNotNull(lstConsumable))
                                {
                                    lstMaterialTypes.Add(lstConsumable);
                                    lstMaterials.Add(item);
                                }
                            }

                            if (lstMaterials != null && lstMaterials.Count > 0 && lstMaterialTypes != null && lstMaterialTypes.Count > 0)
                            {
                                lstMaterials = lstMaterials.OrderBy(o => o.SERVICE_ID).ToList();

                                foreach (var item in lstMaterials)
                                {
                                    var lstMaterialType = lstMaterialTypes.FirstOrDefault(o => o.SERVICE_ID == item.SERVICE_ID);

                                    VatTuTieuHaoss.Add(new ThuocVatTuEmrTDO()
                                    {
                                        TenThuocVatTu = item.TDL_SERVICE_NAME,
                                        MaThuocVatTu = item.TDL_SERVICE_CODE,
                                        SoLuong = item.AMOUNT,
                                        DonVi = lstMaterialType != null ? lstMaterialType.SERVICE_UNIT_NAME : "",
                                        HangSanXuat = lstMaterialType != null ? lstMaterialType.MANUFACTURER_NAME : "",
                                    });
                                }
                            }
                        }

                        var lstServiceName = sereServAlls.FirstOrDefault(o => o.ID == sspttt.SERE_SERV_ID);
                        if (lstServiceName != null && lstServiceName.TDL_SERVICE_NAME != null)
                        {
                            ServiceName = lstServiceName.TDL_SERVICE_NAME;
                        }
                    }

                    long? thoigianylenh = null;
                    string maylenh = "";
                    if (serviceReq != null && serviceReq.SERVICE_REQ_CODE != null)
                    {
                        maylenh = serviceReq.SERVICE_REQ_CODE;
                        thoigianylenh = serviceReq.INTRUCTION_TIME;
                    }

                    HIS_PTTT_GROUP ptttGroup = sspttt.PTTT_GROUP_ID.HasValue ? new HisPtttGroupGet().GetById(sspttt.PTTT_GROUP_ID.Value) : null;

                    result.Add(new PhauThuatThuThuatEmrTDO()
                    {
                        PhuongPhapPhauThuatThuThuat = sspttt.PTTT_METHOD_NAME,
                        PhuongPhapVoCam = sspttt.EMOTIONLESS_METHOD_NAME,
                        NgayPhauThuatThuThuat = datePttt,
                        NgayPhauThuatThuThuat_Gio = beginTime,
                        BacSyPhauThuat = bacSyPhauThuat,
                        BacSyPhauThuatHoVaTen = bacSyPhauThuatHoVaTen,
                        BacSyGayMe = bacSyGayMe,
                        BacSyGayMeHoVaTen = bacSyGayMeHoVaTen,
                        PhuongPhapPhauThuatThuThuat2 = sspttt.EMOTIONLESS_METHOD_SECOND_NAME,
                        CachThucPhauThuatThuThuat = sspttt.MANNER,
                        MaYLenh = maylenh,
                        VatTuTieuHaos = (VatTuTieuHaoss != null && VatTuTieuHaoss.Count > 0) ? VatTuTieuHaoss : new List<ThuocVatTuEmrTDO>(),
                        ChanDoanTruocPhauThuatThuThuat = sspttt.BEFORE_PTTT_ICD_NAME,
                        ChanDoanSauPhauThuatThuThuat = sspttt.AFTER_PTTT_ICD_NAME,
                        ChanDoanChinh = sspttt.ICD_NAME,
                        TenPhauThuatThuThuat = ServiceName,
                        Loai = (ss != null) ? (int)ss.TDL_SERVICE_TYPE_ID : 0,
                        MoTa = ssext != null ? ssext.DESCRIPTION : null,
                        ThoiGianXyLy = (ssext != null) ? MinuteNumberToFinish(ssext.BEGIN_TIME, ssext.END_TIME) : "",
                        EkipThucHien = EkipThucHien,
                        MaBacSyChiDinh = serviceReq != null ? serviceReq.REQUEST_LOGINNAME : "",
                        BacSyChiDinh = serviceReq != null ? serviceReq.REQUEST_USERNAME : "",
                        IDChiTietYLenh = sspttt.SERE_SERV_ID,
                        ThoiGianThuchien = ssext.BEGIN_TIME,
                        ThoiGianKetThuc = ssext.END_TIME,
                        ThoiGianYLenh = thoigianylenh,
                        IdNhomPTTT = ptttGroup != null ? (long?)ptttGroup.ID : null,
                        MaNhomPTTT = ptttGroup != null ? ptttGroup.PTTT_GROUP_CODE : null,
                        TenNhomPTTT = ptttGroup != null ? ptttGroup.PTTT_GROUP_NAME : null
                    });
                }

                result = result.OrderBy(o => o.NgayPhauThuatThuThuat).ToList();
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private string MinuteNumberToFinish(long? Start, long? End)
        {
            string result = "";
            try
            {
                if (Start != null && End != null)
                {
                    result = Inventec.Common.DateTime.Calculation.DifferenceTime(Start ?? 0, End ?? 0, Inventec.Common.DateTime.Calculation.UnitDifferenceTime.MINUTE) + "";
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return result;
        }
    }
}
