namespace EthernetIPAdapterSimulator;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TableLayoutPanel mainLayout;
    private System.Windows.Forms.FlowLayoutPanel actionsLayout;
    private System.Windows.Forms.GroupBox groupConnection;
    private System.Windows.Forms.Label lblEdsFile;
    private System.Windows.Forms.TextBox txtEdsFile;
    private System.Windows.Forms.Button btnBrowseEds;
    private System.Windows.Forms.Button btnLoadEds;
    private System.Windows.Forms.Label lblInterface;
    private System.Windows.Forms.ComboBox cboInterface;
    private System.Windows.Forms.Label lblIpAddress;
    private System.Windows.Forms.TextBox txtIpAddress;
    private System.Windows.Forms.Label lblSubnetMask;
    private System.Windows.Forms.TextBox txtSubnetMask;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage tabConfiguration;
    private System.Windows.Forms.TabPage tabInputs;
    private System.Windows.Forms.TabPage tabOutputs;
    private System.Windows.Forms.DataGridView dgvConfiguration;
    private System.Windows.Forms.DataGridView dgvInputs;
    private System.Windows.Forms.DataGridView dgvOutputs;
    private System.Windows.Forms.GroupBox groupLog;
    private System.Windows.Forms.ListBox lstLog;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
        this.actionsLayout = new System.Windows.Forms.FlowLayoutPanel();
        this.groupConnection = new System.Windows.Forms.GroupBox();
        this.lblEdsFile = new System.Windows.Forms.Label();
        this.txtEdsFile = new System.Windows.Forms.TextBox();
        this.btnBrowseEds = new System.Windows.Forms.Button();
        this.btnLoadEds = new System.Windows.Forms.Button();
        this.lblInterface = new System.Windows.Forms.Label();
        this.cboInterface = new System.Windows.Forms.ComboBox();
        this.lblIpAddress = new System.Windows.Forms.Label();
        this.txtIpAddress = new System.Windows.Forms.TextBox();
        this.lblSubnetMask = new System.Windows.Forms.Label();
        this.txtSubnetMask = new System.Windows.Forms.TextBox();
        this.btnStart = new System.Windows.Forms.Button();
        this.btnStop = new System.Windows.Forms.Button();
        this.lblStatus = new System.Windows.Forms.Label();
        this.tabControl = new System.Windows.Forms.TabControl();
        this.tabConfiguration = new System.Windows.Forms.TabPage();
        this.tabInputs = new System.Windows.Forms.TabPage();
        this.tabOutputs = new System.Windows.Forms.TabPage();
        this.dgvConfiguration = new System.Windows.Forms.DataGridView();
        this.dgvInputs = new System.Windows.Forms.DataGridView();
        this.dgvOutputs = new System.Windows.Forms.DataGridView();
        this.groupLog = new System.Windows.Forms.GroupBox();
        this.lstLog = new System.Windows.Forms.ListBox();

        this.SuspendLayout();

        this.Text = "EtherNet/IP Adapter Simulator";
        this.Size = new System.Drawing.Size(1080, 760);
        this.MinimumSize = new System.Drawing.Size(900, 620);
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);

        this.mainLayout.ColumnCount = 1;
        this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        this.mainLayout.Location = new System.Drawing.Point(0, 0);
        this.mainLayout.Name = "mainLayout";
        this.mainLayout.Padding = new System.Windows.Forms.Padding(12);
        this.mainLayout.RowCount = 4;
        this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));

        this.groupConnection.Dock = System.Windows.Forms.DockStyle.Fill;
        this.groupConnection.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
        this.groupConnection.Padding = new System.Windows.Forms.Padding(12);
        this.groupConnection.Text = "Adapter Configuration";

        var connectionLayout = new System.Windows.Forms.TableLayoutPanel();
        connectionLayout.ColumnCount = 6;
        connectionLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
        connectionLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        connectionLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
        connectionLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
        connectionLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
        connectionLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
        connectionLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        connectionLayout.RowCount = 2;
        connectionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        connectionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));

        this.lblEdsFile.Text = "EDS File:";
        this.lblEdsFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
        connectionLayout.Controls.Add(this.lblEdsFile, 0, 0);

        this.txtEdsFile.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.txtEdsFile.ReadOnly = true;
        this.txtEdsFile.Size = new System.Drawing.Size(430, 23);
        connectionLayout.Controls.Add(this.txtEdsFile, 1, 0);

        this.btnBrowseEds.Text = "Browse";
        this.btnBrowseEds.Click += new System.EventHandler(this.btnBrowseEds_Click);
        connectionLayout.Controls.Add(this.btnBrowseEds, 2, 0);

        this.btnLoadEds.Text = "Load";
        this.btnLoadEds.Click += new System.EventHandler(this.btnLoadEds_Click);
        connectionLayout.Controls.Add(this.btnLoadEds, 3, 0);

        this.lblInterface.Text = "Interface:";
        connectionLayout.Controls.Add(this.lblInterface, 0, 1);

        this.cboInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboInterface.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.cboInterface.Size = new System.Drawing.Size(230, 23);
        connectionLayout.Controls.Add(this.cboInterface, 1, 1);

        this.lblIpAddress.Text = "IP Address:";
        connectionLayout.Controls.Add(this.lblIpAddress, 2, 1);

        this.txtIpAddress.Text = "192.168.1.50";
        this.txtIpAddress.Width = 120;
        connectionLayout.Controls.Add(this.txtIpAddress, 3, 1);

        this.lblSubnetMask.Text = "Subnet:";
        connectionLayout.Controls.Add(this.lblSubnetMask, 4, 1);

        this.txtSubnetMask.Text = "255.255.255.0";
        this.txtSubnetMask.Width = 120;
        connectionLayout.Controls.Add(this.txtSubnetMask, 5, 1);

        this.groupConnection.Controls.Add(connectionLayout);

        this.actionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        this.actionsLayout.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
        this.actionsLayout.AutoSize = true;
        this.actionsLayout.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);

        this.btnStart.Text = "Start";
        this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
        this.actionsLayout.Controls.Add(this.btnStart);

        this.btnStop.Text = "Stop";
        this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
        this.btnStop.Enabled = false;
        this.actionsLayout.Controls.Add(this.btnStop);

        this.lblStatus.Text = "Status: Ready";
        this.lblStatus.AutoSize = true;
        this.actionsLayout.Controls.Add(this.lblStatus);

        this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;

        this.tabConfiguration.Text = "Configuration";
        this.dgvConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvConfiguration.ReadOnly = true;
        this.dgvConfiguration.AllowUserToAddRows = false;
        this.dgvConfiguration.AllowUserToDeleteRows = false;
        this.dgvConfiguration.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.tabConfiguration.Controls.Add(this.dgvConfiguration);

        this.tabInputs.Text = "Inputs";
        this.dgvInputs.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvInputs.ReadOnly = true;
        this.dgvInputs.AllowUserToAddRows = false;
        this.dgvInputs.AllowUserToDeleteRows = false;
        this.dgvInputs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.tabInputs.Controls.Add(this.dgvInputs);

        this.tabOutputs.Text = "Outputs";
        this.dgvOutputs.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvOutputs.ReadOnly = true;
        this.dgvOutputs.AllowUserToAddRows = false;
        this.dgvOutputs.AllowUserToDeleteRows = false;
        this.dgvOutputs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.tabOutputs.Controls.Add(this.dgvOutputs);

        this.tabControl.TabPages.Add(this.tabConfiguration);
        this.tabControl.TabPages.Add(this.tabInputs);
        this.tabControl.TabPages.Add(this.tabOutputs);

        this.groupLog.Text = "Activity Log";
        this.groupLog.Dock = System.Windows.Forms.DockStyle.Fill;
        this.groupLog.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
        this.groupLog.Padding = new System.Windows.Forms.Padding(8);
        this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
        this.lstLog.HorizontalScrollbar = true;
        this.groupLog.Controls.Add(this.lstLog);

        this.mainLayout.Controls.Add(this.groupConnection, 0, 0);
        this.mainLayout.Controls.Add(this.actionsLayout, 0, 1);
        this.mainLayout.Controls.Add(this.tabControl, 0, 2);
        this.mainLayout.Controls.Add(this.groupLog, 0, 3);

        this.Controls.Add(this.mainLayout);

        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
