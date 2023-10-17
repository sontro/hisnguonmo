using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreHoha
{
    partial class HisHoreHohaCreate : EntityBase
    {
        public HisHoreHohaCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_HOHA>();
        }

        private BridgeDAO<HIS_HORE_HOHA> bridgeDAO;

        public bool Create(HIS_HORE_HOHA data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_HORE_HOHA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
