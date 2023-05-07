using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Services;

namespace WebApplicationV5
{
    /// <summary>
    /// Summary description for BrailleSystem
    /// </summary>
    [WebService(Namespace = "http://Braille.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public sealed class Logger
    {
        private static Logger instance;
        private static readonly object lockObject = new object();
        private static readonly string logFilePath = "G:\\projects\\C#\\SA\\WebApplicationV5\\WebApplicationV5\\log.txt";

        private Logger()
        {
            // Private constructor to prevent instantiation
        }

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new Logger();
                        }
                    }
                }
                return instance;
            }
        }

        public void Log(string message)
        {
            // Append the log message to the log file
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }


    public class BrailleSystem : System.Web.Services.WebService
    {
        private Logger logger;

        public BrailleSystem()
        {
            logger = Logger.Instance;
        }


        public static Tuple<string, int> numberToBrailleCodeConverter(double number)
        {
         

   

        //convert input number to a strinng
        string numString = number.ToString();

            //initialize the output Braille code
            string BrailleCode = " ⠼";
            int dotCount = 4;

            for (int counter = 0; counter < numString.Length; counter++)
            {
                string letter = numString[counter].ToString();
                switch (letter)
                {
                    case "0":
                        BrailleCode += "⠴";
                        dotCount += 3;
                        break;
                    case "1":
                        BrailleCode += "⠂";
                        dotCount += 1;
                        break;
                    case "2":
                        BrailleCode += "⠆";
                        dotCount += 2;
                        break;
                    case "3":
                        BrailleCode += "⠒";
                        dotCount += 2;
                        break;
                    case "4":
                        BrailleCode += "⠲";
                        dotCount += 3;
                        break;
                    case "5":
                        BrailleCode += "⠢";
                        dotCount += 2;
                        break;
                    case "6":
                        BrailleCode += "⠖";
                        dotCount += 3;
                        break;
                    case "7":
                        BrailleCode += "⠶";
                        dotCount += 4;
                        break;
                    case "8":
                        BrailleCode += "⠦";
                        dotCount += 3;
                        break;
                    case "9":
                        BrailleCode += "⠔";
                        dotCount += 2;
                        break;
                    case ".":
                        BrailleCode += "⠲";
                        dotCount += 3;
                        break;

                }

            }
            Tuple<string, int> outTuple = new Tuple<string, int>(BrailleCode, dotCount);
            return outTuple;
        }



        [WebMethod]
        public string GetShape_BrailleCode(string option)
        //public BrailleShape GetShapeBrailleCode(string option)
        {
            Dictionary<string, Tuple<string, int>> BrailleDictionary = new Dictionary<string, Tuple<string, int>>();

            BrailleDictionary.Add("circle", new Tuple<string, int>("⠫⠿", 6));
            BrailleDictionary.Add("triangle", new Tuple<string, int>("⠫⠞", 4));
            BrailleDictionary.Add("square", new Tuple<string, int>("⠫⠲", 3));

            string aBraille = "";

            switch (option)
            {
                case "Circle":
                    aBraille = BrailleDictionary["circle"].Item1;
                    break;

                case "Triangle":
                    aBraille = BrailleDictionary["triangle"].Item1;
                    break;

                case "Square":
                    aBraille = BrailleDictionary["square"].Item1;
                    break;

            }

            logger.Log($"GetShape_BrailleCode called with option: {option}");


            return aBraille;
        }

        [WebMethod]
        public int GetShape_DotAmount(string option)
        //public BrailleShape GetShapeBrailleCode(string option)
        {
            Dictionary<string, Tuple<string, int>> BrailleDictionary = new Dictionary<string, Tuple<string, int>>();

            BrailleDictionary.Add("circle", new Tuple<string, int>("⠫⠿", 6));
            BrailleDictionary.Add("triangle", new Tuple<string, int>("⠫⠞", 4));
            BrailleDictionary.Add("square", new Tuple<string, int>("⠫⠲", 3));

            int shapeDotAmount = 0;

            switch (option)
            {
                case "Circle":
                    shapeDotAmount = 4 + BrailleDictionary["circle"].Item2;
                    break;

                case "Triangle":
                    shapeDotAmount = 4 + BrailleDictionary["triangle"].Item2;
                    break;

                case "Square":
                    shapeDotAmount = 4 + BrailleDictionary["square"].Item2;
                    break;

            }


            return shapeDotAmount;
        }

        [WebMethod]
        public string GetShape_Value(string option)
        //public BrailleShape GetShapeBrailleCode(string option)
        {
            Dictionary<string, Tuple<string, int>> BrailleDictionary = new Dictionary<string, Tuple<string, int>>();

            BrailleDictionary.Add("circle", new Tuple<string, int>("⠫⠿", 6));
            BrailleDictionary.Add("triangle", new Tuple<string, int>("⠫⠞", 4));
            BrailleDictionary.Add("square", new Tuple<string, int>("⠫⠲", 3));

            //string aBraille = "";
            //int shapeDotAmount = 0;
            string shape = "";

            switch (option)
            {
                case "Circle":
                    //aBraille = BrailleDictionary["circle"].Item1;
                    //shapeDotAmount = 4 + BrailleDictionary["circle"].Item2;
                    shape = "Circle";
                    break;

                case "Triangle":
                    //aBraille = BrailleDictionary["triangle"].Item1;
                    //shapeDotAmount = 4 + BrailleDictionary["triangle"].Item2;
                    shape = "Triangle";
                    break;

                case "Square":
                    //aBraille = BrailleDictionary["square"].Item1;
                    //shapeDotAmount = 4 + BrailleDictionary["square"].Item2;
                    shape = "Square";
                    break;

            }


            return shape;
        }
    }

    public interface IShape
    {
        string Area_BrailleCode();

        int Area_DotAmount();

        double Area_NumericValue();

        string Perimeter_BrailleCode();

        int Perimeter_DotAmount();
        double Perimeter_NumericValue();
        

    }


    

    public class Shape
    {
        String shape_Name;

        
    }

    public class Square :Shape , IShape
    {
        [WebMethod]

        public static string Area_BrailleCode(double Length, double Width)

        {
            string sqArea_BrailleCode;
            double sqArea_NumericValue;

            double sqLength = Length;
            double sqWidth = Width;

            sqArea_NumericValue = Math.Round(sqLength * sqWidth, 3);

            //getting Calculated Area in braille code- <braillecode,dotAmount>
            Tuple<string, int> sqAreaBraille = BrailleSystem.numberToBrailleCodeConverter(sqArea_NumericValue);

            sqArea_BrailleCode = " ⠁⠗⠑⠁⠐⠶ " + sqAreaBraille.Item1;


            return sqArea_BrailleCode;
        }


        [WebMethod]

        public static int Area_DotAmount(double Length, double Width)

        {
            int sqArea_DotAmount;
            double sqArea_NumericValue;

            double sqLength = Length;
            double sqWidth = Width;

            sqArea_NumericValue = Math.Round(sqLength * sqWidth, 3);

            //getting Calculated Area in braille code- <braillecode,dotAmount>
            Tuple<string, int> sqAreaBraille = BrailleSystem.numberToBrailleCodeConverter(sqArea_NumericValue);

            sqArea_DotAmount = 13 + sqAreaBraille.Item2;

            return sqArea_DotAmount;
        }

        [WebMethod]
        public static double Area_NumericValue(double Length, double Width)

        {
            double sqArea_NumericValue;

            double sqLength = Length;
            double sqWidth = Width;

            sqArea_NumericValue = Math.Round(sqLength * sqWidth, 3);

            return sqArea_NumericValue;
        }


        [WebMethod]
        public static string Perimeter_BrailleCode(double Length, double Width)
        {
            double sqPerimeter_NumericValue;
            string sqPerimeter_BrailleCode;

            double sqLength = Length;
            double sqWidth = Width;

            sqPerimeter_NumericValue = Math.Round((sqLength + sqWidth) * 2, 3);

            //getting Calculated perimeter in braille code- <braillecode,dotAmount>
            Tuple<string, int> sqPerimeterBraille = BrailleSystem.numberToBrailleCodeConverter(sqPerimeter_NumericValue);

            sqPerimeter_BrailleCode = " ⠏⠑⠗⠊⠍⠑⠞⠑⠗⠐⠶ " + sqPerimeterBraille.Item1;

            return sqPerimeter_BrailleCode;
        }

        [WebMethod]
        public static int Perimeter_DotAmount(double Length, double Width)
        {
            double sqPerimeter_NumericValue;
            int sqPerimeter_DotAmount;

            double sqLength = Length;
            double sqWidth = Width;

            sqPerimeter_NumericValue = Math.Round((sqLength + sqWidth) * 2, 3);

            //getting Calculated perimeter in braille code- <braillecode,dotAmount>
            Tuple<string, int> sqPerimeterBraille = BrailleSystem.numberToBrailleCodeConverter(sqPerimeter_NumericValue);

            sqPerimeter_DotAmount = 32 + sqPerimeterBraille.Item2;

            return sqPerimeter_DotAmount;
        }

        [WebMethod]
        public static double Perimeter_NumericValue(double Length, double Width)
        {
            double sqPerimeter_NumericValue;

            double sqLength = Length;
            double sqWidth = Width;

            sqPerimeter_NumericValue = Math.Round((sqLength + sqWidth) * 2, 3);

            return sqPerimeter_NumericValue;
        }

        public string Area_BrailleCode()
        {
            throw new NotImplementedException();
        }

        public int Area_DotAmount()
        {
            throw new NotImplementedException();
        }

        public double Area_NumericValue()
        {
            throw new NotImplementedException();
        }

        public string Perimeter_BrailleCode()
        {
            throw new NotImplementedException();
        }

        public int Perimeter_DotAmount()
        {
            throw new NotImplementedException();
        }

        public double Perimeter_NumericValue()
        {
            throw new NotImplementedException();
        }
    }

    public class Circle: Shape, IShape
    {
        private Shape shapeCircle;

       
        [WebMethod]
        public static string Area_BrailleCode(double circleRadius)
        {
            double circleArea_NumericValue;
            string circleArea_BrailleCode;

            circleArea_NumericValue = Math.Round(Math.PI * circleRadius * circleRadius, 3);

            //getting Calculated Area in braille code- <braillecode,dotAmount>
            Tuple<string, int> circleAreaBraille = BrailleSystem.numberToBrailleCodeConverter(circleArea_NumericValue);

            circleArea_BrailleCode = " ⠁⠗⠑⠁⠐⠶ " + circleAreaBraille.Item1;

            return circleArea_BrailleCode;
        }

        [WebMethod]
        public static int Area_DotAmount(double circleRadius)
        {
            double circleArea_NumericValue;
            int circleArea_DotAmount;

            circleArea_NumericValue = Math.Round(Math.PI * circleRadius * circleRadius, 3);

            //getting Calculated Area in braille code- <braillecode,dotAmount>
            Tuple<string, int> circleAreaBraille = BrailleSystem.numberToBrailleCodeConverter(circleArea_NumericValue);

            circleArea_DotAmount = 13 + circleAreaBraille.Item2;

            return circleArea_DotAmount;

        }

        [WebMethod]
        public static double Area_NumericValue(double circleRadius)
        {
            double circleArea_NumericValue;

            circleArea_NumericValue = Math.Round(Math.PI * circleRadius * circleRadius, 3);

            return circleArea_NumericValue;

        }


        [WebMethod]
        public static string Perimeter_BrailleCode(double circleRadius)
        {
            double circlePerimeter_NumericValue;
            string circlePerimeter_BrailleCode;

            circlePerimeter_NumericValue = Math.Round(2 * Math.PI * circleRadius, 3);

            //getting Calculated Area in braille code- <braillecode,dotAmount>
            Tuple<string, int> circlePerimeterBraille = BrailleSystem.numberToBrailleCodeConverter(circlePerimeter_NumericValue);

            circlePerimeter_BrailleCode = " ⠏⠑⠗⠊⠍⠑⠞⠑⠗⠐⠶ " + circlePerimeterBraille.Item1;

            return circlePerimeter_BrailleCode;
        }

        [WebMethod]
        public static int Perimeter_DotAmount(double circleRadius)
        {
            double circlePerimeter_NumericValue;
            int circlePerimeter_DotAmount;

            circlePerimeter_NumericValue = Math.Round(2 * Math.PI * circleRadius, 3);

            //getting Calculated Area in braille code- <braillecode,dotAmount>
            Tuple<string, int> circlePerimeterBraille = BrailleSystem.numberToBrailleCodeConverter(circlePerimeter_NumericValue);

            circlePerimeter_DotAmount = 13 + circlePerimeterBraille.Item2;

            return circlePerimeter_DotAmount;
        }

        [WebMethod]
        public static double Perimeter_NumericValue(double circleRadius)
        {
            double circlePerimeter_NumericValue;

            circlePerimeter_NumericValue = Math.Round(2 * Math.PI * circleRadius, 3);

            return circlePerimeter_NumericValue;
        }

        public string Area_BrailleCode()
        {
            throw new NotImplementedException();
        }

        public int Area_DotAmount()
        {
            throw new NotImplementedException();
        }

        public double Area_NumericValue()
        {
            throw new NotImplementedException();
        }

        public string Perimeter_BrailleCode()
        {
            throw new NotImplementedException();
        }

        public int Perimeter_DotAmount()
        {
            throw new NotImplementedException();
        }

        public double Perimeter_NumericValue()
        {
            throw new NotImplementedException();
        }
    }

    public class Triangle: Shape, IShape
    {
        [WebMethod]

        public static string Area_BrailleCode(double sideA, double sideB, double AngleC)
        {
            string triArea_BrailleCode;
            double triArea_NumericValue;

            double triSideA = sideA;
            double triSideB = sideB;
            double triAngleC = AngleC;

            double radiansC = triAngleC * Math.PI / 180; //convert to radians

            triArea_NumericValue = Math.Round(0.5 * triSideA * triSideB * Math.Sin(radiansC), 3);

            //getting Calculated Area in braille code- <braillecode,dotAmount>
            Tuple<string, int> triAreaBraille = BrailleSystem.numberToBrailleCodeConverter(triArea_NumericValue);

            triArea_BrailleCode = " ⠁⠗⠑⠁⠐⠶ " + triAreaBraille.Item1;


            return triArea_BrailleCode;
        }

        [WebMethod]

        public static int Area_DotAmount(double sideA, double sideB, double AngleC)
        {
            double triArea_NumericValue;
            int triArea_DotAmount;

            double triSideA = sideA;
            double triSideB = sideB;
            double triAngleC = AngleC;

            double radiansC = triAngleC * Math.PI / 180; //convert to radians
            triArea_NumericValue = Math.Round(0.5 * triSideA * triSideB * Math.Sin(radiansC), 3);

            //getting Calculated Area in braille code- <braillecode,dotAmount>
            Tuple<string, int> triAreaBraille = BrailleSystem.numberToBrailleCodeConverter(triArea_NumericValue);

            triArea_DotAmount = 13 + triAreaBraille.Item2;


            return triArea_DotAmount;
        }


        [WebMethod]
        public static double Area_NumericValue(double sideA, double sideB, double AngleC)
        {
            double triArea_NumericValue;

            double triSideA = sideA;
            double triSideB = sideB;
            double triAngleC = AngleC;

            double radiansC = triAngleC * Math.PI / 180; //convert to radians
            triArea_NumericValue = Math.Round(0.5 * triSideA * triSideB * Math.Sin(radiansC), 3);


            return triArea_NumericValue;
        }

        [WebMethod]
        public static string Perimeter_BrailleCode(double sideA, double sideB, double AngleC)
        {
            string triPerimeter_BrailleCode;
            double triPerimeter_NumericValue;

            double triSideA = sideA;
            double triSideB = sideB;
            double triAngleC = AngleC;


            double radiansC = triAngleC * Math.PI / 180; //convert to radians
            double triSideC = Math.Sqrt(triSideA * triSideA + triSideB * triSideB - 2 * triSideA * triSideB * Math.Cos(radiansC));
            triPerimeter_NumericValue = Math.Round((triSideA + triSideB + triSideC), 3);

            //getting Calculated Area in braille code- <braillecode,dotAmount>
            Tuple<string, int> triAreaBraille = BrailleSystem.numberToBrailleCodeConverter(triPerimeter_NumericValue);

            triPerimeter_BrailleCode = " ⠁⠗⠑⠁⠐⠶ " + triAreaBraille.Item1;


            return triPerimeter_BrailleCode;
        }


        [WebMethod]
        public static int Perimeter_DotAmount(double sideA, double sideB, double AngleC)
        {
            double triPerimeter_NumericValue;
            int triPerimeter_DotAmount;

            double triSideA = sideA;
            double triSideB = sideB;
            double triAngleC = AngleC;


            double radiansC = triAngleC * Math.PI / 180; //convert to radians
            double triSideC = Math.Sqrt(triSideA * triSideA + triSideB * triSideB - 2 * triSideA * triSideB * Math.Cos(radiansC));
            triPerimeter_NumericValue = Math.Round((triSideA + triSideB + triSideC), 3);

            //getting Calculated Area in braille code- <braillecode,dotAmount>
            Tuple<string, int> triAreaBraille = BrailleSystem.numberToBrailleCodeConverter(triPerimeter_NumericValue);

            triPerimeter_DotAmount = 13 + triAreaBraille.Item2;

            return triPerimeter_DotAmount;
        }

        [WebMethod]
        public static double Perimeter_NumericValue(double sideA, double sideB, double AngleC)
        {
            double triPerimeter_NumericValue;

            double triSideA = sideA;
            double triSideB = sideB;
            double triAngleC = AngleC;

            double radiansC = triAngleC * Math.PI / 180; //convert to radians
            double triSideC = Math.Sqrt(triSideA * triSideA + triSideB * triSideB - 2 * triSideA * triSideB * Math.Cos(radiansC));
            triPerimeter_NumericValue = Math.Round((triSideA + triSideB + triSideC), 3);

            return triPerimeter_NumericValue;
        }

        public string Area_BrailleCode()
        {
            throw new NotImplementedException();
        }

        public int Area_DotAmount()
        {
            throw new NotImplementedException();
        }

        public double Area_NumericValue()
        {
            throw new NotImplementedException();
        }

        public string Perimeter_BrailleCode()
        {
            throw new NotImplementedException();
        }

        public int Perimeter_DotAmount()
        {
            throw new NotImplementedException();
        }

        public double Perimeter_NumericValue()
        {
            throw new NotImplementedException();
        }
    }
}
