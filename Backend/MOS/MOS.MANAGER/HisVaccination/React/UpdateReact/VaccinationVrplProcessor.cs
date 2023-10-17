using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisVaccinationVrpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.React
{
    class VaccinationVrplProcessor : BusinessBase
    {
        HisVaccinationVrplCreate hisVaccinationVrplCreate;

        internal VaccinationVrplProcessor()
            : base()
        {
            this.Init();
        }

        internal VaccinationVrplProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisVaccinationVrplCreate = new HisVaccinationVrplCreate(param);
        }

        internal bool Run(List<HIS_VACCINATION_VRPL> hisVaccinationVrpls, HIS_VACCINATION raw, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_VACCINATION_VRPL> olds = new HisVaccinationVrplGet().GetByVaccinationId(raw.ID);
                if (IsNotNullOrEmpty(hisVaccinationVrpls) || IsNotNullOrEmpty(olds))
                {
                    List<HIS_VACCINATION_VRPL> createList = new List<HIS_VACCINATION_VRPL>();
                    List<HIS_VACCINATION_VRPL> updateList = new List<HIS_VACCINATION_VRPL>();
                    List<HIS_VACCINATION_VRPL> deleteList = new List<HIS_VACCINATION_VRPL>();
                    if (IsNotNullOrEmpty(hisVaccinationVrpls))
                    {
                        foreach (HIS_VACCINATION_VRPL item in hisVaccinationVrpls)
                        {
                            HIS_VACCINATION_VRPL old = olds != null ? olds.FirstOrDefault(o => o.VACC_REACT_PLACE_ID == item.VACC_REACT_PLACE_ID) : null;
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
                        if (!hisVaccinationVrplCreate.CreateList(createList))
                        {
                            throw new Exception("hisVaccinationVrplCreate");
                        }
                    }

                    if (IsNotNullOrEmpty(deleteList))
                    {
                        string sql = DAOWorker.SqlDAO.AddInClause(deleteList.Select(s => s.ID).ToList(), "DELETE HIS_VACCINATION_VRPL WHERE %IN_CLAUSE% ", "ID");
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
                this.hisVaccinationVrplCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
