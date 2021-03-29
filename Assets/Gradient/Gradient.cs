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
		return $"<color=#{Utility.ColorToHex(col)}>{s}</color>";
	}
}