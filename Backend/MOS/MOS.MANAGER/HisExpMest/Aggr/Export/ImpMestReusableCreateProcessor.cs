using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisImpMest.Reusable;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMest.Aggr.Export
{
    class ImpMestReusableCreateProcessor : BusinessBase
    {
        private HisImpMestReusableCreate hisImpMestReusableCreate;

        internal ImpMestReusableCreateProcessor()
            : base()
        {
            this.Init();
        }

        internal ImpMestReusableCreateProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestReusableCreate = new HisImpMestReusableCreate(param);
        }

        internal bool Run(HIS_EXP_MEST aggrExpMest, List<HIS_EXP_MEST> children, List<HIS_EXP_MEST_MATERIAL> materials, long reqRoomId)
        {
            bool result = true;
            try
            {
                V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == aggrExpMest.MEDI_STOCK_ID).FirstOrDefault();
                if (mediStock != null && mediStock.IS_AUTO_CREATE_REUSABLE_IMP == UTILITY.Constant.IS_TRUE && children != null)
                {
                    List<long> expMestTypeIds_Reusable = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK, 
                                                                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT, 
                                                                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT };
                    List<HIS_EXP_MEST> expMestsReusable = children.Where(o => expMestTypeIds_Reusable.Contains(o.EXP_MEST_TYPE_ID)).ToList();
                    if (IsNotNullOrEmpty(expMestsReusable) && IsNotNullOrEmpty(materials))
                    {
                        materials = materials.Where(o => o.REMAIN_REUSE_COUNT > 1).ToList();
                        if (!IsNotNullOrEmpty(materials)) return result;
                        var materialIds = materials.Where(o=>o.MATERIAL_ID.HasValue).Select(o => o.MATERIAL_ID.Value).ToList() ?? new List<long>();
                        HisImpMestMaterialViewFilterQuery filter = new HisImpMestMaterialViewFilterQuery();
                        filter.MATERIAL_IDs = materialIds;
                        filter.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT;
                        List<V_HIS_IMP_MEST_MATERIAL> impMestMaterials = new HisImpMestMaterialGet().GetView(filter);
                        if (!IsNotNullOrEmpty(impMestMaterials)) return result;
                        var serialNumbers = impMestMaterials.Select(o => o.SERIAL_NUMBER).ToList();
                        HisMaterialBeanFilterQuery filterMaterialBean = new HisMaterialBeanFilterQuery();
                        filterMaterialBean.SERIAL_NUMBERs = serialNumbers;
                        filterMaterialBean.HAS_MEDI_STOCK_ID = true;
                        var materialBeans = new HisMaterialBeanGet().Get(filterMaterialBean);
                        if (IsNotNullOrEmpty(materialBeans))
                        {
                            serialNumbers = serialNumbers.Where(o => !materialBeans.Select(b => b.SERIAL_NUMBER).Contains(o)).ToList();
                        }
                        if(!IsNotNullOrEmpty(serialNumbers)) return result;
                        List<long> reusableMaterialIds = impMestMaterials.Where(o => serialNumbers.Contains(o.SERIAL_NUMBER)).Select(o => o.MATERIAL_ID).ToList();
                        var reusableMaterials = materials.Where(o => o.MATERIAL_ID.HasValue && reusableMaterialIds.Contains(o.MATERIAL_ID.Value)).ToList();
                        if (IsNotNullOrEmpty(reusableMaterials))
                        {
                            HisImpMestResultSDO resultData = null;
                            HisImpMestReuseSDO impMestReuseSDO = new HisImpMestReuseSDO();
                            impMestReuseSDO.MediStockId = aggrExpMest.MEDI_STOCK_ID;
                            impMestReuseSDO.RequestRoomId = reqRoomId;
                            impMestReuseSDO.MaterialReuseSDOs = new List<ImpMestMaterialReusableSDO>();
                            foreach (var item in reusableMaterials)
                            {
                                var addData = new ImpMestMaterialReusableSDO();
                                addData.SerialNumber = item.SERIAL_NUMBER;
                                addData.MaterialId = item.MATERIAL_ID.Value;
                                addData.ReusCount = item.REMAIN_REUSE_COUNT.Value - 1;
                                impMestReuseSDO.MaterialReuseSDOs.Add(addData);
                            }
                            bool onlyValidRequiredInput = true;
                            if (!this.hisImpMestReusableCreate.Run(impMestReuseSDO, ref resultData))
                            {
                                result = false;
                                Inventec.Common.Logging.LogSystem.Warn("Tao phieu nhap tai su dung that bai EXP_MEST_CODE: " + aggrExpMest.EXP_MEST_CODE);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisImpMestReusableCreate.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
