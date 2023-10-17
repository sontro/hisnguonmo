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

namespace MOS.MANAGER.HisImpMest.Moba.Pres
{
    class ImpMestMaterialProcessor : BusinessBase
    {
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;
        private HisExpMestMaterialIncreaseThAmount hisExpMestMaterialIncreaseThAmount;

        internal ImpMestMaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
            this.hisExpMestMaterialIncreaseThAmount = new HisExpMestMaterialIncreaseThAmount(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HisMobaPresMaterialSDO> mobaPresMaterials, List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials, List<HIS_SERE_SERV> hisSereServs, ref List<HIS_IMP_MEST_MATERIAL> hisImpMestMaterials, ref List<HIS_SERE_SERV> sereServUpdates)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(mobaPresMaterials))
                {
                    List<HIS_IMP_MEST_MATERIAL> listCreate = new List<HIS_IMP_MEST_MATERIAL>();

                    List<HIS_MATERIAL> hisMaterials = new HisMaterialGet().GetByIds(hisExpMestMaterials.Select(s => s.MATERIAL_ID.Value).Distinct().ToList());

                    Dictionary<long, decimal> dicIncrease = new Dictionary<long, decimal>();

                    foreach (var mobaItem in mobaPresMaterials)
                    {
                        HIS_EXP_MEST_MATERIAL expMestMaterial = hisExpMestMaterials.FirstOrDefault(o => o.ID == mobaItem.ExpMestMaterialId);
                        if (expMestMaterial == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc HIS_EXP_MEST_MATERIAL theo id: " + mobaItem.ExpMestMaterialId);
                        }

                        if (!String.IsNullOrWhiteSpace(expMestMaterial.SERIAL_NUMBER) && !this.CheckExitsImpMaterialReus(expMestMaterial.SERIAL_NUMBER, expMestMaterial.EXP_TIME.Value))
                        {
                            throw new Exception("Vat tu tai su dung ton tai phieu nhap. SerialNumber: " + expMestMaterial.SERIAL_NUMBER);
                        }

                        //Kiem tra so luong kha dung thu hoi
                        if ((expMestMaterial.AMOUNT - (expMestMaterial.TH_AMOUNT ?? 0)) < mobaItem.Amount)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMobaImpMest_SoLuongYeuCauThuHoiVuotQuaSoLuongKhaDung);
                            return false;
                        }

                        //Lay ra tat cac cac SereServ tuong uong voi ExpMestMaterial
                        List<HIS_SERE_SERV> sereServs = null;
                        if (hisSereServs.Exists(e => e.SERVICE_REQ_ID == expMestMaterial.TDL_SERVICE_REQ_ID && e.MATERIAL_ID.HasValue && e.EXP_MEST_MATERIAL_ID.HasValue))
                        {
                            sereServs = hisSereServs.Where(o => o.SERVICE_REQ_ID == expMestMaterial.TDL_SERVICE_REQ_ID && o.EXP_MEST_MATERIAL_ID == expMestMaterial.ID).ToList();
                        }
                        else
                        {
                            sereServs = hisSereServs
                            .Where(o => o.SERVICE_REQ_ID == expMestMaterial.TDL_SERVICE_REQ_ID
                                && o.MATERIAL_ID == expMestMaterial.MATERIAL_ID
                                && o.IS_EXPEND == expMestMaterial.IS_EXPEND
                                && o.PATIENT_TYPE_ID == expMestMaterial.PATIENT_TYPE_ID
                                && o.IS_OUT_PARENT_FEE == expMestMaterial.IS_OUT_PARENT_FEE
                                && o.STENT_ORDER == expMestMaterial.STENT_ORDER
                                && o.EQUIPMENT_SET_ID == expMestMaterial.EQUIPMENT_SET_ID
                                )
                            .ToList();
                        }

                        if (!IsNotNullOrEmpty(sereServs))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Khong lay duoc HIS_SERE_SERV tuong ung voi HIS_EXP_MEST_MATERIAL" + LogUtil.TraceData("expMestMaterial", expMestMaterial));
                        }

                        //Oder theo so luong giam dan de update SereServ so luong nho nhat
                        sereServs = sereServs.OrderByDescending(o => o.AMOUNT).ToList();

                        //Kiem tra so luong trong sereServ co du thu hoi hay khong
                        if (mobaItem.Amount > sereServs.Sum(s => s.AMOUNT))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMobaImpMest_SoLuongYeuCauThuHoiVuotQuaSoLuongKhaDung);
                            return false;
                        }

                        //Cong vao tranh truong hop frontend gui len 2 dong cua cung 1 expMestMaterialID
                        expMestMaterial.TH_AMOUNT = (expMestMaterial.TH_AMOUNT ?? 0) + mobaItem.Amount;

                        if (dicIncrease.ContainsKey(expMestMaterial.ID))
                        {
                            dicIncrease[expMestMaterial.ID] += mobaItem.Amount;
                        }
                        else
                        {
                            dicIncrease[expMestMaterial.ID] = mobaItem.Amount;
                        }

                        decimal amount = mobaItem.Amount;
                        foreach (var ss in sereServs)
                        {
                            if (amount <= 0) break;

                            if (ss.AMOUNT <= 0)
                                continue;
                            if (ss.AMOUNT >= amount)
                            {
                                ss.AMOUNT -= amount;
                                sereServUpdates.Add(ss);
                                break;
                            }
                            else
                            {
                                ss.AMOUNT = 0;
                                amount -= ss.AMOUNT;
                                sereServUpdates.Add(ss);
                            }
                        }

                        HIS_IMP_MEST_MATERIAL impMestMaterial = new HIS_IMP_MEST_MATERIAL();
                        impMestMaterial.AMOUNT = mobaItem.Amount;
                        impMestMaterial.MATERIAL_ID = expMestMaterial.MATERIAL_ID.Value;
                        impMestMaterial.IMP_MEST_ID = impMest.ID;
                        impMestMaterial.TH_EXP_MEST_MATERIAL_ID = expMestMaterial.ID;
                        impMestMaterial.SERIAL_NUMBER = expMestMaterial.SERIAL_NUMBER;
                        impMestMaterial.REMAIN_REUSE_COUNT = expMestMaterial.REMAIN_REUSE_COUNT;
                        listCreate.Add(impMestMaterial);
                    }
                    if (!this.hisImpMestMaterialCreate.CreateList(listCreate))
                    {
                        throw new Exception("Tao HIS_IMP_MEST_MATERIAL that bai");
                    }
                    if (!this.hisExpMestMaterialIncreaseThAmount.Run(dicIncrease))
                    {
                        throw new Exception("Update ThAmount cho HIS_EXP_MEST_MATERIAL  that bai");
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
            try
            {
                this.hisExpMestMaterialIncreaseThAmount.RollbackData();
                this.hisImpMestMaterialCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
