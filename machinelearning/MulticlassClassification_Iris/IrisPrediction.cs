using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace MulticlassClassification_Iris
{
   public class IrisPrediction
    {
        [ColumnName("Score")]
        public float[] Score;
    }
}
