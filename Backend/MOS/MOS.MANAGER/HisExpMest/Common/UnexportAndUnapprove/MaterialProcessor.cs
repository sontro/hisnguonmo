using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMaterialBean.Update;
using MOS.MANAGER.HisMediStockMaty;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMest.Common.UnexportAndUnapprove
{
    partial class MaterialProcessor : BusinessBase
    {
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;
        private HisMaterialBeanUnexport hisMaterialBeanUnexport;
        private HisMediStockMatyIncreaseBaseAmount hisMediStockMatyIncreaseBaseAmount;

        internal MaterialProcessor()
            : base()
        {
            this.Init();
        }

        internal MaterialProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
            this.hisMaterialBeanUnexport = new HisMaterialBeanUnexport(param);
            this.hisMediStockMatyIncreaseBaseAmount = new HisMediStockMatyIncreaseBaseAmount(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials)
        {
            try
            {
                this.ProcessExpMestMaterial(hisExpMestMaterials);
                this.ProcessMaterialBean(hisExpMestMaterials, expMest.MEDI_STOCK_ID);
                this.ProcessMediStockMaty(expMest, hisExpMestMaterials);
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Cap nhat his_medicine_bean
        /// </summary>
        /// <param name="hisExpMestMaterials"></param>
        private void ProcessMaterialBean(List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials, long mediStockId)
        {
            if (IsNotNullOrEmpty(hisExpMestMaterials) && !this.hisMaterialBeanUnexport.Run(hisExpMestMaterials, mediStockId))
            {
                throw new Exception("Rollback du lieu");
            }
        }

        /// <summary>
        /// Huy thong tin xuat vao exp_mest_medicine
        /// </summary>
        /// <param name="expMestMaterials"></param>
        /// <param name="loginName"></param>
        /// <param name="userName"></param>
        /// <param name="expTime"></param>
        private void ProcessExpMestMaterial(List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            if (IsNotNullOrEmpty(expMestMaterials))
            {
                Mapper.CreateMap<HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();
                List<HIS_EXP_MEST_MATERIAL> befores = Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(expMestMaterials);
                if (expMestMaterials.Any(a => !String.IsNullOrWhiteSpace(a.SERIAL_NUMBER)))
                {
                    foreach (var item in expMestMaterials)
                    {
                        if (String.IsNullOrWhiteSpace(item.SERIAL_NUMBER))
                            continue;
                        string sql = String.Format("SELECT * FROM V_HIS_IMP_MEST_MATERIAL WHERE SERIAL_NUMBER = '{0}' AND IS_DELETE = 0 ", item.SERIAL_NUMBER);
                        LogSystem.Info("sql: " + sql);
                        List<V_HIS_IMP_MEST_MATERIAL> exists = DAOWorker.SqlDAO.GetSql<V_HIS_IMP_MEST_MATERIAL>(sql);
                        V_HIS_IMP_MEST_MATERIAL hasImp = exists != null ? exists.FirstOrDefault(o => o.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT || o.IMP_TIME.Value > item.EXP_TIME.Value) : null;
                        if (hasImp != null)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_VTTSDCoSoSeriDaTonTaiPhieuNhap, hasImp.SERIAL_NUMBER, MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMediStock_MaPhieuNhap, param.LanguageCode), hasImp.IMP_MEST_CODE);
                            throw new Exception("Vat tu tai su dung da ton tai phieu nhap");
                        }
                    }
                }
                expMestMaterials.ForEach(o =>
                {
                    o.IS_EXPORT = null;
                    o.EXP_LOGINNAME = null;
                    o.EXP_USERNAME = null;
                    o.EXP_TIME = null;
                });

                if (!this.hisExpMestMaterialUpdate.UpdateList(expMestMaterials, befores))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessMediStockMaty(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            if (HisMediStockCFG.IS_USE_BASE_AMOUNT_CABINET && this.IsCheckCabinetAmount(expMest) && IsNotNullOrEmpty(expMestMaterials))
            {
                HisMediStockMatyFilterQuery stockMatyFilter = new HisMediStockMatyFilterQuery();
                stockMatyFilter.MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                stockMatyFilter.MATERIAL_TYPE_IDs = expMestMaterials.Select(s => (s.TDL_MATERIAL_TYPE_ID ?? 0)).Distinct().ToList();
                List<HIS_MEDI_STOCK_MATY> mestMatys = new HisMediStockMatyGet().Get(stockMatyFilter);

                if (!IsNotNullOrEmpty(mestMatys))
                {
                    return;
                }

                Dictionary<long, decimal> dicIncrease = new Dictionary<long, decimal>();
                List<string> messageError = new List<string>();
                foreach (var item in mestMatys)
                {
                    if (!item.ALERT_MAX_IN_STOCK.HasValue || !item.REAL_BASE_AMOUNT.HasValue)
                        continue;
                    var medicines = expMestMaterials.Where(o => o.TDL_MATERIAL_TYPE_ID == item.MATERIAL_TYPE_ID).ToList();
                    if (IsNotNullOrEmpty(medicines))
                    {
                        decimal expAmount = medicines.Sum(s => s.AMOUNT);
                        decimal baseAmount = item.REAL_BASE_AMOUNT.Value + expAmount;
                        if (baseAmount > item.ALERT_MAX_IN_STOCK.Value)
                        {
                            HIS_MATERIAL_TYPE mety = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == item.MATERIAL_TYPE_ID);
                            string name = mety != null ? mety.MATERIAL_TYPE_NAME : "";
                            messageError.Add(String.Format("{0}({1})", name, item.ALERT_MAX_IN_STOCK.Value));
                            continue;
                        }

                        dicIncrease[item.ID] = expAmount;
                    }
                }

                if (messageError.Count > 0)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuSauVuotQuaCoSo, String.Join(";", messageError));
                    throw new Exception("Co vat tu vuot qua co so");
                }

                if (dicIncrease.Count > 0 && !this.hisMediStockMatyIncreaseBaseAmount.Run(dicIncrease))
                {
                    throw new Exception("hisMediStockMatyIncreaseBaseAmount. Ket thuc nghiep vu");
                }
            }
        }

        private bool IsCheckCabinetAmount(HIS_EXP_MEST exp)
        {
            V_HIS_MEDI_STOCK stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == exp.MEDI_STOCK_ID);

            if (stock == null)
                throw new Exception("Khong lay duoc V_HIS_MEDI_STOCK ID: " + exp.MEDI_STOCK_ID);
            if (exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT
                    || exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM
                    || exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK
                    || exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT
                    || exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL
                    || exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK
                    || stock.IS_CABINET != Constant.IS_TRUE)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMaterialUpdate.RollbackData();
            this.hisMaterialBeanUnexport.Rollback();
            this.hisMediStockMatyIncreaseBaseAmount.Rollback();
        }
    }
}
