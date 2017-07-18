using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringToDataTable
{
    class Program
    {
        static void Main(string[] args)
        {
            // #1 Texto com delimitadores e quebra de linhas | Text with delimiters em line breaks
            const string text = "col1;col2;col3\n" +
                                "1;data1;data1\n" +
                                "2;data2;data2\n" +
                                "3;data3;data3\n" +
                                "4;data4;data4\n" +
                                "5;data5;data5\n" +
                                "6;data6;data6\n" +
                                "7;data7;data7\n" +
                                "8;data8;data8\n" +
                                "9;data9;data9\n" +
                                "10;data10;data10\n" +
                                "11;data11;data11\n" +
                                "12;data12;data12\n" +
                                "13;data13;data13\n" +
                                "14;data14;data14\n" +
                                "15;data15;data15\n" +
                                "16;data16;data16\n" +
                                "17;data17;data17\n" +
                                "18;data18;data18\n";

            // #2 Convertendo o tecto para DataTable | Converting text to DataTable
            var table = ConvertToDataTable(text, ';');

            // #18 Exibindo as colunas do DataTable | Displaying the DataTable columns
            foreach (var column in table.Columns)
            {
                Console.Write(column + ";");
            }

            // #18 Exibindo os dados do DataTable | Displaying the DataTable data
            foreach (DataRow row in table.Rows)
            {
                var col1 = row[0].ToString(); // #19 Exemplo de chamada pelo índice da coluna | Example called by column index
                var col2 = row["col2"].ToString(); // #20 Exemplo de chamada pelo nome da coluna | Example called by column name
                var col3 = row["col3"].ToString();

                Console.Write(col1 + ";");
                Console.Write(col2 + ";");
                Console.Write(col3 + ";");
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        public static DataTable ConvertToDataTable(string data, char delimiter)
        {
            var dataTable = new DataTable(); // #3 Instância da DataTable | DataTable instance
            var endOfFirstLine = data.IndexOf("\n", StringComparison.Ordinal); // #5 Final da primeira linha | End of first row
            var dataHeader = data.Substring(0, endOfFirstLine).Split(delimiter); // #6 Conteúdo da primeira linha (colunas) | Conten of the first row (Columns)
            var startContent = ++endOfFirstLine; // #7 Início do conteúdo do texto | Beginning of the contents of the text
            var endContent = data.Length - startContent; // #8 Comprimento do conteúdo em caracteres | Length of content in characters
            var dataContent = data.Substring(startContent, endContent); // #9 Conteúdo | Content

            // #10 Adicionando as colunas do DataTable | Adding the DataTable columns
            foreach (var header in dataHeader)
            {
                dataTable.Columns.Add(header);
            }

            // #10 Declarando uma instância do StreamReader | Declaring an instance of StreamReader
            var sr = new StringReader(dataContent);

            // #11 Laço para ler o conteúdo | Loop to read the contents
            while (sr.Peek() > -1)
            {
                // #12 Retornando a linha atual | Returning the current row
                var line = sr.ReadLine();
                if (line == null) continue;

                // # 13 Criando um array de string usando delimitador | Creating a string array using delimiter
                var lineData = line.Split(delimiter);

                // #14 Declarando uma instância de linha do Datatable | Declarin a row instance of the DataTable
                var dataRow = dataTable.NewRow();

                // #15 Adicionando os dados pelo índice da coluna | Adding data by column index
                for (var i = 0; i < dataHeader.Length; i++)
                {
                    dataRow[i] = lineData[i];
                }

                // #15 Adicionando a linha na DataTable | Adding the row in the DataTable
                dataTable.Rows.Add(dataRow);
            }

            // #16 Fechando o StreamReader | Closing the streamReader
            sr.Close();

            // #17 Retornnando o Datatable | Returning the DataTable
            return dataTable;
        }

    }
}
