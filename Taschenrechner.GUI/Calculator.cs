using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Taschenrechner.GUI
{
    public partial class Calculator : Form
    {
        private StringBuilder input;

        private Taschenrechner.Calculator.Calculator Calc;

        public Calculator()
        {
            InitializeComponent();
            Init();
            Reset();
        }

        private void Init()
        {
            input = new StringBuilder();

            Calc = new Taschenrechner.Calculator.Calculator();

            #region Button Events
            EventHandler fOnButton = new EventHandler(OnButton);
            btnNumber0.Click += fOnButton;
            btnNumber1.Click += fOnButton;
            btnNumber2.Click += fOnButton;
            btnNumber3.Click += fOnButton;
            btnNumber4.Click += fOnButton;
            btnNumber5.Click += fOnButton;
            btnNumber6.Click += fOnButton;
            btnNumber7.Click += fOnButton;
            btnNumber8.Click += fOnButton;
            btnNumber9.Click += fOnButton;
            btnNumberDot.Click += fOnButton;
            btnOperatorPlus.Click += fOnButton;
            btnOperatorMinus.Click += fOnButton;
            btnOperatorMultiply.Click += fOnButton;
            btnOperatorMinus.Click += fOnButton;
            btnCalculate.Click += fOnButton;
            btnAdvanced.Click += fOnButton;
            #endregion

            ToggleAdvanced();
        }

        #region Events

        private void OnButton(object sender, EventArgs e)
        {
            if (!(sender is Button))
                return;

            switch (((Button) sender).Name)
            {
                #region Number Buttons
                case "btnNumber0": AddNumber('0'); break;
                case "btnNumber1": AddNumber('1'); break;
                case "btnNumber2": AddNumber('2'); break;
                case "btnNumber3": AddNumber('3'); break;
                case "btnNumber4": AddNumber('4'); break;
                case "btnNumber5": AddNumber('5'); break;
                case "btnNumber6": AddNumber('6'); break;
                case "btnNumber7": AddNumber('7'); break;
                case "btnNumber8": AddNumber('8'); break;
                case "btnNumber9": AddNumber('9'); break;
                case "btnNumberDot": AddNumber('.'); break;
                #endregion

                #region Operator Buttons
                case "btnOperatorPlus": ApplyOperator('+'); break;
                case "btnOperatorMinus": ApplyOperator('-'); break;
                case "btnOperatorMultiply": ApplyOperator('*'); break;
                case "btnOperatorDivide": ApplyOperator('/'); break;
                #endregion

                case "btnCalculate": Calculate(); break;

                case "btnAdvanced": ToggleAdvanced(); break;

                default:
                    break;
            }

            UpdateDisplay();
        }
        
        #endregion

        private void AddNumber(char number)
        {
            input.Append(number);
        }

        private void ApplyOperator(char op)
        {
            input.Append(op);
        }

        private void Calculate()
        {
            string expression = input.ToString();
            input.Clear();
            try
            {
                double result = Calc.Evaluate(input.ToString());
                input.Append(result);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            txbDisplay.Text = input.ToString();
        }

        private void Reset()
        {
            input.Clear();
            input.Append("0");
            UpdateDisplay();
        }

        const string AdvancedOpenText = "\\/";
        const string AdvancedCloseText = "/\\";
        const int MinSize = 308;
        int MaxSize = 0;
        bool AdvancedOpen = true;
        private void ToggleAdvanced()
        {
            if (MaxSize == 0 && AdvancedOpen)
            {
                MaxSize = Size.Height;
            }

            Size s = Size;
            if (AdvancedOpen)
            {
                btnAdvanced.Text = AdvancedOpenText;
                s.Height = MinSize;
            }
            else
            {
                btnAdvanced.Text = AdvancedCloseText;
                s.Height = MaxSize;
            }
            AdvancedOpen = !AdvancedOpen;
            Size = s;
        }
    }
}
