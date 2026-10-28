using System.Net;
using System.Net.NetworkInformation;

namespace EthernetIPAdapterSimulator;

public partial class Form1 : Form
{
    private readonly SimulatorRuntime _simulator = new();
    private EdsFileDefinition? _currentEdsFile;

    public Form1()
    {
        InitializeComponent();
        _simulator.LogUpdated += (_, message) => AppendLog(message);
        _ = LoadDefaultEdsAsync();
        RefreshInterfaceList();
        UpdateStatus("Ready");
    }

    private async Task LoadDefaultEdsAsync()
    {
        var candidates = new[]
        {
            Path.Combine(Environment.CurrentDirectory, "PLX31-EIPASCII.EDS"),
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "PLX31-EIPASCII.EDS"),
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "PLX31-EIPASCII.EDS")
        };

        foreach (var candidate in candidates)
        {
            var fullPath = Path.GetFullPath(candidate);
            if (File.Exists(fullPath))
            {
                await LoadEdsFileAsync(fullPath);
                return;
            }
        }

        txtEdsFile.Text = string.Empty;
    }

    private async void btnBrowseEds_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "EDS files (*.eds)|*.eds|All files (*.*)|*.*",
            Title = "Select EDS file",
            InitialDirectory = Environment.CurrentDirectory
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            await LoadEdsFileAsync(dialog.FileName);
        }
    }

    private async void btnLoadEds_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtEdsFile.Text) && File.Exists(txtEdsFile.Text))
        {
            await LoadEdsFileAsync(txtEdsFile.Text);
            return;
        }

        MessageBox.Show(this, "Please select a valid EDS file first.", "No EDS loaded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
        if (_currentEdsFile is null)
        {
            MessageBox.Show(this, "Load an EDS file before starting the simulator.", "EDS required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!IPAddress.TryParse(txtIpAddress.Text, out _))
        {
            MessageBox.Show(this, "Please enter a valid IPv4 address.", "Invalid IP address", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!IPAddress.TryParse(txtSubnetMask.Text, out _))
        {
            MessageBox.Show(this, "Please enter a valid subnet mask.", "Invalid subnet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var bindAddress = cboInterface.SelectedItem?.ToString() ?? "0.0.0.0";
        _simulator.Start(txtIpAddress.Text, txtSubnetMask.Text, bindAddress);

        btnStart.Enabled = false;
        btnStop.Enabled = true;
        UpdateStatus("Running");
        AppendLog($"Simulator started for device {_currentEdsFile.DeviceName}.");
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
        _simulator.Stop();
        btnStart.Enabled = true;
        btnStop.Enabled = false;
        UpdateStatus("Stopped");
        AppendLog("Simulator stopped.");
    }

    private async Task LoadEdsFileAsync(string filePath)
    {
        try
        {
            var parsed = await Task.Run(() => EdsParser.Parse(filePath));
            _currentEdsFile = parsed;
            txtEdsFile.Text = filePath;

            BindGrid(dgvConfiguration, parsed.ConfigurationParameters);
            BindGrid(dgvInputs, parsed.InputParameters);
            BindGrid(dgvOutputs, parsed.OutputParameters);

            AppendLog($"Loaded EDS file: {Path.GetFileName(filePath)}");
            UpdateStatus("EDS loaded");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Unable to load the EDS file:\n{ex.Message}", "EDS load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void RefreshInterfaceList()
    {
        var interfaces = NetworkInterface.GetAllNetworkInterfaces()
            .Where(i => i.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(i => new
            {
                Name = i.Name,
                Address = i.GetIPProperties().UnicastAddresses
                    .Where(a => a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .Select(a => a.Address.ToString())
                    .FirstOrDefault() ?? "0.0.0.0"
            })
            .OrderBy(i => i.Name)
            .ToList();

        cboInterface.Items.Clear();
        foreach (var item in interfaces)
        {
            cboInterface.Items.Add($"{item.Name} ({item.Address})");
        }

        if (cboInterface.Items.Count > 0)
        {
            cboInterface.SelectedIndex = 0;
        }
    }

    private static void BindGrid(DataGridView grid, IReadOnlyCollection<EdsParameter> parameters)
    {
        var rows = parameters
            .Select(p => new
            {
                Number = p.Number,
                Name = p.Name,
                DataType = p.DataType,
                Size = p.DataSize
            })
            .ToList();

        grid.Rows.Clear();
        grid.AutoGenerateColumns = false;
        grid.Columns.Clear();

        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ParameterNumber", HeaderText = "No.", DataPropertyName = "Number", ReadOnly = true, Width = 60 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ParameterName", HeaderText = "Name", DataPropertyName = "Name", ReadOnly = true, Width = 200 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ParameterType", HeaderText = "Type", DataPropertyName = "DataType", ReadOnly = true, Width = 80 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ParameterSize", HeaderText = "Size (bytes)", DataPropertyName = "Size", ReadOnly = true, Width = 90 });

        foreach (var row in rows)
        {
            grid.Rows.Add(row.Number, row.Name, row.DataType, row.Size);
        }
    }

    private void AppendLog(string message)
    {
        if (lstLog.InvokeRequired)
        {
            lstLog.BeginInvoke(new Action<string>(AppendLog), message);
            return;
        }

        lstLog.Items.Add($"{DateTime.Now:HH:mm:ss}  {message}");
        if (lstLog.Items.Count > 200)
        {
            lstLog.Items.RemoveAt(0);
        }

        lstLog.SelectedIndex = lstLog.Items.Count - 1;
    }

    private void UpdateStatus(string message)
    {
        lblStatus.Text = $"Status: {message}";
    }

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
        _simulator.Dispose();
    }
}
