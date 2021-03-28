using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class Converter : MonoBehaviour
{
	public GameObject bar;										//ненужные вещи
	public RectTransform scroll;								//это все строки
	public List<GameObject> barList = new List<GameObject>();	//с картой и кнопкой добавления

	public string path;//путь к ккарте

	public string[] old_data;//старые данные
	public string[] new_data;//новые данные

	public string[] maps;//все имеющиеся карты(для списка)

	public Text debug;//дебаг консоль, не нужно


	public GameObject ConvertWindow;//окошко с сообением об удачной конвертации
	
	private void Start()
	{
		GetMaps();	//получаем карты
		Init();		//инициализируем список
	}


	public void Init()
	{
		try
		{
			path = null;
			if (maps != null)
			{
				foreach (Transform go in scroll)
				{
					barList.Remove(go.gameObject);	//убираем предыдущий
					Destroy(go.gameObject);			//список
				}
				foreach (string s in maps)
				{
					var g = Instantiate(bar.gameObject);	//создаём строку
					g.transform.SetParent(scroll, false);	//и присваиваем ей родителя-scrollview

					string ss = Path.GetFileName(s);				//
					g.GetComponentInChildren<Text>().text = ss;		//
					Button b = g.GetComponentInChildren<Button>();	//
					b.onClick.AddListener
						(
						() =>
						{
							foreach (GameObject gg in barList)
							{
								if (gg != null) gg.GetComponentInChildren<Image>().color = Color.HSVToRGB(0, 0, 0.71f);//
								path = s;//
							}

							g.GetComponentInChildren<Image>().color = Color.white;//
						}
						);

					barList.Add(g);//
				}
				scroll.sizeDelta = new Vector2(768, 112 * maps.Length);//
			}
		}
		catch (IOException exc)
		{
			debug.text = exc.ToString();//дебаг(не нужно)
		}
	}


	void GetMaps()
	{
		if (Directory.Exists("/storage/emulated/0/Android/data/ru.iamtagir.teamrun/files/maps"))//
		{
			try
			{
				maps = Directory.GetFiles("/storage/emulated/0/Android/data/ru.iamtagir.teamrun/files/maps");//
			}
			catch (IOException exc)
			{
				debug.text = exc.ToString();
			}
		}
		else
		{
			ConvertWindow.SetActive(true);  
			ConvertWindow.GetComponentInChildren<Text>().text = "Не удалось найти папку с картами. Похоже, Team Run не установлен или произошла какая-то ошибка.";// 
		}
	}


	public void Cnv()//
	{
		try
		{
			old_data = File.ReadAllLines(path);		//
			new_data = new string[old_data.Length]; //
			if (!Directory.Exists("/storage/emulated/0/Team Run Tools/Map Converter Backups")) Directory.CreateDirectory("/storage/emulated/0/Team Run Tools/Map Converter Backups");
			File.Copy(path, "/storage/emulated/0/Team Run Tools/Map Converter Backups/" + Path.GetFileName(path), true);

			int cnt = 0;
			foreach (string str in old_data)
			{
				if (cnt == 0) new_data[0] = old_data[0];//
				else
				{
					int cn = 0;
					string[] sa = ToArray(str);//преобразуем строку в массив
					List<string> ns = new List<string>();//листыы
					List<string> ms = new List<string>();//ыыыы
					List<string> ps = new List<string>();//ыыы
					List<string> cs = new List<string>();//ыы
					List<string> rs = new List<string>();//ы
					for (int i = 0; cn < 6; i++)
					{
						ns.Add(sa[cn]);//ну тут всё одинаково
						cn++;
					}


					if (sa[6] != "Z")
					{
						ms.Add("M");
						while (sa[cn] != "S")
						{
							ms.Add(sa[cn]);//добавляем всё в move-список
							cn++;
						}
						ms.Add("M");
					}
					cn++;

					if (sa[cn] != "W") ns.Add(sa[cn]);//добавляем родителя
					else ns.Add("-1");//добавляем нулёвого родителя если угол не равен 0
					cn++;

					string css = "";
					if (sa[cn] != "0") css = sa[cn];
					if (css.Contains("E")) css = "0";//если число с ешками, то делаем из него просто ноль
					if (sa[cn] == "0") css = "0";
					ns.Add(css);
					cn++;

					if (sa[cn] != "0")
					{
						rs.Add("R");
						rs.Add(sa[cn]);//добавляем скорость вращения
						rs.Add("R");
					}
					cn++;

					if (sa[0] == "5" |
						sa[0] == "11" |
						sa[0] == "12" |
						sa[0] == "14" |
						sa[0] == "15" |
						sa[0] == "16" |
						sa[0] == "18" |
						sa[0] == "19" |
						sa[0] == "24" |
						sa[0] == "25" |
						sa[0] == "28")

					{
						ps.Add("¶");
						while (sa[cn] != "Z")
						{
							ps.Add(sa[cn]);//добавляем параметры
							cn++;
						}
						ps.Add("¶");
					}

					if (sa[0] == "17" | sa[0] == "29")
					{
						ps.Add("¶");
						string sss = "";
						while (sa[cn] != "Z")
						{
							sss += sa[cn] + ";";//добавляем к общей строке подстроки и вставляем между ними греческий знак вопроса(это не точка с запятой)
							cn++;
						}
						ps.Add(sss);
						ps.Add("¶");
					}

					if (sa[0] == "20" |
						sa[0] == "21" |
						sa[0] == "22" |
						sa[0] == "23" |
						sa[0] == "33" |
						sa[0] == "34" |
						sa[0] == "35" |
						sa[0] == "36")
					{
						cs.Add("C");
						while (sa[cn] != "Z")
						{
							cs.Add(sa[cn]);//добавляем параметры цвета
							cn++;
						}
						cs.Add("C");
					}


					new_data[cnt] = ToString(ns) + ToString(ps) + ToString(ms) + ToString(rs) + ToString(cs);//складываем всё вместе
				}
				cnt++;//крутим счётчик
			}

			File.WriteAllLines(path, new_data);//записываем новые данные в карту

			ConvertWindow.SetActive(true);//не нужно
			ConvertWindow.GetComponentInChildren<Text>().text = $"Файл сконвертирован удачно по пути {path}";//не неужно
		}
		catch (IOException exc)
		{
			debug.text = exc.ToString();//дебаг
		}
	}

	public string[] ToArray(string s)
	{
		List<string> l = new List<string>();
		string ss = "";
		foreach(char c in s)
		{
			if (c != ';') ss += c;//делим строку по ; на ячейки в массиве
			else
			{
				l.Add(ss);//добавляем в список переменную
				ss = "";
			}
		}
		return l.ToArray();
	}

	public string ToString(List<string> l)
	{
		string s = "";
		foreach(string st in l)
		{
			s += st + ';';//складываем все ячейки в массиве и ставим между ними ;
		}
		return s;
	}
}
