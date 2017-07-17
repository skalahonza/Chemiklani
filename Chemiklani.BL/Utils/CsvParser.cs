using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace Chemiklani.BL.Utils
{
    public class CsvParser
    {
        /// <summary>
        /// Parse collection fo dtos from given csv
        /// </summary>
        /// <typeparam name="TDTO">DTO that is meant to be parsed</typeparam>
        /// <param name="stream">CSV stream</param>
        /// <param name="lineParserAction">Function that can parse DTO from one csv line</param>
        /// <param name="output">List of parsed dtos</param>
        public void ParseDtos<TDTO>(Stream stream, Func<string[], TDTO> lineParserAction, out List<TDTO> output)
        {
            output = new List<TDTO>();

            using (var parser = new TextFieldParser(stream))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",", ";", "\t");
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] row = parser.ReadFields();
                    output.Add(lineParserAction(row));
                }
            }
        }
    }
}