using MachineLearningML.Model;
using System;

namespace myMLApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new ModelInput();

            input.SentimentText = "That is rude.";

            ModelOutput result = ConsumeModel.Predict(input);

            Console.WriteLine($"Text: {input.SentimentText}\n Is Toxic: {result.Prediction} {result.Score}");

            Console.WriteLine("Get Started ML.NET!");

            Console.WriteLine("Press any key to exit......");
            Console.ReadKey();
        }
    }
}
