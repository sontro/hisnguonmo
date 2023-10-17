using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Export
{
    class ImpMaterialProcessor : BusinessBase
    {
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;
        private HisMaterialBeanCreateSql hisMaterialBeanCreate;

        internal ImpMaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
            this.hisMaterialBeanCreate = new HisMaterialBeanCreateSql(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HIS_EXP_MEST_MATERIAL> expMaterials, ref List<HIS_IMP_MEST_MATERIAL> impMaterials)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(expMaterials))
                {
                    List<HIS_IMP_MEST_MATERIAL> materials = new List<HIS_IMP_MEST_MATERIAL>();
                    Dictionary<long, List<HIS_EXP_MEST_MATERIAL>> dicMaterial = new Dictionary<long, List<HIS_EXP_MEST_MATERIAL>>();
                    var Groups = expMaterials.GroupBy(g => g.MATERIAL_ID).ToList();
                    foreach (var group in Groups)
                    {
                        decimal amount = group.Sum(s => s.AMOUNT);
                        if (amount <= 0)
                            continue;

                        HIS_IMP_MEST_MATERIAL impMaterial = new HIS_IMP_MEST_MATERIAL();
                        impMaterial.MATERIAL_ID = group.Key.Value;
                        impMaterial.IMP_MEST_ID = impMest.ID;
                        impMaterial.AMOUNT = amount;
                        materials.Add(impMaterial);

                        dicMaterial[group.Key.Value] = group.ToList();
                    }
                    if (IsNotNullOrEmpty(materials))
                    {
                        if (!this.hisImpMestMaterialCreate.CreateList(materials))
                        {
                            throw new Exception("Tao HIS_IMP_MEST_MATERIAL that bai");
                        }
                        List<HIS_EXP_MEST_MATERIAL> beforeUpdates = new List<HIS_EXP_MEST_MATERIAL>();
                        List<HIS_EXP_MEST_MATERIAL> listUpdate = new List<HIS_EXP_MEST_MATERIAL>();
                        Mapper.CreateMap<HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();

                        foreach (var item in materials)
                        {
                            List<HIS_EXP_MEST_MATERIAL> listGroup = dicMaterial[item.MATERIAL_ID];
                            List<HIS_EXP_MEST_MATERIAL> befores = Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(listGroup);
                            beforeUpdates.AddRange(befores);
                            listGroup.ForEach(o => o.CK_IMP_MEST_MATERIAL_ID = item.ID);
                            listUpdate.AddRange(listGroup);
                        }
                        if (!this.hisExpMestMaterialUpdate.UpdateList(listUpdate, beforeUpdates))
                        {
                            throw new Exception("Update CK_IMP_MEST_MATERIAL_ID cho HIS_EXP_MEST_MATERIAL that bai");
                        }

                        this.ProcessMaterialBean(impMest, materials);

                        impMaterials = materials;
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

        private void ProcessMaterialBean(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MATERIAL> impMaterials)
        {
            if (IsNotNullOrEmpty(impMaterials))
            {
                List<HIS_MATERIAL> hisMedicines = new HisMaterialGet().GetByIds(impMaterials.Select(s => s.MATERIAL_ID).Distinct().ToList());
                var Groups = impMaterials.GroupBy(g => g.MATERIAL_ID).ToList();
                List<HIS_MATERIAL_BEAN> toInserts = new List<HIS_MATERIAL_BEAN>();
                foreach (var group in Groups)
                {
                    List<HIS_IMP_MEST_MATERIAL> listByGroup = group.ToList();
                    HIS_MATERIAL_BEAN bean = new HIS_MATERIAL_BEAN();
                    bean.AMOUNT = listByGroup.Sum(s => s.AMOUNT);
                    bean.MATERIAL_ID = group.Key;
                    bean.MEDI_STOCK_ID = impMest.MEDI_STOCK_ID;
                    bean.IS_CK = MOS.UTILITY.Constant.IS_TRUE;

                    HisMaterialBeanUtil.SetTdl(bean, hisMedicines.FirstOrDefault(o => o.ID == group.Key));

                    toInserts.Add(bean);
                }

                if (IsNotNullOrEmpty(toInserts) && !this.hisMaterialBeanCreate.Run(toInserts))
                {
                    throw new Exception("hisMaterialBeanCreate. Ket thuc nghiep vu.");
                }
            }
        }

        internal void Rollback()
        {
            try
            {
                this.hisMaterialBeanCreate.Rollback();
                this.hisExpMestMaterialUpdate.RollbackData();
                this.hisImpMestMaterialCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
