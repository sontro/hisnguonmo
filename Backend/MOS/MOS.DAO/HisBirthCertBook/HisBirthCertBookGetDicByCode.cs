using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBirthCertBook
{
    partial class HisBirthCertBookGet : EntityBase
    {
        public Dictionary<string, HIS_BIRTH_CERT_BOOK> GetDicByCode(HisBirthCertBookSO search, CommonParam param)
        {
            Dictionary<string, HIS_BIRTH_CERT_BOOK> dic = new Dictionary<string, HIS_BIRTH_CERT_BOOK>();
            try
            {
                List<HIS_BIRTH_CERT_BOOK> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.BIRTH_CERT_BOOK_CODE))
                        {
                            dic.Add(item.BIRTH_CERT_BOOK_CODE, item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param), LogType.Error);
                LogSystem.Error(ex);
                dic.Clear();
            }
            return dic;
        }
    }
}
