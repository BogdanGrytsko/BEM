using System;
using System.Collections.Generic;
using System.Text;
using BEM.Common.Points;

namespace BEM.Common
{
    public class Vector
    {
        private readonly List<double> vector;
        private int n;

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            Vector vectorResult = new Vector(vector1.n);
            for (int i = 0; i < vector2.n; i++)
            {
                vectorResult[i] = vector1[i] + vector2[i];
            }
            return vectorResult;
        }

        public static Vector operator -(Vector vector1, Vector vector2)
        {
            Vector vectorResult = new Vector(vector1.n);
            for (int i = 0; i < vector2.n; i++)
            {
                vectorResult[i] = vector1[i] - vector2[i];
            }
            return vectorResult;
        }

        public static Vector operator *(double v1, Vector v2)
        {
            Vector vectorResult = new Vector();
            for (int i = 0; i < v2.n; i++)
            {
                vectorResult[i] = v1*v2[i];
            }
            return vectorResult;
        }

        public static double ScalarMultiply(Vector v1, Vector v2)
        {
            double vResult = 0;
            for (int i = 0; i < v2.n; i++)
            {
                vResult += v1[i] * v2[i];
            }
            return vResult;
        }

        public double ScalarMultiply(Vector v2)
        {
            return ScalarMultiply(this, v2);
        }

        public double Norma()
        {
            return Math.Sqrt(ScalarMultiply(this, this));
        }

        public Vector()
        {
            vector = new List<double>();
        }

        public Vector(int n)
        {
            this.n = n;
            vector = new List<double>(n);
            for (int i = 0; i < n; i++)
            {
                vector.Add(0);
            }
        }

        public void CopyVector(Vector other)
        {
            if (other.Count != Count)
            {
                throw new Exception("Wrong vector size");
            }
            for (int i = 0; i < n; i++)
            {
                this[i] = other[i];
            }
        }

        public double this[int i]
        {
            get { return vector[i]; }
            set { vector[i] = value; }
        }

        public int Count
        {
            get { return n; }
        }

        public void Add(double value)
        {
            vector.Add(value);
            n++;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                var formatted = string.Format("{0,6:0.0000} ", vector[i]);
                builder.AppendLine(formatted);
            }
            return builder.ToString();
        }
    }
}