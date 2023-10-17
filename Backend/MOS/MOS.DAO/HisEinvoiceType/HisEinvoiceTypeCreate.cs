using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEinvoiceType
{
    partial class HisEinvoiceTypeCreate : EntityBase
    {
        public HisEinvoiceTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EINVOICE_TYPE>();
        }

        private BridgeDAO<HIS_EINVOICE_TYPE> bridgeDAO;

        public bool Create(HIS_EINVOICE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EINVOICE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
