using System;

namespace lab9
{
    delegate void CalculatorUpdateOutput(Calculator sender, double value, int precision);
    delegate void CalculatorInternalError(Calculator sender, string message);

    enum CalculatorOperation { Add, Sub, Mul, Div }

    class Calculator
    {
        double? input = null;
        double? result = null;
        bool point = false;
        int? count = null;

        CalculatorOperation? op = null;

        public event CalculatorUpdateOutput DidUpdateValue;
        public event CalculatorInternalError InputError;
        public event CalculatorInternalError EqullyError;

        public void AddDigit(int digit)
        {
            if (input.HasValue && Math.Log10(input.Value) > 9)
            {
                InputError?.Invoke(this, "Value is too long for this calculator");
                return;
            }
            if (count > 8)
            {
                InputError?.Invoke(this, "Value is too long for this calculator");
                return;
            }
            if (!op.HasValue)
            {
                result = null;
            }

            if (point)
            {
                count = (count ?? 0);
                input = ((input ?? 0) * Math.Pow(10, (double)count) + (double)digit / 10) / Math.Pow(10, (double)count);
                DidUpdateValue?.Invoke(this, input.Value, 0);
                count++;
            }
            else
            {
                input = (input ?? 0) * 10 + digit;
                DidUpdateValue?.Invoke(this, input.Value, 0);
            }
        }

        public void AddOperation(CalculatorOperation op)
        {
            if (input.HasValue && result.HasValue && this.op.HasValue)
            {
                Compute();
            }

            if (!input.HasValue)
            {
                result = 0;
            }

            if (!result.HasValue)
            {
                result = input;
                input = null;
            }
            this.op = op;
        }

        public void Compute()
        {
            switch (op)
            {
                case CalculatorOperation.Add:
                    result = result + (input ?? 0);
                    DidUpdateValue?.Invoke(this, result.Value, 0);
                    input = 0;
                    break;

                case CalculatorOperation.Div:
                    if (input != null && input.Value != 0)
                    {
                        result = result / (input ?? 0);
                        DidUpdateValue?.Invoke(this, result.Value, 0);
                        input = 0;
                        break;
                    }
                    else
                    {
                        EqullyError?.Invoke(this, "Division by zero");
                        break;
                    }

                case CalculatorOperation.Mul:
                    result = result - (input ?? 0);
                    DidUpdateValue?.Invoke(this, result.Value, 0);
                    input = 0;
                    break;

                case CalculatorOperation.Sub:
                    result = result * (input ?? 0);
                    DidUpdateValue?.Invoke(this, result.Value, 0);
                    input = 0;
                    break;
            }
            op = null;
        }

        public void Clear()
        {
            input = null;
            DidUpdateValue?.Invoke(this, 0, 0);
        }

        public void ClearAll()
        {
            input = null;
            result = null;
            op = null;
            DidUpdateValue?.Invoke(this, 0, 0);
        }

        public void Point()
        {
            point = true;
        }

        public void Polar()
        {
            input = -(input);
            DidUpdateValue?.Invoke(this, input.Value, 0);
        }
        
        public void CountPointOff()
        {
            count = null;
            point = false;
        }
        
        public void SQRT()
        {
            if (left.HasValue && left != 0)
            {
                left = Math.Sqrt((double)left);
            }
            DidUpdateValue?.Invoke(this, input.Value, count);
        }
    }
}

