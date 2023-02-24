namespace LabApp;

public partial class CalculatorPage : ContentPage
{

    string currentInput = "";
    int currentState = 1;
    string currentOperator;
    double firstNum, secondNum;
    int dotCounter = 0;
    int operationCounter = 0;
    bool error = false;
    int a = 1;
    int b = 0;

	public CalculatorPage()
	{
		InitializeComponent();
        ButtonClearClicked(this, null);

	}

    private void ButtonNumberClicked(object sender, EventArgs e)
    {
        if (!error)
        {
            Button button = (Button)sender;
            string input = button.Text;
            currentInput += input;


            if ((this.ResultLabel.Text == "0" && input == "0") ||
                (currentInput.Length <= 1 && input != "0") ||
                currentState < 0)
            {
                this.ResultLabel.Text = "";
                if (currentState < 0)
                {
                    currentState *= -1;
                }
            }


            if (input == ".")
            {
                dotCounter++;
            }
            if (input == "." && dotCounter > 1)
            {
                input = "";
            }


            this.ResultLabel.Text += input;
        }
    }

    private void ButtonClearClicked(object sender, EventArgs e)
    {
        firstNum = 0;
        secondNum = 0;
        currentState= 1;
        this.ResultLabel.Text = "0";
        this.InputLabel.Text = "";
        currentInput = "";
        dotCounter= 0;
        operationCounter= 0;
        b = 0;
        error = false;
    }

    private void ButtonCalculateClicked(object sender, EventArgs e)
    {
        if (currentState==2) {
            if (secondNum==0)
            {
                GrabNumbers(ResultLabel.Text);
            }
            double result = Calculator.Calculate(firstNum, secondNum, currentOperator);
            this.InputLabel.Text = $"{firstNum} {currentOperator} {secondNum}";
            this.ResultLabel.Text = result.ToString();
            firstNum = result;
            secondNum = 0;
            currentState = -1;
            currentInput= "";
            dotCounter = 0;
            operationCounter = 0;
            b = 0;
            error = false;
        }
    }

    private void GrabNumbers(string text)
    {
        double num;
        if (double.TryParse(text, out num)) {
            if ((currentState == 1 || currentState==-1) )
            {
                firstNum= num;
            }
            else
            {
                secondNum= num;
            }
            currentInput = "";
        }
    }

    private void ButtonNegativeClicked(object sender, EventArgs e)
    {
        if (!error)
        {
            b = a ^ b;
            if (currentState >= 1 && Convert.ToBoolean(b))
            {
                this.ResultLabel.Text = "-" + this.ResultLabel.Text;
               
            }
            else if (currentState >= 1 && !Convert.ToBoolean(b))
            {
                this.ResultLabel.Text = this.ResultLabel.Text.Replace("-", "");
            }
        }
    }

    private void ButtonBackspaceClicked(object sender, EventArgs e)
    {
        if (!error)
        {
            if (this.ResultLabel.Text.Length == 1)
            {
                if (this.ResultLabel.Text == ".")
                {
                    dotCounter = 0;
                }
                this.ResultLabel.Text = "0";
            }
            else
            {
                if (this.ResultLabel.Text.EndsWith("."))
                {
                    dotCounter = 0;
                }
                string temp = this.ResultLabel.Text;
                this.ResultLabel.Text = temp.Remove(temp.Length - 1, 1);
                if (this.ResultLabel.Text == "-")
                {
                    this.ResultLabel.Text = "0";
                }
            }
        }

    }

    private void ButtonSqrtClicked(object sender, EventArgs e)
    {
        if (!error)
        {
            if (this.ResultLabel.Text.StartsWith("-"))
            {
                this.ResultLabel.Text = "Can't calculate";
                error = true;
            }
            else
            {
                this.ResultLabel.Text = Math.Sqrt(Convert.ToDouble(this.ResultLabel.Text)).ToString();
            }
        }
    }

    private void ButtonAbsClicked(object sender, EventArgs e)
    {
        double num = 0;
        if (double.TryParse(this.ResultLabel.Text, out num))
        {
            this.ResultLabel.Text = Math.Abs(num).ToString();
        }
    }

    private void ButtonSquareClicked(object sender, EventArgs e)
    {
        double num = 0;
        if (double.TryParse(this.ResultLabel.Text, out num))
        {
            this.ResultLabel.Text = (num*num).ToString();
        }
    }

    private void ButtonInverseClicked(object sender, EventArgs e)
    {

        double num = 0;
        if (double.TryParse(this.ResultLabel.Text, out num))
        {
            this.ResultLabel.Text = (1/num).ToString();
        }
    }

    private void ButtonPercentClicked(object sender, EventArgs e)
    {
        double num = 0;
        if (double.TryParse(this.ResultLabel.Text, out num))
        {
            this.ResultLabel.Text = (num/100).ToString();
        }
    }

    private void ButtonOperationClicked(object sender, EventArgs e)
    {
        operationCounter++;
        if (operationCounter <= 1)
        {
            b = 0;
            dotCounter = 0;
            GrabNumbers(ResultLabel.Text);
            currentState = -2;
            Button button = (Button)sender;
            string input = button.Text;
            currentOperator = input;
            
        }

    }
}