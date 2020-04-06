using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace EasySoundBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int deviceId = 0; deviceId < WaveOut.DeviceCount; deviceId++)
            {
                var capabilities = WaveOut.GetCapabilities(deviceId);
                comboBoxWaveOutDevice.Items.Add(capabilities.ProductName);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var waveOut = new WaveOutEvent() { DeviceNumber = comboBoxWaveOutDevice.SelectedIndex };
            try
            {
                var mp3Reader = new Mp3FileReader(TextBox.Text);    
                waveOut.Init(mp3Reader);
                waveOut.Play();
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147024894)
                {
                    MessageBox.Show("Incorrect file path!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DefaultDialogService defaultDialog = new DefaultDialogService();
            defaultDialog.OpenFileDialog();
            TextBox.Text = defaultDialog.FilePath;
        }
    }
}
