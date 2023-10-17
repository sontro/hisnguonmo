using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisKskOccupational
{
    partial class HisKskOccupationalCreate : EntityBase
    {
        public HisKskOccupationalCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_OCCUPATIONAL>();
        }

        private BridgeDAO<HIS_KSK_OCCUPATIONAL> bridgeDAO;

        public bool Create(HIS_KSK_OCCUPATIONAL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_KSK_OCCUPATIONAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
