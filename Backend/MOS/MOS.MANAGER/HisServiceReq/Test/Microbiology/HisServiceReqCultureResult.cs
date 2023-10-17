using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq.Update.Status;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.Microbiology
{
    class HisServiceReqCultureResult : BusinessBase
    {
        private HisSereServExtCreate sereServExtCreate;
        private HisSereServExtUpdate sereServExtUpdate;

        internal HisServiceReqCultureResult()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqCultureResult(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.sereServExtCreate = new HisSereServExtCreate(param);
            this.sereServExtUpdate = new HisSereServExtUpdate(param);
        }

        internal bool Run(CultureResultTDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERVICE_REQ raw = null;
                List<HIS_SERE_SERV> sereServs = null;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && !String.IsNullOrWhiteSpace(data.ServiceReqCode);
                valid = valid && IsNotNullOrEmpty(data.TestIndexDatas);
                valid = valid && this.HasServiceReq(data.ServiceReqCode, ref raw, ref sereServs);
                valid = valid && checker.HasExecute(raw);
                if (valid)
                {
                    List<long> sereServIds = sereServs.Select(s => s.ID).ToList();
                    List<HIS_SERE_SERV_EXT> sereServExts = new HisSereServExtGet().GetBySereServIds(sereServIds);

                    List<HIS_SERE_SERV_EXT> listCreate = new List<HIS_SERE_SERV_EXT>();
                    List<HIS_SERE_SERV_EXT> listUpdate = new List<HIS_SERE_SERV_EXT>();
                    List<HIS_SERE_SERV_EXT> listBefore = new List<HIS_SERE_SERV_EXT>();
                    Mapper.CreateMap<HIS_SERE_SERV_EXT, HIS_SERE_SERV_EXT>();

                    foreach (CultureDetailTDO dto in data.TestIndexDatas)
                    {
                        HIS_SERE_SERV ss = sereServs.FirstOrDefault(o => o.TDL_SERVICE_CODE == dto.TestIndexCode);
                        if (ss == null)
                        {
                            LogSystem.Warn("Khong lay duoc SereServ" + LogUtil.TraceData("Dto", dto));
                            continue;
                        }

                        HIS_SERE_SERV_EXT exist = sereServExts != null ? sereServExts.FirstOrDefault(o => o.SERE_SERV_ID == ss.ID) : null;
                        if (exist != null)
                        {
                            listBefore.Add(Mapper.Map<HIS_SERE_SERV_EXT>(exist));
                            exist.MICROCOPY_RESULT = dto.KetQuaSoi;
                            exist.IMPLANTION_RESULT = dto.KetQuaCay;
                            listUpdate.Add(exist);
                        }
                        else
                        {
                            exist = new HIS_SERE_SERV_EXT();
                            exist.MICROCOPY_RESULT = dto.KetQuaSoi;
                            exist.IMPLANTION_RESULT = dto.KetQuaCay;
                            exist.SERE_SERV_ID = ss.ID;
                            HisSereServExtUtil.SetTdl(exist, ss);
                            listCreate.Add(exist);
                        }
                    }

                    if (IsNotNullOrEmpty(listCreate))
                    {
                        if (!this.sereServExtCreate.CreateList(listCreate))
                        {
                            throw new Exception("sereServExtCreate. Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(listUpdate))
                    {
                        if (!this.sereServExtUpdate.UpdateList(listUpdate, listBefore))
                        {
                            throw new Exception("sereServExtUpdate. Ket thuc nghiep vu");
                        }
                    }
                    if (raw.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                    {
                        HIS_SERVICE_REQ serviceReqRaw = null;
                        if (!new HisServiceReqUpdateStart().Start(raw, false, ref serviceReqRaw, Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName(), Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName()))
                        {
                            LogSystem.Warn("Tu dong cap nhat trang thai His_service_req tu 'y/c' sang 'dang thuc hien' that bai." + LogUtil.TraceData("serviceReq", raw));
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
            this.sereServExtUpdate.RollbackData();
            this.sereServExtCreate.RollbackData();
        }
    }
}
