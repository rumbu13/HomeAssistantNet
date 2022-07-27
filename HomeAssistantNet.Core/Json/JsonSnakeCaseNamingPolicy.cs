using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeAssistantNet.Core.Json;

public class JsonSnakeCaseNamingPolicy : JsonNamingPolicy
{
       
    static void Dump(ref Span<char> buffer, ref int position, in ReadOnlySpan<char> word)
    {
        if (word.IsEmpty)
            return;

        if (position != 0)
            buffer[position++] = '_';

        Span<char> output = buffer.Slice(position);
        word.ToLowerInvariant(output);
        position += word.Length;

    }

    public override string ConvertName(string name)
    {
        if (String.IsNullOrEmpty(name))
            return name;

        int bufferLength = name.Length * 2;

        Span<char> buffer = bufferLength > 512 ? new char[bufferLength] : stackalloc char[512];

        ReadOnlySpan<char> chars = name.AsSpan();

        int pos = 0;
        int j = 0;
        int prevCateg = 0;
        

        for (int i = 0; i < chars.Length; i++)
        {
            char c = chars[i];
            var category = Char.GetUnicodeCategory(c);
            
            if (category == UnicodeCategory.SpaceSeparator 
                ||(category >= UnicodeCategory.ConnectorPunctuation
                   && category <= UnicodeCategory.OtherPunctuation))
            {
                Dump(ref buffer, ref pos, chars.Slice(j, i - j));
                prevCateg = 0;
                j = i + 1;
                continue;
            }

            if (i + 1 < chars.Length)
            {
                char next = chars[i + 1];
                var currCateg = prevCateg;
                if (category == UnicodeCategory.LowercaseLetter)
                    currCateg = 1;
                else if (category == UnicodeCategory.UppercaseLetter)
                    currCateg = 2;

                if (currCateg == 1 && Char.IsUpper(next) || next == '_')
                {
                    Dump(ref buffer, ref pos, chars.Slice(j, i - j + 1));
                    prevCateg = 0;
                    j = i + 1;
                    continue;
                }

                if (prevCateg == 2 && category == UnicodeCategory.UppercaseLetter && Char.IsLower(next))
                {
                    Dump(ref buffer, ref pos, chars.Slice(j, i - j));
                    prevCateg = 0;
                    j = i;
                    continue;
                }

                prevCateg = currCateg;
            }

        }

        Dump(ref buffer, ref pos, chars.Slice(j));

        var result = buffer.Slice(0, pos).ToString();

        return result;
    }
}
