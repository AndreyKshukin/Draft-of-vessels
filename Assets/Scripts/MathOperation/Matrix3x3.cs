using UnityEngine;

/// <summary>
/// Матрица поворота
/// </summary>
public class Matrix3x3 : MonoBehaviour
{
    private float[] angle = new float[3];                               //угол повора
    private float[,] matrix = new float[3, 3];
    private float[,] inverseMatrix = new float[3, 3];
    private float[] vectorParent = new float[3] { 0, 0, 1 };              //Направление вектора родителя (y,
    private float[] vectorChild = new float[3];                         //Направление вектора объекта
    private float determinant;                                          //определитель для матрицы

    public Matrix3x3(Vector3 startPositionChild, Vector3 angle)
    {
        this.angle[1] = angle.x;
        this.angle[2] = angle.y;
        this.angle[0] = angle.z;

        vectorParent[1] = startPositionChild.x;
        vectorParent[2] = startPositionChild.y;
        vectorParent[0] = startPositionChild.z;
    }

    private float[,] MatrixX(float angleX)
    {
        float[,] matrix = new float[3, 3];

        matrix[0, 0] = 1;
        matrix[0, 1] = 0;
        matrix[0, 2] = 0;

        matrix[1, 0] = 0;
        matrix[1, 1] = Mathf.Cos(angleX * Mathf.PI / 180);
        matrix[1, 2] = -Mathf.Sin(angleX * Mathf.PI / 180);

        matrix[2, 0] = 0;
        matrix[2, 1] = Mathf.Sin(angleX * Mathf.PI / 180);
        matrix[2, 2] = Mathf.Cos(angleX * Mathf.PI / 180);

        return matrix;
    }

    private float[,] MatrixY(float angleY)
    {
        float[,] matrix = new float[3, 3];

        matrix[0, 0] = Mathf.Cos(angleY * Mathf.PI / 180);
        matrix[0, 1] = 0;
        matrix[0, 2] = Mathf.Sin(angleY * Mathf.PI / 180);

        matrix[1, 0] = 0;
        matrix[1, 1] = 1;
        matrix[1, 2] = 0;

        matrix[2, 0] = -Mathf.Sin(angleY * Mathf.PI / 180);
        matrix[2, 1] = 0;
        matrix[2, 2] = Mathf.Cos(angleY * Mathf.PI / 180);

        return matrix;
    }

    private float[,] MatrixZ(float angleZ)
    {
        float[,] matrix = new float[3, 3];

        matrix[0, 0] = Mathf.Cos(angleZ * Mathf.PI / 180);
        matrix[0, 1] = -Mathf.Sin(angleZ * Mathf.PI / 180);
        matrix[0, 2] = 0;

        matrix[1, 0] = Mathf.Sin(angleZ * Mathf.PI / 180);
        matrix[1, 1] = Mathf.Cos(angleZ * Mathf.PI / 180);
        matrix[1, 2] = 0;

        matrix[2, 0] = 0;
        matrix[2, 1] = 0;
        matrix[2, 2] = 1;

        return matrix;
    }

    private void MulMatrixOnVector(float[,] matrix, float[] vector)
    {
        float[] vec = new float[3];

        vec[0] = matrix[0, 0] * vector[0] + matrix[0, 1] * vector[1] + matrix[0, 2] * vector[2];
        vec[1] = matrix[1, 0] * vector[0] + matrix[1, 1] * vector[1] + matrix[1, 2] * vector[2];
        vec[2] = matrix[2, 0] * vector[0] + matrix[2, 1] * vector[1] + matrix[2, 2] * vector[2];

        vectorChild = vec;
    }

