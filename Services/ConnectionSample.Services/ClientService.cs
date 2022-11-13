using ConnectionSample.Core.Commands;
using ConnectionSample.Core.Consts;
using ConnectionSample.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConnectionSample.Services
{
    /// <inheritdoc/>
    public class ClientService : IClientService
    {

        #region パブリックメソッド
        /// <inheritdoc/>
        async public Task SendCommandAsync(ApplicationCommand command)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var client = new TcpClient(command.IPAddress, ApplicationConst.Port))
                    using (var stream = client.GetStream())
                    {
                        // 引数で受け取ったコマンドクラスをJSONにし、ストリームへ
                        // これにより指定されているIPアドレスへJSONが送信される
                        JsonSerializer.Serialize(stream, command);
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message);
            }
        }
        #endregion

    }
}
