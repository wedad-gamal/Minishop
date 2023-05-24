namespace Minishop.Infrastructure
{
    public class MinishopDBInitializer : DropCreateDatabaseAlways<MinishopDBContext>
    {
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void InitializeDatabase(MinishopDBContext context)
        {
            base.InitializeDatabase(context);
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        protected override void Seed(MinishopDBContext context)
        {
            base.Seed(context);
        }
    }
}
