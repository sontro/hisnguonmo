using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisContraindication
{
    partial class HisContraindicationCreate : EntityBase
    {
        public HisContraindicationCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONTRAINDICATION>();
        }

        private BridgeDAO<HIS_CONTRAINDICATION> bridgeDAO;

        public bool Create(HIS_CONTRAINDICATION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CONTRAINDICATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
