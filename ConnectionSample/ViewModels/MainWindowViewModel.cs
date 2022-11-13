using Prism.Mvvm;
using Reactive.Bindings;

namespace ConnectionSample.ViewModels
{
    /// <summary>
    /// メインウィンドウビューモデルクラスです。
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        
        /// <summary>
        /// ウィンドウタイトル
        /// </summary>
        public ReactiveProperty<string> Title { get; } = new("通信サンプル");

        public MainWindowViewModel()
        {
        }

    }
}
