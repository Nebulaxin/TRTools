using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleFileBrowser;

public class Skin_Converter : MonoBehaviour
{
	public string png;
	public Texture2D tex;

	public bool is16 = true;
	public Button b;
	public Button b2;


	public string savePng;
	public Texture2D tex2;
	public InputField input;


	public void Set16()
	{
		if (is16)
		{
			b.GetComponentInChildren<Text>().text = "20x18";
			b2.onClick.RemoveAllListeners();
			b2.onClick.AddListener(Convert2018);
			is16 = false;
		}
		else
		{
			b.GetComponentInChildren<Text>().text = "16x16";
			b2.onClick.RemoveAllListeners();
			b2.onClick.AddListener(Convert16);
			is16 = true;
		}

	}
	void SetPngLoad(string[] pth)  { png = pth[0]; tex.LoadImage(File.ReadAllBytes(png), false);}
	void SetPngSave(string[] pth)  { savePng = pth[0];}

	public void GetPngLoad()
	{
		FileBrowser.Filter ff = new FileBrowser.Filter("Skin", ".png");
		FileBrowser.SetFilters(false, new FileBrowser.Filter[] { ff });
		FileBrowser.ShowLoadDialog(SetPngLoad, null, FileBrowser.PickMode.Files, false, null, null, "Select PNG file", "Select");
	}
	public void Convert16()
	{
		Debug.Log("1616");
		List<string> l = new List<string>();
		for (int v = 0; v < 40; v++)
		{
			l.Add("00000000");
		}
		for (int i = 15; i > -1; i--)
		{
			l.Add("00000000;00000000");
			for (int j = 0; j < 16; j++)
			{
				l.Add(Utility.ColorToHex(tex.GetPixel(j, i), true));
			}
			l.Add("00000000;00000000");
		}

		GUIUtility.systemCopyBuffer = "trSkin1" + Utility.Compress(Utility.ToStringIncludeSemicolon(l));
	}
	public void Convert2018()
	{
		Debug.Log("2018");
		List<string> l = new List<string>();
		for (int i = 17; i > -1; i--)
		{
			for (int j = 0; j < 20; j++)
			{
				l.Add(Utility.ColorToHex(tex.GetPixel(j, i), true));
			}
		}
		GUIUtility.systemCopyBuffer = "trSkin1" + Utility.Compress(Utility.ToStringIncludeSemicolon(l));
	}


	public void GetPngSave()
	{
		FileBrowser.Filter fff = new FileBrowser.Filter("Image", ".png");
		FileBrowser.SetFilters(false, new FileBrowser.Filter[] { fff });
		FileBrowser.ShowSaveDialog(SetPngSave, null, FileBrowser.PickMode.Files, false, null, "TRSkin.png", "Select folder to save skin", "Select");
	}
	public void ConvertToPng()
	{
		List<Color> l = new List<Color>();
		foreach(string s in Utility.ToListExcludeSemicolon(Utility.Decompress(input.text.Remove(0, 7))))
		{
			l.Add(Utility.HexToColor(s));
		}
		int a = 0;

		for (int i = 17; i > -1; i--)
		{
			for (int j = 0; j < 20; j++)
			{
				tex2.SetPixel(j, i, l[a]);
				tex2.Apply();
				//Debug.Log($"x = {j}; y = {i}; count = {a}; color = {l[a]}, tex color = {tex2.GetPixel(j, i)}");
				a++;
				
			}
		}
		var f = File.Create(savePng);
		f.Close();
		File.WriteAllBytes(savePng, tex2.EncodeToPNG());
	}
}
