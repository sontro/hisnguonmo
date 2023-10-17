using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBornResult
{
    partial class HisBornResultCreate : EntityBase
    {
        public HisBornResultCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BORN_RESULT>();
        }

        private BridgeDAO<HIS_BORN_RESULT> bridgeDAO;

        public bool Create(HIS_BORN_RESULT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BORN_RESULT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
