using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace SentimentAnalysis
{
    public class SentimentData
    {
        [Column("0")]
        public string SentimentText;

        [Column("1",name:"Label")]
        public float Sentiment;
    }
}
