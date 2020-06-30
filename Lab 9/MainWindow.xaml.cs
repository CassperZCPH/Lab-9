using System.Windows;
using System.Windows.Controls;

namespace lab9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Calculator calc;
        public MainWindow()
        {
            InitializeComponent();
            calc = new Calculator();
            calc.DidUpdateValue += CalculatorDidUpdateValue;
            calc.InputError += CalculatorInputError;
            calc.Clear();
        }

        private void CalculatorInputError(Calculator sender, string message)
        {
            MessageBox.Show(message, "Error");
        }

        private void CalculatorDidUpdateValue(Calculator sender, double value, int precision)
        {
            label1.Content = value.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (int.TryParse(b.Content.ToString(), out int digit))
            {
                calc.AddDigit(digit);
            }
            else
            {
                switch (b.Name)
                {
                    case "minus":
                        calc.CountPointOff();
                        calc.AddOperation(CalculatorOperation.Mul);
                        break;
                    case "plus":
                        calc.AddOperation(CalculatorOperation.Add);
                        calc.CountPointOff();
                        break;
                    case "clear":
                        calc.CountPointOff();
                        calc.Clear();
                        break;
                    case "div":
                        calc.AddOperation(CalculatorOperation.Div);
                        calc.CountPointOff();
                        break;
                    case "mult":
                        calc.AddOperation(CalculatorOperation.Sub);
                        calc.CountPointOff();
                        break;
                    case "equally":
                        calc.Compute();
                        calc.CountPointOff();
                        break;
                    case "ClearAll":
                        calc.ClearAll();
                        calc.CountPointOff();
                        break;
                    case "point":
                        label1.Content += ",";
                        calc.Point();
                        break;
                    case "sqrt":
                        calc.CountPointOff();
                        calc.SQRT();
                        break;
                    case "polar":
                        calc.CountPointOff();
                        calc.Polar();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
