using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServFile
{
    partial class HisSereServFileCheck : EntityBase
    {
        public HisSereServFileCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_FILE>();
        }

        private BridgeDAO<HIS_SERE_SERV_FILE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
