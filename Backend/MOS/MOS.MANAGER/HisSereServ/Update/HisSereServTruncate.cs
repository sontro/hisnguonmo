using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServTein;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisSereServFile;
using MOS.MANAGER.HisSereServSuin;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisSereServReha;
using Inventec.Core;
using MOS.MANAGER.HisSereServBill;

namespace MOS.MANAGER.HisSereServ
{
    class HisSereServTruncate : BusinessBase
    {
        internal HisSereServTruncate()
            : base()
        {

        }

        internal HisSereServTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool TruncateList(List<HIS_SERE_SERV> listRaw)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listRaw);
                HisSereServCheck checker = new HisSereServCheck(param);

                List<long> ids = listRaw.Select(o => o.ID).ToList();
                List<HIS_SERE_SERV> children = new HisSereServGet().GetByParentIds(ids);

                List<HIS_SERE_SERV> all = new List<HIS_SERE_SERV>();
                all.AddRange(listRaw);

                if (IsNotNullOrEmpty(children))
                {
                    all.AddRange(children);
                }

                List<long> sereServIds = listRaw.Select(o => o.ID).ToList();

                valid = valid && checker.IsUnLock(all);
                valid = valid && checker.HasNoBill(listRaw);//Chi cho phep xoa doi voi cac sere_serv chua co bill
                valid = valid && checker.HasNoInvoice(all);//Chi cho phep xoa doi voi cac sere_serv chua co invoice

                if (valid)
                {
                    //Xoa cac bang luu du lieu chi tiet cua sere_serv
                    new HisSereServTeinTruncate(param).TruncateBySereServIds(sereServIds);
                    new HisSereServFileTruncate(param).TruncateBySereServIds(sereServIds);
                    new HisSereServSuinTruncate(param).TruncateBySereServIds(sereServIds);
                    new HisSereServPtttTruncate(param).TruncateBySereServIds(sereServIds);
                    new HisSereServRehaTruncate(param).TruncateBySereServIds(sereServIds);

                    //Xoa cac bang sere_serv "con" (child) truoc
                    DAOWorker.HisSereServDAO.TruncateList(children);

                    //Xoa du lieu sere_serv
                    result = DAOWorker.HisSereServDAO.TruncateList(listRaw);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
