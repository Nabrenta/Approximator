using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Solver;

namespace TestWork
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Number of our sourse dots (5, according to the task)
        /// </summary>
        private const int NUMBBER_OF_SOURSE_DOTS = 5;

        /// <summary>
        /// Position of the first sourse dot (1, according to the task)
        /// </summary>
        private const double FIRST_DOT_X = 1;

        /// <summary>
        /// Name of series represent sourse dots
        /// </summary>
        private const string SOURSE_DOTS_SERIES_NAME = "Dots";

        /// <summary>
        /// Bottom of Y axis
        /// </summary>
        private const double MIN_Y = -10;

        /// <summary>
        /// Top of Y axis
        /// </summary>
        private const double MAX_Y = 10;

        /// <summary>
        /// Data we need to summorite
        /// Key = x
        /// Value = y
        /// </summary>
        private SortedDictionary<double, double> SourseDots;

        /// <summary>
        /// Captured dot
        /// </summary>
        private double captured = 0;

        public MainForm()
        {
            //Initializate data set's 
            SourseDots = new SortedDictionary<double, double>();

            InitializeComponent();
        }

        /// <summary>
        /// When we click chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Clicked dot</param>
        private void MainChart_MouseDown(object sender, MouseEventArgs e)
        {
            //Information abour click result
            var DotWeGot = MainChart.HitTest(e.X, e.Y);

            //If we click one of x dots
            if ((DotWeGot.Series != null) && (DotWeGot.Series.Name == SOURSE_DOTS_SERIES_NAME))

                //capture it
                captured = MainChart.Series[SOURSE_DOTS_SERIES_NAME].Points[DotWeGot.PointIndex].XValue;

            //Els if we click on chart area
            else if (DotWeGot.ChartArea != null)
            {
                //Finde x coordinate of click on chart
                double clickedColumn = MainChart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);

                //If we clicked almost on one of the start dot's column
                if ((Math.Abs(Math.Round(clickedColumn) - clickedColumn) < this.Width*0.00005) && (Math.Round(clickedColumn) >= FIRST_DOT_X) && (Math.Round(clickedColumn) < (FIRST_DOT_X + NUMBBER_OF_SOURSE_DOTS)))
                {
                    //Move start dot
                    SourseDots[Math.Round(clickedColumn)] = MainChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);

                    //Rewrite all functions according to 
                    Rewrite();
                }
            }
        }

        private void MainChart_MouseMove(object sender, MouseEventArgs e)
        {
            //If mous left button is clamped and some dot is captured
            if ((MouseButtons == MouseButtons.Left) && (captured != 0))
            {
                //If we still on our chart area
                if (MainChart.HitTest(e.X, e.Y).ChartArea != null)
                {
                    //Move captured dot
                    SourseDots[captured] = MainChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);

                    //Rebuid series
                    Rewrite();
                }
            }
        }

        private void MainChart_MouseUp(object sender, MouseEventArgs e)
        {
            //Release captured dot
            captured = 0;
        }

        /// <summary>
        /// Rewrite series
        /// </summary>
        private void Rewrite ()
        {
            //Rewrite line of Lagrange polinom
            LineCollector(Solver.Solver.Lagrange, SourseDots, FIRST_DOT_X - 1, 0.001, FIRST_DOT_X+NUMBBER_OF_SOURSE_DOTS);

            //Rewrite line of Minimal Squear Method 3't power polinom
            LineCollector(Solver.Solver.MinSquearApproximation, SourseDots, FIRST_DOT_X - 1, 0.001, FIRST_DOT_X + NUMBBER_OF_SOURSE_DOTS);

            //Rewrite start dots;
            Filler(SourseDots, SOURSE_DOTS_SERIES_NAME, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Define axes features

            //Define axis X features
            MainChart.ChartAreas[0].AxisX.Maximum = FIRST_DOT_X - 1;
            MainChart.ChartAreas[0].AxisX.Maximum = FIRST_DOT_X + NUMBBER_OF_SOURSE_DOTS;
            MainChart.ChartAreas[0].AxisX.Interval = 1;

            //Define axis Y feature
            MainChart.ChartAreas[0].AxisY.Minimum = MIN_Y;
            MainChart.ChartAreas[0].AxisY.Maximum = MAX_Y;

            //Define start dot's positions on the start of the programm
            for (int i = 1; i <= NUMBBER_OF_SOURSE_DOTS; i++)
                SourseDots.Add(i, 1);
            Rewrite();
        }

        /// <summary>
        /// Collect and define data for serial
        /// </summary>
        /// <param name="Function">
        /// Function we use to get data:
        /// / SortedDictionary - sors data for summoration
        /// // Key = x
        /// // Value = y
        /// / double - start of range
        /// / double - step of range
        /// / double - end of range
        /// / KeyValuePair - result of function
        /// // Lines - Function we used
        /// // SortedDictionary - result range
        /// /// Key = x
        /// /// Value = y
        /// </param>
        /// <param name="StartDots">Sours for function</param>
        /// <param name="start">Start of range</param>
        /// <param name="step">Step of range</param>
        /// <param name="end">End of range</param>
        private void LineCollector(Func<SortedDictionary<double, double>, double, double, double, KeyValuePair<Lines, SortedDictionary<double, double>>> Function, SortedDictionary<double, double> StartDots, double start, double step, double end)
        {
            //Get rande using function
            KeyValuePair<Lines, SortedDictionary<double, double>> FunctionResult = Function(StartDots, start, step, end);

            //Create Series
            Filler(FunctionResult.Value, FunctionResult.Key.ToString(), System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line);
        }

        /// <summary>
        /// Creator of serie from data set and name
        /// </summary>
        /// <param name="Sours">Sours data for serie</param>
        /// <param name="Name">Name of serie</param>
        /// <param name="Type">Type of serie</param>
        private void Filler(SortedDictionary<double, double> Sours, String Name, System.Windows.Forms.DataVisualization.Charting.SeriesChartType Type)
        {
            //If we don't have such serie now
            if (MainChart.Series.IsUniqueName(Name))
            {
                //Create serie
                MainChart.Series.Add(Name);

                //Define type of serie
                MainChart.Series[Name].ChartType = Type;

                //Identify legend for this series
                MainChart.Legends.Add(Name);
                MainChart.Legends[Name].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
                MainChart.Series[Name].Legend = Name;
            }
            else
                //Clera serie
                MainChart.Series[Name].Points.Clear();

            //Define serie's data
            foreach (KeyValuePair<double, double> p in Sours)
                MainChart.Series[Name].Points.AddXY(p.Key, p.Value);
        }
    }
}
