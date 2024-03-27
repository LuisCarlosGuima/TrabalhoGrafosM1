using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoGrafosM1;

public class BFSRepository
{
    public static void BFS(int[,] matrizAdj, char[] vertices, int inicio)
    {
        Queue<int> fila = new Queue<int>();
        bool[] visitado = new bool[vertices.Length];

        fila.Enqueue(inicio);
        visitado[inicio] = true;

        while (fila.Count > 0)
        {
            int v = fila.Dequeue();
            Console.Write(vertices[v] + " ");

            for (int i = 0; i < vertices.Length; i++)
            {
                if (matrizAdj[v, i] == 1 && !visitado[i])
                {
                    fila.Enqueue(i);
                    visitado[i] = true;
                }
            }
        }
    }

    public static void BFSCompleto(int[,] matrizAdj, char[] vertices)
    {
        bool[] visitado = new bool[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            if (!visitado[i])
                BFS(matrizAdj, vertices, i);
        }
    }
}
