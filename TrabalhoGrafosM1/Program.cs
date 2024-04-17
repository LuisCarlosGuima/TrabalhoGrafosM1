using System;
using System.Collections.Generic;

public class Grafo
{
    private int[,] aresta;
    private char[] vertices;
    private int[,] matrizAdj;

    private void PassaValores()
    {
        var tamanho = vertices.Length;
        aresta = new int[tamanho, tamanho];
        for (int i = 0; i < tamanho; i++)
        {
            for (int j = 0; j < tamanho; j++)
                aresta[i, j] = matrizAdj[i, j];
        }
    }

    public Grafo(char[] vertices, int[,] matrizAdj)
    {
        this.vertices = vertices;
        this.matrizAdj = matrizAdj;
        PassaValores();
    }

    public bool GrafoEhVazio()
    {
        for (int i = 1; i < vertices.Length; i++)
        {
            for (int j = 1; j < vertices.Length; j++)
            {
                if (aresta[i, j] == 1)
                    return false;
            }
        }
        return true;
    }

    public bool AdicionaVertice(char vertice1, char vertice2)
    {
        vertice1 = char.ToUpper(vertice1);
        vertice2 = char.ToUpper(vertice2);

        var index1 = Array.IndexOf(vertices, vertice1);
        var index2 = Array.IndexOf(vertices, vertice2);

        if (index1 == -1 || index2 == -1)
        {
            Console.WriteLine("Um ou ambos os vértices informados não foram encontrados.");
            Console.WriteLine($"Vertices informados: ({vertice1}, {vertice2}). Utilize a opção 3 para visualizar a matriz");
            return false;
        }
        else if (ExisteLigacao(index1, index2))
        {
            Console.WriteLine($"Já existe uma ligação entre os vértices {vertice1} e {vertice2}.");
            return false;
        }
        else if (index1 == index2)
        {
            Console.WriteLine("Não pode ligar a si mesmo!");
            return false;
        }
        else
        {
            aresta[index1, index2] = 1;
            return true;
        }
    }


    public bool RemoveVertice(char vertice1, char vertice2)
    {
        var index1 = Array.IndexOf(vertices, vertice1);
        var index2 = Array.IndexOf(vertices, vertice2);

        if (ExisteLigacao(index1, index2))
        {
            aresta[index1, index2] = 0;
            Console.WriteLine($"Arestas removidas das vértices {vertice1} e {vertice2}");
            return true;
        }
        else
        {
            Console.WriteLine($"Não foi possível deletar ({vertice1}, {vertice2}). Ligação não foi encontrada.");
            return false;
        }
    }

