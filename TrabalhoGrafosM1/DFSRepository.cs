using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoGrafosM1;
public class DFSRepository
{
    public static void DFS(int[,] matrizAdj, char[] vertices, int v, bool[] visitado)
    {
        Console.Write(vertices[v] + " ");
        visitado[v] = true;

        for (int i = 0; i < vertices.Length; i++)
        {
            if (matrizAdj[v, i] == 1 && !visitado[i])
                DFS(matrizAdj, vertices, i, visitado);
        }
    }

    public static void DFSCompleto(int[,] matrizAdj, char[] vertices)
    {
        bool[] visitado = new bool[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            if (!visitado[i])
                DFS(matrizAdj, vertices, i, visitado);
        }
    }
}
