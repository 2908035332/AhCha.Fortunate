using ZXing;
using ZXing.ZKWeb;
using ZXing.Common;
using ZXing.QrCode;
using System.DrawingCore;
using ZXing.QrCode.Internal;
using System.DrawingCore.Imaging;

namespace AhCha.Fortunate.Common.Utility
{
    /// <summary>
    /// 二维码
    /// </summary>
    public class QRCodeUtil
    {
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns>Bitmap</returns>
        public static Bitmap CreateQrCode(string content, int width = 300, int height = 300)
        {
            MultiFormatWriter writer = new MultiFormatWriter();
            Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();
            hints.Add(EncodeHintType.CHARACTER_SET, "utf-8");
            hints.Add(EncodeHintType.MARGIN, 0);
            hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            BitMatrix bm = writer.encode(content, BarcodeFormat.QR_CODE, width, height, hints);
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            return barcodeWriter.Write(bm);
        }

        /// <summary>
        /// 保存二维码
        /// </summary>
        /// <param name="Path">保存路径</param>
        /// <param name="content">内容</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns>文件名称</returns>
        public static string SaveQrCode(string content, int width = 300, int height = 300)
        {
            QRCodeWriter QRCode = new QRCodeWriter();
            Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();
            hints.Add(EncodeHintType.CHARACTER_SET, "utf-8");
            hints.Add(EncodeHintType.MARGIN, 0);
            hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            BitMatrix Bit = QRCode.encode(content, BarcodeFormat.QR_CODE, width, height, hints);
            BarcodeWriter Barcode = new BarcodeWriter();
            Barcode.Options = new EncodingOptions()
            {
                Margin = 0,
            };
            Bitmap bitmap = Barcode.Write(Bit);
            string Name = Guid.NewGuid().ToString().Replace("-", string.Empty) + $".{ImageFormat.Png}";
            string SavePath = Path.Combine(FileUtil.GetSystemDirectory, "QrCode", Name);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(SavePath, ImageFormat.Png);
            }
            return Name;
        }

        /// <summary>
        /// 图片转Base64，png后缀
        /// </summary>
        /// <param name="img">Image</param>
        /// <returns>Base64字符串</returns>
        public static string BitmapToBase64(Image img)
        {
            MemoryStream ms1 = new MemoryStream();
            img.Save(ms1, ImageFormat.Png);
            byte[] arr1 = new byte[ms1.Length];
            ms1.Position = 0;
            ms1.Read(arr1, 0, (int)ms1.Length);
            ms1.Close();
            return "data:image/png;base64," + Convert.ToBase64String(arr1);
        }

    }
}
