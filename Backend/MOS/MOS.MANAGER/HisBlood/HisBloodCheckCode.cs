using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBlood
{
    partial class HisBloodCheck : BusinessBase
    {
        /// <summary>
        /// Kiem tra ma da ton tai hay chua, id duoc su dung trong truong hop muon bo qua chinh ma cua minh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool ExistsCode(string code, long? id)
        {
            bool valid = true;
            try
            {
                if (DAOWorker.HisBloodDAO.ExistsCode(code, id))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.MaDaTonTaiTrenHeThong, code);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool ExistsCode(List<HIS_BLOOD> listData)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(listData))
                {
                    HisBloodFilterQuery filter = new HisBloodFilterQuery();
                    filter.BLOOD_CODEs = listData.Select(o => o.BLOOD_CODE).ToList();
                    List<HIS_BLOOD> exists = new HisBloodGet().Get(filter);
                    List<string> invalids = IsNotNullOrEmpty(exists) ? exists
                        .Where(o => listData.Exists(t => t.BLOOD_CODE == o.BLOOD_CODE && o.ID != t.ID))
                        .Select(o => o.BLOOD_CODE).ToList() : null;
                    if (IsNotNullOrEmpty(invalids))
                    {
                        string code = string.Join(",", invalids);
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.MaDaTonTaiTrenHeThong, code);
                        valid = false;
                    }
                }
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
