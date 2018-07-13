using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MulticlassClassification_Iris
{
    class Program
    {
        private static string AppPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        private static string TrainDataPath => Path.Combine(AppPath, "datasets", "iris-train.txt");
        private static string TestDataPath => Path.Combine(AppPath, "datasets", "iris-test.txt");
        private static string ModelPath => Path.Combine(AppPath, "IrisModel.zip");

        static async Task Main(string[] args)
        {
            var model = await TrainAsync();
        }

        private static async Task<PredictionModel<IrisData, IrisPrediction>> TrainAsync()
        {
            var pipeLine = new LearningPipeline {
                new TextLoader(TrainDataPath).CreateFrom<IrisData>(),
                new ColumnConcatenator("Features","SepalLength","SepalWidth","PetalLength","PetalWidth"),
                new StochasticDualCoordinateAscentClassifier()
            };

            Console.WriteLine("=============== Training model ===============");

            var model = pipeLine.Train<IrisData, IrisPrediction>();

            await model.WriteAsync(ModelPath);

            Console.WriteLine("=============== End training ===============");
            Console.WriteLine("The model is saved to {0}", ModelPath);

            return model;
        }
    }
}
