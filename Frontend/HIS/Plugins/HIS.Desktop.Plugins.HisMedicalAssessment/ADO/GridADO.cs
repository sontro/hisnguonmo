using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMedicalAssessment.ADO
{
    public class GridADO
    {
        public long ID { get; set; }
        public string LOGINNAME { get; set; }
        public string USERNAME { get; set; }
        public bool IS_ABS { get; set; }
        public bool IS_ON_BE { get; set; }
        public int ActionType { get; set; }
        public string DISAGREE_REASON { get; set; }
        public GridADO()
        {
            try
            {
                ActionType = 1;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
