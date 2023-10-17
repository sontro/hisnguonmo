using AutoMapper;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMestMaterial
{
    class StentUtil
    {
        /// <summary>
        /// Neu vat tu duoc ke la "stent" va co so luong lon hon 1, thi tach thanh nhieu dong,
        /// de dam bao moi dong so luong khong vuot qua 1
        /// </summary>
        /// <param name="materials"></param>
        /// <returns></returns>
        internal static List<PresMaterialSDO> MakeSingleStent(List<PresMaterialSDO> materials)
        {
            List<PresMaterialSDO> list = new List<PresMaterialSDO>();

            try
            {
                foreach (PresMaterialSDO sdo in materials)
                {
                    if (sdo.Amount > 1 && HisMaterialTypeCFG.IsStent(sdo.MaterialTypeId))
                    {
                        decimal remain = sdo.Amount;

                        Mapper.CreateMap<PresMaterialSDO, PresMaterialSDO>();
                        while (remain > 0)
                        {
                            PresMaterialSDO s = Mapper.Map<PresMaterialSDO>(sdo);
                            s.Amount = remain > 1 ? 1 : remain;

                            list.Add(s);
                            remain = remain - s.Amount;
                        }
                    }
                    else
                    {
                        list.Add(sdo);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

            return list;
        }

        /// <summary>
        /// Xu ly de set so thu tu trong truong hop phieu xuat co chua stent
        /// </summary>
        /// <param name="data"></param>
        internal static void SetStentOrder(List<HIS_EXP_MEST_MATERIAL> data)
        {
            try
            {
                //Neu vat tu la stent thi xu ly
                if (data != null && data.Count > 0 && data.Exists(t => HisMaterialTypeCFG.IsStent(t.TDL_MATERIAL_TYPE_ID.Value)))
                {
                    HisExpMestMaterialFilterQuery filter = new HisExpMestMaterialFilterQuery();
                    filter.TDL_TREATMENT_ID = data[0].TDL_TREATMENT_ID;
                    filter.HAS_STENT_ORDER = true;
                    List<HIS_EXP_MEST_MATERIAL> existsMaterials = new HisExpMestMaterialGet().Get(filter);

                    long lastStentOrder = existsMaterials != null && existsMaterials.Count > 0 ? 
                        existsMaterials.Where(t => HisMaterialTypeCFG.IsStent(t.TDL_MATERIAL_TYPE_ID.Value)
                            && t.STENT_ORDER.HasValue).Select(t => t.STENT_ORDER.Value).Max() : 0;

                    foreach (HIS_EXP_MEST_MATERIAL m in data)
                    {
                        if (HisMaterialTypeCFG.IsStent(m.TDL_MATERIAL_TYPE_ID.Value))
                        {
                            m.STENT_ORDER = ++lastStentOrder;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Xu ly de set so thu tu trong truong hop phieu xuat co chua stent
        /// </summary>
        /// <param name="data"></param>
        internal static void SetStentOrder(HIS_EXP_MEST_MATERIAL data)
        {
            SetStentOrder(new List<HIS_EXP_MEST_MATERIAL>() { data });
        }
    }
}