    private void CalculationDeterminant(float[,] matrix)
    {
        determinant = matrix[0, 0] * (matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1])
                            - matrix[0, 1] * (matrix[1, 0] * matrix[2, 2] - matrix[1, 2] * matrix[2, 0])
                            + matrix[0, 2] * (matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0]);
    }

    private void CalculationInverseMatrix(float[,] matrix)
    {
        if (determinant != 0)
        {
            inverseMatrix[0, 0] = 1 / determinant * (matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1]);
            inverseMatrix[0, 1] = -1 / determinant * (matrix[0, 1] * matrix[2, 2] - matrix[0, 2] * matrix[2, 1]);
            inverseMatrix[0, 2] = 1 / determinant * (matrix[0, 1] * matrix[1, 2] - matrix[0, 2] * matrix[1, 1]);


            inverseMatrix[1, 0] = -1 / determinant * (matrix[1, 0] * matrix[2, 2] - matrix[1, 2] * matrix[2, 0]);
            inverseMatrix[1, 1] = 1 / determinant * (matrix[0, 0] * matrix[2, 2] - matrix[0, 2] * matrix[2, 0]);
            inverseMatrix[1, 2] = -1 / determinant * (matrix[0, 0] * matrix[1, 2] - matrix[0, 2] * matrix[1, 0]);


            inverseMatrix[2, 0] = 1 / determinant * (matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0]);
            inverseMatrix[2, 1] = -1 / determinant * (matrix[0, 0] * matrix[2, 1] - matrix[0, 1] * matrix[2, 0]);
            inverseMatrix[2, 2] = 1 / determinant * (matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]);
        }
    }

    public Vector3 Vector()
    {
        Vector3 vector;

        MulMatrixOnVector(MatrixX(angle[0]), vectorParent);
        MulMatrixOnVector(MatrixY(angle[1]), vectorChild);
        MulMatrixOnVector(MatrixZ(angle[2]), vectorChild);

        vector.x = vectorChild[1];
        vector.y = vectorChild[2];
        vector.z = vectorChild[0];

        return vector;
    }

    public Vector3 Vector(bool fuck)
    {
        Vector3 vector;

        Debug.Log("Угол: x=" + angle[0] + "; y=" + angle[1] + "; z=" + angle[2]);
        Debug.Log("Начальный вектор: x=" + vectorParent[0] + "; y=" + vectorParent[1] + "; z=" + vectorParent[2]);
        MulMatrixOnVector(MatrixX(angle[0]), vectorParent);
        Debug.Log("Вектор Х: x=" + vectorChild[0] + "; y=" + vectorChild[1] + "; z=" + vectorChild[2]);
        MulMatrixOnVector(MatrixY(angle[1]), vectorChild);
        Debug.Log("Вектор Y: x=" + vectorChild[0] + "; y=" + vectorChild[1] + "; z=" + vectorChild[2]);
        MulMatrixOnVector(MatrixZ(angle[2]), vectorChild);
        Debug.Log("Вектор Z: x=" + vectorChild[0] + "; y=" + vectorChild[1] + "; z=" + vectorChild[2]);

        vector.x = vectorChild[1];
        vector.y = vectorChild[2];
        vector.z = vectorChild[0];

        return vector;
    }

    public Vector3 InverseVector()
    {
        Vector3 vector;

        float[,] matrix = MatrixX(angle[0]);
        CalculationDeterminant(matrix);
        CalculationInverseMatrix(matrix);

        //CheckMatrix(matrix);
        //Debug.Log("Определитель" + determinant);
        //CheckMatrix(inverseMatrix);

        MulMatrixOnVector(inverseMatrix, vectorParent);

        matrix = MatrixY(angle[1]);
        CalculationDeterminant(matrix);
        CalculationInverseMatrix(matrix);

        MulMatrixOnVector(inverseMatrix, vectorChild);

        matrix = MatrixZ(angle[2]);
        CalculationDeterminant(matrix);
        CalculationInverseMatrix(matrix);

        MulMatrixOnVector(inverseMatrix, vectorChild);

        //Debug.Log("x=" + vectorChild[0] + "; y=" + vectorChild[1] + "; z=" + vectorChild[2]);

        vector.x = vectorChild[1];
        vector.y = vectorChild[2];
        vector.z = vectorChild[0];

        return vector;
    }


    private void CheckMatrix(float[,] matrix)
    {
        Debug.Log("Матрица");
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(matrix[i, 0] + " " + matrix[i, 1] + " " + matrix[i, 2]);
        }
    }
}