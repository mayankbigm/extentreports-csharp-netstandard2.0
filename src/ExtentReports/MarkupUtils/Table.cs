using System.Text;

namespace AventStack.ExtentReports.MarkupUtils
{
    internal class Table : IMarkup
    {
        public string[][] ArrayData { get; set; }
        public string[,] Data { get; set; }

        public string GetMarkup()
        {
            var sb = new StringBuilder();
            sb.Append("<table class='runtime-table table-striped table'>");
            if (ArrayData != null)
            {
                foreach (var row in ArrayData)
                {
                    sb.Append("<tr>");
                    foreach (var column in row)
                    {
                        sb.Append("<td>" + column + "</td>");
                    }
                    sb.Append("</tr>");
                }
            }
            if (Data != null)
            {
                for (int row = 0; row < Data.GetLength(0); row++)
                {
                    sb.Append("<tr>");
                    for (int col = 0; col < Data.GetLength(1); col++)
                    {
                        sb.Append("<td>" + Data[row,col] + "</td>");
                    }
                    sb.Append("</tr>");
                }
            }
            sb.Append("</table>");
            return sb.ToString();
        }
    }
}
