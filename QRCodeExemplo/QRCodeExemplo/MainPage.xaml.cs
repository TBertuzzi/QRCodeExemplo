using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QRCodeExemplo
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            btnQrcode.Clicked += async (sender, e) => {

                var ZXing = new ZXing.Mobile.MobileBarcodeScanner();

                var resultado = await ZXing.Scan();

                if (resultado != null)
                {
                    lblResultado.Text = "QRCODE: " + resultado.Text;
                }
            };
        }
	}
}
