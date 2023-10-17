using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.Microbiology
{
    class HisServiceReqAntibioticMapResult : BusinessBase
    {
        private HisSereServTeinCreate sereServTeinCreate;
        private HisSereServTeinUpdate sereServTeinUpdate;

        internal HisServiceReqAntibioticMapResult()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqAntibioticMapResult(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.sereServTeinCreate = new HisSereServTeinCreate(param);
            this.sereServTeinUpdate = new HisSereServTeinUpdate(param);
        }

        internal bool Run(AntibioticMapResultTDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERVICE_REQ raw = null;
                List<HIS_SERE_SERV> sereServs = null;

                HisServiceReqCheck checker = new HisServiceReqCheck(param);

                valid = valid && this.VerifyRequireField(data);
                valid = valid && this.HasServiceReq(data.ServiceReqCode, ref raw, ref sereServs);
                valid = valid && checker.HasExecute(raw);

                if (valid)
                {
                    List<long> sereServIds = sereServs.Select(s => s.ID).ToList();
                    List<HIS_SERE_SERV_TEIN> sereServTeins = new HisSereServTeinGet().GetBySereServIds(sereServIds);
                    sereServTeins = sereServTeins != null ? sereServTeins.OrderByDescending(o => o.ID).ToList() : null;
                    List<HIS_SERE_SERV_TEIN> listCreate = new List<HIS_SERE_SERV_TEIN>();
                    List<HIS_SERE_SERV_TEIN> listUpdate = new List<HIS_SERE_SERV_TEIN>();
                    List<HIS_SERE_SERV_TEIN> listBefore = new List<HIS_SERE_SERV_TEIN>();
                    Mapper.CreateMap<HIS_SERE_SERV_TEIN, HIS_SERE_SERV_TEIN>();

                    foreach (AntibioticMapDetailTDO detail in data.TestIndexDatas)
                    {
                        HIS_SERE_SERV ss = sereServs.FirstOrDefault(o => o.TDL_SERVICE_CODE == detail.TestIndexCode);
                        if (ss == null)
                        {
                            LogSystem.Warn("Khong lay duoc SereServ" + LogUtil.TraceData("DetailTDO", detail));
                            continue;
                        }
                        List<HIS_SERE_SERV_TEIN> ssTeins = sereServTeins != null ? sereServTeins.Where(o => o.SERE_SERV_ID == ss.ID && o.BACTERIUM_CODE == detail.MaViKhuan).ToList() : null;
                        if (!IsNotNullOrEmpty(detail.DanhSachDeKhang) && !IsNotNullOrEmpty(detail.DanhSachKhangSinh))
                        {
                            HIS_SERE_SERV_TEIN vk = ssTeins != null ? ssTeins.FirstOrDefault() : null;
                            if (vk != null)
                            {
                                listBefore.Add(Mapper.Map<HIS_SERE_SERV_TEIN>(vk));
                                vk.BACTERIUM_AMOUNT = detail.SoLuongViKhuan.HasValue ? detail.SoLuongViKhuan.ToString() : null;
                                vk.BACTERIUM_CODE = detail.MaViKhuan;
                                vk.BACTERIUM_DENSITY = detail.MatDoViKhuan.HasValue ? detail.MatDoViKhuan.ToString() : null;
                                vk.BACTERIUM_NAME = detail.TenViKhuan;
                                vk.BACTERIUM_NOTE = detail.GhiChuViKhuan;
                                listUpdate.Add(vk);
                            }
                            else
                            {
                                vk = new HIS_SERE_SERV_TEIN();
                                vk.BACTERIUM_AMOUNT = detail.SoLuongViKhuan.HasValue ? detail.SoLuongViKhuan.ToString() : null;
                                vk.BACTERIUM_CODE = detail.MaViKhuan;
                                vk.BACTERIUM_DENSITY = detail.MatDoViKhuan.HasValue ? detail.MatDoViKhuan.ToString() : null;
                                vk.BACTERIUM_NAME = detail.TenViKhuan;
                                vk.BACTERIUM_NOTE = detail.GhiChuViKhuan;
                                vk.SERE_SERV_ID = ss.ID;
                                vk.TDL_TREATMENT_ID = ss.TDL_TREATMENT_ID;
                                vk.TDL_SERVICE_REQ_ID = ss.SERVICE_REQ_ID;
                                listCreate.Add(vk);
                            }
                        }
                        else
                        {
                            if (IsNotNullOrEmpty(detail.DanhSachDeKhang))
                            {
                                foreach (ResitanceResultTDO resi in detail.DanhSachDeKhang)
                                {
                                    HIS_SERE_SERV_TEIN deKhang = ssTeins != null ? ssTeins.FirstOrDefault(o => String.IsNullOrWhiteSpace(o.ANTIBIOTIC_RESISTANCE_CODE) && o.ANTIBIOTIC_RESISTANCE_NAME == resi.TenDeKhang) : null;
                                    if (deKhang != null)
                                    {
                                        listBefore.Add(Mapper.Map<HIS_SERE_SERV_TEIN>(deKhang));
                                        deKhang.VALUE = resi.KetQuaDeKhang;
                                        listUpdate.Add(deKhang);
                                    }
                                    else
                                    {
                                        deKhang = new HIS_SERE_SERV_TEIN();
                                        deKhang.VALUE = resi.KetQuaDeKhang;
                                        deKhang.BACTERIUM_AMOUNT = detail.SoLuongViKhuan.HasValue ? detail.SoLuongViKhuan.ToString() : null;
                                        deKhang.BACTERIUM_CODE = detail.MaViKhuan;
                                        deKhang.BACTERIUM_DENSITY = detail.MatDoViKhuan.HasValue ? detail.MatDoViKhuan.ToString() : null;
                                        deKhang.BACTERIUM_NAME = detail.TenViKhuan;
                                        deKhang.BACTERIUM_NOTE = detail.GhiChuViKhuan;
                                        deKhang.ANTIBIOTIC_RESISTANCE_NAME = resi.TenDeKhang;
                                        deKhang.SERE_SERV_ID = ss.ID;
                                        deKhang.TDL_TREATMENT_ID = ss.TDL_TREATMENT_ID;
                                        deKhang.TDL_SERVICE_REQ_ID = ss.SERVICE_REQ_ID;
                                        listCreate.Add(deKhang);
                                    }
                                }
                            }
                            if (IsNotNullOrEmpty(detail.DanhSachKhangSinh))
                            {
                                foreach (AntibioticResultTDO anti in detail.DanhSachKhangSinh)
                                {
                                    HIS_SERE_SERV_TEIN khangSinh = ssTeins != null ? ssTeins.FirstOrDefault(o => !String.IsNullOrWhiteSpace(o.ANTIBIOTIC_RESISTANCE_CODE) && o.ANTIBIOTIC_RESISTANCE_CODE == anti.MaKhangSinh) : null;
                                    if (khangSinh != null)
                                    {
                                        listBefore.Add(Mapper.Map<HIS_SERE_SERV_TEIN>(khangSinh));
                                        khangSinh.VALUE = anti.KetQua;
                                        khangSinh.SRI_CODE = anti.SRI;
                                        khangSinh.ANTIBIOTIC_RESISTANCE_NAME = anti.TenKhangSinh;
                                        listUpdate.Add(khangSinh);
                                    }
                                    else
                                    {
                                        khangSinh = new HIS_SERE_SERV_TEIN();
                                        khangSinh.VALUE = anti.KetQua;
                                        khangSinh.SRI_CODE = anti.SRI;
                                        khangSinh.BACTERIUM_AMOUNT = detail.SoLuongViKhuan.HasValue ? detail.SoLuongViKhuan.ToString() : null;
                                        khangSinh.BACTERIUM_CODE = detail.MaViKhuan;
                                        khangSinh.BACTERIUM_DENSITY = detail.MatDoViKhuan.HasValue ? detail.MatDoViKhuan.ToString() : null;
                                        khangSinh.BACTERIUM_NAME = detail.TenViKhuan;
                                        khangSinh.BACTERIUM_NOTE = detail.GhiChuViKhuan;
                                        khangSinh.ANTIBIOTIC_RESISTANCE_CODE = anti.MaKhangSinh;
                                        khangSinh.ANTIBIOTIC_RESISTANCE_NAME = anti.TenKhangSinh;
                                        khangSinh.SERE_SERV_ID = ss.ID;
                                        khangSinh.TDL_TREATMENT_ID = ss.TDL_TREATMENT_ID;
                                        khangSinh.TDL_SERVICE_REQ_ID = ss.SERVICE_REQ_ID;
                                        listCreate.Add(khangSinh);
                                    }
                                }
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(listCreate) && !this.sereServTeinCreate.CreateList(listCreate))
                    {
                        throw new Exception("sereServTeinCreate. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(listUpdate) && !this.sereServTeinUpdate.UpdateList(listUpdate, listBefore))
                    {
                        throw new Exception("sereServTeinUpdate. Ket thuc nghiep vu");
                    }

                    if (raw.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                    {
                        HIS_SERVICE_REQ serviceReqRaw = null;
                        //tra ket qua xet nghiem ko can verify treatment
                        if (!new HisServiceReqUpdateFinish().Finish(raw, false, ref serviceReqRaw, null, null))
                        {
                            LogSystem.Warn("Tu dong cap nhat trang thai His_service_req sang 'hoan thanh' that bai." + LogUtil.TraceData("hisServiceReq", raw));
                        }
                    }
                    result = true;
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

        internal bool VerifyRequireField(AntibioticMapResultTDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (String.IsNullOrWhiteSpace(data.ServiceReqCode)) throw new ArgumentNullException("data.ServiceReqCode");
                if (!IsNotNullOrEmpty(data.TestIndexDatas)) throw new ArgumentNullException("data.TestIndexDatas");
                foreach (AntibioticMapDetailTDO detail in data.TestIndexDatas)
                {
                    if (String.IsNullOrWhiteSpace(detail.TestIndexCode)) throw new ArgumentNullException("detail.TestIndexCode");
                    if (String.IsNullOrWhiteSpace(detail.MaViKhuan)) throw new ArgumentNullException("detail.MaViKhuan");
                    //if (!IsNotNullOrEmpty(detail.DanhSachDeKhang) && !IsNotNullOrEmpty(detail.DanhSachKhangSinh)) throw new ArgumentNullException("detail.DanhSachDeKhang && detail.DanhSachKhangSinh");
                }
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private bool HasServiceReq(string serviceReqCode, ref HIS_SERVICE_REQ serviceReq, ref List<HIS_SERE_SERV> sereServs)
        {
            bool result = false;
            try
            {
                serviceReq = new HisServiceReqGet().GetByServiceReqCode(serviceReqCode);

                if (serviceReq == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Ko ton tai service_req tuong ung voi serviceReqCode:" + serviceReqCode);
                }

                //Lay du lieu HisSereServ tuong ung voi ServiceReq o tren
                sereServs = new HisSereServGet().GetByServiceReqId(serviceReq.ID);
                if (!IsNotNullOrEmpty(sereServs))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Ko tim thay sereServs tuong ung voi SERVICE_REQ_ID: " + serviceReq.ID);
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
        private void Rollback()
        {
            this.sereServTeinUpdate.RollbackData();
            this.sereServTeinCreate.RollbackData();
        }
    }
}
