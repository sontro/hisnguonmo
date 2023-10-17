using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisBedLog
{
    public class HisBedLogUtil : BusinessBase
    {
        internal static decimal? CalculateBedAmount(List<HIS_SERVICE_REQ> serviceReqs, HIS_SERVICE_REQ X1, HIS_BED_LOG newBedLog, long reqDepartmentId, DateTime dtStart, DateTime dtIntruc, DateTime dtIncome)
        {
            decimal? K = null;
            try
            {
                K = HisBedLogUtil.CalculateBedAmount(serviceReqs, X1, newBedLog, reqDepartmentId, dtStart, dtIntruc, dtIncome, false);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

            return K;
        }

        internal static decimal? CalculateBedAmount(List<HIS_SERVICE_REQ> serviceReqs, HIS_SERVICE_REQ X1, HIS_BED_LOG newBedLog, long reqDepartmentId, DateTime dtStart, DateTime dtIntruc, DateTime dtIncome, bool isDeleted)
        {
            decimal? K = null;
            try
            {
                // Lay ra y lenh co thoi gian y lenh lon hon thoi gian y lenh dang can tinh va la thoi gian y lenh nho nhat
                // Neu da xoa cac y lenh co intruction lon hon ket thuc thi khong lay ra x2
                HIS_SERVICE_REQ X2 = isDeleted ? null : serviceReqs.Where(o => o.INTRUCTION_TIME > X1.INTRUCTION_TIME).OrderBy(o => o.INTRUCTION_TIME).FirstOrDefault();

                long T;
                if (X2 == null)
                {
                    T = newBedLog.FINISH_TIME.Value;
                }
                else
                {
                    T = X2.INTRUCTION_TIME;
                }

                DateTime dtT = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(T).Value;

                if (dtStart.Date == dtIncome.Date)
                {
                    K = (dtT - dtIntruc).TotalHours < 4 ? 0 : (decimal)0.5;
                }
                else if (dtStart.Date > dtIncome.Date)
                {
                    HIS_DEPARTMENT reqDepartment = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == reqDepartmentId);

                    if (reqDepartment != null && reqDepartment.IS_EMERGENCY != Constant.IS_TRUE)
                    {
                        K = (dtT - dtIntruc).TotalHours < 24 ? (decimal)0.5 : 1;
                    }
                    else
                    {
                        if ((dtT - dtIntruc).TotalHours < 4)
                            K = 0;
                        else if (4 < (dtT - dtIntruc).TotalHours && (dtT - dtIntruc).TotalHours < 24)
                            K = (decimal)0.5;
                        else
                            K = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

            return K;
        }
    }
}