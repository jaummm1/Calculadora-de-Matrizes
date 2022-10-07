using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_Matrizes
{
    /// <summary>
    /// Classe criada para armazenar um array de strings contendo as explicações para a calculadora
    /// </summary>
    class explicacao
    {
        /// <summary>
        /// Array contendo todas as explicações
        /// </summary>
        public static string[] explicacoes = new string[] 
        { 
        "Matrizes são tabelas em que se dispõe um conjunto numérico. Cada um destes números é denominado elemento da matriz. Elas possuem, por convenção, nomes em letras maiúsculas e seus elementos a respectiva minúscula. Funcionam como mecanismo de resolução de sistemas lineares.",
        "A adição de matrizes é uma operação que só pode ser feita por matrizes do mesmo tipo com o mesmo número de linhas e colunas, sendo que nessa operação nós simplesmente somamos os elementos correspondentes de A e B",
        "Em matemática, determinante é uma função matricial que associa a cada matriz quadrada um escalar; ela transforma essa matriz em um número real. Esta função permite saber se a matriz tem ou não inversa, pois as que não têm são precisamente aquelas cujo determinante é igual a 0.",
        "Matriz do tipo 1xn,ou seja, com uma única linha",
        "Matriz do tipo mx1,ou seja, com uma única coluna",
        "Tem a mesma quantidade de linhas e colunas",
        "Todos os elementos são iguais a zero",
        "Matriz quadrada com os elementos da diagonal principal iguais a 1 e os demais nulos",
        "A obtida trocando os sinais de todos os elementos de A",
        "Matriz A^t obtida a partir de A,trocando , ordenamente, as linhas por colunas,ou as colunas por linhas",
        "Cada componente de uma linha/coluna é um elemento da matriz;  i = representa a linha / j = representa a coluna",
        "Tem a mesma quantidade de linhas e colunas",
        "Para subtrair duas ou mais matrizes,subtraímos os termos da mesma posição em cada matriz,gerando uma nova (resultante)",
        "Ao multiplicarmos um número real por uma matriz,multiplicamos cada termo desta matriz pelo número, gerando uma nova matriz (resultante)",
        "A multiplicação de matrizes é feita através da soma dos produtos dos elementos de uma linha pelos de uma coluna",
        "A inversa de uma matriz (A^-1) é aquele que,ao ser multiplicada por sua original resulta numa matriz identidade",
        "Matriz quadrada onde A = A^t",
        "A inversa de uma matriz (A^-1) é aquele que,ao ser multiplicada por sua original resulta numa matriz identidade"

        };
    }
}
