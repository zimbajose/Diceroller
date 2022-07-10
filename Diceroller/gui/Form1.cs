
using Diceroller.interpreter;
namespace Diceroller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rollbutton_Click(object sender, EventArgs e)
        {
            string new_result= "Resultado ";
            Expression exp = null;
            try
            {
                exp = Interpreter.interpret(expressionbox.Text);
                if (exp is ComparativeRoll)
                {
                    new_result += exp.getResult().ToString() + " Acertos";
                }
                else if (exp is SimpleRoll)
                {
                    new_result += exp.getResult().ToString();
                }
                else 
                {
                    new_result += "0";
                }
            }
            catch (Exception ex) 
            { 
                new_result = new_result + ex.Message;
                Console.WriteLine(ex.StackTrace);
            }
            this.resultlabel.Text = new_result;
        }
    }
}