    public bool VerificaSeConexo()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            if (BuscaEmProfundidadeDFSconexo(i).Count < vertices.Length)
            {
                return false;
            }
        }
        return true;
    }

    public bool ExisteLigacao(int vertice1, int vertice2)
    {
        if (vertice1 >= 0 && vertice1 < vertices.Length &&
            vertice2 >= 0 && vertice2 < vertices.Length)
        {
            return aresta[vertice1, vertice2] == 1;
        }
        return false;
    }

    public void GeraGrafo()
    {
        Console.Write("  ");
        for (int i = 0; i < vertices.Length; i++)
            Console.Write(vertices[i] + " ");

        Console.WriteLine();

        for (int i = 0; i < vertices.Length; i++)
        {
            Console.Write(vertices[i] + " ");
            for (int j = 0; j < vertices.Length; j++)
                Console.Write(aresta[i, j] + " ");
            Console.WriteLine();
        }
    }

    private List<int> BuscaEmProfundidadeDFSconexo(int vertice)
    {
        var pilha = new Stack<int>();
        var pilhaAuxiliarValoresInvertido = new Stack<int>();
        var valordescoberto = new List<int>();

        pilha.Push(vertice);

        while (pilha.Count > 0 || pilhaAuxiliarValoresInvertido.Count > 0)
        {
            if (pilhaAuxiliarValoresInvertido.Count > 0)
            {
                vertice = pilhaAuxiliarValoresInvertido.Pop();
            }
            else
            {
                vertice = pilha.Pop();
            }

            if (!valordescoberto.Contains(vertice))
            {
                valordescoberto.Add(vertice);
                for (int i = 0; i < vertices.Length; i++)
                {
                    if (aresta[vertice, i] == 1)
                    {
                        pilha.Push(i);
                    }
                }
                while (pilha.Count > 0)
                {
                    pilhaAuxiliarValoresInvertido.Push(pilha.Pop());
                }
            }
        }
        return valordescoberto;
    }

    public void BuscaEmProfundidadeDFS(char vertice)
    {
        var pilha = new Stack<int>();
        var pilhaAuxiliarValoresInvertido = new Stack<int>();
        var valordescoberto = new List<char>();


        int index = Array.IndexOf(vertices, vertice);
        pilha.Push(index);

        if (index == -1)
        {
            Console.WriteLine("Vertice não encontrado/invalido!");
            return;
        }

        Console.WriteLine("Iniciando a busca em profundidade");

        while (pilha.Count > 0 || pilhaAuxiliarValoresInvertido.Count > 0)
        {
            if (pilhaAuxiliarValoresInvertido.Count > 0)
            {
                index = pilhaAuxiliarValoresInvertido.Pop();
            }
            else
            {
                index = pilha.Pop();
            }

            if (!valordescoberto.Contains(vertices[index]))
            {
                Console.WriteLine($"Aresta visitada: {vertices[index]}");
                valordescoberto.Add(vertices[index]);
                for (int i = 0; i < vertices.Length; i++)
                {
                    if (aresta[index, i] == 1)
                    {
                        pilha.Push(i);
                    }
                }
                while (pilha.Count > 0)
                {
                    pilhaAuxiliarValoresInvertido.Push(pilha.Pop());
                }
            }
        }

        Console.WriteLine("Busca finalizada!");
        Console.WriteLine();
    }

    public void BuscaEmLarguraBFS(char vertice)
    {
        var fila = new Queue<int>();
        var vetor = new List<char>();


        var index = Array.IndexOf(vertices, vertice);
        fila.Enqueue(index);
        vetor.Add(vertice);

        if (index == -1)
        {
            Console.WriteLine("Vertice não encontrado/invalido!");
            return;
        }

        Console.WriteLine("Iniciando a busca em largura");

        Console.WriteLine($"Aresta visitada: {vertice}");
        while (fila.Count > 0)
        {
            var intTemporario = fila.Dequeue();
            for (int i = 0; i < vertices.Length; i++)
            {
                if (aresta[intTemporario, i] == 1)
                {
                    if (!vetor.Contains(vertices[i]))
                    {
                        Console.WriteLine($"Aresta visitada: {vertices[i]}");
                        vetor.Add(vertices[i]);
                        fila.Enqueue(i);
                    }
                }
            }
        }

        Console.WriteLine("Busca Finalizada!");
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Digite os vértices separados por vírgula:");
        var inputVertices = Console.ReadLine().Replace(",", "").ToUpper();
        var vertices = inputVertices.Trim().ToCharArray();

        int numVertices = vertices.Length;
        int[,] matrizAdj = new int[numVertices, numVertices];

        var grafo = new Grafo(vertices, matrizAdj);

        while (true)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Menu");
            Console.WriteLine("1) Adicionar Aresta");
            Console.WriteLine("2) Deletar Vértice das arestas");
            Console.WriteLine("3) Mostrar Matriz");
            Console.WriteLine("4) Busca em profundidade");
            Console.WriteLine("5) Busca em Largura");
            Console.WriteLine("6) Verificar se conexo");
            Console.WriteLine("7) Reseta matriz");
            Console.WriteLine("0) Sair do programa");

            char escolha = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (escolha)
            {
                case '1':
                    char vertice1, vertice2;
                    ObtemVertices(out vertice1, out vertice2);
                    grafo.AdicionaVertice(vertice1, vertice2);
                    break;
                case '2':
                    ObtemVertices(out vertice1, out vertice2);
                    grafo.RemoveVertice(vertice1, vertice2);
                    break;
                case '3':
                    grafo.GeraGrafo();
                    break;
                case '4':
                    char verticeProfundidade = ObtemVerticeProfundidade();
                    grafo.BuscaEmProfundidadeDFS(verticeProfundidade);
                    break;
                case '5':
                    char verticeLargura = ObtemVerticeLargura();
                    grafo.BuscaEmLarguraBFS(verticeLargura);
                    break;
                case '6':
                    if (grafo.VerificaSeConexo())
                        Console.WriteLine("O grafo é conexo.");
                    else
                        Console.WriteLine("O grafo não é conexo.");
                    break;
                case '7':
                    numVertices = vertices.Length;
                    matrizAdj = new int[numVertices, numVertices];

                    grafo = new Grafo(vertices, matrizAdj);
                    Console.WriteLine("A matriz foi zerada!");
                    break;
                case '0':
                    return;
                default:
                    Console.WriteLine("Escolha inválida.");
                    break;
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    private static char ObtemVerticeLargura()
    {

        try
        {
            Console.WriteLine("Informe o vértice para iniciar a busca em largura: ");
            return char.Parse(Console.ReadLine()!.ToUpper());
        }
        catch (Exception e)
        {
            Console.WriteLine($"Caracter digitado inválido, digite novamente! Erro: {e?.Message}");
            Console.ReadKey();
            Console.Clear();
            return ObtemVerticeProfundidade();
        }
    }
    private static char ObtemVerticeProfundidade()
    {
        try
        {
            Console.WriteLine("Informe o vértice para iniciar a busca em profundidade: ");
            return char.Parse(Console.ReadLine()!.ToUpper());
        }
        catch (Exception e)
        {
            Console.WriteLine($"Caracter digitado inválido, digite novamente! Erro: {e?.Message}");
            Console.ReadKey();
            Console.Clear();
            return ObtemVerticeProfundidade();
        }

    }

    private static void ObtemVertices(out char vertice1, out char vertice2)
    {
        try
        {
            Console.WriteLine("Informe o vértice 1: ");
            vertice1 = char.Parse(Console.ReadLine()!.ToUpper());
            Console.WriteLine("Informe o vértice 2: ");
            vertice2 = char.Parse(Console.ReadLine()!.ToUpper());
        }
        catch (Exception e)
        {
            Console.WriteLine($"Caracter digitado inválido, digite novamente! Erro: {e?.Message}");
            Console.ReadKey();
            Console.Clear();
            ObtemVertices(out vertice1, out vertice2);
        }
    }
}
