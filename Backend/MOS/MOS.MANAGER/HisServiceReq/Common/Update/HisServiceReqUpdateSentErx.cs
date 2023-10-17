using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common.Update
{
    class HisServiceReqUpdateSentErx : BusinessBase
    {
         internal HisServiceReqUpdateSentErx()
            : base()
        {

        }

        internal HisServiceReqUpdateSentErx(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<HIS_SERVICE_REQ> listData, ref List<HIS_SERVICE_REQ> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_SERVICE_REQ> listRaw = new List<HIS_SERVICE_REQ>();
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && checker.VerifyIds(listData.Select(s => s.ID).ToList(), listRaw);
                
                if (valid)
                {
                    foreach (var raw in listRaw)
                    {
                        HIS_SERVICE_REQ data = listData.FirstOrDefault(o => o.ID == raw.ID);
                        raw.IS_SENT_ERX = data.IS_SENT_ERX;
                        raw.ERX_DESC = data.ERX_DESC;
                    }
                    if (!DAOWorker.HisServiceReqDAO.UpdateList(listRaw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_ThemMoiThatBai);
                        throw new Exception("update IS_SENT_ERX HisServiceReq that bai." + LogUtil.TraceData("listRaw", listRaw));
                    }
                    result = true;
                    resultData = listRaw;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
    
}
