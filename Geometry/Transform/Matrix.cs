namespace Geometry
{
    public class Matrix
    {
        private readonly uint rows;
        private readonly uint cols;
        private readonly float[,] matrix;

        public uint Rows { get { return rows; } }
        public uint Cols { get { return cols; } }

        public Matrix(uint size = 4)
        {
            this.rows = this.cols = size;
            matrix = new float[this.rows, this.cols];
        }

        public Matrix(uint rows, uint cols)
        {
            this.rows = rows;
            this.cols = cols;
            matrix = new float[this.rows, this.cols];
        }

        public Matrix(float[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            this.rows = (uint)matrix.GetLength(0);
            this.cols = (uint)matrix.GetLength(1);

            this.matrix = new float[this.rows, this.cols];

            for(uint i = 0; i < this.rows; i++)
                for (uint j = 0; j < this.cols; j++)
                    this.matrix[i, j] = matrix[i, j];
        }

        public float this[uint row, uint col] 
        {
            get => matrix[row, col];
            set => matrix[row, col] = value;
        }


        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.Cols != b.Rows)
                throw new ArgumentException("Матрицы несовместны! Количество столбцов первой должно равняться количеству строк второй.");

            Matrix result = new Matrix(a.Rows, b.Cols);

            uint n = a.Cols;

            for (uint i = 0; i < a.Rows; i++)
                for (uint j = 0; j < b.Cols; j++)
                    for (uint k = 0; k < n; k++)
                        result[i, j] += a[i, k] * b[k, j];

            return result;
        }

        public static Matrix Identity(uint size = 4)
        {
            Matrix identity = new Matrix(size);

            for (uint i = 0; i < size; i++)
                identity[i, i] = 1.0f;

            return identity;
        }
    }
}
