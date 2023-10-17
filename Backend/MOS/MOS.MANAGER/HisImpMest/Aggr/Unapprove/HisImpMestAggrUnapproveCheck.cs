using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr.Unapprove
{
    class HisImpMestAggrUnapproveCheck : BusinessBase
    {
        internal HisImpMestAggrUnapproveCheck()
            : base()
        {
        }

        internal HisImpMestAggrUnapproveCheck(CommonParam param)
            : base(param)
        {
        }

        /// <summary>
        /// Kiem tra xem du lieu truyen vao co hop le hay ko
        /// </summary>
        /// <param name="data"></param>
        /// <param name="aggrImpMest"></param>
        /// <param name="rejectedItemImpMests">Cac phieu nhap chua thuoc/vat tu bi tu choi khi duyet</param>
        /// <returns></returns>
        internal bool IsValidData(long aggrImpMestId, ref List<HIS_IMP_MEST> rejectedItemImpMests)
        {
            bool valid = true;
            try
            {
                List<HIS_IMP_MEST> impMests = new HisImpMestGet().GetByApprovalImpMestId(aggrImpMestId);

                List<string> notRequestImpMestCodes = IsNotNullOrEmpty(impMests) ? impMests
                    .Where(o => o.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL
                    || o.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT)
                    .Select(o => o.IMP_MEST_CODE).ToList() : null;

                if (IsNotNullOrEmpty(notRequestImpMestCodes))
                {
                    string codeStr = string.Join(",", notRequestImpMestCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_CacPhieuHoanDaDuocDuyetHoacThucNhap, codeStr);
                    return false;
                }

                rejectedItemImpMests = impMests;

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
