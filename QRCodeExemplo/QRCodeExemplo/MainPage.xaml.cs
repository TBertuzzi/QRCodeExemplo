using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace QRCodeExemplo
{
	public partial class MainPage : ContentPage
	{

		public MainPage()
		{
			InitializeComponent();

            btnQrcode.Clicked += async (sender, e) => {

                ZXingView zXingView = new ZXingView();
                zXingView.BarcodeReaded += ZXingView_BarcodeReaded;

                await Navigation.PushModalAsync(zXingView);

            };
        }

        void ZXingView_BarcodeReaded(object sender, string e)
        {
            lblResultado.Text = "QRCODE: " + e;
        }

	}
}
