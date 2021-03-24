using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using HSVPicker;

public class Gradient : MonoBehaviour
{
	public bool ishue;

	public float hue1;
	public float hue2;
	public Vector2 sv1;
	public Vector2 sv2;

	public InputField nickname;
	public Text debug;
	public Text end;
	string text;

	public void sethue1(float a) => hue1 = a; 
	public void sethue2(float a) => hue2 = a; 
	public void setsv1(float a, float b) => sv1 = new Vector2(a, b); 
	public void setsv2(float a, float b) => sv2 = new Vector2(a, b);

	public void setHSVMode(bool a) => ishue = a;




	public void Copy()
	{
		GUIUtility.systemCopyBuffer = end.text;
	}

	public void SetNickname()
	{
		if (ishue)
		{
			if (text != nickname.text) text = nickname.text;
			try
			{
				string endNick = "";
				int realLehnth = 0;
				foreach (char c in text)
				{
					if (c != ' ') realLehnth++;
				}
				float delta = (hue2 - hue1) / realLehnth;
				float newhue = hue1;
	
				Debug.Log(delta.ToString());
				foreach (char c in text)
				{
					if (c == ' ') endNick += " ";
					else
					{
						Debug.Log(newhue.ToString());
						endNick += ColorString(Color.HSVToRGB(newhue, sv1.x, sv1.y), c.ToString());
						newhue += delta;
					}
				}
				end.text = endNick;
			}
			catch (Exception exc)
			{
				debug.text = exc.ToString();
				Debug.Log(exc.ToString());
			}
		}
		else
		{
			if (text != nickname.text) text = nickname.text;
			try
			{
				string endNick = "";
				int realLehnth = 0;
				foreach (char c in text)
				{
					if (c != ' ') realLehnth++;
				}
				Vector2 delta = (sv2 - sv1) / 10;
				Vector2 newsv = sv1;
				Debug.Log(delta.ToString());
				foreach (char c in text)
				{
					if (c == ' ') endNick += " ";
					else
					{
						Debug.Log(newsv.ToString());
						endNick += ColorString(Color.HSVToRGB(hue1, newsv.x, newsv.y), c.ToString());
						newsv += delta;
					}
				}
				end.text = endNick;
			}
			catch (Exception exc)
			{
				debug.text = exc.ToString();
				Debug.Log(exc.ToString());
			}
		}
	}


	public string ColorString(Color col, string s)
	{
		Debug.Log($"{(int)(col.r * 255)} {(int)(col.g * 255)} {(int)(col.b * 255)}");
		Debug.Log(((int)(col.r * 255)).ToString("x") + ((int)(col.g * 255)).ToString("x") + ((int)(col.b * 255)).ToString("x"));
		int[] color = new int[3];
		color[0] = (int)(col.r * 255);
		color[1] = (int)(col.g * 255);
		color[2] = (int)(col.b * 255);
		string[] clr = new string[3];
		for(int i = 0; i < 3; i++)
		{
			if (color[i] < 16) clr[i] = "0" + color[i].ToString("x");
			else clr[i] = color[i].ToString("x");
			Debug.Log(clr[i]);
		}
		string hex = clr[0] + clr[1] + clr[2];
		return $"<color=#{hex}>{s}</color>";
	}
}