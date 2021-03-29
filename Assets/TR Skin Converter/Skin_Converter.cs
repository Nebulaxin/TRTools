using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleFileBrowser;

public class Skin_Converter : MonoBehaviour
{
	public string png;
	public SpriteRenderer sp;
	public Texture2D tex;

	void SetPNGs(string[] pth)  { png = pth[0]; tex.LoadImage(File.ReadAllBytes(png), false);}
	public void GetPNGs()
	{
		FileBrowser.Filter ff = new FileBrowser.Filter("Skin", ".png");
		FileBrowser.SetFilters(false, new FileBrowser.Filter[] { ff });
		FileBrowser.ShowLoadDialog(SetPNGs, null, FileBrowser.PickMode.Files, false, null, null, "Select PNG file", "Select");
		
	}
	public void Convert()
	{
		List<string> l = new List<string>();
		for(int v = 0; v < 40; v++)
		{
			l.Add("00000000");
		}
		for(int i = 15; i > -1; i--)
		{
			l.Add("00000000;00000000");
			for(int j = 0; j < 16; j++)
			{
				l.Add(Utility.ColorToHex(tex.GetPixel(j, i)));
			}
			l.Add("00000000;00000000");
		}

		GUIUtility.systemCopyBuffer = "trSkin1" + Utility.Compress(Utility.ToStringIncludeSemicolon(l));
	}
}
