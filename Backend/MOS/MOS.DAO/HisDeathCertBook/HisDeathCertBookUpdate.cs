using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDeathCertBook
{
    partial class HisDeathCertBookUpdate : EntityBase
    {
        public HisDeathCertBookUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEATH_CERT_BOOK>();
        }

        private BridgeDAO<HIS_DEATH_CERT_BOOK> bridgeDAO;

        public bool Update(HIS_DEATH_CERT_BOOK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DEATH_CERT_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
