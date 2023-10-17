using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAcinInteractive
{
    class HisAcinInteractiveCreate : BusinessBase
    {
        private List<HIS_ACIN_INTERACTIVE> hisRecentAcinInteractives = new List<HIS_ACIN_INTERACTIVE>();

        internal HisAcinInteractiveCreate()
            : base()
        {

        }

        internal HisAcinInteractiveCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ACIN_INTERACTIVE data)
        {
            bool result = false;
            try
            {
                List<HIS_ACIN_INTERACTIVE> listData = new List<HIS_ACIN_INTERACTIVE>() { data };
                result = this.CreateList(listData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool CreateList(List<HIS_ACIN_INTERACTIVE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAcinInteractiveCheck checker = new HisAcinInteractiveCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    List<HIS_ACIN_INTERACTIVE> listCreate = new List<HIS_ACIN_INTERACTIVE>();
                    List<HIS_ACIN_INTERACTIVE> listUpdate = new List<HIS_ACIN_INTERACTIVE>();

                    foreach (HIS_ACIN_INTERACTIVE data in listData)
                    {
                        HIS_ACIN_INTERACTIVE exists = DAOWorker.SqlDAO.GetSqlSingle<HIS_ACIN_INTERACTIVE>("SELECT * FROM HIS_ACIN_INTERACTIVE WHERE ACTIVE_INGREDIENT_ID = :param1 AND CONFLICT_ID = :param2", data.ACTIVE_INGREDIENT_ID, data.CONFLICT_ID);
                        if (IsNotNull(exists))
                        {
                            exists.INTERACTIVE_GRADE_ID = data.INTERACTIVE_GRADE_ID;
                            exists.CONSEQUENCE = data.CONSEQUENCE;
                            exists.MECHANISM = data.MECHANISM;

                            exists.DESCRIPTION = data.DESCRIPTION;
                            exists.INSTRUCTION = data.INSTRUCTION;
                            data.ID = exists.ID;
                            listUpdate.Add(exists);
                        }
                        else
                        {
                            listCreate.Add(data);
                        }
                    }
                    if (IsNotNullOrEmpty(listCreate) && !DAOWorker.HisAcinInteractiveDAO.CreateList(listCreate))
                    {
                        throw new Exception("Tao HIS_ACIN_INTERACTIVE that bai");
                    }

                    this.hisRecentAcinInteractives.AddRange(listCreate);

                    if (IsNotNullOrEmpty(listUpdate) && !DAOWorker.HisAcinInteractiveDAO.UpdateList(listUpdate))
                    {
                        throw new Exception("Update HIS_ACIN_INTERACTIVE that bai. Rollback du lieu");
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }

            return result;
        }

        private void RollbackData()
        {
            try
            {
                if (IsNotNullOrEmpty(this.hisRecentAcinInteractives))
                {
                    if (!DAOWorker.HisAcinInteractiveDAO.TruncateList(this.hisRecentAcinInteractives))
                    {
                        LogSystem.Warn("Rollback HIS_ACIN_INTERACTIVE that bai. kiem tra lai du lieu");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
