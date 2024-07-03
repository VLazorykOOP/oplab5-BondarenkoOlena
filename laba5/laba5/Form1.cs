using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace laba5
{
    public partial class Form1 : Form
    {
        private Point P1;
        private Point P2;
        private Vector V1;
        private Vector V2;

        private PointF P3;
        private PointF P4;
        private int depth = 5; 
        private double initialLength = 80; 
        private double angle = -Math.PI / 2;

        private PointF center;
        public Form1()
        {
            InitializeComponent();
        }
        private void DrawHermiteCurve(Graphics g, Pen pen)
        {
            float t;
            PointF[] curvePoints = new PointF[100];
            for (int i = 0; i < 100; i++)
            {
                t = (float)i / 100;
                float h1 = 2 * t * t * t - 3 * t * t + 1;
                float h2 = -2 * t * t * t + 3 * t * t;
                float h3 = t * t * t - 2 * t * t + t;
                float h4 = t * t * t - t * t;

                float x = h1 * P1.X + h2 * P2.X + h3 * V1.X + h4 * V2.X;
                float y = h1 * P1.Y + h2 * P2.Y + h3 * V1.Y + h4 * V2.Y;

                curvePoints[i] = new PointF(x, y);
            }
            g.DrawLines(pen, curvePoints);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            if (int.TryParse(textBox1.Text, out int p1x) &&
                int.TryParse(textBox2.Text, out int p1y) &&
                int.TryParse(textBox3.Text, out int p2x) &&
                int.TryParse(textBox4.Text, out int p2y) &&
                int.TryParse(textBox5.Text, out int v1x) &&
                int.TryParse(textBox6.Text, out int v1y) &&
                int.TryParse(textBox7.Text, out int v2x) &&
                int.TryParse(textBox8.Text, out int v2y))
            {
                P1 = new Point(p1x, p1y);
                P2 = new Point(p2x, p2y);
                V1 = new Vector(v1x, v1y);
                V2 = new Vector(v2x, v2y);

                Pen curvePen = new Pen(Color.Blue);
                DrawHermiteCurve(g, curvePen);
                curvePen.Dispose();
            }
            else
            {
                MessageBox.Show("Please enter valid integer values for coordinates and vectors.");
            }
        }
        private void DrawDandelion(Graphics g, int order, PointF start, double length, double angle)
        {
            if (order == 0) return;

            Pen pen = new Pen(Color.Blue);

            double x2 = start.X + length * Math.Cos(angle);
            double y2 = start.Y + length * Math.Sin(angle);
            PointF end = new PointF((float)x2, (float)y2);

            g.DrawLine(pen, start, end);

            DrawDandelion(g, order - 1, end, length * 0.7, angle + Math.PI / 4);
            DrawDandelion(g, order - 1, end, length * 0.7, angle - Math.PI / 4);

            pen.Dispose();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int p1x, p1y, p2x, p2y;

            if (int.TryParse(textBox1.Text, out p1x) &&
                int.TryParse(textBox2.Text, out p1y) &&
                int.TryParse(textBox3.Text, out p2x) &&
                int.TryParse(textBox4.Text, out p2y))
            {
                P3 = new PointF(p1x, p1y);
                P4 = new PointF(p2x, p2y);

                Graphics g = pictureBox1.CreateGraphics();
                g.Clear(Color.White);

                PointF center = new PointF((P3.X + P4.X) / 2, (P3.Y + P4.Y) / 2);
                double length = Math.Sqrt(Math.Pow(P4.X - P3.X, 2) + Math.Pow(P4.Y - P3.Y, 2));
                double angle = Math.Atan2(P4.Y - P3.Y, P4.X - P3.X);

                DrawDandelion(g, depth, center, initialLength, angle);
            }
            else
            {
                MessageBox.Show("Please enter valid integer values for coordinates.");
            }
        }
    }

    public struct Vector
    {
        public int X;
        public int Y;

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

