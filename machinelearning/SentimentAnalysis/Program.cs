using Microsoft.ML;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SentimentAnalysis
{
    class Program
    {
        private static string AppPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        private static string TrainDataPath => Path.Combine(AppPath, "datasets", "sentiment-imdb-train.txt");
        private static string RestDataPath => Path.Combine(AppPath, "datasets", "sentiment-yelp-test.txt");
        private static string ModelPath => Path.Combine(AppPath, "SentimentModel.zip");

        static async void Main(string[] args)
        {
            var model = await TrainModelAsync();
        }

        private static async Task<PredictionModel<SentimentData, SentimentPrediction>> TrainModelAsync()
        {
            var pipeLine = new LearningPipeline();

           
        }
    }
}
