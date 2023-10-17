using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediReactSum
{
    partial class HisMediReactSumCreate : EntityBase
    {
        public HisMediReactSumCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_REACT_SUM>();
        }

        private BridgeDAO<HIS_MEDI_REACT_SUM> bridgeDAO;

        public bool Create(HIS_MEDI_REACT_SUM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_REACT_SUM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
