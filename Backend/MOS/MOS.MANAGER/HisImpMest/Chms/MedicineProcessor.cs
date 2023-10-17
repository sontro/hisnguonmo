using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMestMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Chms
{
    class MedicineProcessor : BusinessBase
    {
        HisImpMestMedicineCreate hisImpMestMedicineCreate;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestMedicineCreate = new HisImpMestMedicineCreate(param);
        }


        internal bool Run(HIS_IMP_MEST impMest, List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines, ref List<HIS_IMP_MEST_MEDICINE> hisImpMestMedicines, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(hisExpMestMedicines))
                {
                    List<HIS_IMP_MEST_MEDICINE> impMestMedicines = new List<HIS_IMP_MEST_MEDICINE>();
                    Dictionary<long, List<HIS_EXP_MEST_MEDICINE>> dicExpMestMedicine = new Dictionary<long, List<HIS_EXP_MEST_MEDICINE>>();
                    var Groups = hisExpMestMedicines.GroupBy(g => g.MEDICINE_ID).ToList();
                    foreach (var group in Groups)
                    {
                        decimal amount = group.Sum(s => s.AMOUNT);
                        if (amount <= 0)
                            continue;
                        HIS_IMP_MEST_MEDICINE impMedicine = new HIS_IMP_MEST_MEDICINE();
                        impMedicine.MEDICINE_ID = group.Key.Value;
                        impMedicine.IMP_MEST_ID = impMest.ID;
                        impMedicine.AMOUNT = amount;
                        impMestMedicines.Add(impMedicine);
                        dicExpMestMedicine[group.Key.Value] = group.ToList();
                    }
                    if (IsNotNullOrEmpty(impMestMedicines))
                    {
                        if (!this.hisImpMestMedicineCreate.CreateList(impMestMedicines))
                        {
                            throw new Exception("Tao HIS_EXP_MEST_MEDICINE that bai");
                        }


                        foreach (var item in impMestMedicines)
                        {
                            List<HIS_EXP_MEST_MEDICINE> listGroup = dicExpMestMedicine[item.MEDICINE_ID];
                            listGroup.ForEach(o => o.CK_IMP_MEST_MEDICINE_ID = item.ID);

                            string sql = DAOWorker.SqlDAO.AddInClause(listGroup.Select(s => s.ID).ToList(), String.Format("UPDATE HIS_EXP_MEST_MEDICINE SET CK_IMP_MEST_MEDICINE_ID = {0} WHERE %IN_CLAUSE%", item.ID), "ID");
                            sqls.Add(sql);
                        }
                        hisImpMestMedicines = impMestMedicines;
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

        internal void Rollback()
        {
            try
            {
                this.hisImpMestMedicineCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
