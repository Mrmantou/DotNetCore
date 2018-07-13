using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Models;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentAnalysis
{
    class Program
    {
        private static string AppPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        private static string TrainDataPath => Path.Combine(AppPath, "datasets", "sentiment-imdb-train.txt");
        private static string TestDataPath => Path.Combine(AppPath, "datasets", "sentiment-yelp-test.txt");
        private static string ModelPath => Path.Combine(AppPath, "SentimentModel.zip");

        static async Task Main(string[] args)
        {
            //1、创建模型
            var model = await TrainModelAsync();

            //2、测试精确度
            Evaluate(model);

            //3、预测，识别情感类型
            var predictions = model.Predict(TestSentimentData.Sentiments);

            var sentimentsAndPredictions = TestSentimentData.Sentiments.Zip(predictions, (sentiment, prediction) => (sentiment, prediction));

            foreach (var item in sentimentsAndPredictions)
            {
                Console.WriteLine($"Sentiment: {item.sentiment.SentimentText}|Prediction: {(item.prediction.Sentiment ? "Positive" : "Negative")}");
            }

            Console.WriteLine();
        }

        private static void Evaluate(PredictionModel<SentimentData, SentimentPrediction> model)
        {
            var testData = new TextLoader(TestDataPath).CreateFrom<SentimentData>();

            var evaluator = new BinaryClassificationEvaluator();

            Console.WriteLine("=============== Evaluating model ===============");

            var metrics = evaluator.Evaluate(model, testData);

            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"Auc: {metrics.Auc:P2}");
            Console.WriteLine($"F1Score: {metrics.F1Score:P2}");
            Console.WriteLine("=============== End evaluating ===============");
            Console.WriteLine();
        }

        private static async Task<PredictionModel<SentimentData, SentimentPrediction>> TrainModelAsync()
        {
            //LearningPipeline定义机器学习任务需要的步骤
            var pipeLine = new LearningPipeline();
            //添加data loader
            pipeLine.Add(new TextLoader(TrainDataPath).CreateFrom<SentimentData>());
            //添加transforms
            pipeLine.Add(new TextFeaturizer("Features", "SentimentText"));
            //添加trainer/learner  这里添加 快速二叉树分类器
            pipeLine.Add(new FastTreeBinaryClassifier() { NumLeaves = 5, NumTrees = 5, MinDocumentsInLeafs = 2 });

            Console.WriteLine("=============== Training Model =============== ");
            var model = pipeLine.Train<SentimentData, SentimentPrediction>();

            await model.WriteAsync(ModelPath);

            Console.WriteLine("=============== End training ===============");
            Console.WriteLine("The model is saved to {0}", ModelPath);

            return model;
        }
    }
}
