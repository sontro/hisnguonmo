using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSurgRemuneration
{
    partial class HisSurgRemunerationCreate : EntityBase
    {
        public HisSurgRemunerationCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SURG_REMUNERATION>();
        }

        private BridgeDAO<HIS_SURG_REMUNERATION> bridgeDAO;

        public bool Create(HIS_SURG_REMUNERATION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SURG_REMUNERATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
