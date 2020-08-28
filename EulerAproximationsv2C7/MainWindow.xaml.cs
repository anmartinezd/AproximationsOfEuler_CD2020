using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EulerAproximationsv2C7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var TaylorValues = AproximationByTaylorsSumatory();
            var seriesValues = AproximationByLimits();
            var stirlingValues = AproximationByStirlingLimit();
            var euler = Euler();

            //creación del gráfico
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title =  "Euler",
                    Values = euler
                },
                new LineSeries
                {
                    Title = "Método de Taylor",
                    Values = TaylorValues,
                },
                new LineSeries
                {
                    Title = "Método del Límite",
                    Values = seriesValues,
                },
                 new LineSeries
                {
                    Title = "Metodo de Stirling",
                    Values = stirlingValues,
                },
            };
            DataContext = this;

            
        }

        //Función para llenar una lista de el valor de Euler
        public ChartValues<decimal> Euler()
        {
            int n = 0;
            ChartValues<decimal> points = new ChartValues<decimal>();
            while (n <= 100)
            {
                points.Add((decimal)Math.E);
                n++;
            }
            return points;
        }

        //Creamos la función para hacer la aproximación del número de euler
        //por medio de  la sumatoria de Taylor
        public ChartValues<decimal> AproximationByTaylorsSumatory(int first_n_Numbers = 100)
        {
            ChartValues<decimal> points = new ChartValues<decimal>();
            decimal sumatory = 0;
            int n = 0;
            while (n <= first_n_Numbers)
            {
                sumatory = sumatory + (decimal)(1 / (double)Factorial(n));
                points.Add(sumatory);
                n++;
            }
            return points;
        }

        //Creamos la función para hacer la aproximación del número de euler
        //por medio del límite mostrado en el númeral 2 de la guía
        public static ChartValues<decimal> AproximationByLimits(int first_n_Numbers = 100)
        {
            ChartValues<decimal> points = new ChartValues<decimal>();
            decimal limit = 0;
            int n = 1;
            while (n <= first_n_Numbers)
            {
                double fracc = 1 / (double)n;
                limit = (decimal)(Math.Pow(1 + fracc, n));
                points.Add(limit);
                n++;
            }

            return points;
        }


        //Creamos la función para hacer la aproximación del número de euler
        //por medio del límite de Stirling
        public static ChartValues<decimal> AproximationByStirlingLimit(int first_n_Numbers = 100)
        {
            ChartValues<decimal> points = new ChartValues<decimal>();
            decimal limit = 0;
            int n = 1;
            double nFact = 0;
            while (n <= 100)
            {
                nFact = (double)Factorial(n);
                double exp = (1 / (double)n);
                decimal denom = (decimal)Math.Pow(nFact, (double)exp);
                limit =(n / denom);
                points.Add(limit);
                n++;
            }
            return points;
        }


        //función para realizar el cálculo del factorial
        private static BigInteger Factorial(int k)
        {
            if (k == 0 )
            {
                return 1;
            }
            else
            {
                return k * Factorial(k - 1);
            }
        }


        //propiedades para la graficación
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }
}
