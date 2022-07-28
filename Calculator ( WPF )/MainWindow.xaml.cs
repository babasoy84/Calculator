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
using System.Web;

namespace Calculator___WPF__
{
    public partial class MainWindow : Window
    {
        double number1 = 0;
        double number2 = 0;
        double mTotal = 0;
        string operation = "";

        public MainWindow()
        {
            InitializeComponent();
            lbl_operation.Content = "";
        }

        private void ContentToNumber()
        {
            if (operation == "")
            {
                number1 = double.Parse(lbl_result.Content.ToString());
            }
            else
            {
                number2 = double.Parse(lbl_result.Content.ToString());
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void ButtonDot_Click(object sender, RoutedEventArgs e)
        {
            bool b = true;
            foreach (var ch in lbl_result.Content.ToString())
            {
                if (ch == '.')
                {
                    b = false;
                    break;
                }
            }
            if (b)
            {
                if (operation != "" && number2 == 0)
                {
                    lbl_result.Content = "0";
                }
                lbl_result.Content += ".";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lbl_result.Content.ToString() == "0" || (operation != "" && number2 == 0 && lbl_result.Content.ToString().Last() != '.'))
            {
                lbl_result.Content = (sender as Button).Content.ToString();
            }
            else if (!string.IsNullOrEmpty(lbl_operation.Content.ToString()) && lbl_operation.Content.ToString().Last() == '=')
            {
                lbl_operation.Content = "";
                lbl_result.Content = (sender as Button).Content.ToString();
            }
            else if (!string.IsNullOrEmpty(lbl_operation.Content.ToString()) && lbl_operation.Content.ToString().Split('(').Length > 1)
            {
                if (operation == "")
                {
                    lbl_operation.Content = "";
                    lbl_result.Content = (sender as Button).Content.ToString();
                }
                else if (lbl_operation.Content.ToString().Split('(').Length == 3 || (lbl_operation.Content.ToString().Split('(').Length == 2 && lbl_operation.Content.ToString().Split(operation.First())[0].Split('(').Length == 1))
                {
                    if (lbl_operation.Content.ToString().Split('(').Length == 2)
                    {
                        lbl_operation.Content = $"{number1}{operation}";
                    }
                    else if (lbl_operation.Content.ToString().First() == '√')
                    {
                        lbl_operation.Content = $"√({number1 * number1}){operation}";
                    }
                    else if (lbl_operation.Content.ToString().First() == 's')
                    {
                        lbl_operation.Content = $"sqr({Math.Sqrt(number1)}){operation}";
                    }
                    else
                    {
                        lbl_operation.Content = $"1/({Math.Sqrt(1/number1)}){operation}";
                    }
                    lbl_result.Content = (sender as Button).Content.ToString();
                }
                else
                {
                    lbl_result.Content += (sender as Button).Content.ToString();
                }
            }
            else
            {
                lbl_result.Content += (sender as Button).Content.ToString();
            }
            ContentToNumber();
        }

        private void ButtonOp_Click(object sender, RoutedEventArgs e)
        {
            bool b = false;
            if (operation == "")
            {
                b = true;
            }
            if (!string.IsNullOrEmpty(lbl_operation.Content.ToString()) && lbl_operation.Content.ToString().Last() != '=' && double.Parse(lbl_result.Content.ToString()) == number2)
            {
                switch (operation)
                {
                    case "+":
                        number1 += number2;
                        number2 = 0;
                        break;
                    case "-":
                        number1 -= number2;
                        number2 = 0;
                        break;
                    case "*":
                        number1 *= number2;
                        number2 = 0;
                        break;
                    case "%":
                        number1 = number1 * number2 / 100;
                        break;
                    case "/":
                        if (number2 == 0)
                        {
                            operation = "";
                            number1 = 0;
                            number2 = 0;
                            lbl_operation.Content = "";
                            lbl_result.Content = "Error";
                        }
                        else
                        {
                            number1 /= number2;
                            number2 = 0;
                        }
                        break;
                    default:
                        break;
                }
            }
            switch ((sender as Button).Name)
            {
                case "btn_add":
                    operation = "+";
                    break;
                case "btn_sub":
                    operation = "-";
                    break;
                case "btn_mult":
                    operation = "*";
                    break;
                case "btn_div":
                    operation = "/";
                    break;
                case "btn_per":
                    operation = "%";
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(lbl_operation.Content.ToString()) && b && lbl_operation.Content.ToString().Last() == ')')
            {
                lbl_operation.Content += operation;
            }
            else
            {
                lbl_operation.Content = number1.ToString() + operation;
            }
            lbl_result.Content = number1.ToString();
        }

        private void ButtonEqual_Click(object sender, RoutedEventArgs e)
        {
            if (operation != "")
            {
                if (lbl_operation.Content.ToString().Last() == operation.First())
                {
                    if (number1.ToString() == lbl_result.Content.ToString())
                    {
                        number2 = number1;
                    }
                    lbl_operation.Content += $"{number2}=";
                }
                else if (lbl_operation.Content.ToString().Last() != '=')
                {
                    lbl_operation.Content += "=";
                }
                switch (operation)
                {
                    case "+":
                        number1 += number2;
                        lbl_result.Content = number1.ToString();
                        break;
                    case "-":
                        number1 -= number2;
                        lbl_result.Content = number1.ToString();
                        break;
                    case "*":
                        number1 *= number2;
                        lbl_result.Content = number1.ToString();
                        break;
                    case "%":
                        number1 = number1 * number2 / 100;
                        lbl_result.Content = number1.ToString();
                        break;
                    case "/":
                        if (number2 != 0)
                        {
                            number1 /= number2;
                            lbl_result.Content = number1.ToString();
                        }
                        else
                        {
                            lbl_result.Content = "ERROR";
                            number1 = 0;
                            number2 = 0;
                            operation = "";
                        }
                        break;
                    default:
                    break;
                }
                number2 = 0;
                operation = "";
            }
        }

        private void ButtonC_Click(object sender, RoutedEventArgs e)
        {
            number1 = 0;
            number2 = 0;
            operation = "";
            lbl_operation.Content = "";
            lbl_result.Content = "0";
        }

        private void ButtonCE_Click(object sender, RoutedEventArgs e)
        {
            if (lbl_operation.Content.ToString().Split('=').Length == 2)
            {
                ButtonC_Click(sender, e);
            }
            else
            {
                if (operation != "")
                {
                    number2 = 0;
                    lbl_result.Content = "0";
                }
                else
                {
                    number1 = 0;
                    lbl_result.Content = "0";
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbl_operation.Content == null)
            {
                if (lbl_result.Content.ToString().Length == 1)
                {
                    lbl_result.Content = "0";
                }
                else
                {
                    string str = "";
                    for (int i = 0; i < lbl_result.Content.ToString().Length - 1; i++)
                    {
                        str += lbl_result.Content.ToString()[i];
                    }
                    lbl_result.Content = "";
                    if (str.Last() == '.')
                    {
                        for (int i = 0; i < str.Length - 1; i++)
                        {
                            lbl_result.Content += $"{str[i]}";
                        }
                    }
                    else
                    {
                        for (int i = 0; i < str.Length; i++)
                        {
                            lbl_result.Content += $"{str[i]}";
                        }
                    }

                }
                if (operation != "")
                {
                    number2 = float.Parse(lbl_result.Content.ToString());
                }
                else
                {
                    number1 = float.Parse(lbl_result.Content.ToString());
                }
            }
            else if (lbl_operation.Content.ToString().Split('=').Length == 1)
            {
                if (lbl_result.Content.ToString().Length == 1)
                {
                    lbl_result.Content = "0";
                }
                else
                {
                    string str = "";
                    for (int i = 0; i < lbl_result.Content.ToString().Length - 1; i++)
                    {
                        str += lbl_result.Content.ToString()[i];
                    }
                    lbl_result.Content = "";
                    if (str.Last() == '.')
                    {
                        for (int i = 0; i < str.Length - 1; i++)
                        {
                            lbl_result.Content += $"{str[i]}";
                        }
                    }
                    else
                    {
                        for (int i = 0; i < str.Length; i++)
                        {
                            lbl_result.Content += $"{str[i]}";
                        }
                    }

                }
                if (operation != "")
                {
                    number2 = float.Parse(lbl_result.Content.ToString());
                }
                else
                {
                    number1 = float.Parse(lbl_result.Content.ToString());
                }
            }
        }

        private void ButtonPN_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "")
            {
                number1 -= number1 * 2;
                lbl_result.Content = number1.ToString();
            }
            else
            {
                number2 -= number2 * 2;
                lbl_result.Content = number2.ToString();
            }
        }

        private void ButtonRoot_Click(object sender, RoutedEventArgs e)
        {
            double d = Math.Sqrt(double.Parse(lbl_result.Content.ToString()));
            lbl_result.Content = d.ToString();
            if (operation == "" || lbl_operation.Content.ToString().Last() == '=')
            {
                lbl_operation.Content = $"√({number1})";
                number1 = d;
            }
            else if (lbl_operation.Content.ToString().Split('=').Length == 1)
            {
                if (lbl_operation.Content.ToString().First() == '√')
                {
                    lbl_operation.Content = $"√({number1 * number1}){operation}";
                }
                else if (lbl_operation.Content.ToString().First() == 's')
                {
                    lbl_operation.Content = $"sqr({Math.Sqrt(number1)}){operation}";
                }
                else if (lbl_operation.Content.ToString()[2] == '(')
                {
                    lbl_operation.Content = $"1/({1 / number1}){operation}";
                }
                lbl_operation.Content += $"√({number2})";
                number2 = d;
            }
        }

        private void ButtonSquare_Click(object sender, RoutedEventArgs e)
        {
            double d = double.Parse(lbl_result.Content.ToString());
            d *= d;
            lbl_result.Content = d.ToString();
            if (operation == "" || lbl_operation.Content.ToString().Last() == '=')
            {
                lbl_operation.Content = $"sqr({number1})";
                number1 = d;
            }
            else if (lbl_operation.Content.ToString().Split('=').Length == 1)
            {
                if (lbl_operation.Content.ToString().First() == '√')
                {
                    lbl_operation.Content = $"√({number1 * number1}){operation}";
                }
                else if (lbl_operation.Content.ToString().First() == 's')
                {
                    lbl_operation.Content = $"sqr({Math.Sqrt(number1)}){operation}";
                }
                else if (lbl_operation.Content.ToString()[2] == '(')
                {
                    lbl_operation.Content = $"1/({1 / number1}){operation}";
                }
                lbl_operation.Content += $"sqr({number2})";
                number2 = d;
            }
        }

        private void ButtonDivideByOne_Click(object sender, RoutedEventArgs e)
        {
            double d = 1 / double.Parse(lbl_result.Content.ToString());
            lbl_result.Content = d.ToString();
            if (operation == "" || lbl_operation.Content.ToString().Last() == '=')
            {
                lbl_operation.Content = $"1/({number1})";
                number1 = d;
            }
            else if (lbl_operation.Content.ToString().Split('=').Length == 1)
            {
                if (lbl_operation.Content.ToString().First() == '√')
                {
                    lbl_operation.Content = $"√({number1 * number1}){operation}";
                }
                else if (lbl_operation.Content.ToString().First() == 's')
                {
                    lbl_operation.Content = $"sqr({Math.Sqrt(number1)}){operation}";
                }
                else if (lbl_operation.Content.ToString()[2] == '(')
                {
                    lbl_operation.Content = $"1/({1 / number1}){operation}";
                }
                lbl_operation.Content += $"1/({number2})";
                number2 = d;
            }
        }

        private void ButtonMPlus_Click(object sender, RoutedEventArgs e)
        {
            if (lbl_result.Content.ToString().Last() == '.')
            {
                string s = lbl_result.Content.ToString();
                lbl_result.Content = "";
                foreach (var item in s)
                {
                    if (item != '.')
                    {
                        lbl_result.Content += item.ToString();
                    }
                }
            }
            mTotal += double.Parse(lbl_result.Content.ToString());
            btn_MC.IsEnabled = true;
            btn_MR.IsEnabled = true;
            btn_M.IsEnabled = true;
        }

        private void ButtonMMinus_Click(object sender, RoutedEventArgs e)
        {
            if (lbl_result.Content.ToString().Last() == '.')
            {
                string s = lbl_result.Content.ToString();
                lbl_result.Content = "";
                foreach (var item in s)
                {
                    if (item != '.')
                    {
                        lbl_result.Content += item.ToString();
                    }
                }
            }
            mTotal -= double.Parse(lbl_result.Content.ToString());
            btn_MC.IsEnabled = true;
            btn_MR.IsEnabled = true;
            btn_M.IsEnabled = true;
        }

        private void ButtonMC_Click(object sender, RoutedEventArgs e)
        {
            mTotal = 0;
            btn_MC.IsEnabled = false;
            btn_MR.IsEnabled = false;
            btn_M.IsEnabled = false;
        }

        private void ButtonMS_Click(object sender, RoutedEventArgs e)
        {
            mTotal = 0;
            btn_MC.IsEnabled = true;
            btn_MR.IsEnabled = true;
            btn_M.IsEnabled = true;
        }

        private void ButtonMR_Click(object sender, RoutedEventArgs e)
        {
            lbl_operation.Content = "";
            operation = "";
            number2 = 0;
            lbl_result.Content = mTotal.ToString();
            ContentToNumber();
        }
    }
}
