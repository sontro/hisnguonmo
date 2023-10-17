using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisImpMestMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Chms
{
    class MaterialProcessor : BusinessBase
    {
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;
        //private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            //this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials, ref List<HIS_IMP_MEST_MATERIAL> hisImpMestMaterials, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(hisExpMestMaterials))
                {
                    List<HIS_IMP_MEST_MATERIAL> impMestMaterials = new List<HIS_IMP_MEST_MATERIAL>();
                    Dictionary<long, List<HIS_EXP_MEST_MATERIAL>> dicExpMestMaterial = new Dictionary<long, List<HIS_EXP_MEST_MATERIAL>>();
                    var Groups = hisExpMestMaterials.GroupBy(g => g.MATERIAL_ID).ToList();
                    foreach (var group in Groups)
                    {
                        decimal amount = group.Sum(s => s.AMOUNT);
                        if (amount <= 0)
                            continue;
                        if (group.Any(a => !String.IsNullOrWhiteSpace(a.SERIAL_NUMBER)))
                        {
                            foreach (var item in group.ToList())
                            {
                                HIS_IMP_MEST_MATERIAL impMaterial = new HIS_IMP_MEST_MATERIAL();
                                impMaterial.MATERIAL_ID = group.Key.Value;
                                impMaterial.IMP_MEST_ID = impMest.ID;
                                impMaterial.AMOUNT = item.AMOUNT;
                                impMaterial.SERIAL_NUMBER = item.SERIAL_NUMBER;
                                impMaterial.REMAIN_REUSE_COUNT = item.REMAIN_REUSE_COUNT;
                                impMestMaterials.Add(impMaterial);
                            }
                        }
                        else
                        {
                            HIS_IMP_MEST_MATERIAL impMaterial = new HIS_IMP_MEST_MATERIAL();
                            impMaterial.MATERIAL_ID = group.Key.Value;
                            impMaterial.IMP_MEST_ID = impMest.ID;
                            impMaterial.AMOUNT = amount;
                            impMestMaterials.Add(impMaterial);
                        }
                        dicExpMestMaterial[group.Key.Value] = group.ToList();
                    }
                    if (IsNotNullOrEmpty(impMestMaterials))
                    {
                        if (!this.hisImpMestMaterialCreate.CreateList(impMestMaterials))
                        {
                            throw new Exception("Tao HIS_EXP_MEST_MATERIAL that bai");
                        }
                        Mapper.CreateMap<HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();

                        foreach (var item in impMestMaterials)
                        {
                            List<HIS_EXP_MEST_MATERIAL> listGroup = dicExpMestMaterial[item.MATERIAL_ID];
                            listGroup.ForEach(o => o.CK_IMP_MEST_MATERIAL_ID = item.ID);

                            string sql = DAOWorker.SqlDAO.AddInClause(listGroup.Select(s => s.ID).ToList(), String.Format("UPDATE HIS_EXP_MEST_MATERIAL SET CK_IMP_MEST_MATERIAL_ID = {0} WHERE %IN_CLAUSE%", item.ID), "ID");
                            sqls.Add(sql);
                        }
                        hisImpMestMaterials = impMestMaterials;
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
                //this.hisExpMestMaterialUpdate.RollbackData();
                this.hisImpMestMaterialCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
