using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Models.GA
{
    class GA
    {
        private int MaxIteration { get { return 150; } }

        Population CreateInitialPopulation()
        {
            return new Population();
        }
        bool TerminationCondition(Population population)
        {
            if (population.Generation > 0) return true;

            return false;
        }

        bool Evaluate(Population population)
        {
            return false;
        }

        Population Selection(Population population)
        {

            return population;
        }

        Population HillClimbing(Population population)
        {
            return population;
        }

        Population Hepermutation(Population population)
        {
            return population;
        }
        Population Mutation(Population population)
        {
            return population;
        }
        Population Crossover(Population population)
        {
            return population;
        }



        void Execute()
        {
            int generationNumber = 0;
            Population p = this.CreateInitialPopulation();
            this.Evaluate(p);

            while( !TerminationCondition(p) )
            {
                generationNumber++;
                p = this.Selection(p);
                p = this.Crossover(p);
                p = Mutation(p);
            }
        }

    }
}
