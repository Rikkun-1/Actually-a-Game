using System;
using System.Linq;
using NSpec;
using NSpec.Domain;
using NSpec.Domain.Formatters;

namespace Tests
{
    class TestRunner
    {
        static void Main(string[] args)
        {
            var types      = typeof(TestRunner).Assembly.GetTypes();
            var finder     = new SpecFinder(types, "");
            var tagsFilter = new Tags().Parse("");
            var builder    = new ContextBuilder(finder, tagsFilter, new DefaultConventions());
            var runner     = new ContextRunner(tagsFilter, new ConsoleFormatter(), false);
            var results    = runner.Run(builder.Contexts().Build());

            if (results.Failures().Any())
            {
                Environment.Exit(1);
            }
            //Console.ReadKey();
        }
    }
}