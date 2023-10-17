using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Export
{
    class ImpMedicineProcessor : BusinessBase
    {
        private HisImpMestMedicineCreate hisImpMestMedicineCreate;
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;
        private HisMedicineBeanCreateSql hisMedicineBeanCreate;

        internal ImpMedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
            this.hisImpMestMedicineCreate = new HisImpMestMedicineCreate(param);
            this.hisMedicineBeanCreate = new HisMedicineBeanCreateSql(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HIS_EXP_MEST_MEDICINE> expMedicines, ref List<HIS_IMP_MEST_MEDICINE> impMedicines)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(expMedicines))
                {
                    List<HIS_IMP_MEST_MEDICINE> medicines = new List<HIS_IMP_MEST_MEDICINE>();
                    Dictionary<long, List<HIS_EXP_MEST_MEDICINE>> dicMedicine = new Dictionary<long, List<HIS_EXP_MEST_MEDICINE>>();
                    var Groups = expMedicines.GroupBy(g => g.MEDICINE_ID).ToList();
                    foreach (var group in Groups)
                    {
                        decimal amount = group.Sum(s => s.AMOUNT);
                        if (amount <= 0)
                            continue;

                        HIS_IMP_MEST_MEDICINE impMedicine = new HIS_IMP_MEST_MEDICINE();
                        impMedicine.MEDICINE_ID = group.Key.Value;
                        impMedicine.IMP_MEST_ID = impMest.ID;
                        impMedicine.AMOUNT = amount;
                        medicines.Add(impMedicine);

                        dicMedicine[group.Key.Value] = group.ToList();
                    }
                    if (IsNotNullOrEmpty(medicines))
                    {
                        if (!this.hisImpMestMedicineCreate.CreateList(medicines))
                        {
                            throw new Exception("Tao HIS_IMP_MEST_MEDICINE that bai");
                        }
                        List<HIS_EXP_MEST_MEDICINE> beforeUpdates = new List<HIS_EXP_MEST_MEDICINE>();
                        List<HIS_EXP_MEST_MEDICINE> listUpdate = new List<HIS_EXP_MEST_MEDICINE>();
                        Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();

                        foreach (var item in medicines)
                        {
                            List<HIS_EXP_MEST_MEDICINE> listGroup = dicMedicine[item.MEDICINE_ID];
                            List<HIS_EXP_MEST_MEDICINE> befores = Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(listGroup);
                            beforeUpdates.AddRange(befores);
                            listGroup.ForEach(o => o.CK_IMP_MEST_MEDICINE_ID = item.ID);
                            listUpdate.AddRange(listGroup);
                        }
                        if (!this.hisExpMestMedicineUpdate.UpdateList(listUpdate, beforeUpdates))
                        {
                            throw new Exception("Update CK_IMP_MEST_MEDICINE_ID cho HIS_EXP_MEST_MEDICINE that bai");
                        }

                        this.ProcessMedicineBean(impMest, medicines);

                        impMedicines = medicines;
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


        private void ProcessMedicineBean(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MEDICINE> impMedicines)
        {
            if (IsNotNullOrEmpty(impMedicines))
            {
                List<HIS_MEDICINE> hisMedicines = new HisMedicineGet().GetByIds(impMedicines.Select(s => s.MEDICINE_ID).Distinct().ToList());
                var Groups = impMedicines.GroupBy(g => g.MEDICINE_ID).ToList();
                List<HIS_MEDICINE_BEAN> toInserts = new List<HIS_MEDICINE_BEAN>();
                foreach (var group in Groups)
                {
                    List<HIS_IMP_MEST_MEDICINE> listByGroup = group.ToList();
                    HIS_MEDICINE_BEAN bean = new HIS_MEDICINE_BEAN();
                    bean.AMOUNT = listByGroup.Sum(s => s.AMOUNT);
                    bean.MEDICINE_ID = group.Key;
                    bean.MEDI_STOCK_ID = impMest.MEDI_STOCK_ID;
                    bean.IS_CK = MOS.UTILITY.Constant.IS_TRUE;

                    HisMedicineBeanUtil.SetTdl(bean, hisMedicines.FirstOrDefault(o => o.ID == group.Key));

                    toInserts.Add(bean);
                }

                if (IsNotNullOrEmpty(toInserts) && !this.hisMedicineBeanCreate.Run(toInserts))
                {
                    throw new Exception("hisMedicineBeanCreate. Ket thuc nghiep vu.");
                }
            }
        }


        internal void Rollback()
        {
            try
            {
                this.hisMedicineBeanCreate.Rollback();
                this.hisExpMestMedicineUpdate.RollbackData();
                this.hisImpMestMedicineCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
