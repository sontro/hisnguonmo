using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSuimIndexUnit
{
    partial class HisSuimIndexUnitCreate : EntityBase
    {
        public HisSuimIndexUnitCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUIM_INDEX_UNIT>();
        }

        private BridgeDAO<HIS_SUIM_INDEX_UNIT> bridgeDAO;

        public bool Create(HIS_SUIM_INDEX_UNIT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SUIM_INDEX_UNIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
