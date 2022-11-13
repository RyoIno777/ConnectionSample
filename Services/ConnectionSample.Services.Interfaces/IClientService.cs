using ConnectionSample.Core.Commands;
using System.Threading.Tasks;

namespace ConnectionSample.Services.Interfaces
{
    /// <summary>
    /// クライアントサービスクラスです。
    /// </summary>
    public interface IClientService
    {

        /// <summary>
        /// コマンドを送信します。
        /// </summary>
        /// <param name="command">送信するコマンド</param>
        /// <returns>タスク</returns>
        Task SendCommandAsync(ApplicationCommand command);

    }
}
