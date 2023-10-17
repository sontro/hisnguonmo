using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisEmrForm
{
    class HisEmrFormChangeActive : BusinessBase
    {
        internal HisEmrFormChangeActive()
            : base()
        {

        }

        internal HisEmrFormChangeActive(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<HIS_EMR_FORM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmrFormCheck checker = new HisEmrFormCheck(param);
                List<HIS_EMR_FORM> listRaw = new List<HIS_EMR_FORM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                if (valid)
                {
                    List<HIS_EMR_FORM> updates = new List<HIS_EMR_FORM>();
                    List<HIS_EMR_FORM> befores = new List<HIS_EMR_FORM>();
                    Mapper.CreateMap<HIS_EMR_FORM, HIS_EMR_FORM>();
                    foreach (HIS_EMR_FORM data in listData)
                    {
                        data.IS_ACTIVE = (data.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE) ? IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE : IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                        HIS_EMR_FORM raw = listRaw.FirstOrDefault(o => o.ID == data.ID);
                        HIS_EMR_FORM be = Mapper.Map<HIS_EMR_FORM>(raw);
                        raw.IS_ACTIVE = data.IS_ACTIVE;
                        if (raw.IS_ACTIVE != be.IS_ACTIVE)
                        {
                            updates.Add(raw);
                            befores.Add(be);
                        }
                    }

                    if (IsNotNullOrEmpty(updates) && !DAOWorker.HisEmrFormDAO.UpdateList(updates))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisEmrForm_CapNhatThatBai);
                        throw new Exception("Update IS_ACTIVE HIS_EMR_FORM that bai");
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
