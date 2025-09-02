using EFCore_CodeFirst.Db.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Reflection.Metadata;
using System.Text;

namespace EFCore_CodeFirst.Formatter
{
    public sealed class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add("text/csv");
            SupportedMediaTypes.Add("application/xml");
            SupportedMediaTypes.Add("text/xml");
        }

        protected override bool CanWriteType(Type? type)
        => typeof(IEnumerable<object>).IsAssignableFrom(type) || type.IsClass;

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext ctx, Encoding enc)
        {
            var response = ctx.HttpContext.Response;
            using var writer = new StreamWriter(response.Body, enc);

            var buffer = new StringBuilder();

            if (ctx.Object is IEnumerable<Player> players)
            {
                // header
                buffer.AppendLine("Id,NickName,JoinedDate");

                foreach (var player in players)
                {
                    FormatCsv(buffer, player);
                }
            }
            else if (ctx.Object is Player singlePlayer)
            {
                buffer.AppendLine("Id,NickName,JoinedDate");
                FormatCsv(buffer, singlePlayer);
            }

            await writer.WriteAsync(buffer.ToString());
        }
        private static void FormatCsv(StringBuilder buffer, Player player)
        {
            // escape dấu phẩy và dấu ngoặc kép nếu có
            string Escape(string value) =>
                value.Contains(",") || value.Contains("\"")
                    ? $"\"{value.Replace("\"", "\"\"")}\""
                    : value;

            buffer.AppendLine($"{player.PlayerId},{Escape(player.NickName)},{Escape(player.JoinedDate.ToString())}");
        }
    }
}
