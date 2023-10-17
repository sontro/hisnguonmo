using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDeathCertBook
{
    partial class HisDeathCertBookCreate : EntityBase
    {
        public HisDeathCertBookCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEATH_CERT_BOOK>();
        }

        private BridgeDAO<HIS_DEATH_CERT_BOOK> bridgeDAO;

        public bool Create(HIS_DEATH_CERT_BOOK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEATH_CERT_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
