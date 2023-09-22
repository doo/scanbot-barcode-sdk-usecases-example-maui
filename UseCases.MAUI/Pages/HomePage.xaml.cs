using BarcodeSDK.MAUI.Configurations;
using BarcodeSDK.MAUI.Constants;
using BarcodeSDK.MAUI.Services;
using SBSDK = BarcodeSDK.MAUI.ScanbotBarcodeSDK;

namespace UseCases.MAUI.Pages;

public partial class HomePage : ContentPage
{
    private List<UseCaseOption> useCases;
    private List<UseCaseOption> arUseCases;

    private const int rowHeight = 55;

    public HomePage()
    {
        InitializeComponent();

        useCases = new List<UseCaseOption>
                {
                    new UseCaseOption("Scanning Single Barcodes", ScanSingleBarcode),
                    new UseCaseOption("Scanning Multiple Barcodes", ScanMultipleBarcodes),
                    new UseCaseOption("Batch Scanning", ScanBarcodesInBatch),
                    new UseCaseOption("Scanning Tiny Barcodes", ScanTinyBarcodes),
                    new UseCaseOption("Scanning Distant Barcodes", ScanDistantBarcodes),
                    new UseCaseOption("Detecting Barcodes on Still Images", DetectOnStillImage)
            };

        arUseCases = new List<UseCaseOption>
            {
                    new UseCaseOption("AR-MultiScan", ArMultipleScan),
                    new UseCaseOption("AR-SelectScan", ArSelectScan)
            };

        UseCaseList.ItemsSource = useCases;
        UseCaseList.MaximumHeightRequest = rowHeight * (useCases.Count + 1); // + 1 for the header

        ArUseCaseList.ItemsSource = arUseCases;
        ArUseCaseList.MaximumHeightRequest = rowHeight * (arUseCases.Count + 1); // + 1 for the header
    }

    private async Task ScanSingleBarcode()
    {
        await SBSDK.BarcodeService.OpenBarcodeScannerView(new BarcodeScannerConfiguration
        {
            SuccessBeepEnabled = false,
            FinderEnabled = true,
            ConfirmationDialogConfiguration = new BarcodeConfirmationDialogConfiguration
            {
                ResultWithConfirmationEnabled = true,
                Title = "Scanning Single Barcodes",
                Message = "",
                ConfirmButtonTitle = "Dismiss",
                TextFormat = BarcodeTextFormat.Code,
                RetryButtonTitle = "Retry"
            }
        });
    }

    private async Task ScanMultipleBarcodes()
    {
        await SBSDK.BarcodeService.OpenBatchBarcodeScannerView(new BatchBarcodeScannerConfiguration
        {
            SuccessBeepEnabled = false,
            FinderEnabled = false,
            FinderTextHint = string.Empty,
            CodeDensity = BarcodeDensity.High
        });
    }

    private async Task ScanBarcodesInBatch()
    {
        await SBSDK.BarcodeService.OpenBatchBarcodeScannerView(new BatchBarcodeScannerConfiguration
        {
            SuccessBeepEnabled = false,
            FinderEnabled = true,
        });
    }

    private async Task ScanTinyBarcodes()
    {
        await SBSDK.BarcodeService.OpenBarcodeScannerView(new BarcodeScannerConfiguration
        {
            SuccessBeepEnabled = false,
            FinderEnabled = true,
            MinFocusDistanceLock = true,
        });
    }

    private async Task ScanDistantBarcodes()
    {
        await SBSDK.BarcodeService.OpenBarcodeScannerView(new BarcodeScannerConfiguration
        {
            CameraZoomLevel = 1.0f,
            SuccessBeepEnabled = false
        });
    }

    private async Task DetectOnStillImage()
    {
        var selectedImage = await SBSDK.PickerService.PickImageAsync(new ImagePickerConfiguration { Title = "Gallery" });
        var barcodes = await SBSDK.DetectionService.DetectBarcodesFrom(selectedImage, new BarcodeDetectionConfiguration
        {
            EngineMode = EngineMode.NextGen,
            AdditionalParameters = new BarcodeScannerAdditionalParameters
            {
                CodeDensity = BarcodeDensity.High,
            }
        });

        if (barcodes.Count > 0)
        {
            var barcodesAsText = barcodes.Select(barcode => $"{barcode.Format}: {barcode.Text}").ToArray();
            await DisplayActionSheet("Found barcodes", "Dismiss", null, barcodesAsText);
        }
        else
        {
            await DisplayAlert("No barcodes detected", "Counld not find any barcodes in the selected image. Try again", "Dismiss");
        }
    }

    private async Task ArMultipleScan()
    {
        await SBSDK.BarcodeService.OpenBatchBarcodeScannerView(new BatchBarcodeScannerConfiguration
        {
            SuccessBeepEnabled = false,
            FinderEnabled = false,
            FinderTextHint = string.Empty,
            CodeDensity = BarcodeDensity.High,
            OverlayConfiguration = new SelectionOverlayConfiguration(
                automaticSelectionEnabled: true,
                overlayFormat: BarcodeTextFormat.None,
                polygon: Colors.Red,
                text: Colors.White,
                textContainer: Colors.Red)
        });
    }

    private async Task ArSelectScan()
    {
        await SBSDK.BarcodeService.OpenBatchBarcodeScannerView(new BatchBarcodeScannerConfiguration
        {
            SuccessBeepEnabled = false,
            FinderEnabled = false,
            FinderTextHint = string.Empty,
            CodeDensity = BarcodeDensity.High,
            OverlayConfiguration = new SelectionOverlayConfiguration(
                automaticSelectionEnabled: false,
                overlayFormat: BarcodeTextFormat.Code,
                polygon: Colors.Red,
                text: Colors.White,
                textContainer: Colors.Red)
        });
    }

    private async void UseCaseSelected(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        if (e?.CurrentSelection.FirstOrDefault() is UseCaseOption service && service.NavigationAction != null)
        {

            if (SBSDK.SDKService.GetLicenseInfo()?.IsValid != true)
            {
                await DisplayAlert("Oops!", "License expired or invalid", "Dismiss");
                return;
            }

            await service.NavigationAction();
        }

        if (sender is CollectionView listView)
        {
            listView.SelectedItem = null;
        }
    }
}
