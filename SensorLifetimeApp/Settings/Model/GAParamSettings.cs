using SensorLifetimeApp.Enums;
using System.Text;

namespace SensorLifetimeApp.Settings.Model
{
    public class GAParamSettings
    {
        public int PopulationSize { get; set; }
        public int NumberOfGenerations { get; set; }
        public CrossoverType CrossoverType { get; set; }
        public MutationType MutationType { get; set; }
        public decimal MutationProbability { get; set; }

        public decimal Coverage { get; set; }
        public SearchMode SearchMode { get; set; }

        // TODO: gen_number, seaching q_req ? <- jakis enum 

        public decimal MaxBatteryTime { get; set; }
        public int MaxNumberSingleSolutionSerachRuns { get; set; }
        public int Multiruns { get; set; }
        public int Seed { get; set; }

        public GAParamSettings()
        {
            this.PopulationSize = 100;
            this.NumberOfGenerations = 100;
            this.CrossoverType = CrossoverType.Type1;
            this.MutationType = MutationType.Type1;
            this.MutationProbability = 0.03m;
            this.Coverage = 0.8m;
            this.SearchMode = SearchMode.SearchingSingleSolution;

            this.MaxBatteryTime = 5;
            this.MaxNumberSingleSolutionSerachRuns = 1;
            this.Multiruns = 1;
            this.Seed = 0;
        }

        public string TestToString()
        {
            var builder = new StringBuilder();
            builder.Append($"PopulationSize = {this.PopulationSize}").Append("\n");
            builder.Append($"NumberOfGenerations = {this.NumberOfGenerations}").Append("\n");
            builder.Append($"CrossoverType = {this.CrossoverType}").Append("\n");
            builder.Append($"MutationType = {this.MutationType}").Append("\n");
            builder.Append($"MutationProbability = {this.MutationProbability}").Append("\n");
            builder.Append($"Coverage = {this.Coverage}").Append("\n");
            builder.Append($"SearchMode = {this.SearchMode}").Append("\n");
            builder.Append($"MaxBatteryTime = {this.MaxBatteryTime}").Append("\n");
            builder.Append($"MaxNumberSingleSolutionSerachRuns = {this.MaxNumberSingleSolutionSerachRuns}").Append("\n");
            builder.Append($"Multiruns = {this.Multiruns}").Append("\n");
            builder.Append($"Seed = {this.Seed}").Append("\n");

            return builder.ToString();
        }
    }


}