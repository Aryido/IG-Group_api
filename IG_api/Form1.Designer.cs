namespace IG_api
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.TypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Epic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstrumentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changeRateImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changeRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TypeName,
            this.Epic,
            this.InstrumentName,
            this.changeRateImage,
            this.changeRate,
            this.price});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.dataGridView1.Location = new System.Drawing.Point(32, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(983, 600);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // TypeName
            // 
            this.TypeName.HeaderText = "TypeName";
            this.TypeName.Name = "TypeName";
            this.TypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Epic
            // 
            this.Epic.HeaderText = "Epic";
            this.Epic.MinimumWidth = 7;
            this.Epic.Name = "Epic";
            this.Epic.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Epic.Width = 200;
            // 
            // InstrumentName
            // 
            this.InstrumentName.HeaderText = "InstrumentName";
            this.InstrumentName.Name = "InstrumentName";
            this.InstrumentName.Width = 180;
            // 
            // changeRateImage
            // 
            this.changeRateImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.changeRateImage.HeaderText = "changeRateImage";
            this.changeRateImage.MinimumWidth = 7;
            this.changeRateImage.Name = "changeRateImage";
            this.changeRateImage.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.changeRateImage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.changeRateImage.Width = 200;
            // 
            // changeRate
            // 
            this.changeRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.changeRate.HeaderText = "changeRate";
            this.changeRate.Name = "changeRate";
            this.changeRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.changeRate.Width = 65;
            // 
            // price
            // 
            this.price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.price.HeaderText = "price";
            this.price.MinimumWidth = 7;
            this.price.Name = "price";
            this.price.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 703);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "IG-Group-API";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Epic;
        private System.Windows.Forms.DataGridViewTextBoxColumn InstrumentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn changeRateImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn changeRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
    }
}

