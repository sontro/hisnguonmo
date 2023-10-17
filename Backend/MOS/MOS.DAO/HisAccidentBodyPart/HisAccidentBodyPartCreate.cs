using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentBodyPart
{
    partial class HisAccidentBodyPartCreate : EntityBase
    {
        public HisAccidentBodyPartCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_BODY_PART>();
        }

        private BridgeDAO<HIS_ACCIDENT_BODY_PART> bridgeDAO;

        public bool Create(HIS_ACCIDENT_BODY_PART data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ACCIDENT_BODY_PART> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
