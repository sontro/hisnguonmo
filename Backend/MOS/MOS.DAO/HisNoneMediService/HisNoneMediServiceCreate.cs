using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisNoneMediService
{
    partial class HisNoneMediServiceCreate : EntityBase
    {
        public HisNoneMediServiceCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NONE_MEDI_SERVICE>();
        }

        private BridgeDAO<HIS_NONE_MEDI_SERVICE> bridgeDAO;

        public bool Create(HIS_NONE_MEDI_SERVICE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_NONE_MEDI_SERVICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
