using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update.PayslipInfo;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.ApproveNotTaken
{
    class SereServProcessor : BusinessBase
    {
        private HisSereServUpdatePayslipInfo hisSereServUpdatePayslipInfo;

        internal SereServProcessor()
            : base()
        {
            this.Init();
        }

        internal SereServProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServUpdatePayslipInfo = new HisSereServUpdatePayslipInfo(param);
        }

        internal bool Run(HIS_EXP_MEST expMest)
        {
            bool result = false;
            try
            {
                if (!expMest.TDL_TREATMENT_ID.HasValue || !expMest.SERVICE_REQ_ID.HasValue)
                {
                    return true;
                }

                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(expMest.TDL_TREATMENT_ID.Value);

                if (treatment == null)
                {
                    return false;
                }

                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                if (!treatChecker.IsUnLock(treatment)
                    || !treatChecker.IsUnTemporaryLock(treatment)
                    || !treatChecker.IsUnLockHein(treatment))
                {
                    if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(expMest.SERVICE_REQ_ID.Value);
                if (!IsNotNullOrEmpty(sereServs))
                {
                    return true;
                }
                else
                {
                    sereServs.ForEach(o => o.IS_NO_EXECUTE = Constant.IS_TRUE);

                    HisSereServPayslipSDO sdo = new HisSereServPayslipSDO();
                    sdo.Field = UpdateField.IS_NO_EXECUTE;
                    sdo.SereServs = sereServs;
                    sdo.TreatmentId = treatment.ID;
                    List<HIS_SERE_SERV> resultData = null;
                    //Voi api nay (trong tinh huong luon update theo service-req, tuc la update ca don,
                    //chu ko phai la update 1 vai thuoc trong don) thi cho phep cap nhat "no-execute" voi thuoc
                    result = this.hisSereServUpdatePayslipInfo.Run(sdo, treatment, true, ref resultData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool Run(List<HIS_EXP_MEST> expMest)
        {
            bool result = false;
            try
            {
                List<long> treatmentIds = expMest.Where(o => o.TDL_TREATMENT_ID.HasValue).Select(s => s.TDL_TREATMENT_ID.Value).Distinct().ToList();
                if (!IsNotNullOrEmpty(treatmentIds) || treatmentIds.Count != 1)
                {
                    return false;
                }

                List<long> serviceReqIds = expMest.Where(o => o.SERVICE_REQ_ID.HasValue).Select(s => s.SERVICE_REQ_ID.Value).Distinct().ToList();
                if (!IsNotNullOrEmpty(serviceReqIds))
                {
                    return true;
                }

                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(treatmentIds.First());

                if (treatment == null)
                {
                    return false;
                }

                //Neu ho so da khoa thi bo qua, ko xu ly bang ke
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                if (!treatChecker.IsUnLock(treatment)
                    || !treatChecker.IsUnTemporaryLock(treatment)
                    || !treatChecker.IsUnLockHein(treatment))
                {
                    if (expMest.Exists(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqIds(serviceReqIds);
                if (!IsNotNullOrEmpty(sereServs))
                {
                    return true;
                }
                else
                {
                    sereServs.ForEach(o => o.IS_NO_EXECUTE = Constant.IS_TRUE);

                    HisSereServPayslipSDO sdo = new HisSereServPayslipSDO();
                    sdo.Field = UpdateField.IS_NO_EXECUTE;
                    sdo.SereServs = sereServs;
                    sdo.TreatmentId = treatment.ID;
                    List<HIS_SERE_SERV> resultData = null;
                    //Voi api nay (trong tinh huong luon update theo service-req, tuc la update ca don,
                    //chu ko phai la update 1 vai thuoc trong don) thi cho phep cap nhat "no-execute" voi thuoc
                    result = this.hisSereServUpdatePayslipInfo.Run(sdo, treatment, true, ref resultData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
