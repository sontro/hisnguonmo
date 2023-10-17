using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using MOS.MANAGER.HisSereServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServExt
{
    class HisSereServExtUpdateForEmr : BusinessBase
    {
        private HisSereServExtCreate hisSereServExtCreate;
		private HisSereServExtUpdate hisSereServExtUpdate;

        internal HisSereServExtUpdateForEmr()
            : base()
        {
            this.Init();
        }

        internal HisSereServExtUpdateForEmr(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
			this.hisSereServExtUpdate = new HisSereServExtUpdate(param);
        }
		
        internal bool Run(UpdateForEmrSDO data)
        {
            bool result = false;
            try
            {
                HIS_SERE_SERV ss = null;
                HisSereServCheck ssChecker = new HisSereServCheck(param);
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && ssChecker.VerifyId(data.SERE_SERV_ID, ref ss);
                if (valid)
                {
                    HIS_SERE_SERV_EXT ext = new HisSereServExtGet().GetBySereServId(data.SERE_SERV_ID);
                    if (ext == null)
                    {
                        ext = new HIS_SERE_SERV_EXT();
                        ext.BEGIN_TIME = data.BEGIN_TIME;
                        ext.END_TIME = data.END_TIME;
                        
                        HisSereServExtUtil.SetTdl(ext, ss);

                        if (!this.hisSereServExtCreate.Create(ext))
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu tao moi thong tin dich vu");
                        }
                    }
                    else
                    {
                        ext.BEGIN_TIME = data.BEGIN_TIME;
                        ext.END_TIME = data.END_TIME;
                        if (!this.hisSereServExtUpdate.Update(ext, false))
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu cap nhat thong tin dich vu");
                        }
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisSereServExtCreate.RollbackData();
            this.hisSereServExtUpdate.RollbackData();
        }
    }
}