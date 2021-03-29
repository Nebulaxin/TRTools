using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.IO.Compression;

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

	public static string ColorToHex(Color col)
	{
		int[] color = new int[4];
		color[0] = (int)(col.r * 255);
		color[1] = (int)(col.g * 255);
		color[2] = (int)(col.b * 255);
		color[3] = (int)(col.a * 255);
		string[] clr = new string[4];
		for (int i = 0; i < 4; i++)
		{
			if (color[i] < 16) clr[i] = "0" + color[i].ToString("x");
			else clr[i] = color[i].ToString("x");
			Debug.Log(clr[i]);
		}
		return clr[0] + clr[1] + clr[2] + clr[3];
	}
	public static Color HexToColor(string s)
	{
		Color col = new Color();
		if(s.Length == 6)
		{
			string[] cols = new string[3];

		}
		if(s.Length == 8)
		{

		}
		return col;
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