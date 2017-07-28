using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using PasswordZipMaker.View;
using System.Reflection;

namespace PasswordZipMaker
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// アプリケーションが開始される時のイベント。
        /// </summary>
        /// <param name="e">イベント データ。</param>
        protected override void OnStartup( StartupEventArgs e )
        {
            // GUIDの取得
            Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Runtime.InteropServices.GuidAttribute myGuid 
                = (System.Runtime.InteropServices.GuidAttribute)Attribute.GetCustomAttribute(myAssembly, typeof(System.Runtime.InteropServices.GuidAttribute));

            // 多重起動チェック
            App._mutex = new Mutex(false, myGuid.Value);
            if (!App._mutex.WaitOne(0, false))
            {
                App._mutex.Close();
                App._mutex = null;
                this.Shutdown();
                return;
            }

            // メイン ウィンドウ表示
            MainWindow window = new MainWindow();
            window.Show();
        }
 
        /// <summary>
        /// 多重起動を防止する為のミューテックス。
        /// </summary>
        private static Mutex _mutex;
    }
}
