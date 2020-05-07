using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace WEB_API_TEST
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _output;
        
        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }
        
        [Fact]
        public void Test1()
        {
            _output.WriteLine("Test1");
            
            Aspose.Html.Converters.Converter.ConvertMHTML(Path.Join(".", "Resources", "ch007-kindle_split_006.mhtml"), 
                new Aspose.Html.Saving.PdfSaveOptions(), "output.pdf");

        }
    }
}