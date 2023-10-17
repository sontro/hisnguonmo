using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.Fund
{
    class HisTreatmentUpdateFundPayTime : BusinessBase
    {
        internal HisTreatmentUpdateFundPayTime()
            : base()
        {

        }

        internal HisTreatmentUpdateFundPayTime(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<HIS_TREATMENT> listData, ref List<HIS_TREATMENT> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_TREATMENT> listRaw = new List<HIS_TREATMENT>();
                HisTreatmentFundPayTimeCheck checker = new HisTreatmentFundPayTimeCheck(param);
                HisTreatmentCheck commonChecker = new HisTreatmentCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<long> listId = listData.Select(s => s.ID).ToList();
                valid = valid && commonChecker.VerifyIds(listId, listRaw);
                valid = valid && checker.HasFund(listRaw);
                valid = valid && checker.HasNoFundPayTime(listRaw);
                valid = valid && this.ValiData(listData);
                if (valid)
                {
                    foreach (HIS_TREATMENT data in listData)
                    {
                        HIS_TREATMENT raw = listRaw.FirstOrDefault(o => o.ID == data.ID);
                        raw.FUND_PAY_TIME = data.FUND_PAY_TIME;
                    }
                    if (!DAOWorker.HisTreatmentDAO.UpdateList(listRaw))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Update FUND_PAY_TIME cho HIS_TREATMENT that bai");
                    }
                    result = true;
                    resultData = listRaw;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private bool ValiData(List<HIS_TREATMENT> listData)
        {
            bool valid = true;
            try
            {
                if (listData.Exists(e => !e.FUND_PAY_TIME.HasValue))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    throw new Exception("Ton tai FUND_PAY_TIME null");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
