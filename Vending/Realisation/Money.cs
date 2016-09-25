namespace ProductVendingMachine
{
    public struct Money
    {
        public int Evros { get; set; }
        public int Cents { get; set; }

        /// <summary>
        /// For comfortable work we will realise simple ways to work with money
        /// (using overriding operators "+", "-" and bool logic.
        /// </summary>
        public static Money operator + (Money firstItem, Money secondItem)
        {
            Money m = new Money();
            m.Cents = firstItem.InCents() + secondItem.InCents();
            m.Transfer();
            return m;
        }

        public static Money operator - (Money min, Money sub)
        {
            Money m = new Money();
            m.Cents = min.InCents() - sub.InCents();
            m.Transfer();
            return m;
        }

        public static bool operator == (Money m1, Money m2)
        {
            return m1.InCents() == m2.InCents();
        }

        public static bool operator != (Money m1, Money m2)
        {
            return m1.InCents() != m2.InCents();
        }

        public static bool operator > (Money m1, Money m2)
        {
            return m1.InCents() > m2.InCents();
        }

        public static bool operator < (Money m1, Money m2)
        {
            return m1.InCents() < m2.InCents();
        }

        public static bool operator >= (Money m1, Money m2)
        {
            return m1.InCents() >= m2.InCents();
        }

        public static bool operator <= (Money m1, Money m2)
        {
            return m1.InCents() <= m2.InCents();
        }


        /// <summary>
        /// Method for correct "+" and "-" operations
        /// </summary>
        /// <returns></returns>
        public int InCents()
        {
            return Evros * 100 + Cents;
        }

        /// <summary>
        /// Method for correct Money presentation
        /// </summary>
        internal void Transfer()
        {
            while (Cents >= 100)
            {
                Cents -= 100;
                Evros += 1;
            }
        }

        /// <summary>
        /// Correct Money presentation in string form
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            this.Transfer();
            return Evros.ToString() + "." + Cents.ToString();
        }
    }
}
