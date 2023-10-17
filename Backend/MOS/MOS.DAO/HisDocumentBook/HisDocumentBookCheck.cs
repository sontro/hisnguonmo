using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDocumentBook
{
    partial class HisDocumentBookCheck : EntityBase
    {
        public HisDocumentBookCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DOCUMENT_BOOK>();
        }

        private BridgeDAO<HIS_DOCUMENT_BOOK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
