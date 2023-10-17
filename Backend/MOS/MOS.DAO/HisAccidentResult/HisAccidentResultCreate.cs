using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentResult
{
    partial class HisAccidentResultCreate : EntityBase
    {
        public HisAccidentResultCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_RESULT>();
        }

        private BridgeDAO<HIS_ACCIDENT_RESULT> bridgeDAO;

        public bool Create(HIS_ACCIDENT_RESULT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ACCIDENT_RESULT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
