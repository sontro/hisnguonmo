using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Blood.Update
{
    class ExpMestBltyReqProcessor : BusinessBase
    {
        private HisExpMestBltyReqCreate hisExpMestBltyReqCreate;
        private HisExpMestBltyReqUpdate hisExpMestBltyReqUpdate;
        private HisExpMestBltyReqTruncate hisExpMestBltyReqTruncate;

        internal ExpMestBltyReqProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestBltyReqCreate = new HisExpMestBltyReqCreate(param);
            this.hisExpMestBltyReqTruncate = new HisExpMestBltyReqTruncate(param);
            this.hisExpMestBltyReqUpdate = new HisExpMestBltyReqUpdate(param);
        }

        internal bool Run(PatientBloodPresSDO data, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_BLTY_REQ> resultData)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_BLTY_REQ> listReq = new List<HIS_EXP_MEST_BLTY_REQ>();
                List<HIS_EXP_MEST_BLTY_REQ> listCreate = new List<HIS_EXP_MEST_BLTY_REQ>();
                List<HIS_EXP_MEST_BLTY_REQ> listUpdate = new List<HIS_EXP_MEST_BLTY_REQ>();
                List<HIS_EXP_MEST_BLTY_REQ> listDelete = new List<HIS_EXP_MEST_BLTY_REQ>();
                List<HIS_EXP_MEST_BLTY_REQ> beforeUpdates = new List<HIS_EXP_MEST_BLTY_REQ>();

                List<HIS_EXP_MEST_BLTY_REQ> oldExpMestBltyReqs = new HisExpMestBltyReqGet().GetByExpMestId(expMest.ID);
                Mapper.CreateMap<HIS_EXP_MEST_BLTY_REQ, HIS_EXP_MEST_BLTY_REQ>();
                List<HIS_EXP_MEST_BLTY_REQ> createList = Mapper.Map<List<HIS_EXP_MEST_BLTY_REQ>>(data.ExpMestBltyReqs);

                foreach (var req in createList)
                {
                    if (req.ID > 0)
                    {
                        HIS_EXP_MEST_BLTY_REQ oldReq = oldExpMestBltyReqs != null ? oldExpMestBltyReqs.FirstOrDefault(o => o.ID == req.ID) : null;
                        if (oldReq == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("HIS_EXP_MEST_BLTY_REQ.ID Invalid: " + req.ID);
                        }
                        if (req.BLOOD_TYPE_ID != oldReq.BLOOD_TYPE_ID)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong cho phep sua Loai mau NewID: " + req.BLOOD_TYPE_ID + "; OldID: " + oldReq.BLOOD_TYPE_ID);
                        }
                        if (req.AMOUNT != oldReq.AMOUNT || req.PATIENT_TYPE_ID != oldReq.PATIENT_TYPE_ID)
                        {
                            listUpdate.Add(req);
                            beforeUpdates.Add(oldReq);
                        }
                    }
                    else
                    {
                        listCreate.Add(req);
                    }
                    req.EXP_MEST_ID = expMest.ID;
                    req.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                    listReq.Add(req);
                }

                listDelete = oldExpMestBltyReqs != null ? oldExpMestBltyReqs.Where(o => !listReq.Exists(e => e.ID == o.ID)).ToList() : null;

                if (IsNotNullOrEmpty(listUpdate))
                {
                    if (!this.hisExpMestBltyReqUpdate.UpdateList(listUpdate, beforeUpdates))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                }
                if (IsNotNullOrEmpty(listDelete))
                {
                    if (!this.hisExpMestBltyReqTruncate.TruncateList(listDelete))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                }
                if (IsNotNullOrEmpty(listCreate))
                {
                    if (!this.hisExpMestBltyReqCreate.CreateList(listCreate))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                }
                resultData = listReq;
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

        internal void Rollback()
        {
            try
            {
                this.hisExpMestBltyReqCreate.RollbackData();
                this.hisExpMestBltyReqUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
