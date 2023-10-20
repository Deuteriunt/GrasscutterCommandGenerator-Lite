/**
 *  Grasscutter Tools
 *  Copyright (C) 2023 jie65535
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License as published
 *  by the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 *
 **/

using System;
using System.Linq;
using System.Windows.Forms;
using GrasscutterTools.Forms;
using GrasscutterTools.Game;
using GrasscutterTools.Properties;

namespace GrasscutterTools.Pages
{
    internal partial class PageSettings : BasePage
    {
        private bool isChanged;

        public override string Text => Resources.PageSettingsTitle;

        public PageSettings()
        {
            InitializeComponent();
            if (DesignMode) return;

            // 玩家UID
            NUDUid.Value = Settings.Default.Uid;
            NUDUid.ValueChanged += (o, e) => Settings.Default.Uid = NUDUid.Value;

            // 是否包含UID
            ChkIncludeUID.Checked = Settings.Default.IsIncludeUID;
            ChkIncludeUID.CheckedChanged += (o, e) => Settings.Default.IsIncludeUID = ChkIncludeUID.Checked;


            // 置顶
            ChkTopMost.Checked = FormMain.Instance.TopMost = Settings.Default.IsTopMost;
            ChkTopMost.CheckedChanged += (o, e) => Settings.Default.IsTopMost = FormMain.Instance.TopMost = ChkTopMost.Checked;

            // 命令版本初始化
            if (Version.TryParse(Settings.Default.CommandVersion, out Version current))
                CommandVersion.Current = current;
            CmbGcVersions.DataSource = CommandVersion.List.Select(it => it.ToString(3)).ToList();
            CmbGcVersions.SelectedIndex = Array.IndexOf(CommandVersion.List, CommandVersion.Current);
            CmbGcVersions.SelectedIndexChanged += (o, e) => CommandVersion.Current = CommandVersion.List[CmbGcVersions.SelectedIndex];
            CommandVersion.VersionChanged += (o, e) => Settings.Default.CommandVersion = CommandVersion.Current.ToString(3);

        }
    }
}