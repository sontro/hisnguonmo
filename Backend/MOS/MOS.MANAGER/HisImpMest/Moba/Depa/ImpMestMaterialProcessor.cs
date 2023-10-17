using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Moba.Depa
{
    class ImpMestMaterialProcessor : BusinessBase
    {
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;
        private HisExpMestMaterialIncreaseThAmount hisExpMestMaterialIncreaseThAmount;

        internal ImpMestMaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestMaterialIncreaseThAmount = new HisExpMestMaterialIncreaseThAmount(param);
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HisMobaMaterialSDO> mobaMaterials, HIS_EXP_MEST expMest, ref List<HIS_IMP_MEST_MATERIAL> hisImpMestMaterials)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(mobaMaterials))
                {
                    List<HIS_IMP_MEST_MATERIAL> listCreate = new List<HIS_IMP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> existedExpMestMaterials = new HisExpMestMaterialGet().GetExportedByExpMestId(expMest.ID);

                    Dictionary<long, decimal> dicIncrease = new Dictionary<long, decimal>();

                    if (!IsNotNullOrEmpty(existedExpMestMaterials))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong lay duoc HIS_EXP_MEST_MATERIAL theo ExpMestId: " + expMest.ID);
                    }
                    List<HIS_MATERIAL> hisMaterials = new HisMaterialGet().GetByIds(existedExpMestMaterials.Select(s => s.MATERIAL_ID.Value).ToList());
                    if (!IsNotNullOrEmpty(hisMaterials))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Khong lay duoc HIS_MATERIAL theo IDs");
                    }

                    var Groups = mobaMaterials.GroupBy(g => g.MaterialId).ToList();
                    foreach (var group in Groups)
                    {
                        List<HisMobaMaterialSDO> listByGroup = group.ToList();
                        decimal amount = listByGroup.Sum(s => s.Amount);
                        List<HIS_EXP_MEST_MATERIAL> listByMaterial = existedExpMestMaterials.Where(o => o.MATERIAL_ID == group.Key).ToList();
                        if (!IsNotNullOrEmpty(listByMaterial))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc HIS_EXP_MEST_MATERIAL theo medicineId: " + group.Key);
                        }

                        HIS_MATERIAL medicine = hisMaterials.FirstOrDefault(o => o.ID == group.Key);
                        if (medicine == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Khong lay duoc HIS_MATERIAL theo ID: " + group.Key);
                        }

                        decimal canMoba = listByMaterial.Sum(s => (s.AMOUNT - (s.TH_AMOUNT ?? 0)));
                        if (canMoba < amount)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMobaImpMest_SoLuongYeuCauThuHoiVuotQuaSoLuongKhaDung);
                            throw new Exception("Yeu cau thu hoi lon hon kha dung thu hoi");
                        }
                        if (listByMaterial.Any(a => !String.IsNullOrWhiteSpace(a.SERIAL_NUMBER)))
                        {
                            if (listByMaterial.Any(a => String.IsNullOrWhiteSpace(a.SERIAL_NUMBER)))
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                                throw new Exception("Ton tai material vua co so seri vua khong co. MaterialId: " + group.Key);
                            }

                            foreach (var exp in listByMaterial)
                            {
                                if (amount <= 0) break;
                                if (exp.AMOUNT == (exp.TH_AMOUNT ?? 0)) break;
                                if (!this.CheckExitsImpMaterialReus(exp.SERIAL_NUMBER, exp.EXP_TIME.Value))
                                {
                                    throw new Exception("Vat tu tai su dung ton tai phieu nhap. SerialNumber: " + exp.SERIAL_NUMBER);
                                }
                                HIS_IMP_MEST_MATERIAL impMestMaterial = new HIS_IMP_MEST_MATERIAL();
                                impMestMaterial.AMOUNT = (exp.AMOUNT - (exp.TH_AMOUNT ?? 0));
                                impMestMaterial.IMP_MEST_ID = impMest.ID;
                                impMestMaterial.MATERIAL_ID = exp.MATERIAL_ID.Value;
                                impMestMaterial.TH_EXP_MEST_MATERIAL_ID = exp.ID;
                                impMestMaterial.SERIAL_NUMBER = exp.SERIAL_NUMBER;
                                impMestMaterial.REMAIN_REUSE_COUNT = exp.REMAIN_REUSE_COUNT;
                                dicIncrease[exp.ID] = impMestMaterial.AMOUNT;
                                amount = amount - impMestMaterial.AMOUNT;
                                listCreate.Add(impMestMaterial);
                            }
                        }
                        else
                        {
                            foreach (var exp in listByMaterial)
                            {
                                if ((exp.AMOUNT - (exp.TH_AMOUNT ?? 0)) >= amount)
                                {
                                    dicIncrease[exp.ID] = amount;
                                    HIS_IMP_MEST_MATERIAL impMestMaterial = new HIS_IMP_MEST_MATERIAL();
                                    impMestMaterial.AMOUNT = amount;
                                    impMestMaterial.IMP_MEST_ID = impMest.ID;
                                    impMestMaterial.MATERIAL_ID = exp.MATERIAL_ID.Value;
                                    impMestMaterial.TH_EXP_MEST_MATERIAL_ID = exp.ID;
                                    listCreate.Add(impMestMaterial);
                                    break;
                                }
                                else
                                {
                                    dicIncrease[exp.ID] = exp.AMOUNT - (exp.TH_AMOUNT ?? 0);
                                    amount = amount - dicIncrease[exp.ID];
                                    HIS_IMP_MEST_MATERIAL impMestMaterial = new HIS_IMP_MEST_MATERIAL();
                                    impMestMaterial.AMOUNT = dicIncrease[exp.ID];
                                    impMestMaterial.IMP_MEST_ID = impMest.ID;
                                    impMestMaterial.MATERIAL_ID = exp.MATERIAL_ID.Value;
                                    impMestMaterial.TH_EXP_MEST_MATERIAL_ID = exp.ID;
                                    listCreate.Add(impMestMaterial);
                                }
                            }
                        }
                    }

                    if (!this.hisImpMestMaterialCreate.CreateList(listCreate))
                    {
                        throw new Exception("Tao HIS_IMP_MEST_MATERIAL that bai");
                    }
                    if (!this.hisExpMestMaterialIncreaseThAmount.Run(dicIncrease))
                    {
                        throw new Exception("Update ThAmount cho HIS_IMP_MEST_MATERIAL that bai");
                    }
                    hisImpMestMaterials = listCreate;
                }

                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private bool CheckExitsImpMaterialReus(string serialNumber, long expTime)
        {
            bool valid = true;
            try
            {
                if (String.IsNullOrWhiteSpace(serialNumber))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Ton tai cac expMestMaterial cua mot lo co va khong co so seri");
                }
                string sql = String.Format("SELECT A.* FROM V_HIS_IMP_MEST_MATERIAL A WHERE A.SERIAL_NUMBER = '{0}' AND A.IS_DELETE = 0 ", serialNumber);
                LogSystem.Info("sql: " + sql);
                List<V_HIS_IMP_MEST_MATERIAL> exists = DAOWorker.SqlDAO.GetSql<V_HIS_IMP_MEST_MATERIAL>(sql);

                if (!IsNotNullOrEmpty(exists))
                {
                    return true;
                }
                V_HIS_IMP_MEST_MATERIAL import = exists.FirstOrDefault(o => o.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT);
                if (import == null)
                {
                    import = exists.Where(o => o.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT && o.IMP_TIME > expTime).FirstOrDefault();
                }
                if (import != null)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_VTTSDCoSeriDaTonTaiPhieuNhap, import.SERIAL_NUMBER, MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMediStock_MaPhieuNhap, param.LanguageCode), import.IMP_MEST_CODE);
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
            this.hisExpMestMaterialIncreaseThAmount.RollbackData();
            this.hisImpMestMaterialCreate.RollbackData();
        }
    }
}
