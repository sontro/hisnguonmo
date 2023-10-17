using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.UpdateDetail
{
    class MaterialProcessor : BusinessBase
    {
        private HisImpMestMaterialUpdate hisImpMestMaterialUpdate;
        private HisMaterialUpdate hisMaterialUpdate;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestMaterialUpdate = new HisImpMestMaterialUpdate(param);
            this.hisMaterialUpdate = new HisMaterialUpdate(param);
        }

        internal bool Run(HisImpMestUpdateDetailSDO data, HIS_IMP_MEST impMest, HisImpMestUpdateDetailLog logProcessor)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(data.ImpMestMaterials))
                {
                    List<HIS_IMP_MEST_MATERIAL> listRaw = new List<HIS_IMP_MEST_MATERIAL>();
                    List<HIS_MATERIAL> rawMaterials = new List<HIS_MATERIAL>();
                    HisImpMestMaterialCheck checker = new HisImpMestMaterialCheck(param);
                    HisMaterialCheck mateChecker = new HisMaterialCheck(param);
                    bool valid = true;
                    valid = valid && checker.VerifyIds(data.ImpMestMaterials.Select(s => s.Id).ToList(), listRaw);
                    valid = valid && checker.IsUnLock(listRaw);
                    valid = valid && mateChecker.VerifyIds(listRaw.Select(s => s.MATERIAL_ID).ToList(), rawMaterials);
                    valid = valid && mateChecker.IsUnLock(rawMaterials);
                    if (!valid)
                    {
                        return false;
                    }
                    Mapper.CreateMap<HIS_IMP_MEST_MATERIAL, HIS_IMP_MEST_MATERIAL>();
                    List<HIS_IMP_MEST_MATERIAL> beforeImpMestMaterials = Mapper.Map<List<HIS_IMP_MEST_MATERIAL>>(listRaw);
                    Mapper.CreateMap<HIS_MATERIAL, HIS_MATERIAL>();
                    List<HIS_MATERIAL> beforeMaterials = Mapper.Map<List<HIS_MATERIAL>>(rawMaterials);
                    if (!this.CheckDetail(data, impMest, listRaw, rawMaterials, logProcessor))
                    {
                        return false;
                    }

                    if (!this.hisImpMestMaterialUpdate.UpdateList(listRaw, beforeImpMestMaterials))
                    {
                        throw new Exception("hisImpMestMaterialUpdate. Ket thuc nghiep vu");
                    }

                    if (!this.hisMaterialUpdate.UpdateList(rawMaterials, beforeMaterials))
                    {
                        throw new Exception("hisMaterialUpdate. Ket thuc nghiep vu");
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

        private bool CheckDetail(HisImpMestUpdateDetailSDO data, HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MATERIAL> listRaw, List<HIS_MATERIAL> rawMaterials, HisImpMestUpdateDetailLog logProcessor)
        {
            bool valid = true;
            try
            {
                if (listRaw.Exists(e => e.IMP_MEST_ID != impMest.ID))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai HIS_IMP_MEST_MATERIAL khong thuoc phieu nhap can sua");
                }

                if (valid)
                {
                    foreach (HisImpMestMaterialSDO sdo in data.ImpMestMaterials)
                    {
                        HIS_IMP_MEST_MATERIAL impMestMedi = listRaw.FirstOrDefault(o => o.ID == sdo.Id);
                        HIS_MATERIAL material = rawMaterials.FirstOrDefault(o => o.ID == impMestMedi.MATERIAL_ID);
                        HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == material.MATERIAL_TYPE_ID);
                        HIS_MATERIAL_TYPE newMaterialType = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == sdo.MaterialTypeId);
                        if (!this.CheckExists(material, impMest, materialType))
                        {
                            return false;
                        }

                        if ((impMestMedi.TDL_IMP_UNIT_ID.HasValue || newMaterialType.IMP_UNIT_ID.HasValue) && newMaterialType.ID != materialType.ID)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepDoiLoaiVatTuCoCauHinhDonViNhap);
                            throw new Exception("Khong cho phep doi loai vat tu co thong tin TDL_IMP_UNIT_ID");
                        }

                        logProcessor.GenerateLogMessage(material, materialType, newMaterialType, sdo);

                        impMestMedi.VAT_RATIO = sdo.ImpVatRatio;
                        impMestMedi.DOCUMENT_PRICE = sdo.DocumentPrice;
                        if (impMestMedi.TDL_IMP_UNIT_ID.HasValue)
                        {
                            impMestMedi.IMP_UNIT_PRICE = sdo.ImpPrice;
                            material.IMP_UNIT_PRICE = sdo.ImpPrice;
                            impMestMedi.PRICE = impMestMedi.IMP_UNIT_PRICE.Value / impMestMedi.TDL_IMP_UNIT_CONVERT_RATIO.Value;
                            material.IMP_PRICE = impMestMedi.PRICE.Value;
                        }
                        else
                        {
                            impMestMedi.PRICE = sdo.ImpPrice;
                            material.IMP_PRICE = sdo.ImpPrice;
                            impMestMedi.IMP_UNIT_PRICE = null;
                            material.IMP_UNIT_PRICE = null;
                        }
                        //impMestMedi.AMOUNT = sdo.Amount;
                        //material.AMOUNT = sdo.Amount;
                        material.IMP_VAT_RATIO = sdo.ImpVatRatio;
                        material.EXPIRED_DATE = sdo.ExpireDate;
                        material.PACKAGE_NUMBER = sdo.PackageNumber;
                        material.INTERNAL_PRICE = newMaterialType.INTERNAL_PRICE;
                        material.MATERIAL_TYPE_ID = newMaterialType.ID;
                        material.TDL_SERVICE_ID = newMaterialType.SERVICE_ID;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        private bool CheckExists(HIS_MATERIAL matecine, HIS_IMP_MEST impMest, HIS_MATERIAL_TYPE materialType)
        {
            bool valid = true;
            try
            {
                List<V_HIS_EXP_MEST_MATERIAL> existsExps = new HisExpMestMaterialGet().GetViewByMaterialId(matecine.ID);
                if (IsNotNullOrEmpty(existsExps))
                {
                    List<string> expMestCodes = existsExps.Select(s => s.EXP_MEST_CODE).Distinct().ToList();
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_VatTuDaThuocPhieuXuat, materialType.MATERIAL_TYPE_NAME, String.Join(";", expMestCodes));
                    return false;
                }
                List<V_HIS_IMP_MEST_MATERIAL> existsImps = new HisImpMestMaterialGet().GetViewByMaterialId(matecine.ID);
                existsImps = existsImps != null ? existsImps.Where(o => o.IMP_MEST_ID != impMest.ID).ToList() : null;
                if (IsNotNullOrEmpty(existsImps))
                {
                    List<string> impMestCodes = existsImps.Select(s => s.IMP_MEST_CODE).Distinct().ToList();
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_VatTuDaThuocPhieuNhap, materialType.MATERIAL_TYPE_NAME, String.Join(";", impMestCodes));
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisMaterialUpdate.RollbackData();
                this.hisImpMestMaterialUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
