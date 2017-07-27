using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordZipMaker.Model
{
    class PasswordModel
    {
        #region member

        private Random _cRandom = null; // 乱数オブジェクト
        private uint uiLength = 8;  // パスワード長さ
        private bool isNumberEnable = true;
        private bool isLowerEnable = true;
        private bool isUpperEnable = false;
        private bool isSymbolEnable = false;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PasswordModel()
        {
            _cRandom = new System.Random();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PasswordModel(uint uiLen)
        {
            _cRandom = new System.Random();
            uiLength = uiLen;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PasswordModel(uint uiLen, bool num, bool up, bool low, bool sym)
        {
            _cRandom = new System.Random();
            uiLength = uiLen;
            isNumberEnable = num;
            isUpperEnable = up;
            isLowerEnable = low;
            isSymbolEnable = sym;
        }

        /// <summary>
        /// パスワード生成
        /// </summary>
        /// <returns>パスワード文字列</returns>
        public string MakePassword()
        {
            System.Text.StringBuilder sbTmp = new System.Text.StringBuilder();
            int i;

            for (i = 0; i < uiLength; i++ )
            {
                sbTmp.Append(GetCharactor());
            }

            return sbTmp.ToString();
        }

        /// <summary>
        /// 1文字取得
        /// </summary>
        /// <returns>文字</returns>
        private char GetCharactor()
        {
            char cRet = ' ';
            bool isExit = true;

            do
            {
                switch ((_cRandom.Next() % 4))
                {
                    case 0: // 数字
                        cRet = (char)_cRandom.Next(48, 57);
                        isExit = isNumberEnable;
                        break;
                    case 1: // 英小文字
                        cRet = (char)_cRandom.Next(97, 122);
                        isExit = isLowerEnable;
                        break;
                    case 2: // 英大文字
                        cRet = (char)_cRandom.Next(65, 90);
                        isExit = isUpperEnable;
                        break;
                    case 3: // 記号
                        cRet = (char)_cRandom.Next(32, 47);
                        isExit = isSymbolEnable;
                        break;
                    default:
                        cRet = '?';
                        isExit = true;
                        break;
                }
            } while (false == isExit);

            return cRet;
        }
    }
}
