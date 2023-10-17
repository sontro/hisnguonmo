using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMediStockMaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.UnImport
{
    class MaterialReusableProcessor : BusinessBase
    {
        private HisMaterialUpdate hisMaterialUpdate;
        private HisMaterialBeanTakeBySerial hisMaterialBeanTakeBySerial;
        private HisMaterialBeanUnimport hisMaterialBeanUnimport;

        internal MaterialReusableProcessor(CommonParam param)
            : base(param)
        {
            this.hisMaterialUpdate = new HisMaterialUpdate(param);
            this.hisMaterialBeanTakeBySerial = new HisMaterialBeanTakeBySerial(param);
            this.hisMaterialBeanUnimport = new HisMaterialBeanUnimport(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MATERIAL> materials, ref List<long> materialTypeIds)
        {
            bool result = false;
            try
            {
                List<HIS_IMP_MEST_MATERIAL> hisImpMestMaterials = materials != null ? materials.Where(o => !String.IsNullOrWhiteSpace(o.SERIAL_NUMBER)).ToList() : null;
                if (IsNotNullOrEmpty(hisImpMestMaterials))
                {
                    List<HIS_MATERIAL> hisMaterials = new List<HIS_MATERIAL>();
                    List<string> serialNumbers = new List<string>();
                    foreach (var item in hisImpMestMaterials)
                    {
                        if (HisImpMestContanst.TYPE_MUST_CREATE_PACKAGE_IDS.Contains(impMest.IMP_MEST_TYPE_ID) && !hisMaterials.Any(a => a.ID == item.MATERIAL_ID))
                        {
                            HIS_MATERIAL material = new HisMaterialGet().GetById(item.MATERIAL_ID);
                            if (material == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                                throw new Exception("MATERIAL_ID Invalid: " + item.MATERIAL_ID);
                            }
                            if (material.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                                throw new Exception("Lo thuoc dang bi khoa: " + LogUtil.TraceData("Material", material));
                            }
                            hisMaterials.Add(material);
                        }
                        if (!this.CheckExitsExpMaterialReus(item.SERIAL_NUMBER, impMest.IMP_TIME.Value))
                        {
                            throw new Exception("Vat tu tai su dung da ton tai phieu xuat. SerialNumber: " + item.SERIAL_NUMBER);
                        }
                        serialNumbers.Add(item.SERIAL_NUMBER);
                    }

                    List<HIS_MATERIAL_BEAN> hisMaterialBeans = null;
                    if (!this.hisMaterialBeanTakeBySerial.Run(serialNumbers, impMest.MEDI_STOCK_ID, ref hisMaterialBeans))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_VTTSDDaDuocXuat);
                        throw new Exception("Khong tach du bean theo serialnumber, co the do khong du so luong " + LogUtil.TraceData("serialNumbers", serialNumbers));
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
                            materialTypeIds = materialTypeIds.Distinct().ToList();
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
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool CheckExitsExpMaterialReus(string serialNumber, long impTime)
        {
            bool valid = true;
            try
            {
                if (String.IsNullOrWhiteSpace(serialNumber))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Ton tai cac expMestMaterial cua mot lo co va khong co so seri");
                }
                string sql = String.Format("SELECT A.* FROM HIS_EXP_MEST_MATERIAL A "
                    + "JOIN HIS_EXP_MEST EXP ON A.EXP_MEST_ID = EXP.ID "
                    + "WHERE A.SERIAL_NUMBER = '{0}' AND A.IS_DELETE = 0 AND EXP.IS_NOT_TAKEN IS NULL", serialNumber);
                LogSystem.Info("sql: " + sql);
                List<HIS_EXP_MEST_MATERIAL> exists = DAOWorker.SqlDAO.GetSql<HIS_EXP_MEST_MATERIAL>(sql);

                if (!IsNotNullOrEmpty(exists))
                {
                    return true;
                }
                HIS_EXP_MEST_MATERIAL export = exists.FirstOrDefault(o => !o.IS_EXPORT.HasValue);
                if (export == null)
                {
                    export = exists.Where(o => o.IS_EXPORT.HasValue && o.EXP_TIME > impTime).FirstOrDefault();
                }
                if (export != null)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_VTTSDCoSeriDaTonTaiPhieuXuat, export.SERIAL_NUMBER);
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

        internal void Rollback()
        {
            try
            {
                this.hisMaterialBeanUnimport.Rollback();
                this.hisMaterialBeanTakeBySerial.Rollback();
                this.hisMaterialUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
