using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.LibraryHein.Common;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Config.CFG;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ
{
    internal partial class HisSereServUpdateHein : BusinessBase
    {
        private void SetOtherSourcePrice(List<HIS_SERE_SERV> sereServs, HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER patientTypeAlter)
        {
            if (IsNotNullOrEmpty(sereServs))
            {
                //reset truoc khi thuc hien tinh toan lai
                sereServs.ForEach(o => o.OTHER_SOURCE_PRICE = null);

                HIS_BRANCH branch = HisBranchCFG.DATA.Where(o => o.ID == treatment.BRANCH_ID).FirstOrDefault();
                HivFundPriceCalculator hivFund = new HivFundPriceCalculator();
                OtherSourcePriceCalculator otherSource = new OtherSourcePriceCalculator();
                PoorFundPriceCalculator poorFund = new PoorFundPriceCalculator(branch.HEIN_PROVINCE_CODE);

                hivFund.Run(sereServs);
                poorFund.Run(sereServs, patientTypeAlter.HNCODE, patientTypeAlter.HEIN_CARD_NUMBER);
                otherSource.Run(sereServs);
            }
        }
    }
}
