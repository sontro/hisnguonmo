using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;
using HIS.Desktop.Plugins.ConnectionTest.ADO;
using HIS.Desktop.Utility;
using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConnectionTest
{
    public partial class UC_ConnectionTest : UserControlBase
    {

        private bool IsAllSelected(TreeList tree)
        {
            return tree.GetAllCheckedNodes().Count > 0 && tree.GetAllCheckedNodes().Count == tree.AllNodesCount;
        }

        private bool IsAllSelected(GridView grid)
        {
            List<LisSampleADO> data = null;
            if (grid.DataSource != null)
            {
                data = (List<LisSampleADO>)grid.DataSource;
            }

            return data != null && data.Count == data.Count(o => o.IsCheck);
        }

        protected void DrawCheckBox(GraphicsCache cache, RepositoryItemCheckEdit edit, Rectangle r, bool Checked)
        {
            DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo info;
            DevExpress.XtraEditors.Drawing.CheckEditPainter painter;
            DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs args;
            info = edit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
            painter = edit.CreatePainter() as DevExpress.XtraEditors.Drawing.CheckEditPainter;
            info.EditValue = Checked;
            info.Bounds = r;
            info.CalcViewInfo();
            args = new DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs(info, cache, r);
            painter.Draw(args);
        }

        private void EmbeddedCheckBoxChecked(TreeList tree)
        {
            try
            {
                if (IsAllSelected(tree))
                {
                    tree.BeginUpdate();
                    tree.NodesIterator.DoOperation(new UnSelectNodeOperation());
                    tree.EndUpdate();
                }
                else
                {
                    tree.BeginUpdate();
                    tree.NodesIterator.DoOperation(new SelectNodeOperation());
                    tree.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void EmbeddedCheckBoxChecked(GridView grid)
        {
            try
            {
                grid.BeginUpdate();
                if (IsAllSelected(grid))
                {
                    List<LisSampleADO> data = null;
                    if (grid != null)
                    {
                        data = (List<LisSampleADO>)grid.DataSource;
                    }

                    if (data != null && data.Count > 0)
                    {
                        data.ForEach(o => o.IsCheck = false);
                    }
                }
                else
                {
                    List<LisSampleADO> data = null;
                    if (grid != null)
                    {
                        data = (List<LisSampleADO>)grid.DataSource;
                    }

                    if (data != null && data.Count > 0)
                    {
                        data.ForEach(o => o.IsCheck = true);
                    }
                }

                grid.EndUpdate();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        class SelectNodeOperation : TreeListOperation
        {
            public override void Execute(TreeListNode node)
            {
                node.Checked = true;
            }
        }

        class UnSelectNodeOperation : TreeListOperation
        {
            public override void Execute(TreeListNode node)
            {
                node.Checked = false;
            }
        }
    }
}
