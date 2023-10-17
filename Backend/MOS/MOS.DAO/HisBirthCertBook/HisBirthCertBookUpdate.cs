using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBirthCertBook
{
    partial class HisBirthCertBookUpdate : EntityBase
    {
        public HisBirthCertBookUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BIRTH_CERT_BOOK>();
        }

        private BridgeDAO<HIS_BIRTH_CERT_BOOK> bridgeDAO;

        public bool Update(HIS_BIRTH_CERT_BOOK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BIRTH_CERT_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
