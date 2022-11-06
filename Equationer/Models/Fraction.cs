
namespace Equationer
{
    class Fraction
    {
        private double numerator, denominator;

        public double Numerator
        {
            get { return this.numerator ;}
        }

        public double Denominator
        {
           get { return this.denominator ;}
        }

        public bool Set(double a, double b)
        {
            if (b != 0)
            {
                numerator = a;
                denominator = b;
                Simplify();
                return true;
            }

            return false;
        }

        public string Value
        {
            get { return this.numerator.ToString() + "/" + denominator.ToString(); }
        }

        // simplify : public or private ?
        private void Simplify()
        {
            if (numerator % denominator == 0)
            {
                numerator /= Denominator;
                denominator = 1.0;
            }
            else if(Denominator % numerator == 0)
            {
                denominator /= numerator;
                numerator = 1.0;
            }
            else
            {
                int end = numerator <= denominator ? (int)(numerator) : (int)(denominator); // error? why the fuck ?
                end /= 2;
                for(int i = 2; i <= end; i++)
                    if (numerator % i == 0 && denominator % i == 0) // shut up fuck
                    {
                        numerator /= i;
                        denominator /= i;
                        i--;
                    }
            }

        }

        public Fraction(double pNum, double pDenom)
        {
            Set(pNum, pDenom);
        }
    }
}
