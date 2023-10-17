using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMediStockMaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Import
{
    class MaterialProcessor : BusinessBase
    {
        private HisMaterialBeanCreateSql hisMaterialBeanCreate;

        private List<long> materialIds;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisMaterialBeanCreate = new HisMaterialBeanCreateSql(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MATERIAL> hisImpMestMaterials, ref List<long> materialTypeIds)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(hisImpMestMaterials))
                {
                    List<HIS_MATERIAL> hisMaterials = null;
                    // lay ve tat cac cac material phuc vu set cac truong TDL trong bean va update available amount
                    hisMaterials = new HisMaterialGet().GetByIds(hisImpMestMaterials.Select(s => s.MATERIAL_ID).ToList());
                    materialTypeIds = hisMaterials.Select(s => s.MATERIAL_TYPE_ID).Distinct().ToList();
                    //Kiem tra xem co material nao bi khoa hay khong
                    if (hisMaterials.Exists(e => e.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                        throw new Exception("Ton tai lo vat tu dang bi khoa " + LogUtil.TraceData("hisMaterials", hisMaterials));
                    }
                    //Neu loai nhap la nhap NCC, nhap KK, nhap DK, nhap Khac => Update ImpTime trong MATERIAL
                    //Xet impTime phuc vu cho SetTdl materialBean
                    if (HisImpMestContanst.TYPE_MUST_CREATE_PACKAGE_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                    {
                        hisMaterials.ForEach(o => o.IMP_TIME = impMest.IMP_TIME);
                        string updateMaterial = new StringBuilder().Append("UPDATE HIS_MATERIAL SET IMP_TIME = ").Append(impMest.IMP_TIME.Value).Append(", IS_PREGNANT = NULL").Append(" WHERE %IN_CLAUSE% ").ToString();
                        updateMaterial = DAOWorker.SqlDAO.AddInClause(hisImpMestMaterials.Select(s => s.MATERIAL_ID).ToList(), updateMaterial, "ID");
                        if (!DAOWorker.SqlDAO.Execute(updateMaterial))
                        {
                            throw new Exception("Update IMP_TIME trong HIS_MATERIAL that bai. Rollback du lieu");
                        }
                        this.materialIds = hisImpMestMaterials.Select(s => s.MATERIAL_ID).ToList();
                    }

                    var Groups = hisImpMestMaterials.GroupBy(g => g.MATERIAL_ID).ToList();
                    List<HIS_MATERIAL_BEAN> toInserts = new List<HIS_MATERIAL_BEAN>();
                    foreach (var group in Groups)
                    {
                        List<HIS_IMP_MEST_MATERIAL> listByGroup = group.ToList();
                        if (listByGroup.Any(a => !String.IsNullOrWhiteSpace(a.SERIAL_NUMBER)))
                        {
                            foreach (var item in listByGroup)
                            {
                                HIS_MATERIAL_BEAN bean = new HIS_MATERIAL_BEAN();
                                bean.AMOUNT = 1;
                                bean.MATERIAL_ID = item.MATERIAL_ID;
                                bean.MEDI_STOCK_ID = impMest.MEDI_STOCK_ID;
                                bean.SERIAL_NUMBER = item.SERIAL_NUMBER;
                                bean.REMAIN_REUSE_COUNT = item.REMAIN_REUSE_COUNT;
                                if (HisImpMestContanst.TYPE_MOBA_IDS.Contains(impMest.IMP_MEST_TYPE_ID) || impMest.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__THT)
                                {
                                    bean.IS_TH = MOS.UTILITY.Constant.IS_TRUE;
                                }
                                else if (HisImpMestContanst.TYPE_CHMS_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                                {
                                    bean.IS_CK = MOS.UTILITY.Constant.IS_TRUE;
                                }
                                HisMaterialBeanUtil.SetTdl(bean, hisMaterials.FirstOrDefault(o => o.ID == item.MATERIAL_ID));

                                toInserts.Add(bean);
                            }
                        }
                        else
                        {
                            HIS_MATERIAL_BEAN bean = new HIS_MATERIAL_BEAN();
                            bean.AMOUNT = listByGroup.Sum(s => s.AMOUNT);
                            bean.MATERIAL_ID = group.Key;
                            bean.MEDI_STOCK_ID = impMest.MEDI_STOCK_ID;
                            if (HisImpMestContanst.TYPE_MOBA_IDS.Contains(impMest.IMP_MEST_TYPE_ID) || impMest.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__THT)
                            {
                                bean.IS_TH = MOS.UTILITY.Constant.IS_TRUE;
                            }
                            else if (HisImpMestContanst.TYPE_CHMS_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                            {
                                bean.IS_CK = MOS.UTILITY.Constant.IS_TRUE;
                            }
                            HisMaterialBeanUtil.SetTdl(bean, hisMaterials.FirstOrDefault(o => o.ID == group.Key));

                            toInserts.Add(bean);
                        }
                    }

                    if (IsNotNullOrEmpty(toInserts) && !this.hisMaterialBeanCreate.Run(toInserts))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
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
                if (IsNotNullOrEmpty(this.materialIds))
                {
                    string rollbackMaterialSql = DAOWorker.SqlDAO.AddInClause(this.materialIds, "UPDATE HIS_MATERIAL SET IMP_TIME = NULL, IS_PREGNANT = 1 WHERE %IN_CLAUSE% ", "ID");
                    if (!DAOWorker.SqlDAO.Execute(rollbackMaterialSql))
                    {
                        LogSystem.Warn("Rollback HIS_MATERIAL that bai SQL: " + rollbackMaterialSql);
                    }
                    this.materialIds = null;
                }

                this.hisMaterialBeanCreate.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
