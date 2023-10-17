using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAcinInteractive
{
    class HisAcinInteractiveTruncate : BusinessBase
    {
        internal HisAcinInteractiveTruncate()
            : base()
        {

        }

        internal HisAcinInteractiveTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool TruncateList(List<long> ids)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(ids);
                HisAcinInteractiveCheck checker = new HisAcinInteractiveCheck(param);
                List<HIS_ACIN_INTERACTIVE> listRaw = new List<HIS_ACIN_INTERACTIVE>();
                valid = valid && checker.VerifyIds(ids, listRaw);
                if (valid)
                {
                    List<HIS_ACIN_INTERACTIVE> listRawPlus = new List<HIS_ACIN_INTERACTIVE>();
                    listRawPlus.AddRange(listRaw);
                    foreach (HIS_ACIN_INTERACTIVE raw in listRaw)
                    {
                        HIS_ACIN_INTERACTIVE exists = DAOWorker.SqlDAO.GetSqlSingle<HIS_ACIN_INTERACTIVE>("SELECT * FROM HIS_ACIN_INTERACTIVE WHERE ACTIVE_INGREDIENT_ID = :param1 AND CONFLICT_ID = :param2", raw.CONFLICT_ID, raw.ACTIVE_INGREDIENT_ID);
                        if (IsNotNull(exists) && !listRawPlus.Exists(e => e.ID == exists.ID))
                        {
                            listRawPlus.Add(exists);
                        }
                    }
                    result = this.TruncateList(listRawPlus);
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

        internal bool Truncate(HIS_ACIN_INTERACTIVE data)
        {
            bool result = false;
            try
            {
                result = this.TruncateList(new List<long>() { data.ID });
            }
            catch (Exception ex)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDangDuocSuDungKhongChoPhepXoa);
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool TruncateList(List<HIS_ACIN_INTERACTIVE> listRaw)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listRaw);
                HisAcinInteractiveCheck checker = new HisAcinInteractiveCheck(param);
                valid = valid && checker.IsUnLock(listRaw);

                if (valid)
                {
                    result = DAOWorker.HisAcinInteractiveDAO.TruncateList(listRaw);
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
