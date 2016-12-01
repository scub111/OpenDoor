namespace OpenDoor
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnOpenDoor = new System.Windows.Forms.Button();
            this.tmrCommon = new System.Windows.Forms.Timer(this.components);
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnPing = new System.Windows.Forms.Button();
            this.lblServerConnetion = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.niCommon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tmrMinimize = new System.Windows.Forms.Timer(this.components);
            this.tmrOpened = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenDoor
            // 
            this.btnOpenDoor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenDoor.Enabled = false;
            this.btnOpenDoor.Location = new System.Drawing.Point(3, 53);
            this.btnOpenDoor.Name = "btnOpenDoor";
            this.btnOpenDoor.Size = new System.Drawing.Size(250, 64);
            this.btnOpenDoor.TabIndex = 0;
            this.btnOpenDoor.Text = "Открыть дверь";
            this.btnOpenDoor.UseVisualStyleBackColor = true;
            this.btnOpenDoor.Click += new System.EventHandler(this.btnOpenDoor_Click);
            // 
            // tmrCommon
            // 
            this.tmrCommon.Enabled = true;
            this.tmrCommon.Interval = 10000;
            this.tmrCommon.Tick += new System.EventHandler(this.tmrCommon_Tick);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(3, 30);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Статус:";
            // 
            // btnPing
            // 
            this.btnPing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPing.Enabled = false;
            this.btnPing.Location = new System.Drawing.Point(3, 123);
            this.btnPing.Name = "btnPing";
            this.btnPing.Size = new System.Drawing.Size(250, 24);
            this.btnPing.TabIndex = 2;
            this.btnPing.Text = "Пинг";
            this.btnPing.UseVisualStyleBackColor = true;
            this.btnPing.Click += new System.EventHandler(this.btnPing_Click);
            // 
            // lblServerConnetion
            // 
            this.lblServerConnetion.AutoSize = true;
            this.lblServerConnetion.Location = new System.Drawing.Point(3, 10);
            this.lblServerConnetion.Name = "lblServerConnetion";
            this.lblServerConnetion.Size = new System.Drawing.Size(44, 13);
            this.lblServerConnetion.TabIndex = 1;
            this.lblServerConnetion.Text = "Сервер";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblServerConnetion, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnPing, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblStatus, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnOpenDoor, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(256, 161);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // niCommon
            // 
            this.niCommon.Text = "notifyIcon1";
            this.niCommon.Visible = true;
            this.niCommon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.niCommon_MouseDown);
            // 
            // tmrMinimize
            // 
            this.tmrMinimize.Enabled = true;
            this.tmrMinimize.Interval = 1000;
            this.tmrMinimize.Tick += new System.EventHandler(this.tmrMinimize_Tick);
            // 
            // tmrOpened
            // 
            this.tmrOpened.Tick += new System.EventHandler(this.tmrOpened_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 161);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Входная дверь";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenDoor;
        private System.Windows.Forms.Timer tmrCommon;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnPing;
        private System.Windows.Forms.Label lblServerConnetion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NotifyIcon niCommon;
        private System.Windows.Forms.Timer tmrMinimize;
        private System.Windows.Forms.Timer tmrOpened;
    }
}

