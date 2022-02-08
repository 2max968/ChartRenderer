using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
	public class TextUnescaper
	{
		public static readonly Dictionary<string, string> GreekCharacters
			= new Dictionary<string, string>()
		{
				{"Alpha", "\u0391" },
				{"alpha", "\u03B1" },
				{"Beta", "\u0392" },
				{"beta", "\u03B2" },
				{"Gamma", "\u0393" },
				{"gamma", "\u03B3" },
				{"Delta", "\u0394" },
				{"delta", "\u03B4" },
				{"Epsilon", "\u0395" },
				{"epsilon", "\u03B5" },
				{"Zeta", "\u0396" },
				{"zeta", "\u03B6" },
				{"Eta", "\u0397" },
				{"eta", "\u03B7" },
				{"Theta", "\u0398" },
				{"theta", "\u03B8" },
				{"Iota", "\u0399" },
				{"iota", "\u03B9" },
				{"Kappa", "\u039A" },
				{"kappa", "\u03BA" },
				{"Lambda", "\u039B" },
				{"lambda", "\u03BB" },
				{"Mu", "\u039C" },
				{"mu", "\u03BC" },
				{"Nu", "\u039D" },
				{"nu", "\u03BD" },
				{"Xi", "\u039E" },
				{"xi", "\u03BE" },
				{"Omicron", "\u039F" },
				{"omicron", "\u03BF" },
				{"Pi", "\u03A0" },
				{"pi", "\x03C0" },
				{"Rho", "\u03A1" },
				{"rho", "\u03C1" },
				{"Sigma", "\u03A3" },
				{"sigma", "\u03C3" },
				{"sigma2", "\u03C2" },
				{"Tau", "\u03A4" },
				{"tau", "\u03C4" },
				{"Upsilon", "\u03A5" },
				{"upsilon", "\u03C5" },
				{"Phi", "\u03A6" },
				{"phi", "\u03C6" },
				{"Chi", "\u03A7" },
				{"chi", "\u03C7" },
				{"Psi", "\u03A8" },
				{"psi", "\u03C8" },
				{"Omega", "\u03A9" },
				{"omega", "\u03C9" }
			};

		public static readonly Dictionary<string, string> MathSymbols
			= new Dictionary<string, string>()
			{
				{"cdot", "\u2219" },
				{"+-", "\u00B1" },
				{"-+", "\u2213" },
				{"div", "\u00F7" },
				{"inf", "\u221E" },
				{"neq", "\u2260" },
				{"sqrt", "\u221A" },
				{">=", "\u2265" },
				{"<=", "\u2264" }
			};

		public static string GetCharacter(string name)
		{
			if (GreekCharacters.ContainsKey(name))
				return GreekCharacters[name];
			if(MathSymbols.ContainsKey(name))
				return MathSymbols[name];
			if ((name.StartsWith("u") || name.StartsWith("x")) && int.TryParse(name.Substring(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int character))
				return "" + (char)character;
			return "\x25AF";
		}

		public static string Unescape(string input)
		{
			string output = "";
			int index = 0;
			int lastIndex = 0;
			while (true)
			{
				index = input.IndexOf('\\', index);
				if (index < 0)
				{
					output += input.Substring(lastIndex);
					break;
				}
				output += input.Substring(lastIndex, index - lastIndex);
				if (input.Length > index + 1 && input[index + 1] == '[')
				{
					int close = input.IndexOf(']', index);
					int nextOpen = input.IndexOf('[', index + 2);
					if (close < 0 || (nextOpen < close && nextOpen >= 0))
					{
						output += "\\";
						index++;
						lastIndex = index;
						continue;
					}
					string name = input.Substring(index + 2, close - index - 2);
					Console.WriteLine("Found word: " + name);
					output += GetCharacter(name);
					index = close;
				}
				else
				{
					output += "\\";
				}
				index++;
				lastIndex = index;
			}
			return output;
		}
	}
}
