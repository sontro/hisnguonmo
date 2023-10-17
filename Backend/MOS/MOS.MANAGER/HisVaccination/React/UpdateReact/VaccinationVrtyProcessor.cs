using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisVaccinationVrty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.React
{
    class VaccinationVrtyProcessor : BusinessBase
    {
        HisVaccinationVrtyCreate hisVaccinationVrtyCreate;

        internal VaccinationVrtyProcessor()
            : base()
        {
            this.Init();
        }

        internal VaccinationVrtyProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisVaccinationVrtyCreate = new HisVaccinationVrtyCreate(param);
        }

        internal bool Run(List<HIS_VACCINATION_VRTY> hisVaccinationVrtys, HIS_VACCINATION raw, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_VACCINATION_VRTY> olds = new HisVaccinationVrtyGet().GetByVaccinationId(raw.ID);
                if (IsNotNullOrEmpty(hisVaccinationVrtys) || IsNotNullOrEmpty(olds))
                {
                    List<HIS_VACCINATION_VRTY> createList = new List<HIS_VACCINATION_VRTY>();
                    List<HIS_VACCINATION_VRTY> updateList = new List<HIS_VACCINATION_VRTY>();
                    List<HIS_VACCINATION_VRTY> deleteList = new List<HIS_VACCINATION_VRTY>();
                    if (IsNotNullOrEmpty(hisVaccinationVrtys))
                    {
                        foreach (HIS_VACCINATION_VRTY item in hisVaccinationVrtys)
                        {
                            HIS_VACCINATION_VRTY old = olds != null ? olds.FirstOrDefault(o => o.VACC_REACT_TYPE_ID == item.VACC_REACT_TYPE_ID) : null;
                            if (old != null)
                            {
                                updateList.Add(old);
                            }
                            else
                            {
                                item.VACCINATION_ID = raw.ID;
                                createList.Add(item);
                            }
                        }
                    }
                    deleteList = olds != null ? olds.Where(o => !updateList.Exists(e => e.ID == o.ID)).ToList() : null;

                    if (IsNotNullOrEmpty(createList))
                    {
                        if (!hisVaccinationVrtyCreate.CreateList(createList))
                        {
                            throw new Exception("hisVaccinationVrtyCreate");
                        }
                    }

                    if (IsNotNullOrEmpty(deleteList))
                    {
                        string sql = DAOWorker.SqlDAO.AddInClause(deleteList.Select(s => s.ID).ToList(), "DELETE HIS_VACCINATION_VRTY WHERE %IN_CLAUSE% ", "ID");
                        sqls.Add(sql);
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisVaccinationVrtyCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
