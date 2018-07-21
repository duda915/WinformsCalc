using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsCalc
{
    public enum Sign
    {
        NONE,
        ADD,
        SUBTRACT,
        MULTIPLY,
        DIVIDE
    }

    public enum Digit
    {
        ZERO,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE
    }

    public class Calc : INotifyPropertyChanged
    {
        private String firstField;
        private String secondField;
        private String sign;
        private String equation;

        private double firstNumber;
        private double secondNumber;
        private Sign operation;

        public event PropertyChangedEventHandler PropertyChanged;

        public Calc()
        {
            firstField = "";
            secondField = "";
            sign = "";
            equation = "";
        }

        public String Equation
        {
            get
            { 
                return equation;
            }

            private set
            {
                equation = value;
                OnPropertyChanged("Equation");
            }

        }

        public void InsertValue(double insertValue)
        {
            if (operation == Sign.NONE)
            {
                firstField += insertValue.ToString();

                try
                {
                    firstNumber = Double.Parse(firstField);
                }
                catch (FormatException e)
                {
                    //Only valid input allowed
                }

            } else
            {
                secondField += insertValue.ToString();

                try
                {
                    secondNumber = Double.Parse(secondField);
                }
                catch (FormatException e)
                {
                    //Only valid input allowed
                }

            }


            UpdateEquation();
            
        }

        public void InsertDigit(Digit digit)
        {
            if (operation == Sign.NONE)
            {
                firstField += (int) digit;

                try
                {
                    firstNumber = Double.Parse(firstField);
                }
                catch (FormatException e)
                {
                    //Only valid input allowed
                }

            }
            else
            {

                secondField += (int)digit;

                try
                {
                    secondNumber = Double.Parse(secondField);
                }
                catch (FormatException e)
                {
                    //Only valid input allowed
                }

            }

            UpdateEquation();
        }

        public void FlipSign()
        {
            if (operation == Sign.NONE)
            {
                if(firstNumber == 0)
                {
                    return;
                }
                else if(firstNumber > 0)
                {
                    firstField = firstField.Insert(0, "-");
                }
                else
                {
                    firstField = firstField.Remove(0, 1);
                }

                try
                {
                    firstNumber = Double.Parse(firstField);
                }
                catch (FormatException e)
                {
                    //Only valid input allowed
                }

            }
            else
            {

                if (secondNumber == 0)
                {
                    return;
                }
                else if (secondNumber > 0)
                {
                    secondField = secondField.Insert(0, "-");
                }
                else
                {
                    secondField = secondField.Remove(0, 1);
                }


                try
                {
                    secondNumber = Double.Parse(secondField);
                }
                catch (FormatException e)
                {
                    //Only valid input allowed
                }

            }
            UpdateEquation();
        }

        public void InsertComma()
        {
            if (operation == Sign.NONE && !firstField.Contains(","))
            {
                firstField += ",";

                try
                {
                    firstNumber = Double.Parse(firstField);
                }
                catch (FormatException e)
                {
                    //Only valid input allowed
                }

            }
            else if(operation != Sign.NONE && !secondField.Contains(","))
            {

                secondField += ",";

                try
                {
                    secondNumber = Double.Parse(secondField);
                }
                catch (FormatException e)
                {
                    //Only valid input allowed
                }

            }

            UpdateEquation();
        }

        public void SetOperation(Sign sign)
        {
            //if (operation == Sign.NONE)
            //{
            //    try
            //    {
            //        firstNumber = Double.Parse(firstField);
            //    }
            //    catch (FormatException e)
            //    {
            //        return;

            //    }
            //    operation = sign;
            //    InsertSign(sign);
            //}
            //else
            //{
            //    CalculateResult();
            //    firstField = Equation;

            //    try
            //    {
            //        firstNumber = Double.Parse(firstField);
            //    }
            //    catch (FormatException e)
            //    {
            //        return;

            //    }

            //    operation = sign;
            //    InsertSign(sign);
            //}

            if (firstField.Equals(""))
                return;
            else if(secondField.Equals(""))
            {             
                operation = sign;
                InsertSign(sign);
            }
            else 
            {
                CalculateResult();
                InsertValue(Double.Parse(Equation));

                operation = sign;
                InsertSign(sign);
            }

            UpdateEquation();
        }

        private void InsertSign(Sign sign)
        {
            if (sign == Sign.ADD)
                this.sign = "+";
            else if (sign == Sign.DIVIDE)
                this.sign = "/";
            else if (sign == Sign.MULTIPLY)
                this.sign = "*";
            else if (sign == Sign.SUBTRACT)
                this.sign = "-";
        }

        public void Clear()
        {
            SoftClear();
            UpdateEquation();
        }
        
        /// <summary>
        /// Clear without notifying GUI
        /// </summary>
        private void SoftClear()
        {
            //without notifying
            firstField = "";
            firstNumber = 0;
            secondField = "";
            secondNumber = 0;
            sign = "";
            operation = Sign.NONE;
        }

        public void CalculateResult()
        {
            try
            {
                Equation = GetResult();
            }
            catch(FormatException e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            SoftClear();
        }

        private String GetResult()
        {

            if (operation == Sign.ADD)
                return (firstNumber + secondNumber).ToString();
            else if (operation == Sign.DIVIDE)
                return (firstNumber / secondNumber).ToString();
            else if (operation == Sign.MULTIPLY)
                return (firstNumber * secondNumber).ToString();
            else if (operation == Sign.SUBTRACT)
                return (firstNumber - secondNumber).ToString();
            else
                return equation;

        }

        private void UpdateEquation()
        {
            Equation = firstField + " " + sign + " " + secondField;
        }

        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

    }
}
