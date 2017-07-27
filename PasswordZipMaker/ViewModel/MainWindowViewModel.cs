using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using PasswordZipMaker.Model;
using Microsoft.Win32;

namespace PasswordZipMaker.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void Initialize()
        {
            this.IsLengthTextReadOnly = false;
            this.LengthText = "8";
            this.PasswordText = "";
            this.IsPasswordTextReadOnly = true;
            this.IsNumberSelected = true;
            this.IsLowerAlphabetSelected = true;
            this.IsUpperAlphabetSelected = true;
            this.IsSymbolSelected = false;
            this.ZipInputFile = "";
            this.ZipSaveFile = @"C:\Users\Public\Documents\sample.zip";
        }

        #region 設定

        private string _LengthText;
        public string LengthText
        {
            get
            {
                return this._LengthText;
            }

            set
            {
                this._LengthText = value;
                this.RaisePropertyChanged("LengthText");
            }
        }

        private bool _IsLengthTextReadOnly;
        public bool IsLengthTextReadOnly
        {
            get
            {
                return this._IsLengthTextReadOnly;
            }

            set
            {
                this._IsLengthTextReadOnly = value;
                this.RaisePropertyChanged("IsLengthTextReadOnly");
            }
        }

        #region チェックボックス＆ラジオボタン

        private bool _IsNumberSelected;
        public bool IsNumberSelected
        {
            get { return this._IsNumberSelected; }
            private set
            {
                this._IsNumberSelected = value;
                RaisePropertyChanged("IsNumberSelected");
            }
        }

        private bool _IsUpperAlphabetSelected;
        public bool IsUpperAlphabetSelected
        {
            get { return this._IsUpperAlphabetSelected; }
            private set
            {
                this._IsUpperAlphabetSelected = value;
                RaisePropertyChanged("IsUpperAlphabetSelected");
            }
        }

        private bool _IsLowerAlphabetSelected;
        public bool IsLowerAlphabetSelected
        {
            get { return this._IsLowerAlphabetSelected; }
            private set
            {
                this._IsLowerAlphabetSelected = value;
                RaisePropertyChanged("IsLowerAlphabetSelected");
            }
        }

        private bool _IsSymbolSelected;
        public bool IsSymbolSelected
        {
            get { return this._IsSymbolSelected; }
            private set
            {
                this._IsSymbolSelected = value;
                RaisePropertyChanged("IsSymbolSelected");
            }
        }

        #endregion

        #endregion

        #region テキスト

        private string _ZipInputFile;

        public string ZipInputFile
        {
            get
            {
                return this._ZipInputFile;
            }

            set
            {
                this._ZipInputFile = value;
                this.RaisePropertyChanged("ZipInputFile");
            }
        }

        private string _ZipSaveFile;

        public string ZipSaveFile
        {
            get
            {
                return this._ZipSaveFile;
            }

            set
            {
                this._ZipSaveFile = value;
                this.RaisePropertyChanged("ZipSaveFile");
            }
        }

        private string _PasswordText;

        public string PasswordText
        {
            get
            {
                return this._PasswordText;
            }

            set
            {
                this._PasswordText = value;
                this.RaisePropertyChanged("PasswordText");
            }
        }

        private bool _IsPasswordTextReadOnly;

        public bool IsPasswordTextReadOnly
        {
            get
            {
                return this._IsPasswordTextReadOnly;
            }

            set
            {
                this._IsPasswordTextReadOnly = value;
                this.RaisePropertyChanged("IsPasswordTextReadOnly");
            }
        }

        #endregion

        #region 実行

        /// <summary>
        /// 圧縮元ファイル選択
        /// </summary>
        private DelegateCommand _SelectedInputFile;
        public DelegateCommand SelectedInputFile
        {
            get
            {
                if (this._SelectedInputFile == null)
                {
                    this._SelectedInputFile = new DelegateCommand(_SelectInputs);
                }

                return this._SelectedInputFile;
            }
        }

        private void _SelectInputs()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "open file";
            dialog.Filter = "All(*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                this.ZipInputFile = dialog.FileName;
            }
            else
            {
                this.ZipInputFile = "";
            }
        }

        /// <summary>
        /// パスワード生成コマンド
        /// </summary>
        private DelegateCommand _ExecuteMaking;
        public DelegateCommand ExecuteMaking
        {
            get
            {
                if (this._ExecuteMaking == null)
                {
                    this._ExecuteMaking = new DelegateCommand(_Execute);
                }

                return this._ExecuteMaking;
            }
        }

        private void _Execute()
        {
            string strPass = "Passw0rd";
            PasswordModel oPass = new PasswordModel(uint.Parse(this._LengthText), this._IsNumberSelected, this._IsUpperAlphabetSelected, this._IsLowerAlphabetSelected, this._IsSymbolSelected);

            // パスワード生成
            strPass = oPass.MakePassword();
            this.PasswordText = strPass;
            
            // クリップボードにコピー
            System.Windows.Clipboard.SetText(this._PasswordText);

            oPass = null;

            // zip圧縮
            ZipModel zip = new ZipModel();
            zip.makeZip(this._ZipSaveFile, this._ZipInputFile, "", this._PasswordText);
            zip = null;
        }

        #endregion
    }
}
