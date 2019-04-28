using System.Threading;

namespace AventStack.ExtentReports.MarkupUtils
{
    internal class CodeBlock : IMarkup
    {
        public string Code { get; set; }
        public CodeLanguage CodeLang { get; set; }

        private int Id { get; } = Interlocked.Increment(ref _counter);
        private static int _counter;

        public string GetMarkup()
        {
            if (CodeLang == CodeLanguage.Json)
            {
                string function = $"<div class='json-tree' id='code-block-json-{Id}'></div>" +
                                  "<script>" +
                                  $"function jsonTreeCreate{Id}() " +
                                      "{" +
                                          $" document.getElementById('code-block-json-{Id}')" +
                                          $".innerHTML = JSONTree.create({Code});" +
                                      " }" +
                                      $"jsonTreeCreate{Id}();" +
                                  "</script>";
                return function;
            }

            const string lhs = "<textarea disabled class='code-block'>";
            const string rhs = "</textarea>";
            return lhs + Code + rhs;
        }
    }
}
