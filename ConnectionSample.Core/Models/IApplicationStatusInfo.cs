using ConnectionSample.Core.Commands;
using ConnectionSample.Core.Enums;
using Reactive.Bindings;

namespace ConnectionSample.Core.Models
{
    /// <summary>
    /// アプリケーションの状態を管理するクラスです。
    /// </summary>
    public interface IApplicationStatusInfo
    {

        /// <summary>
        /// アプリケーションの状態用です。
        /// </summary>
        /// <returns>アプリケーションの状態</returns>
        ReactivePropertySlim<ApplicationStatus> GetApplicationStatus();
        /// <summary>
        /// 表示メッセージです。
        /// </summary>
        /// <returns></returns>
        ReactivePropertySlim<string> GetMessage();
        /// <summary>
        /// アプリケーションコマンドです。
        /// </summary>
        /// <returns>アプリケーションコマンド</returns>
        ReactivePropertySlim<ApplicationCommand> GetApplicationCommand();

    }
}
