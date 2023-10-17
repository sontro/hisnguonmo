using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisImpMest.Chms;
using MOS.MANAGER.HisImpMest.Reusable;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Export
{
    class ImportAutoMaterialProcessor : BusinessBase
    {
        private HisImpMestReusableCreate hisImpMestReusableCreate;

        internal ImportAutoMaterialProcessor()
            : base()
        {
            this.hisImpMestReusableCreate = new HisImpMestReusableCreate();
        }

        internal ImportAutoMaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestReusableCreate = new HisImpMestReusableCreate();
        }

        internal bool Run(List<HIS_EXP_MEST_MATERIAL> materials, HIS_EXP_MEST expMest, long reqRoomId)
        {
            bool result = true;
            try
            {
                if (expMest == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("expMest null");
                }
                List<long> expMestTypeIds = new List<long>{
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK,
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT,
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT
                        };
                V_HIS_MEDI_STOCK stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == expMest.MEDI_STOCK_ID);

                List<HIS_EXP_MEST_MATERIAL> materialProcess = IsNotNullOrEmpty(materials) ? materials.Where(o => o.REMAIN_REUSE_COUNT > 1
                        && o.MATERIAL_ID.HasValue
                        && o.TDL_MEDI_STOCK_ID.HasValue
                        && !String.IsNullOrWhiteSpace(o.SERIAL_NUMBER)).ToList() : null;

                if (IsNotNullOrEmpty(materialProcess)
                    && stock != null && stock.IS_AUTO_CREATE_REUSABLE_IMP == Constant.IS_TRUE
                    && expMestTypeIds.Contains(expMest.EXP_MEST_TYPE_ID))
                {
                    List<string> serialNumbers = materialProcess.Select(o => o.SERIAL_NUMBER).Distinct().ToList();

                    HisImpMestMaterialViewFilterQuery filterImp = new HisImpMestMaterialViewFilterQuery();
                    filterImp.SERIAL_NUMBERs = serialNumbers;
                    List<V_HIS_IMP_MEST_MATERIAL> imp = new HisImpMestMaterialGet().GetView(filterImp);

                    HisMaterialBeanFilterQuery filterBean = new HisMaterialBeanFilterQuery();
                    filterBean.SERIAL_NUMBERs = serialNumbers;
                    List<HIS_MATERIAL_BEAN> bean = new HisMaterialBeanGet().Get(filterBean);

                    HisImpMestReuseSDO sdo = new HisImpMestReuseSDO();
                    sdo.MediStockId = stock.ID;
                    sdo.Description = null;
                    sdo.RequestRoomId = reqRoomId;
                    sdo.MaterialReuseSDOs = new List<ImpMestMaterialReusableSDO>();
                    foreach (var mate in materialProcess)
                    {
                        List<V_HIS_IMP_MEST_MATERIAL> impMest = IsNotNullOrEmpty(imp) ? imp.Where(o => o.SERIAL_NUMBER == mate.SERIAL_NUMBER && o.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT).ToList() : null;
                        List<HIS_MATERIAL_BEAN> mateBean = IsNotNullOrEmpty(bean) ? bean.Where(o => o.SERIAL_NUMBER == mate.SERIAL_NUMBER && o.MEDI_STOCK_ID.HasValue).ToList() : null;
                        //không có phiếu nhập chưa thực nhập và không có bean trong kho
                        if (!IsNotNullOrEmpty(impMest) && !IsNotNullOrEmpty(mateBean))
                        {
                            ImpMestMaterialReusableSDO resuableSdo = new ImpMestMaterialReusableSDO();
                            resuableSdo.SerialNumber = mate.SERIAL_NUMBER;
                            resuableSdo.ReusCount = mate.REMAIN_REUSE_COUNT.Value - 1;
                            resuableSdo.MaterialId = mate.MATERIAL_ID.Value;
                            sdo.MaterialReuseSDOs.Add(resuableSdo);

                        }
                    }

                    HisImpMestResultSDO resultData = null;

                    if (!this.hisImpMestReusableCreate.Run(sdo, ref resultData))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TuDongNhapTaiSuDungThatBai);
                    }
                    result = true;
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
