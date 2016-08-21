using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;
using System.Threading.Tasks;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace LEDTest
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int LED_PIN = 5;
        private GpioPin pin;

        public MainPage()
        {
            this.InitializeComponent();

            // GPIO の初期化メソッドを呼び出します
            InitGPIO();

            // LED の ON / OFF のためのループ処理を呼び出します
            loop();
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // GPIO コントローラーがない場合
            if (gpio == null)
            {
                pin = null;
                return;
            }

            // GPIO の5晩ピンを開きます
            pin = gpio.OpenPin(LED_PIN);

            // 5番ピンを High に設定します
            pin.Write(GpioPinValue.High);

            // 5番ピンを出力として使うように設定します
            pin.SetDriveMode(GpioPinDriveMode.Output);
        }

        // 1秒おきで LED を ON / OFF させるためのループ処理
        private async void loop()
        {
            while (true)
            {
                pin.Write(GpioPinValue.Low);
                await Task.Delay(100);
                pin.Write(GpioPinValue.High);
                await Task.Delay(100);
                pin.Write(GpioPinValue.Low);
                await Task.Delay(100);
                pin.Write(GpioPinValue.High);
                await Task.Delay(1000);
            }
        }
    }
}
