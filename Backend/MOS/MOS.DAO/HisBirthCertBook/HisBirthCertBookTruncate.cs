using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBirthCertBook
{
    partial class HisBirthCertBookTruncate : EntityBase
    {
        public HisBirthCertBookTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BIRTH_CERT_BOOK>();
        }

        private BridgeDAO<HIS_BIRTH_CERT_BOOK> bridgeDAO;

        public bool Truncate(HIS_BIRTH_CERT_BOOK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BIRTH_CERT_BOOK> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
