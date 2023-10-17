using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisEmrCoverType
{
    class HisEmrCoverTypeChangeActive : BusinessBase
    {
        internal HisEmrCoverTypeChangeActive()
            : base()
        {

        }

        internal HisEmrCoverTypeChangeActive(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<HisEmrCoverTypeSDO> lstData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_EMR_COVER_TYPE> listRaw = new List<HIS_EMR_COVER_TYPE>();
                HisEmrCoverTypeCheck checker = new HisEmrCoverTypeCheck(param);
                valid = valid && IsNotNullOrEmpty(lstData);
                valid = valid && checker.VerifyIds(lstData.Select(s => s.EmrCoverTypeId).ToList(), listRaw);
                if (valid)
                {
                    foreach (HisEmrCoverTypeSDO sdo in lstData)
                    {
                        HIS_EMR_COVER_TYPE raw = listRaw.FirstOrDefault(o => o.ID == sdo.EmrCoverTypeId);
                        raw.IS_ACTIVE = sdo.IsActive ? Constant.IS_TRUE : Constant.IS_FALSE;
                    }

                    if (!DAOWorker.HisEmrCoverTypeDAO.UpdateList(listRaw))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisEmrCoverType_CapNhatThatBai);
                        throw new Exception("Update IsActive HIS_EMR_COVER_TYPE That bai");
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
