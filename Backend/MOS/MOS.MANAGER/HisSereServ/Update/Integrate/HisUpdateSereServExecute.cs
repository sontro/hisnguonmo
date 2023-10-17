using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ.Update.PayslipInfo;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.TDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.Update.Integrate
{
    class HisUpdateSereServExecute : BusinessBase
    {
        HisSereServUpdatePayslipInfo hisSereServUpdatePayslipInfo;

        internal HisUpdateSereServExecute()
            : base()
        {
            this.hisSereServUpdatePayslipInfo = new HisSereServUpdatePayslipInfo(param);
        }

        internal HisUpdateSereServExecute(CommonParam param)
            : base(param)
        {
            this.hisSereServUpdatePayslipInfo = new HisSereServUpdatePayslipInfo(param);
        }

        public bool Run(HisUpdateServiceExecuteTDO data)
        {
            bool result = false;
            try
            {
                HIS_SERVICE_REQ serviceReq = null;
                List<HIS_SERE_SERV> sereServs = null;
                if (this.IsValidData(data, ref serviceReq, ref sereServs))
                {
                    if (data.IsNoExecute)
                    {
                        sereServs.ForEach(t => t.IS_NO_EXECUTE = Constant.IS_TRUE);
                    }
                    else
                    {
                        sereServs.ForEach(t => t.IS_NO_EXECUTE = null);
                    }
                    
                    HisSereServPayslipSDO sdo = new HisSereServPayslipSDO();
                    sdo.Field = UpdateField.IS_NO_EXECUTE;
                    sdo.TreatmentId = serviceReq.TREATMENT_ID;
                    sdo.SereServs = sereServs;

                    List<HIS_SERE_SERV> resultData = null;
                    result = this.hisSereServUpdatePayslipInfo.Run(sdo, ref resultData);
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

        private bool IsValidData(HisUpdateServiceExecuteTDO data, ref HIS_SERVICE_REQ serviceReq, ref List<HIS_SERE_SERV> sereServs)
        {
            bool result = true;
            try
            {
                if (data == null || string.IsNullOrWhiteSpace(data.ServiceReqCode) || !IsNotNullOrEmpty(data.ServiceCodes))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data, ServiceReqCode hoac ServiceCodes null");
                    return false;
                }

                HIS_SERVICE_REQ sr = new HisServiceReqGet().GetByCode(data.ServiceReqCode);
                if (sr == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("serviceReqCode ko ton tai");
                    return false;
                }

                //Lay ID de truy van sere_serv --> tang hieu nang
                List<long> serviceIds = HisServiceCFG.DATA_VIEW
                    .Where(o => data.ServiceCodes.Contains(o.SERVICE_CODE))
                    .Select(o => o.ID).ToList();

                if (!IsNotNullOrEmpty(serviceIds))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("ServiceCodes ko ton tai");
                    return false;
                }

                HisSereServFilterQuery filter = new HisSereServFilterQuery();
                filter.SERVICE_IDs = serviceIds;
                filter.SERVICE_REQ_ID = sr.ID;
                List<HIS_SERE_SERV> ss = new HisSereServGet().Get(filter);

                if (!IsNotNullOrEmpty(ss))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("sereServs ko ton tai");
                    return false;
                }
                sereServs = ss;
                serviceReq = sr;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }

            return result;
        }
    }
}
