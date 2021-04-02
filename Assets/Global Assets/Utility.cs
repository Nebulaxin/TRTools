using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEngine;


public class Utility
{
	public static string Compress(string uncompressedString)
	{
		byte[] compressedBytes;

		using (var uncompressedStream = new MemoryStream(Encoding.UTF8.GetBytes(uncompressedString)))
		{
			using (var compressedStream = new MemoryStream())
			{
				using (var compressorStream = new DeflateStream(compressedStream, System.IO.Compression.CompressionLevel.Fastest, true))
				{
					uncompressedStream.CopyTo(compressorStream);
				}
				compressedBytes = compressedStream.ToArray();
			}
		}
		return Convert.ToBase64String(compressedBytes);
	}

	public static string Decompress(string compressedString)
	{
		byte[] decompressedBytes;

		var compressedStream = new MemoryStream(Convert.FromBase64String(compressedString));

		using (var decompressorStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
		{
			using (var decompressedStream = new MemoryStream())
			{
				decompressorStream.CopyTo(decompressedStream);

				decompressedBytes = decompressedStream.ToArray();
			}
		}

		return Encoding.UTF8.GetString(decompressedBytes);
	}


	public static string ColorToHex(Color col, bool isAlpha = false)
	{
		if (col.a == 1 | !isAlpha) return ColorUtility.ToHtmlStringRGB(col);
		else return ColorUtility.ToHtmlStringRGBA(col);
	}
	public static Color HexToColor(string s)
	{
		ColorUtility.TryParseHtmlString("#" + s, out Color c);
		return c;
	}


	public static List<string> ToListExcludeSemicolon(string s)
	{
		string ss = "";
		List<string> l = new List<string>();
		foreach (char c in s)
		{
			if (c != ';') ss += c;//делим строку по ; на ячейки в массиве
			else
			{
				l.Add(ss);//добавляем в список переменную
				ss = "";
			}
		}
		return l;
	}
	public static List<string> ArraryToList(string[] s)
	{
		List<string> l = new List<string>();
		foreach (string a in s) l.Add(a);
		return l;
	}
	public static string ToStringIncludeSemicolon(List<string> l)
	{
		string s = "";
		foreach (string st in l)
		{
			s += st + ';';//складываем все ячейки в массиве и ставим между ними ;
		}
		return s;
	}
}