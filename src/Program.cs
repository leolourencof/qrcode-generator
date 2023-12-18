using ZXing.Common;
using ZXing.QrCode;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace App
{
    public static class Program
    {
        static void Main()
        {
            Console.WriteLine("Insert link or text:");
            string input = Console.ReadLine() ?? "";

            QRCodeWriter qRCodeWriter = new();
            BitMatrix qrCode = qRCodeWriter.encode(input, ZXing.BarcodeFormat.QR_CODE, 500, 500);

            using Image<Rgba32> image = new(qrCode.Width, qrCode.Height);

            foreach (int y in Enumerable.Range(0, qrCode.Height))
            {
                foreach (int x in Enumerable.Range(0, qrCode.Width))
                {
                    image[x, y] = qrCode[x, y] ? Color.Black : Color.White;
                }
            }

            string currentPath = Directory.GetCurrentDirectory();
            string qrCodePath = Path.Combine(currentPath, "..", "..", "..");

            string fileName = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");

            image.Save($"{Path.GetFullPath(qrCodePath)}\\QR Code {fileName}.png");
        }
    }
}
