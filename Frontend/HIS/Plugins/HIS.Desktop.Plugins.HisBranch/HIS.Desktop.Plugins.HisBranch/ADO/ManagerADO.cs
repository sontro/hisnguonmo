using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBranch.ADO
{
	public class ManagerADO
    {
		public string LOGINNAME { get; set; }

		public string USERNAME { get; set; }

		public ManagerADO()
		{
		}

		public ManagerADO(ACS_USER data)
		{
			if (data != null)
			{
				LOGINNAME = data.LOGINNAME;
				USERNAME = data.USERNAME;
			}
		}
	}
}
