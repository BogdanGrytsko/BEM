using System;
using System.Text;

namespace BEM.Common
{
    public class Matrix
    {
        private double[,] matrix;

        private int n, k;

        private Matrix tMatrix, aOnStep;
        private Vector bOnStep;

        public Matrix()
            :this(0)
        {
        }

        public Matrix(int n)
        {
            this.n = n;
            matrix = new double[n, n];
        }

        private void CopyMatrix(Matrix other, int l)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = l; j < n; j++)
                {
                    matrix[i, j] = other.matrix[i, j];
                }
            }
        }

        public int Count
        {
            get
            {
                return n;
            }
        }

        public Vector Solve(Vector b)
        {
            if (n != b.Count)
            {
                throw new Exception("Matrix to b vector mismatch");
            }
            if (tMatrix == null)
            {
                tMatrix = new Matrix(n);
                aOnStep = new Matrix(n);
                bOnStep = new Vector(n);
                SolveFirstTime(b);
            }
            else
            {
                SolveAgain(b);
            }
            return b;
        }

        private void SolveFirstTime(Vector b)
        {
            for (int l  = 0; l < n; l ++)
            {
                CreateTMatrix(l);
                CreateNextAMatrix(l);
                CreateNewBVector(b, l);
                CopyMatrix(aOnStep, l);
                b.CopyVector(bOnStep);
            }
        }

        private void SolveAgain(Vector b)
        {
            var bnew = new Vector(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    bnew[j] = tMatrix.matrix[j, i] * b[i];
                    if (i != j)
                    {
                        bnew[j] += b[j];
                    }
                }
                b.CopyVector(bnew);
            }
        }

        private void CreateNewBVector(Vector b, int l)
        {
            for (int i = 0; i < b.Count; i++)
            {
                double value = tMatrix.matrix[i, l] * b[l];
                if (i != l)
                {
                    value += b[i];
                }
                bOnStep[i] = value;
            }
        }

        private void CreateNextAMatrix(int l)
        {
            for (int i = 0; i < n; i++)
            {
                var tmVal = tMatrix.matrix[i, l];
                for (int j = l; j < n; j++)
                {
                    double value = tmVal * matrix[l, j];
                    if (i != l)
                    {
                        value += matrix[i, j];
                    }
                    aOnStep.matrix[i, j] = value;
                }
            }
        }

        private void CreateTMatrix(int l)
        {
            var mid = matrix[l, l];
            for (int i = 0; i < n; i++)
            {
                tMatrix.matrix[i, l] = -matrix[i, l] / mid;
            }
            tMatrix.matrix[l, l] = 1 / mid;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    builder.AppendFormat("{0,6:0.000} ", matrix[i, j]);
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }

        public void Add(Vector vector)
        {
            if (n == 0)
            {
                n = vector.Count;
                matrix = new double[n, n];
            }
            for (int i = 0; i < vector.Count; i++)
            {
                matrix[k, i] = vector[i];
            }
            k++;
        }
    }

    public class CopyOfMatrix
    {
        private double[,] matrix;

        private int n, k;

        private CopyOfMatrix tMatrix, aOnStep;
        private Vector bOnStep;

        public CopyOfMatrix()
            : this(0)
        {
        }

        public CopyOfMatrix(int n)
        {
            this.n = n;
            matrix = new double[n, n];
        }

        private void CopyMatrix(CopyOfMatrix other, int l)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = l; j < n; j++)
                {
                    matrix[i, j] = other.matrix[i, j];
                }
            }
        }

        public int Count
        {
            get
            {
                return n;
            }
        }

        public Vector Solve(Vector b)
        {
            if (n != b.Count)
            {
                throw new Exception("CopyOfMatrix to b vector mismatch");
            }
            if (tMatrix == null)
            {
                tMatrix = new CopyOfMatrix(n);
                aOnStep = new CopyOfMatrix(n);
                bOnStep = new Vector(n);
                SolveFirstTime(b);
            }
            else
            {
                SolveAgain(b);
            }
            return b;
        }

        private void SolveFirstTime(Vector b)
        {
            for (int l = 0; l < n; l++)
            {
                CreateTMatrix(l);
                CreateNextAMatrix(l);
                CreateNewBVector(b, l);
                CopyMatrix(aOnStep, l);
                b.CopyVector(bOnStep);
            }
        }

        private void SolveAgain(Vector b)
        {
            var bnew = new Vector(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    bnew[j] = tMatrix.matrix[j, i] * b[i];
                    if (i != j)
                    {
                        bnew[j] += b[j];
                    }
                }
                b.CopyVector(bnew);
            }
        }

        private void CreateNewBVector(Vector b, int l)
        {
            for (int i = 0; i < b.Count; i++)
            {
                double value = tMatrix.matrix[i, l] * b[l];
                if (i != l)
                {
                    value += b[i];
                }
                bOnStep[i] = value;
            }
        }

        private void CreateNextAMatrix(int l)
        {
            for (int i = 0; i < n; i++)
            {
                var tmVal = tMatrix.matrix[i, l];
                for (int j = l; j < n; j++)
                {
                    double value = tmVal * matrix[l, j];
                    if (i != l)
                    {
                        value += matrix[i, j];
                    }
                    aOnStep.matrix[i, j] = value;
                }
            }
        }

        private void CreateTMatrix(int l)
        {
            var mid = matrix[l, l];
            for (int i = 0; i < n; i++)
            {
                tMatrix.matrix[i, l] = -matrix[i, l] / mid;
            }
            tMatrix.matrix[l, l] = 1 / mid;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    builder.AppendFormat("{0,6:0.000} ", matrix[i, j]);
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }

        public void Add(Vector vector)
        {
            if (n == 0)
            {
                n = vector.Count;
                matrix = new double[n, n];
            }
            for (int i = 0; i < vector.Count; i++)
            {
                matrix[k, i] = vector[i];
            }
            k++;
        }
    }
}