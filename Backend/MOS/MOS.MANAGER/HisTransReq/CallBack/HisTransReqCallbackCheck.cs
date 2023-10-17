using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSeseTransReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.CallBack
{
    public class HisTransReqCallbackCheck : BusinessBase
    {
        internal HisTransReqCallbackCheck()
            : base()
        {

        }

        internal HisTransReqCallbackCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool VerifyRequireField(HisTransReqCallbackSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (String.IsNullOrWhiteSpace(data.TransReqCode)) throw new ArgumentNullException("data.TransReqCode)");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }


        internal bool VerifyTransReqCode(HisTransReqCallbackSDO data, ref List<HIS_TRANS_REQ> transReqs, ref List<HIS_SESE_TRANS_REQ> seseTransReqs, ref List<HIS_SERE_SERV> listSereServ)
        {
            bool valid = true;
            try
            {
                if (!String.IsNullOrWhiteSpace(data.TransReqCode))
                {
                    HisTransReqFilterQuery filter = new HisTransReqFilterQuery();
                    filter.TIG_TRANSACTION_CODE__EXACT = data.TransReqCode;

                    transReqs = new HisTransReqGet().Get(filter);
                    if (!IsNotNullOrEmpty(transReqs))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("TRANS_REQ_CODE invalid: \n" + LogUtil.TraceData("Data", data));
                    }

                    seseTransReqs = new HisSeseTransReqGet().GetByTransReqIds(transReqs.Select(s => s.ID).ToList());

                    if (IsNotNullOrEmpty(seseTransReqs))
                    {
                        listSereServ = new HisSereServGet().GetByIds(seseTransReqs.Select(s => s.SERE_SERV_ID).Distinct().ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsSttRequest(List<HIS_TRANS_REQ> transReqs)
        {
            bool valid = true;
            try
            {
                if (transReqs.Any(a => a.TRANS_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__REQUEST))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransReq_TrangThaiCuaYeuCauKhongHopLe);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
