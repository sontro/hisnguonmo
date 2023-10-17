using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMediStockMaty;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.UnImport
{

    class MaterialProcessor : BusinessBase
    {
        private HisMaterialUpdate hisMaterialUpdate;
        private HisMaterialBeanSplit hisMaterialBeanSplit;
        private HisMaterialBeanUnimport hisMaterialBeanUnimport;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisMaterialUpdate = new HisMaterialUpdate(param);
            this.hisMaterialBeanSplit = new HisMaterialBeanSplit(param);
            this.hisMaterialBeanUnimport = new HisMaterialBeanUnimport(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, ref List<HIS_IMP_MEST_MATERIAL> materials, ref List<long> materialTypeIds)
        {
            bool result = false;
            try
            {
                materials = new HisImpMestMaterialGet().GetByImpMestId(impMest.ID);
                List<HIS_IMP_MEST_MATERIAL> hisImpMestMaterials = materials != null ? materials.Where(o => String.IsNullOrWhiteSpace(o.SERIAL_NUMBER)).ToList() : null;
                if (IsNotNullOrEmpty(hisImpMestMaterials))
                {
                    HisImpMestCheck checker = new HisImpMestCheck(param);
                    List<HIS_MATERIAL> hisMaterials = new List<HIS_MATERIAL>();
                    List<ExpMaterialSDO> materialSDOs = new List<ExpMaterialSDO>();
                    var Groups = hisImpMestMaterials.GroupBy(g => g.MATERIAL_ID).ToList();
                    foreach (var group in Groups)
                    {
                        List<HIS_IMP_MEST_MATERIAL> listGroup = group.ToList();
                        if (HisImpMestContanst.TYPE_MUST_CREATE_PACKAGE_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                        {
                            HIS_MATERIAL material = new HisMaterialGet().GetById(group.Key);
                            if (material == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                                throw new Exception("MATERIAL_ID Invalid: " + group.Key);
                            }
                            if (material.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                                throw new Exception("Lo thuoc dang bi khoa: " + LogUtil.TraceData("Material", material));
                            }
                            hisMaterials.Add(material);
                        }
                        if (!checker.CheckCorrectImpExpMaterial(group.Key, impMest.MEDI_STOCK_ID, impMest.ID))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                            throw new Exception("Du lieu nhap, xuat khong con tinh dung dan khi huy nhap. MaterialId: " + group.Key);
                        }
                        decimal totalAmount = group.Sum(s => s.AMOUNT);
                        if (totalAmount <= 0) continue;
                        ExpMaterialSDO sdo = new ExpMaterialSDO();
                        sdo.Amount = totalAmount;
                        sdo.MaterialId = group.Key;
                        materialSDOs.Add(sdo);
                    }
                    List<HIS_MATERIAL_BEAN> hisMaterialBeans = null;
                    if (!this.hisMaterialBeanSplit.SplitByMaterial(materialSDOs, impMest.MEDI_STOCK_ID, ref hisMaterialBeans))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                        throw new Exception("Khong tach du bean theo material, co the do khong du so luong " + LogUtil.TraceData("materialSDOs", materialSDOs));
                    }

                    if (!this.hisMaterialBeanUnimport.Run(hisMaterialBeans.Select(s => s.ID).ToList(), impMest.MEDI_STOCK_ID))
                    {
                        throw new Exception("Update IS_ACTIVE, MEDI_STOCK_ID cho MATERIAL_BEAN that bai");
                    }

                    if (IsNotNullOrEmpty(hisMaterials))
                    {
                        if (HisImpMestContanst.TYPE_MUST_CREATE_PACKAGE_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                        {
                            materialTypeIds.AddRange(hisMaterials.Select(s => s.MATERIAL_TYPE_ID).Distinct().ToList());
                        }
                        Mapper.CreateMap<HIS_MATERIAL, HIS_MATERIAL>();
                        List<HIS_MATERIAL> materialBefores = Mapper.Map<List<HIS_MATERIAL>>(hisMaterials);
                        hisMaterials.ForEach(o =>
                        {
                            o.IMP_TIME = null;
                            o.IS_PREGNANT = MOS.UTILITY.Constant.IS_TRUE;
                        });
                        if (!this.hisMaterialUpdate.UpdateList(hisMaterials, materialBefores))
                        {
                            throw new Exception("Update HIS_MATERIAL that bai");
                        }
                    }
                }
                result = true;
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
                this.hisMaterialBeanUnimport.Rollback();
                this.hisMaterialBeanSplit.RollBack();
                this.hisMaterialUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
