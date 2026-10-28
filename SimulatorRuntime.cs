using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EthernetIPAdapterSimulator;

public sealed class SimulatorRuntime : IDisposable
{
    private UdpClient? _listener;
    private CancellationTokenSource? _cts;
    private Task? _listenerTask;

    public event EventHandler<string>? LogUpdated;

    public bool IsRunning => _listener is not null && _listenerTask is not null && !_listenerTask.IsCompleted;

    public void Start(string ipAddress, string subnetMask, string bindAddress)
    {
        if (IsRunning)
        {
            return;
        }

        _cts = new CancellationTokenSource();
        _listener = new UdpClient(new IPEndPoint(IPAddress.Any, 44818));

        _listenerTask = Task.Run(() => ListenAsync(ipAddress, subnetMask, _cts.Token));
        Notify($"EtherNet/IP simulator started on port 44818 for {ipAddress}/{subnetMask}.");
    }

    public void Stop()
    {
        if (_cts is null)
        {
            return;
        }

        _cts.Cancel();

        try
        {
            _listener?.Close();
        }
        catch
        {
            // ignore cleanup errors
        }

        _listener = null;
        _cts.Dispose();
        _cts = null;
        Notify("EtherNet/IP simulator stopped.");
    }

    private async Task ListenAsync(string ipAddress, string subnetMask, CancellationToken token)
    {
        if (_listener is null)
        {
            return;
        }

        while (!token.IsCancellationRequested)
        {
            try
            {
                var result = await _listener.ReceiveAsync(token);
                var remote = result.RemoteEndPoint;
                var payload = result.Buffer;
                Notify($"Scanner packet received from {remote.Address}:{remote.Port} ({payload.Length} bytes).");

                var responseText = $"OpENer simulator active | IP={ipAddress} | Subnet={subnetMask}";
                var response = Encoding.ASCII.GetBytes(responseText);
                await _listener.SendAsync(response, response.Length, remote);
                Notify($"Response sent to {remote.Address}:{remote.Port}.");
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (ObjectDisposedException)
            {
                break;
            }
            catch (Exception ex)
            {
                Notify($"UDP listener error: {ex.Message}");
            }
        }
    }

    private void Notify(string message)
    {
        LogUpdated?.Invoke(this, message);
    }

    public void Dispose()
    {
        Stop();
    }
}
