using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsCalc
{
    public partial class Form1 : Form
    {
        #region TitleBarDrag
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion


        private Calc newCalculator;
        private Boolean mouseDown = false;
        private Point lastLocation;

        public Form1()
        {
            InitializeComponent();
            newCalculator = new Calc();

            //bindings
            mathBox.DataBindings.Add(new Binding("Text", newCalculator, "Equation"));
        }


        //GUI HANDLING
        private void button7_Click(object sender, EventArgs e)
        {
            newCalculator.InsertDigit(Digit.SEVEN);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            newCalculator.InsertDigit(Digit.EIGHT);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            newCalculator.InsertDigit(Digit.NINE);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            newCalculator.InsertDigit(Digit.FOUR);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            newCalculator.InsertDigit(Digit.FIVE);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            newCalculator.InsertDigit(Digit.SIX);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            newCalculator.InsertDigit(Digit.ONE);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            newCalculator.InsertDigit(Digit.TWO);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            newCalculator.InsertDigit(Digit.THREE);

        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            newCalculator.SetOperation(Sign.ADD);
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            newCalculator.SetOperation(Sign.MULTIPLY);
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            newCalculator.SetOperation(Sign.SUBTRACT);
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            newCalculator.SetOperation(Sign.DIVIDE);
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            newCalculator.CalculateResult();
        }

        private void button0_Click(object sender, EventArgs e)
        {
            newCalculator.InsertDigit(Digit.ZERO);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            newCalculator.Clear();
        }

        private void buttonComma_Click(object sender, EventArgs e)
        {
            newCalculator.InsertComma();
        }

        private void buttonPlusMinus_Click(object sender, EventArgs e)
        {
            newCalculator.FlipSign();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void windowControlPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }


    }
}
