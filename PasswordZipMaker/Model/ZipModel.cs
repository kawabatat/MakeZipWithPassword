using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordZipMaker.Model
{
    class ZipModel
    {
        #region member

        private const string DEFAULT_ENCODE = "shift_jis";
        
        #endregion 

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ZipModel()
        {

        }

        public void makeZip(string zipPath, string filePath, string folderPath, string strPassword)
        {
            //作成するZIP書庫のパス
            //string zipPath = @"C:\test.zip";
            //圧縮するファイルのパス
            //string filePath = @"C:\readme.txt";
            //圧縮するフォルダのパス
            //string folderPath = @"C:\doc";

            //ZipFileを作成する
            //IBM437でエンコードできないファイル名やコメントをShift JISでエンコード
            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(Encoding.GetEncoding(DEFAULT_ENCODE)))
            {
                //IBM437でエンコードできないファイル名やコメントをUTF-8でエンコード
                //zip.UseUnicodeAsNecessary = true;
                //圧縮レベルを変更
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                //圧縮せずに格納する
                //zip.ForceNoCompression = true;
                //必要な時はZIP64で圧縮する。デフォルトはNever。
                zip.UseZip64WhenSaving = Ionic.Zip.Zip64Option.AsNecessary;
                //エラーが出てもスキップする。デフォルトはThrow。
                zip.ZipErrorAction = Ionic.Zip.ZipErrorAction.Skip;
                //ZIP書庫にコメントを付ける
                zip.Comment = "made in japan.";
 
                //パスワードを付ける
                zip.Password = strPassword;
                //AES 256ビット暗号化
                //zip.Encryption = Ionic.Zip.EncryptionAlgorithm.WinZipAes256;
 
                //ファイルを追加する
                zip.AddFile(filePath, "");
                //書庫内に"doc"というディレクトリを作って
                //　そこにfilePathを格納するには次のようにする
                //zip.AddFile(filePath, "doc");
 
                //フォルダを追加する
                //zip.AddDirectory(folderPath);
                //書庫内に"doc"というディレクトリを作って
                //　そこにfolderPathを格納するには次のようにする
                //zip.AddDirectory(folderPath, "doc");
 
                //ZIP書庫を作成する
                zip.Save(zipPath);
            }

        }
    }
}
