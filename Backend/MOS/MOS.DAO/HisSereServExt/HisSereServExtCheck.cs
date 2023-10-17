using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServExt
{
    partial class HisSereServExtCheck : EntityBase
    {
        public HisSereServExtCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_EXT>();
        }

        private BridgeDAO<HIS_SERE_SERV_EXT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
