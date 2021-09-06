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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtInput.Text.Equals("0"))
            {
                txtInput.Text = string.Empty;
            }

            txtInput.Text += ((Button)sender).Content.ToString();
        }

        private void BtnOperation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Button)sender).Content.ToString() == "=" || !string.IsNullOrEmpty(txtOutput.Text))
                {
                    txtInput.Text = Utils.CalculateOperation(txtOutput.Text, txtInput.Text);
                    txtOutput.Text = string.Empty;
                    return;
                }

                txtOutput.Text = txtInput.Text + ((Button)sender).Content;
                txtInput.Text = "0";
            }
            catch(FormatException)
            {
                txtOutput.Text = string.Empty;
                txtInput.Text = "0";
            }
        }

        private void BtnPeriod_Click(object sender, RoutedEventArgs e)
        {
            if (!Utils.PeriodState(txtInput.Text))
            {
                txtInput.Text += ",";
            }
        }

        private void BtnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text = "0";
        }

        private void BtnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (double.Parse(txtInput.Text) < 10 && double.Parse(txtInput.Text) > -10 && !Utils.PeriodState(txtInput.Text))
            {
                txtInput.Text = "0";
                return;
            }

            txtInput.Text = txtInput.Text[0..^1];
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = string.Empty;
            txtInput.Text = "0";
        }

        private void BtnPlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (!txtInput.Text.Equals("0"))
            {
                txtInput.Text = (double.Parse(txtInput.Text) * -1).ToString();
            }
        }

        private void BtnSquare_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text = Math.Sqrt(double.Parse(txtInput.Text)).ToString("0.#######");
        }
        
        private void BtnPercent_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtOutput.Text))
            {
                double previousNumber = double.Parse(txtOutput.Text[0..^1]);

                if (previousNumber.Equals(0))
                {
                    txtInput.Text = "0";
                    return;
                }

                txtInput.Text = ((previousNumber / 100) * double.Parse(txtInput.Text)).ToString("0.#######");
                return;
            }

            txtInput.Text = "0";
        }

        private void BtnReciprocal_Click(object sender, RoutedEventArgs e)
        {
            String reciprocal = "1/";

            txtInput.Text = Utils.CalculateOperation(reciprocal, txtInput.Text);
        }

        private void BtnMS_Click(object sender, RoutedEventArgs e)
        {
            txtMemory.Text = "M";
            txtMemory.DataContext = double.Parse(txtInput.Text);
            txtInput.Text = "0";
        }

        private void BtnMR_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMemory.Text))
            {
                txtInput.Text = "0";
                return;
            }

            txtInput.Text = txtMemory.DataContext.ToString();
        }

        private void BtnMC_Click(object sender, RoutedEventArgs e)
        {
            txtMemory.Text = string.Empty;
            txtInput.Text = "0";
        }

        private void BtnMPlus_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMemory.Text))
            {
                txtMemory.DataContext = 0 + double.Parse(txtInput.Text);
                txtMemory.Text = "M";
                txtInput.Text = "0";
                return;
            }

            txtMemory.DataContext = ((Double)txtMemory.DataContext) + double.Parse(txtInput.Text);

            if (((Double)txtMemory.DataContext) == 0)
            {
                txtMemory.Text = string.Empty;
            }

            txtInput.Text = "0";
        }

        private void BtnMMinus_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMemory.Text))
            {
                txtMemory.DataContext = 0 - double.Parse(txtInput.Text);
                txtMemory.Text = "M";
                txtInput.Text = "0";
                return;
            }

            txtMemory.DataContext = ((Double) txtMemory.DataContext) - double.Parse(txtInput.Text);

            if (((Double)txtMemory.DataContext) == 0)
            {
                txtMemory.Text = string.Empty;
            }

            txtInput.Text = "0";
        }
    }
}
