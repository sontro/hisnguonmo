using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Reusable
{
    class HisImpMestReusableCreate : BusinessBase
    {

        private ImpMestProcessor impMestProcessor;
        private MaterialProcessor materialProcessor;

        private HIS_IMP_MEST recentImpMest = null;

        internal HisImpMestReusableCreate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestReusableCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.impMestProcessor = new ImpMestProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
        }

        internal bool Run(HisImpMestReuseSDO data, ref HisImpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                if (this.ValidInputData(data))
                {
                    if (!this.impMestProcessor.Run(data, ref recentImpMest))
                    {
                        throw new Exception("impMestProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.materialProcessor.Run(data, recentImpMest))
                    {
                        throw new Exception("materialProcessor. Ket thuc nghiep vu");
                    }
                    result = true;
                    this.PassResult(ref resultData);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private bool ValidInputData(HisImpMestReuseSDO data)
        {
            bool valid = true;
            try
            {
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNullOrEmpty(data.MaterialReuseSDOs);
                valid = valid && IsGreaterThanZero(data.MediStockId);
                valid = valid && IsGreaterThanZero(data.RequestRoomId);
                if (!valid)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    LogSystem.Error("Thieu thong tin bat buoc");
                    return false;
                }
                foreach (var sdo in data.MaterialReuseSDOs)
                {
                    valid = valid && IsNotNull(sdo);
                    valid = valid && IsGreaterThanZero(sdo.MaterialId);
                    valid = valid && IsGreaterThanZero(sdo.ReusCount);
                    valid = valid && !String.IsNullOrWhiteSpace(sdo.SerialNumber);
                }
                if (!valid)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    LogSystem.Error("Thieu thong tin bat buoc. MaterialReus");
                    return false;
                }
                WorkPlaceSDO wpsdo = null;
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref wpsdo);
                if (!valid)
                {
                    return false;
                }
                if (wpsdo.MediStockId != data.MediStockId)
                {
                    var stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == data.MediStockId);
                    string name = stock != null ? stock.MEDI_STOCK_NAME : "";
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.BanDangKhongLamViecTaiKho, name);
                    return false;
                }
                var Groups = data.MaterialReuseSDOs.Select(s => s.SerialNumber).GroupBy(g => g).ToList();
                var duplicate = Groups.Where(o => o.Count() >= 2).Select(s => s.FirstOrDefault()).ToList();
                if (IsNotNullOrEmpty(duplicate))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_TonTaiSoSeriTrungNhau, String.Join(";", duplicate));
                    return false;
                }
                List<string> notImpMests = new List<string>();
                List<string> notImporteds = new List<string>();
                List<string> beanInStocks = new List<string>();
                List<string> notExpMests = new List<string>();
                foreach (var sdo in data.MaterialReuseSDOs)
                {
                    HisImpMestMaterialViewFilterQuery vImpMaterialFilter = new HisImpMestMaterialViewFilterQuery();
                    vImpMaterialFilter.SERIAL_NUMBER__EXACT = sdo.SerialNumber;
                    List<V_HIS_IMP_MEST_MATERIAL> exists = new HisImpMestMaterialGet().GetView(vImpMaterialFilter);
                    if (!IsNotNullOrEmpty(exists))
                    {
                        notImpMests.Add(sdo.SerialNumber);
                    }
                    else
                    {
                        var notImported = exists.FirstOrDefault(o => o.IMP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT);
                        if (notImported != null)
                        {
                            var stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == notImported.MEDI_STOCK_ID);
                            string name = stock != null ? stock.MEDI_STOCK_NAME : "";
                            notImporteds.Add(string.Format("{0}({1}:{2},{3}:{4})", sdo.SerialNumber, MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMediStock_MaPhieuNhap, param.LanguageCode),
                                notImported.IMP_MEST_CODE, MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMediStock_Kho, param.LanguageCode), name));
                            continue;
                        }
                        if (exists.Exists(e => e.MATERIAL_ID != sdo.MaterialId))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("MaterialId invalid: " + LogUtil.TraceData("SDO", sdo));
                        }

                        HisMaterialBeanFilterQuery beanFilter = new HisMaterialBeanFilterQuery();
                        beanFilter.SERIAL_NUMBER = sdo.SerialNumber;
                        beanFilter.HAS_MEDI_STOCK_ID = true;
                        List<HIS_MATERIAL_BEAN> existBeans = new HisMaterialBeanGet().Get(beanFilter);
                        if (IsNotNullOrEmpty(existBeans))
                        {
                            var stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == existBeans[0].MEDI_STOCK_ID);
                            string name = stock != null ? stock.MEDI_STOCK_NAME : "";
                            beanInStocks.Add(String.Format("{0}({1}:{2})", sdo.SerialNumber, MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMediStock_Kho, param.LanguageCode), name));
                            continue;
                        }

                        HisExpMestMaterialViewFilterQuery vExpFilter = new HisExpMestMaterialViewFilterQuery();
                        vExpFilter.SERIAL_NUMBER__EXACT = sdo.SerialNumber;
                        vExpFilter.IS_EXPORT = true;
                        vExpFilter.EXP_MEST_TYPE_IDs = new List<long>()
                        {
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT,
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM,
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK,
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT
                        };
                        List<V_HIS_EXP_MEST_MATERIAL> existExps = new HisExpMestMaterialGet().GetView(vExpFilter);
                        if (!IsNotNullOrEmpty(existExps))
                        {
                            notExpMests.Add(sdo.SerialNumber);
                        }
                    }
                }

                if (IsNotNullOrEmpty(notImpMests))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_CacVTTSDCoSoSeriSauKhongTonTaiPhieuNhap, String.Join(",", notImpMests));
                    return false;
                }

                if (IsNotNullOrEmpty(notImporteds))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_CacVTTSDCoSoSeriSauTonTaiPhieuNhapChuaThucNhap, String.Join(",", notImporteds));
                    return false;
                }

                if (IsNotNullOrEmpty(beanInStocks))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_CacVTTSDCoSoSeriSauChuaDuocXuat, String.Join(",", beanInStocks));
                    return false;
                }

                if (IsNotNullOrEmpty(notExpMests))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_CacVTTSDCoSoSeriSauKhongCoPhieuXuatChoBenhNhan, String.Join(",", notExpMests));
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

        private void PassResult(ref HisImpMestResultSDO data)
        {
            data = new HisImpMestResultSDO();
            //truy van lai de lay du lieu IMP_MEST_CODE tra ve client, phuc vu in
            data.ImpMest = new HisImpMestGet().GetViewById(this.recentImpMest.ID);
            data.ImpMaterials = new HisImpMestMaterialGet().GetViewByImpMestId(this.recentImpMest.ID);
        }

        internal void Rollback()
        {
            try
            {
                this.materialProcessor.Rollback();
                this.impMestProcessor.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
