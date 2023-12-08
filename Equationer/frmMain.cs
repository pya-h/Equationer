using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Equationer
{

    public partial class frmMain : Form
    {
        private List<Term> terms;
        private char equationUnknown;
        private int solutionStep = 0;
        private bool calculationRemains = false;

        public frmMain()
        {
            InitializeComponent();
            equationUnknown = Term.Nothing;
            terms = new List<Term>();
            listSolution.DrawMode = DrawMode.OwnerDrawVariable;
            listSolution.MeasureItem += listSolution_MeasureItem;
            listSolution.DrawItem += listSolution_DrawItem;
        }

        private void listSolution_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 30; // Set the height of each item
        }

        private void listSolution_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                e.DrawBackground();

                string expression = listSolution.Items[e.Index].ToString();
                DrawSuperscriptText(expression, e.Graphics, e.Bounds, e.Font, e.ForeColor);

                e.DrawFocusRectangle();
            }
        }

        private void DrawSuperscriptText(string text, Graphics g, Rectangle bounds, Font font, Color color)
        {
            float previousWidth = 0;
            // Split the text into base and exponent parts
            for (int i = 0, j = 1; i < text.Length; i += j, j = 1)
            {
                for (; i + j < text.Length && text[i + j] != '^'; j++) ;
                // Draw the entire text if it doesn't contain '^'
                string baseTerm = text.Substring(i, j);
                
                g.DrawString(baseTerm, font, new SolidBrush(color), previousWidth + bounds.X, bounds.Y);
                i += j + 1; j = 0;
                if (i + j < text.Length)
                {
                    for (; i + j < text.Length && !IsOperator(text[i + j]); j++) ;

                    // TODO: ** WHAT ABOUT A ^ 2 ^ 3 ? **

                    // Draw exponent text as superscript
                    previousWidth += g.MeasureString(baseTerm, font).Width * 0.95f; // TODO: this may change
                    float yOffset = (font.Height - font.GetHeight()) / 2;
                    float fontSize = float.Parse(font.Size.ToString()) * 0.6f;
                    string exponentTerm = text.Substring(i, j);
                    Font exponentTermFont = new Font(font.FontFamily, fontSize);
                    g.DrawString(exponentTerm, exponentTermFont, new SolidBrush(color), bounds.X + previousWidth, bounds.Y - yOffset);
                    previousWidth += g.MeasureString(exponentTerm, exponentTermFont).Width * 0.95f; // TODO: this may change
                }
            }
                
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            txtInput.Focus();
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            string input;
            
            try
            {
                equationUnknown = Term.Nothing;
                input = DeleteNonSense(txtInput.Text);
                terms.Clear();
                AnalizeInput(input);

                UpdateDegrees();
                listSolution.Items.Clear();
                listSolution.Items.Add(GetNewStep());
                
                do
                {
                    calculationRemains = false;
                    while (CalculatePowers()) ;
                    while (CalculateMultiplyOrDevide()) ;
                    while (CalculatePlusOrMinus()) ;
                    UpdateDegrees();
                } while (calculationRemains);

                btnSaveSolution.Enabled = true;
                /*  fucked up errors */
                /****************     2+((3*5*x - 6x + 3) * 2 + 1 + x) = 0         ********************/
                /********** x^2 * x^3 = 0 *****************/
                /************       x^2^2^2 * 3 
                                    x^2 * 4x = 0                  ***************/
                //              ((x^2)^2)^2 ,   (x^2)2)3
                //              ((x + 1) + 1 ) 
                //              ((x^2)^2)^3=1
                //              x * x^-1
                // if parenthesis is unsolveable but has a coefiicient then multiply, ex: 3*(x+1)
                /* maybe write term examiner methods in class! */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Wronge Equation!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listSolution.Items.Add(ex.Message.ToString());
            }
        }

        private void AnalizeInput(string input)
        {
            try
            {
                for(int i = 0;i < input.Length;i++)
                {
                    Console.WriteLine(input[i]);
                    //( 3 + 4 )x , (3 + 4)3
                    // ( 2  = 3 )
                    if (IsNumeric(input[i]) || ((i == 0 || IsOperator(input[i - 1]) || input[i - 1] == '(') && input[i] == equationUnknown))
                    {
                        int j, sign = 1;
                        int pBefore = 0, pAfter = 0;

                        if (i != 0)
                        {
                            int ii = input[i - 1] != '-' ? i : i - 1;
                            for (pBefore = 0 ; pBefore <= ii-1 && input[ii - pBefore - 1] == '('; pBefore++) ;

                            if ((i > 1 && (IsOperator(input[i - 2]) || input[i - 2] == '(') && (input[i - 1] == '+' || input[i - 1] == '-')) || (i == 1 && IsOperator(input[0])))
                                sign = input[i - 1] == '-' ? -1 : 1;
                        }
                        for (j = i + 1; j < input.Length && IsNumeric(input[j]); j++) ;
                        double value = input[i] != equationUnknown ? (j < input.Length ? Double.Parse(input.Substring(i, j - i)) : Double.Parse(input.Substring(i))) : 1.0;
                        value *= sign;
                        char unknown = input[i] != equationUnknown ? Term.Nothing : equationUnknown, op = Term.Nothing; // ??????????????????
                        int operatorPosition;
                        if (j < input.Length && input[j] == equationUnknown)
                        {
                            unknown = equationUnknown;
                            operatorPosition = j + 1;
                        }
                        else
                            operatorPosition = j;

                        if (operatorPosition < input.Length)
                        {
                            if (IsOperator(input[operatorPosition]))
                                op = input[operatorPosition];
                            else
                            {
                                for (; operatorPosition < input.Length && input[operatorPosition] == ')'; operatorPosition++, pAfter++) ;
                                op = operatorPosition < input.Length ? input[operatorPosition] : Term.Nothing;
                            }
                        }
                        i = operatorPosition;                        
                        terms.Add(new Term(value, unknown, op, pBefore, pAfter));
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error in analizing input! cause: " + ex.Message, "Analize Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateDegrees()
        {

            for(int i = 0; i < terms.Count; i++)
            {
                if(terms[i].Unknown == equationUnknown)
                {
                    if(terms[i].Operator == '^')
                    {
                        if(terms[i + 1].ParenthesisBefore == 0)// && terms[i+1].Unknown == Term.Nothing)
                        {
                            terms[i].Degree = new Term(terms[i + 1].Value, terms[i + 1].Unknown, Term.Nothing, 0, 0);
                            RemoveSecondOperand(i);
                            calculationRemains = true;
                            // i-- needed ? i dont think, its just a hint for future debugging
                        }
                        else
                        {
                            // like: x ^ (2 + ...) + ...
                        }
                    }
                    
                }
            }

            
        }

        private bool EditZeroPower()
        {
            for(int i = 0;i < terms.Count; i++)
                if (terms[i].Operator == '^' && terms[i].Unknown != Term.Nothing && terms[i + 1].Value == 0.0)
                {
                    terms[i].Unknown = Term.Nothing;
                    RemoveSecondOperand(i);
                    return true;
                }
            return false;
        }

        private bool CalculatePowers()
        {
            for(int i = 0;i < terms.Count;i++)
            {
                // 2^) ...
                if(terms[i].Operator == '^')
                {
                    while (terms[i + 1].Operator == '^') i++ ;
                    if(terms[i].Unknown == Term.Nothing && terms[i+1].Unknown == Term.Nothing && terms[i].ParenthesisAfter == 0 && terms[i+1].ParenthesisBefore == 0)
                    {
                        terms[i].Value = Math.Pow(terms[i].Value, terms[i + 1].Value);
                        RemoveSecondOperand(i);
                        listSolution.Items.Add(GetNewStep());
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CalculateMultiplyOrDevide()
        {
            for(int i = 0;i < terms.Count;i++)
            {
                bool shallRemove = true;
                // 3 * ) + ...
                // ******   term[i].Operator == '^' ==> find * character position and ...
                if(terms[i].Operator == '*' && terms[i].ParenthesisAfter == 0 && terms[i+1].ParenthesisBefore == 0) 
                {
                    // x^2^2^2 * 3 .... errroooooor
                    // x^2 * 4x = 0
                    
                    terms[i].Value *= terms[i + 1].Value;
                    if(terms[i+1].Unknown != Term.Nothing)
                    {
                        if(terms[i].Unknown == Term.Nothing)
                            terms[i].Unknown = terms[i+1].Unknown;
                        else
                        {
                            if(terms[i+1].Operator != '^')
                            {
                                terms[i].Operator = '^';
                                terms[i+1].Value = 2.0;
                                terms[i+1].Unknown = Term.Nothing;
                                shallRemove = false;
                            }
                            else
                            {
                                int numberOfParanthesis = 0, insertIndex = i+1;
                                do{
                                    numberOfParanthesis += terms[ ++insertIndex ].ParenthesisBefore - terms[insertIndex].ParenthesisAfter;
                                } while (numberOfParanthesis > 0 && insertIndex < terms.Count - 1);
                                terms.Insert(insertIndex + 1,new Term(1.0, Term.Nothing, terms[insertIndex].Operator, 0 , 1));
                                terms[i + 2].ParenthesisBefore++;
                                terms[insertIndex].Operator = '+';
                                /**************     change calculationRemains or Not? ( seems it has been handled by getANewStep() )                  ***************/
                            }
                        }

                    }
                    if(shallRemove)
                        RemoveSecondOperand(i);
                    listSolution.Items.Add(GetNewStep());
                    return true;
                }
                if (terms[i].Operator == '/' && terms[i].ParenthesisAfter == 0 && terms[i + 1].ParenthesisBefore == 0)
                {
                    if (terms[i + 1].Value == 0)
                        throw new Exception(terms[i].Value.ToString() + "/0: Cannot devide by zero!");
                    terms[i].Value /= terms[i + 1].Value;
                    RemoveSecondOperand(i);
                    listSolution.Items.Add(GetNewStep());
                    return true;
                }
            }
            return false;
        }

        private bool CalculatePlusOrMinus()
        {
            for (int i = 0; i < terms.Count - 1; i++)
            {
                /////x-2+3=0. neg & pos
                // 2 + ( 3 + ) =...

                if (terms[i].Operator == '+' || terms[i].Operator == '-')
                {
                    int iNext = 0; bool found = false;
                    // go next until reaching the suitable operand for plus/minus
                    while ((found = i + ++iNext < terms.Count && terms[i + iNext - 1].Operator != '=' ) // check the term hasn't passed the boundary and put the value in bool found
                        // after loop we use bool found to determine that the loop has broken on a suitable operand and not on the boundary 
                        // now we check whether two terms are unifiable
                        && !(terms[i].Unknown == terms[i + iNext].Unknown && terms[i].Degree == terms[i + iNext].Degree
                            && IsTermSeperator(terms[i + iNext - 1].Operator) 
                            // now make sure that the operands are in the same level, meaning they're not seperated by ( or ) 
                            && terms[i].ParenthesisAfter == 0 && terms[i + iNext].ParenthesisBefore == 0 && (i == 0 ||
                                terms[i].ParenthesisBefore > 0 || IsTermSeperator(terms[i - 1].Operator)) 
                                    && (terms[i + iNext].ParenthesisAfter > 0 || IsTermSeperator(terms[i + iNext].Operator)))
                    ) ;
                    if(found) 
                    {
                        int signOfOperand1 = i != 0 && terms[i - 1].Operator == '-' ? -1 : 1,
                        signOfOperand2 = i != 0 && terms[i + iNext - 1].Operator == '-' ? -1 : 1;
                        terms[i].Value = signOfOperand1 * terms[i].Value + signOfOperand2 * terms[i + iNext].Value;
                        if (signOfOperand1 == -1 && !isFirstTermInSide(i))
                        {
                            if (terms[i].Value >= 0) // maybe check for i-1 not be equal sign !! check *A* if u didnt get what i mean:)
                                terms[i - 1].Operator = '+';
                            else CombineReelSignes(i);
                        }

                        RemoveOperand(i + iNext);
                        listSolution.Items.Add(GetNewStep());
                        return true;
                    }
                   // else if()
                }
            }
            return false;
        }

        private bool isFirstTermInSide(int index)
        {
            return index == 0 || terms[index - 1].Operator >= '<' && terms[index - 1].Operator <= '>';
        }

        private void CombineReelSignes(int index)
        {
            // + -a => - a, - -a => + a
            if (index < terms.Count && !isFirstTermInSide(index) && terms[index].Value < 0)
            {
                if(terms[index - 1].Operator == '+')
                {
                    terms[index].Value *= -1;
                    terms[index - 1].Operator = '-';
                }
                else if(terms[index - 1].Operator == '-')
                {
                    terms[index].Value *= -1;
                    terms[index - 1].Operator = '+';
                }
            }

        }
        private void RemoveOperand(int index)
        {
            terms[index - 1].Operator = terms[index].Operator;
            terms[index - 1].ParenthesisAfter = terms[index].ParenthesisAfter;
            terms.RemoveAt(index);
            CombineReelSignes(index);
        }

        private void RemoveSecondOperand(int index)
        {
            terms[index].Operator = terms[index + 1].Operator;
            terms[index].ParenthesisAfter = terms[index + 1].ParenthesisAfter;
            terms.RemoveAt(index + 1);
        }

        private bool IsTermSeperator(char c)
        {
            if (c == '+' || c == '-' || (c >= '<' && c <= '>') || c == Term.Nothing)
                return true;
            return false;
        }

        private bool DeleteZeroes()
        {
            for (int i = 0; i < terms.Count; i++)
            {
                if (terms[i].Value == 0 && (i != terms.Count - 1 || terms[i - 1].Operator < '<' || terms[i - 1].Operator > '>') && IsTermSeperator(terms[i].Operator) && (i == 0 || IsTermSeperator(terms[i - 1].Operator)))
                {
                    if (i != 0)
                    {
                        if (i == 0 || (terms[i - 1].Operator >= '<' && terms[i - 1].Operator <= '>'))
                        {
                            if (terms[i].Operator == '-')
                                terms[i + 1].Value *= -1.0;
                        }
                        else
                        {
                            terms[i - 1].Operator = terms[i].Operator;
                            terms[i - 1].ParenthesisAfter = terms[i].ParenthesisAfter;
                        }
                    }
                    else
                    {
                        if (terms[0].Operator == '=')
                            continue;
                        terms[1].ParenthesisBefore = terms[0].ParenthesisBefore;
                    }
                    terms.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        private string GetNewStep()
        {
             // define a toString() in Term class

            string step = (solutionStep++).ToString() + ".\t"; // use String.Format ;0 => 00, 1 => 01 , ...
            while (DeleteZeroes()) ;
            while (EditZeroPower()) ;
            
            for (int i = 0; i < terms.Count;i++ )
            {

                for (int j = 0; j < terms[i].ParenthesisBefore; j++)
                    step += "( ";
                if (terms[i].Value != 1.0 || terms[i].Unknown != equationUnknown)
                    step += terms[i].Value.ToString(); // add () to negative values
                if (terms[i].Unknown != Term.Nothing)
                {
                    step += terms[i].Unknown.ToString();
                    if (terms[i].Degree != null && terms[i].Degree.Value != 0.0 && terms[i].Degree.Value != 1.0)
                        step += " ^ " + terms[i].Degree.ToString();
                }
                for (int j = 0; j < terms[i].ParenthesisAfter; j++)
                    step += " )";
                if (terms[i].ParenthesisAfter > 0 && terms[i].ParenthesisBefore > 0)
                {
                    if (terms[i].ParenthesisAfter >= terms[i].ParenthesisBefore)
                    {
                        terms[i].ParenthesisAfter -= terms[i].ParenthesisBefore;
                        terms[i].ParenthesisBefore = 0;
                    }
                    else
                    {
                        terms[i].ParenthesisBefore -= terms[i].ParenthesisAfter;
                        terms[i].ParenthesisAfter = 0;
                    }
                    calculationRemains = true;
                }
                step += (IsTermSeperator(terms[i].Operator) ? "  " : " ") + terms[i].Operator.ToString() + (IsTermSeperator(terms[i].Operator) ? "  " : " ");

            }
            return step;
        }

        private string DeleteNonSense(string text)
        {
            text = text.Replace(" ", "").ToLower();
            if (IsOperator(text[0]) && text[0] != '+' && text[0] != '-')
                throw new Exception("The equation is started with an operator!");
            CheckForIllegalChars(text);
            if ((equationUnknown = FindUnknown(text)) == Term.Nothing)
                throw new Exception("The equation has no unknown character!");
            checkForExtraUnknowns(text, equationUnknown);
            text = SimplifyOperators(text);
            solutionStep = 0;
            return text;
        }

        private string SimplifyOperators(string text)
        {
            while (IsOperator(text[text.Length - 1]))
                text = text.Remove(text.Length - 1, 1);
            for (int i = 0; i < text.Length-1;i++ )
            {
                if (text[i] == '+')
                {
                    if (text[i + 1] == '-' || text[i + 1] == '+')
                    {
                        text = text.Remove(i, 1);
                        i--;
                    }
                }
                else if (text[i] == '-')
                {
                    if (text[i + 1] == '+')
                    {
                        text = text.Remove(i+1, 1);
                        i--;
                    }
                    else if(text[i+1] == '-')
                    {
                        if(i == 0)
                        {
                            text = text.Remove(0, 2);
                            i--;
                        }
                        else
                        {
                            text = text.Substring(0, i) + "+" + text.Substring(i + 2);
                            i--;
                        }
                    }
                }
                else if(text[i] == '*' && text[i+1] == equationUnknown && IsNumeric(text[i - 1]))
                   text = text.Remove(i, 1);
                if(text[i] >= '<' && text[i] <= '>')
                {
                    // *= ?
                    while (IsOperator(text[i - 1]))
                    {
                        text = text.Remove(i - 1, 1);
                        i--;
                    }
                }
            }
            return text;
        }
        private void CheckForIllegalChars(string text)
        {
            int numberOfEquals = 0;
            foreach (char c in text)
            {
                if (c < '(' || c == 44 || c == ':' || c == ';' || c == '?' || c == '@' || (c > 'Z' && c < '^') || (c > '^' && c < 'a') || c > 'z')
                    throw new Exception("The equation conains illegal characters!");
                if (c >= '<' && c <= '>')
                    numberOfEquals++;
                if (numberOfEquals > 1)
                    throw new Exception("The equation has more than one \'=\' or \'<\' or \'>\' characters!");
            }
            if (numberOfEquals == 0)
                throw new Exception("The equation has no \'=\' or \'<\' or \'>\' character!");
        }
        private void checkForExtraUnknowns(string text,char unknown)
        {
            foreach(char c in text)
                if (c >= 'a' && c <= 'z' && c != unknown)
                    throw new Exception("The equation has more than one unknown!");
           
        }
        private char FindUnknown(string text)
        {
            foreach(char c in text)
            {
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                    return c;
            }
            return Term.Nothing;
        }

        private bool IsOperator(char c)
        {
            return (c == '^' || (c >= '*' && c <= '/') || (c >= '<' && c <= '>'));
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            
        }

        private bool IsNumeric(char c)
        {
            if (c == '.' || (c >= '0' && c <= '9'))
                return true;
            return false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listSolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtInput_Leave(object sender, EventArgs e)
        {
            txtInput.Focus();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                listSolution.SelectedIndex++;
            }
            catch
            {
                if (listSolution.Items.Count > 0)
                    listSolution.SelectedIndex = -1;
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                listSolution.SelectedIndex--;
            }
            catch
            {
                if (listSolution.Items.Count > 0)
                    listSolution.SelectedIndex = listSolution.Items.Count - 1;

            }
        }

        private void btnSaveSolution_Click(object sender, EventArgs e)
        {
            
            if (dlgSaveSolution.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(dlgSaveSolution.FileName.ToString(), true))
                {
                    foreach (string step in listSolution.Items)
                        sw.WriteLine(step);
                    sw.WriteLine();
                    sw.WriteLine();
                }
            }
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            listSolution.Items.Clear();
            btnSaveSolution.Enabled = false;
        }
    }

}
