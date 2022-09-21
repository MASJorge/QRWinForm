using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;
using MixERP.Net.VCards;
using MixERP.Net.VCards.Serializer;
using MixERP.Net.VCards.Types;
using System.Text;

namespace QRWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //referencia https://www.youtube.com/watch?v=R_EAZ19AkHU
        private void button1_Click(object sender, EventArgs e)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(TxtValor.Text.Trim(), out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);
            MemoryStream ms = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemporal = new Bitmap(ms);
            var imagen = new Bitmap(imageTemporal, new Size(new Point(200, 200)));

            ImgQR.BackgroundImage = imagen;
            imagen.Save("imagen.png", ImageFormat.Png);

            btnGuardar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Image image = (Image)ImgQR.BackgroundImage.Clone();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.Filter = "PNG (*.png)|*.png| VCF (*.vcf)|*.vcf";
            saveFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                if(saveFileDialog.Filter.Contains("PNG"))
                image.Save(saveFileDialog.FileName, ImageFormat.Png);
                if (saveFileDialog.Filter.Contains("VCF"))
                {
                    GuardarVCard();
                }


            }
            image.Dispose();
        }

        private void GuardarVCard()
        {
            var vcard = new VCard
            {
                Version = VCardVersion.V4,
                FormattedName = "Jorge Mas",
                FirstName = "Jorge",
                MiddleName = "Ariel",
                LastName = "Mas",
                Classification = ClassificationType.Confidential,
                Categories = new[] { "Friend", "Fella", "Amsterdam" },
                //...
            };

            string serialized = vcard.Serialize();
            string[] fullpaths = { @"C:\Users\JorgeArielMas\OneDrive - Virtusway Spain SLU", "Jorge.vcf" };
            string path = Path.Combine(fullpaths);
            File.WriteAllText(path, serialized);

        }
        private VCard GenerarVCard()
        {
            var vcard = new VCard
            {
                Version = VCardVersion.V4,
                FormattedName = "Jorge Mas",
                FirstName = "Jorge",
                MiddleName = "Ariel",
                LastName = "Mas",
                Classification = ClassificationType.Confidential,
                Categories = new[] { "Friend", "Fella", "Amsterdam" },
                //...
            };

            string serialized = vcard.Serialize();
            string[] fullpaths = { @"C:\Users\JorgeArielMas\OneDrive - Virtusway Spain SLU", "Jorge.vcf" };
            string path = Path.Combine(fullpaths);
            File.WriteAllText(path, serialized);

            string contents = File.ReadAllText(path, Encoding.UTF8);
            IEnumerable<VCard> vcards = MixERP.Net.VCards.Deserializer.GetVCards(contents);

            return vcard;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            var vCard = GenerarVCard();
            string serialized = vCard.Serialize();
            qrEncoder.TryEncode(serialized.Trim(), out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);
            MemoryStream ms = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemporal = new Bitmap(ms);
            var imagen = new Bitmap(imageTemporal, new Size(new Point(200, 200)));

            ImgQR.BackgroundImage = imagen;
            imagen.Save("imagen.png", ImageFormat.Png);

            btnGuardar.Enabled = true;
        }
    }
}


