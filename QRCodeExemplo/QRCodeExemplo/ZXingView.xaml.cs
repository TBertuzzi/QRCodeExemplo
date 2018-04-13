﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace QRCodeExemplo
{
    public partial class ZXingView : ContentPage
    {
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;
        public event EventHandler<string> BarcodeReaded;

        public ZXingView()
        {
            InitializeComponent();

            //Opções de Leitura
            var options = new ZXing.Mobile.MobileBarcodeScanningOptions
            {
                AutoRotate = false,
                UseFrontCameraIfAvailable = false,
                TryHarder = true,
                PossibleFormats = new List<ZXing.BarcodeFormat>
                {
                    ZXing.BarcodeFormat.QR_CODE//ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13
                }
            };

            zxing = new ZXing.Net.Mobile.Forms.ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Options = options
            };

            zxing.OnScanResult += (result) =>
            Device.BeginInvokeOnMainThread(async () =>
            {

                // Para a analise
                zxing.IsAnalyzing = false;

                BarcodeReaded?.Invoke(this, result.Text);

                await Navigation.PopModalAsync();

            });


            overlay = new ZXingDefaultOverlay
            {
                TopText = "Escolhe um QRCode para leitura",
                BottomText = "O Código sera lido automaticamente",
                ShowFlashButton = zxing.HasTorch, //Lanterna
            };

            overlay.FlashButtonClicked += (sender, e) =>
            {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };

            var abort = new Button
            {
                Text = "Cancelar",
                VerticalOptions = LayoutOptions.End,
                TextColor = Color.FromHex("#FFF"),
                BackgroundColor = Color.FromHex("#4F51FF")
            };

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    abort.HeightRequest = 40;
                    break;
                case Device.Android:
                    abort.HeightRequest = 50;
                    break;
            }

            abort.Clicked += (object s, EventArgs e) =>
            {
                zxing.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync();
                });
            };

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            grid.Children.Add(zxing);
            grid.Children.Add(overlay);
            grid.Children.Add(abort);

            Content = grid;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }
    }
}

