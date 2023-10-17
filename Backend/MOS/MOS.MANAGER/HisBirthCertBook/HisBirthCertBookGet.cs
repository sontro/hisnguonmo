using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBirthCertBook
{
    partial class HisBirthCertBookGet : BusinessBase
    {
        internal HisBirthCertBookGet()
            : base()
        {

        }

        internal HisBirthCertBookGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_BIRTH_CERT_BOOK> Get(HisBirthCertBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBirthCertBookDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BIRTH_CERT_BOOK GetById(long id)
        {
            try
            {
                return GetById(id, new HisBirthCertBookFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BIRTH_CERT_BOOK GetById(long id, HisBirthCertBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBirthCertBookDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
