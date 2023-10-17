using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;
using MOS.MANAGER.HisSereServ.Update.PayslipInfo;
using MOS.MANAGER.HisTreatment;

namespace MOS.MANAGER.HisSereServ
{
    partial class HisSereServUpdateNoExecute : BusinessBase
    {
        private HisSereServUpdatePayslipInfo hisSereServUpdatePayslipInfo;

        internal HisSereServUpdateNoExecute()
            : base()
        {
            this.Init();
        }

        internal HisSereServUpdateNoExecute(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServUpdatePayslipInfo = new HisSereServUpdatePayslipInfo(param);
        }

        internal bool Run(HisSereServNoExecuteSDO data, ref List<HIS_SERE_SERV> resultData)
        {
            bool result = false;
            try
            {
                List<HIS_SERE_SERV> sereServs = null;
                if (this.IsAllow(data, ref sereServs))
                {
                    short? isNoExecute = data.IsNoExecute ? (short?) Constant.IS_TRUE : null;

                    sereServs.ForEach(o => o.IS_NO_EXECUTE = isNoExecute);

                    HisSereServPayslipSDO sdo = new HisSereServPayslipSDO();
                    sdo.Field = UpdateField.IS_NO_EXECUTE;
                    sdo.SereServs = sereServs;
                    sdo.TreatmentId = data.TreatmentId;

                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(data.TreatmentId);

                    //Voi api nay (trong tinh huong luon update theo service-req, tuc la update ca don,
                    //chu ko phai la update 1 vai thuoc trong don) thi cho phep cap nhat "no-execute" voi thuoc
                    result = this.hisSereServUpdatePayslipInfo.Run(sdo, treatment, true, ref resultData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Kiem tra xem d/s y lenh ko thuc hien gui len co hop le hay ko
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool IsAllow(HisSereServNoExecuteSDO data, ref List<HIS_SERE_SERV> noExecutes)
        {
            try
            {
                if (IsNotNullOrEmpty(data.ServiceReqIds))
                {
                    List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqIds(data.ServiceReqIds);

                    List<HIS_SERE_SERV> otherTreatments = sereServs != null ? sereServs.Where(o => o.TDL_TREATMENT_ID != data.TreatmentId).ToList() : null;

                    if (IsNotNullOrEmpty(otherTreatments))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Ton tai NoExecuteServiceReqId ko thuoc ho so dieu tri dang thanh toan ");
                        return false;
                    }

                    if (new HisSereServCheck(param).IsAllowUpdateToNoExecute(sereServs))
                    {
                        noExecutes = sereServs;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }
    }
}
