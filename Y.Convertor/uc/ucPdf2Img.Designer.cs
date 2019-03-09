namespace Y.Convertor.uc
{
    partial class ucPdf2Img
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTip = new System.Windows.Forms.Label();
            this.btnStartConvert = new MetroSet_UI.Controls.MetroSetButton();
            this.btnContinueAdd = new MetroSet_UI.Controls.MetroDefaultSetButton();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.cboImgType = new MetroSet_UI.Controls.MetroSetComboBox();
            this.metroSetLabel1 = new MetroSet_UI.Controls.MetroSetLabel();
            this.tableFiles = new XPTable.Models.Table();
            this.columnModel = new XPTable.Models.ColumnModel();
            this.tableModel = new XPTable.Models.TableModel();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTip.ForeColor = System.Drawing.Color.LightGray;
            this.lblTip.Location = new System.Drawing.Point(171, 149);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(288, 25);
            this.lblTip.TabIndex = 1;
            this.lblTip.Text = "拖拽或点击选择PDF文件(可多选)";
            this.lblTip.Click += new System.EventHandler(this.lblTip_Click);
            // 
            // btnStartConvert
            // 
            this.btnStartConvert.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnStartConvert.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnStartConvert.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnStartConvert.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartConvert.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnStartConvert.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnStartConvert.HoverTextColor = System.Drawing.Color.White;
            this.btnStartConvert.Location = new System.Drawing.Point(412, 10);
            this.btnStartConvert.Name = "btnStartConvert";
            this.btnStartConvert.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnStartConvert.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnStartConvert.NormalTextColor = System.Drawing.Color.White;
            this.btnStartConvert.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnStartConvert.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnStartConvert.PressTextColor = System.Drawing.Color.White;
            this.btnStartConvert.Size = new System.Drawing.Size(96, 27);
            this.btnStartConvert.Style = MetroSet_UI.Design.Style.Light;
            this.btnStartConvert.StyleManager = null;
            this.btnStartConvert.TabIndex = 2;
            this.btnStartConvert.Text = "开始转换";
            this.btnStartConvert.ThemeAuthor = "Narwin";
            this.btnStartConvert.ThemeName = "MetroLite";
            this.btnStartConvert.Click += new System.EventHandler(this.btnStartConvert_Click);
            // 
            // btnContinueAdd
            // 
            this.btnContinueAdd.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnContinueAdd.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnContinueAdd.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnContinueAdd.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinueAdd.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnContinueAdd.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnContinueAdd.HoverTextColor = System.Drawing.Color.White;
            this.btnContinueAdd.Location = new System.Drawing.Point(285, 10);
            this.btnContinueAdd.Name = "btnContinueAdd";
            this.btnContinueAdd.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnContinueAdd.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnContinueAdd.NormalTextColor = System.Drawing.Color.Black;
            this.btnContinueAdd.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnContinueAdd.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnContinueAdd.PressTextColor = System.Drawing.Color.White;
            this.btnContinueAdd.Size = new System.Drawing.Size(96, 27);
            this.btnContinueAdd.Style = MetroSet_UI.Design.Style.Light;
            this.btnContinueAdd.StyleManager = null;
            this.btnContinueAdd.TabIndex = 3;
            this.btnContinueAdd.Text = "继续添加";
            this.btnContinueAdd.ThemeAuthor = "Narwin";
            this.btnContinueAdd.ThemeName = "MetroLite";
            this.btnContinueAdd.Click += new System.EventHandler(this.btnContinueAdd_Click);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.cboImgType);
            this.panelBottom.Controls.Add(this.metroSetLabel1);
            this.panelBottom.Controls.Add(this.btnStartConvert);
            this.panelBottom.Controls.Add(this.btnContinueAdd);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 300);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(608, 46);
            this.panelBottom.TabIndex = 4;
            // 
            // cboImgType
            // 
            this.cboImgType.AllowDrop = true;
            this.cboImgType.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.cboImgType.BackColor = System.Drawing.Color.Transparent;
            this.cboImgType.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.cboImgType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.cboImgType.CausesValidation = false;
            this.cboImgType.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.cboImgType.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.cboImgType.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.cboImgType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboImgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImgType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.cboImgType.FormattingEnabled = true;
            this.cboImgType.ItemHeight = 20;
            this.cboImgType.Items.AddRange(new object[] {
            "JPG",
            "PNG",
            "GIF",
            "BMP",
            "TIF"});
            this.cboImgType.Location = new System.Drawing.Point(129, 10);
            this.cboImgType.Name = "cboImgType";
            this.cboImgType.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.cboImgType.SelectedItemForeColor = System.Drawing.Color.White;
            this.cboImgType.Size = new System.Drawing.Size(69, 26);
            this.cboImgType.Style = MetroSet_UI.Design.Style.Light;
            this.cboImgType.StyleManager = null;
            this.cboImgType.TabIndex = 4;
            this.cboImgType.ThemeAuthor = "Narwin";
            this.cboImgType.ThemeName = "MetroLite";
            // 
            // metroSetLabel1
            // 
            this.metroSetLabel1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroSetLabel1.Location = new System.Drawing.Point(31, 12);
            this.metroSetLabel1.Name = "metroSetLabel1";
            this.metroSetLabel1.Size = new System.Drawing.Size(103, 23);
            this.metroSetLabel1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetLabel1.StyleManager = null;
            this.metroSetLabel1.TabIndex = 5;
            this.metroSetLabel1.Text = "生成图片格式：";
            this.metroSetLabel1.ThemeAuthor = "Narwin";
            this.metroSetLabel1.ThemeName = "MetroLite";
            // 
            // tableFiles
            // 
            this.tableFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableFiles.EditStartAction = XPTable.Editors.EditStartAction.SingleClick;
            this.tableFiles.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableFiles.GridColor = System.Drawing.Color.LightGray;
            this.tableFiles.GridLines = XPTable.Models.GridLines.Both;
            this.tableFiles.GridLineStyle = XPTable.Models.GridLineStyle.Dash;
            this.tableFiles.HeaderFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.tableFiles.HideSelection = true;
            this.tableFiles.Location = new System.Drawing.Point(0, 0);
            this.tableFiles.Name = "tableFiles";
            this.tableFiles.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.tableFiles.SelectionForeColor = System.Drawing.Color.White;
            this.tableFiles.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.tableFiles.Size = new System.Drawing.Size(608, 300);
            this.tableFiles.TabIndex = 5;
            this.tableFiles.Text = "table1";
            // 
            // tableModel
            // 
            this.tableModel.RowHeight = 35;
            // 
            // ucPdf2Img
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableFiles);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.lblTip);
            this.Name = "ucPdf2Img";
            this.Size = new System.Drawing.Size(608, 346);
            this.Click += new System.EventHandler(this.ucPdf2Img_Click);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ucPdf2Img_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ucPdf2Imgcs_DragEnter);
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTip;
        private MetroSet_UI.Controls.MetroSetButton btnStartConvert;
        private MetroSet_UI.Controls.MetroDefaultSetButton btnContinueAdd;
        private System.Windows.Forms.Panel panelBottom;
        private MetroSet_UI.Controls.MetroSetComboBox cboImgType;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel1;
        private XPTable.Models.Table tableFiles;
        private XPTable.Models.ColumnModel columnModel;
        private XPTable.Models.TableModel tableModel;
    }
}
