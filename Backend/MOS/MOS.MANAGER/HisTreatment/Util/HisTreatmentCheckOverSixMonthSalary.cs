using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Util
{
    class HisTreatmentCheckOverSixMonthSalary : BusinessBase
    {
        internal HisTreatmentCheckOverSixMonthSalary()
            : base()
        {

        }

        internal HisTreatmentCheckOverSixMonthSalary(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> allPatyAlters, List<HIS_SERE_SERV> allSereServs)
        {
            bool result = false;
            try
            {
                if (treatment != null
                    && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU
                    && HisTreatmentCFG.FIVE_YEAR_WARNING_OVER_SIX_MONTHS)
                {
                    List<HIS_PATIENT_TYPE_ALTER> lstPatyAlter = allPatyAlters;
                    List<HIS_SERE_SERV> lstSereServ = allSereServs;
                    bool isFiveYear = false;
                    bool isSixMonth = false;
                    if (!IsNotNullOrEmpty(lstPatyAlter))
                    {
                        lstPatyAlter = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);
                    }
                    isFiveYear = lstPatyAlter != null ? lstPatyAlter.Any(a => a.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                        && a.JOIN_5_YEAR == MOS.LibraryHein.Bhyt.HeinJoin5Year.HeinJoin5YearCode.TRUE) : false;
                    isSixMonth = lstPatyAlter != null ? lstPatyAlter.Any(a => a.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                        && a.PAID_6_MONTH == MOS.LibraryHein.Bhyt.HeinPaid6Month.HeinPaid6MonthCode.TRUE) : false;

                    if (isFiveYear && !isSixMonth)
                    {
                        decimal totalPatientPriceBhyt = 0;
                        if (IsNotNullOrEmpty(lstSereServ))
                        {
                            totalPatientPriceBhyt = lstSereServ.Where(o => o.AMOUNT > 0
                                && o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                                && o.IS_NO_EXECUTE != Constant.IS_TRUE
                                && o.IS_EXPEND != Constant.IS_TRUE
                                && o.VIR_TOTAL_PATIENT_PRICE_BHYT > 0)
                                .Sum(s => (s.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0)); ;
                        }
                        else
                        {
                            string sql = "SELECT NVL(SUM(VIR_TOTAL_PATIENT_PRICE_BHYT),0) FROM HIS_SERE_SERV WHERE TDL_TREATMENT_ID = :param1 AND PATIENT_TYPE_ID = :param2 AND (IS_NO_EXECUTE IS NULL OR IS_NO_EXECUTE <> 1) AND (IS_EXPEND IS NULL OR IS_EXPEND <> 1) AND AMOUNT > 0 AND VIR_TOTAL_PATIENT_PRICE_BHYT > 0";
                            totalPatientPriceBhyt = DAOWorker.SqlDAO.GetSqlSingle<decimal>(sql, treatment.ID, HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);
                        }
                        HIS_BHYT_PARAM bhytParam = this.GetBhytConstant(treatment);
                        if (totalPatientPriceBhyt >= (6 * bhytParam.BASE_SALARY))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_TongTienBNCCTDaVuotQua6ThangLuongCoBan);
                            LogSystem.Info("Tong tien BNCCT vuot qua 6 thang luong co ban: " + totalPatientPriceBhyt);
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
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
    }
}
