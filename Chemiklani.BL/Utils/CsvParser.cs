using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Chemiklani.BL.DTO;
using Microsoft.VisualBasic.FileIO;

namespace Chemiklani.BL.Utils
{
    public class CsvParser
    {
        public string[] Delimiters { get; set; }

        public CsvParser():this(";", "\t")
        {          
        }

        public CsvParser(params string[] delimiters)
        {
            Delimiters = delimiters;
        }        

        /// <summary>
        /// Parse collection fo dtos from given csv
        /// </summary>
        /// <typeparam name="TDTO">DTO that is meant to be parsed</typeparam>
        /// <param name="csvStream">CSV csvStream</param>
        /// <param name="lineParserAction">Function that can parse DTO from one csv line</param>
        /// <param name="output">List of parsed dtos</param>
        public void ParseDtos<TDTO>(Stream csvStream, Func<string[], TDTO> lineParserAction, out List<TDTO> output)
            where TDTO:BaseDTO
        {
            output = new List<TDTO>();

            using (var parser = new TextFieldParser(csvStream))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(Delimiters);
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] row = parser.ReadFields();
                    output.Add(lineParserAction(row));
                }
            }
        }

        public string ExportDtos<TDTO>(Func<TDTO, string> csvSerializer, List<TDTO> dtos)
            where TDTO : BaseDTO
        {
            var builder = new StringBuilder();
            foreach (var t in dtos)
                builder.AppendLine(csvSerializer(t));

            return builder.ToString();
        }
    }
}