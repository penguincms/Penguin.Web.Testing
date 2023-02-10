using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Penguin.Web.Testing
{
    public class MockView : IView
    {
        #region Properties

        public string Path { get; set; }

        #endregion Properties

        #region Constructors

        public MockView(string path)
        {
            Path = path;
        }

        #endregion Constructors

        #region Methods

        public async Task RenderAsync(ViewContext context)
        {
            if (context is null)
            {
                throw new System.ArgumentNullException(nameof(context));
            }

            List<string> fileLines = System.IO.File.ReadAllLines(context.View.Path).ToList();
            List<string> File = new();

            foreach (string Line in fileLines)
            {
                //strip out the header info so we know if we need to serialize the model
                if (!Regex.IsMatch(Line, @"\@\*.*\*\@") && !Line.Contains("@model "))
                {
                    File.Add(Line);
                }
            }

            string Model = string.Empty;

            //not a perfect check but should drastically reduce the amount of data being stored since most email templates should be empty
            if (File.Any(s => s.Contains('@')))
            {
                Model = JsonConvert.SerializeObject(context.ViewData.Model, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
            }

            string viewBody = Model + System.Environment.NewLine + string.Join(System.Environment.NewLine, File);

            _ = context.Writer.WriteAsync(viewBody);
        }

        #endregion Methods
    }
}