using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Desktop.Common.Modules;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Base
{
    internal class ModuleADO : Module
    {
        public string ModuleGroupCode { get; set; }
        public string ModuleGroupName { get; set; }
        public List<ModuleADO> ChildrenADO { get; set; }
        public List<long> RoomTypeIds { get; set; }
        internal ModuleADO()
            : base()
        {
        }

        internal ModuleADO(Module data)
            : base()
        {
            this.Children = data.Children;
            this.Description = data.Description;
            this.ExtensionInfo = data.ExtensionInfo;
            this.Icon = data.Icon;
            this.ImageIndex = data.ImageIndex;
            this.IsPlugin = data.IsPlugin;
            this.leaf = data.leaf;
            this.ModuleCode = data.ModuleCode;
            this.ModuleLink = data.ModuleLink;
            this.ModuleTypeId = data.ModuleTypeId;
            this.NumberOrder = data.NumberOrder;
            this.PageGroupCaption = data.PageGroupCaption;
            this.PageGroupName = data.PageGroupName;
            this.ModuleGroupCode = data.PageGroupCaption;
            this.ModuleGroupName = data.PageGroupName;
            this.ParentCode = data.ParentCode;
            this.PluginInfo = data.PluginInfo;
            this.RoomId = data.RoomId;
            this.IsNotShowDialog = data.IsNotShowDialog;
                        
            if (data.RoomTypeId > 0)
            {
                this.RoomTypeId = data.RoomTypeId;
            }
            else
            {
                this.RoomTypeIds = DATA.RoomTypeModules.Where(o => o.MODULE_LINK == this.ModuleLink).Select(o => o.ROOM_TYPE_ID).Distinct().ToList();
                if (this.RoomTypeIds != null && this.RoomTypeIds.Count == 1)
                    this.RoomTypeId = this.RoomTypeIds[0];
            }
            this.text = data.text;
            this.Visible = data.Visible;
        }
    }
}
