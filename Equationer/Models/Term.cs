
namespace Equationer
{
    class Term
    {
        // *** HOW ABOUT USING LNCTRL ALGEBRA ALGORYTHM HERE? ***
        public const char Nothing = '\0', Empty = ' ';
    
        private double termValue;
        private Term degree = null;
        public bool degreeDetermined;
        private char unknown, termOperator;
        private int parenthesisBefore, parenthesisAfter;

        // DEFINE A toString() METHOD
        private void updateParenthesis()
        {
            if(this.parenthesisBefore > 0 && this.parenthesisAfter > 0)
            {
                
                if(this.parenthesisAfter >= this.parenthesisBefore)
                {
                    this.parenthesisAfter -= this.parenthesisBefore;
                    this.parenthesisBefore = 0;
                }
                else
                {
                    this.parenthesisBefore -= this.parenthesisAfter;
                    this.parenthesisAfter = 0;
                }
            }
        }

        public int ParenthesisBefore
        {
            get { return parenthesisBefore; }
            set
            {
                parenthesisBefore = value >= 0 ? value : 0;
                updateParenthesis();
            }
        }
        public int ParenthesisAfter
        {
            get { return parenthesisAfter; }
            set
            {
                parenthesisAfter = value >= 0 ? value : 0;
                updateParenthesis();
            }
        }

        public double Value
        {
            get { return termValue; }
            set { termValue = value; }
        }

        public Term Degree
        {
            get { return this.degree; }
            set { this.degree = value; this.DegreeDetermined = true; }
        }

        public bool DegreeDetermined
        {
            get { return this.degreeDetermined; }
            set { this.degreeDetermined = value; }
        }

        public char Unknown
        {
            get { return this.unknown ;}
            set
            {
                this.unknown = value;
            }
        }

        public char Operator
        {
            get { return this.termOperator; }
            set { this.termOperator = value;}
        }

        public Term(double v = 0.0, char u = Term.Nothing, char o = Term.Nothing, int pb = 0, int pa = 0)
        {

            this.Value = v;
            this.Unknown = u == Empty ? Nothing : u;
            this.Operator = o != Term.Empty ? o : Nothing;
            //this.Degree = new Term(this.Unknown == Term.Nothing || this.Unknown == Term.Empty ? 0.0 : this.Operator != '^' ? 1.0 : 2.0);

            this.ParenthesisAfter = pa;
            this.ParenthesisBefore = pb;
            this.DegreeDetermined = false;
        }

        public override string ToString()
        {
            string expression = "";
            if (this.Value != 1.0 || this.Unknown != Term.Nothing)
                expression += this.Value.ToString();
            if (this.Unknown != Term.Nothing)
                expression += this.Unknown;

            return this.Operator != Term.Nothing ? string.Format("{0} {1} ", expression, this.Operator) : expression;
        }
    }
}
