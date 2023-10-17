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
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ
{
    internal partial class HisSereServSetFundAccepted : BusinessBase
    {
        public HisSereServSetFundAccepted()
            : base()
        {
        }

        public HisSereServSetFundAccepted(CommonParam param)
            : base(param)
        {
        }

        /// <summary>
        /// Tu dong set "is_fund_accepted" cho cac dich vu chi dinh moi trong truong hop
        /// tong so tien BN phai tra chua vuot qua han muc quy chi tra
        /// </summary>
        /// <param name="toUpdates"></param>
        /// <param name="existsSereServs"></param>
        /// <param name="treatment"></param>
        /// <returns></returns>
        public void Run(List<HIS_SERE_SERV> toUpdates, List<HIS_SERE_SERV> existsSereServs, HIS_TREATMENT treatment)
        {
            try
            {
                if (treatment != null && treatment.FUND_ID.HasValue && treatment.FUND_BUDGET.HasValue)
                {
                    //Tinh tong chi phi ma quy da chap nhan chi tra
                    decimal total = existsSereServs != null ? existsSereServs
                        .Where(o => o.IS_FUND_ACCEPTED == Constant.IS_TRUE)
                        .Sum(o => o.VIR_TOTAL_PATIENT_PRICE.Value) : 0;

                    if (total > treatment.FUND_BUDGET.Value)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_TongChiPhiQuyChapNhanChiTraVuotQuaHanMuc, total.ToString(), treatment.FUND_BUDGET.Value.ToString());
                        return;
                    }
                    
                    //Neu tong nay chua vuot qua han muc thi thuc hien tu dong gan "quy dong y chi tra"
                    if (total < treatment.FUND_BUDGET.Value && toUpdates != null && toUpdates.Count > 0)
                    {
                        foreach (HIS_SERE_SERV s in toUpdates)
                        {
                            decimal tmp = total + SereServVirtualColumn.VIR_TOTAL_PATIENT_PRICE(s);
                            if (tmp <= treatment.FUND_BUDGET.Value)
                            {
                                s.IS_FUND_ACCEPTED =  Constant.IS_TRUE;
                                total = tmp;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
