using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServFile;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServExt
{
    partial class HisSereServExtSetInstructionNote : BusinessBase
    {
        private HisSereServExtCreate hisSereServExtCreate;
		private HisSereServExtUpdate hisSereServExtUpdate;
		
		internal HisSereServExtSetInstructionNote()
            : base()
        {

        }

        internal HisSereServExtSetInstructionNote(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
			this.hisSereServExtUpdate = new HisSereServExtUpdate(param);
        }
		
        internal bool Run(HIS_SERE_SERV_EXT data, ref HIS_SERE_SERV_EXT resultData)
        {
            bool result = false;
            try
            {
                HIS_SERE_SERV ss = null;
                HisSereServCheck ssChecker = new HisSereServCheck(param);
                bool valid = true;
                valid = valid && data != null;
                valid = valid && ssChecker.VerifyId(data.SERE_SERV_ID, ref ss);

                if (valid)
                {
                    HIS_SERE_SERV_EXT ext = new HisSereServExtGet().GetBySereServId(data.SERE_SERV_ID);
                    if (ext == null)
                    {
                        ext = new HIS_SERE_SERV_EXT();
                        ext.INSTRUCTION_NOTE = data.INSTRUCTION_NOTE;
                        ext.SERE_SERV_ID = data.SERE_SERV_ID;
                        
                        HisSereServExtUtil.SetTdl(ext, ss);

                        if (!this.hisSereServExtCreate.Create(ext))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        ext.INSTRUCTION_NOTE = data.INSTRUCTION_NOTE;
                        if (!this.hisSereServExtUpdate.Update(ext, false))
                        {
                            return false;
                        }
                    }
                    resultData = ext;
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
