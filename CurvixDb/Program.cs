using CurvixDb.Context;

namespace CurvixDb
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using AppDbContext dbContext = new AppDbContext();
        }
    }
}