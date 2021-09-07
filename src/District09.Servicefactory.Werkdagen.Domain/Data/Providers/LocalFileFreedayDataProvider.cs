using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using District09.Servicefactory.Werkdagen.Domain.Configuration;
using District09.Servicefactory.Werkdagen.Domain.Contracts;
using District09.Servicefactory.Werkdagen.Domain.Models;
using ExcelDataReader;
using Microsoft.Extensions.Options;

namespace District09.Servicefactory.Werkdagen.Domain.Data.Providers
{
    public class LocalFileFreedayDataProvider : IFreedayDataProvider
    {
        private readonly ExcellConfigOptions _excellConfigOptions;

        public LocalFileFreedayDataProvider(IOptions<ExcellConfigOptions> configOptions)
        {
            _excellConfigOptions = configOptions.Value;
        }

        public WerkDagData ProvideData()
        {
            var path = Path.GetDirectoryName(Assembly.GetAssembly(GetType())?.Location) ?? "";
            var filePath = Path.Combine(path, _excellConfigOptions.ExcellFilePath);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            var werkdagData = new WerkDagData();
            do
            {
                while (reader.Read())
                {
                    if (reader.Depth == 0) continue;
                    var datestr = reader.GetString(_excellConfigOptions.DateInColumn);
                    var date = DateTime.Parse(datestr);
                    werkdagData.Werkdagen.Add(new WerkDag { DateTime = date, IsWerkDag = false });
                }
            } while (reader.NextResult());

            return werkdagData;
        }
    }
}