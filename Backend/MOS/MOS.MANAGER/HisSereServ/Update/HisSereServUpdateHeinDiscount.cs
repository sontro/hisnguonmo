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
    internal partial class HisSereServUpdateHein : BusinessBase
    {
        /// <summary>
        /// Tu dong dien so tien mien giam trong truong hop "ho so dieu tri" co tick "tu dong mien giam"
        /// </summary>
        /// <param name="hisSereServs"></param>
        /// <returns></returns>
        private bool SetDiscount(List<HIS_SERE_SERV> hisSereServs, HIS_TREATMENT treatment)
        {
            bool result = true;
            try
            {
                if (treatment != null && treatment.IS_AUTO_DISCOUNT == Constant.IS_TRUE
                    && treatment.AUTO_DISCOUNT_RATIO.HasValue
                    && hisSereServs != null && hisSereServs.Count > 0)
                {
                    foreach (HIS_SERE_SERV s in hisSereServs)
                    {
                        //if (s.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                        //{
                        //    s.DISCOUNT = null;
                        //    continue;
                        //}
                        s.DISCOUNT = SereServVirtualColumn.VIR_TOTAL_PATIENT_PRICE_NO_DC(s) * treatment.AUTO_DISCOUNT_RATIO.Value;
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
