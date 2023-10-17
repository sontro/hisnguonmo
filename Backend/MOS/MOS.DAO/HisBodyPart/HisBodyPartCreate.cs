using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBodyPart
{
    partial class HisBodyPartCreate : EntityBase
    {
        public HisBodyPartCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BODY_PART>();
        }

        private BridgeDAO<HIS_BODY_PART> bridgeDAO;

        public bool Create(HIS_BODY_PART data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BODY_PART> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
