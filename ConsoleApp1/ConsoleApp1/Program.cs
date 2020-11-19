using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;//地図を準備する
using ConsoleApp1;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace 飛行棋
{
    class Program
    {
        public static string[] playerName = new string[2];//プレイ人数
        public static int[] Maps = new int[200];
        public static int[] playerPos = new int[2];
        public static bool[] flags = new bool[2];

        static void Main(string[] args)
        {
            GameHead();
            #region プレイヤの名前を入力
            List<shi> listPers = new List<shi> { };
            shi per1 = new shi(" playerName[0]");
            shi per2 = new shi(" playerName[1]");
            listPers.Add(per1);
            listPers.Add(per2);
            SerializeMethod(listPers);
            static void SerializeMethod(
                List<shi> listPres
            )
            {
                using (FileStream fs = new FileStream("1.bin", FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    //BinaryFormatter bf2 = new BinaryFormatter();
                   // bf.Serialize(fs, listPres);
                    //bf2.Serialize(fs, listPres);
                }
            }
            Console.WriteLine("プレイヤ1の名前を入力してください:");
            playerName[0] = Console.ReadLine();
            while (playerName[0] == "")
            {
                Console.WriteLine("プレイヤ1の名前を入力");
                playerName[0] = Console.ReadLine();
            }
            Console.WriteLine("プレイヤ2の名前を入力してください:");
            playerName[1] = Console.ReadLine();
            while (playerName[1] == "")
            {
                Console.WriteLine("プレイヤ2の名前を入力");
                playerName[1] = Console.ReadLine();
            }
            
            #endregion
            Console.Clear();//データをクリア
            GameHead();
            Console.WriteLine("ゲーム開始");
            Console.WriteLine("プレイヤ1はAです{0}", playerName[0]);
            Console.WriteLine("プレイヤ2はBです{0}", playerName[1]);
            Console.ReadKey(true);
            InitialMap();//地図リセット
            drawMap();//地図を準備する
            #region ゲーム開始
            while (playerPos[0] < 99 || playerPos[1] < 99)
            {
                #region プレイヤ1
                if (flags[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    flags[0] = false;
                }
                if (playerPos[0] >= 99)
                {
                    Console.WriteLine("プレイヤ{0}胜利!!!", playerName[0]);
                    break;
                }
                #endregion
                #region プレイヤ2
                if (flags[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    flags[1] = false;
                }
                if (playerPos[1] >= 99)
                {
                    Console.WriteLine("プレイヤ{0}胜利!!!", playerName[1]);
                    break;
                }
                #endregion
            }

            #endregion
            Win();
            Console.ReadKey();

        }
        //ゲーム画面を作ろう
        public static void GameHead()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*******************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("***********簡単な飛行棋***********");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*******************************");
            Console.ForegroundColor = ConsoleColor.White;
        }

        //初始化地图
        public static void InitialMap()
        {
            //罠
            int[] landMine = { 4, 25, 47, 74, 86 };
            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = 2;
            }
            //一回休み
            int[] pause = { 12, 15, 20, 43, 69, 80, 95 };
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 1;
            }
            //ジャンプ
            int[] space = { 29, 58, 75, 90 };
            for (int i = 0; i < space.Length; i++)
            {
                Maps[space[i]] = 3;
            }

        }
        public static void drawMap()
        {
            #region 第一横
            for (int i = 0; i <= 31; i++)
            {
                Console.Write(drawStringMap(i));
            }
            Console.WriteLine();
            #endregion
            #region 第一竖
            for (int i = 30; i <= 32; i++)
            {
                for (int j = 0; j <= 29; j++)
                {
                    Console.Write("  ");
                }
                Console.WriteLine(drawStringMap(i));
            }
            #endregion
            #region 第二横
            for (int i = 68; i >= 38; i--)
            {
                Console.Write(drawStringMap(i));
            }
            Console.WriteLine();
            #endregion
            #region 第二竖
            for (int i = 65; i < 72; i++)
            {
                Console.WriteLine(drawStringMap(i));
            }
            #endregion
            #region 第三横
            for (int i = 70; i < 100; i++)
            {
                Console.Write(drawStringMap(i));
            }
            Console.WriteLine();
            #endregion
        }

        public static string drawStringMap(int i)
        {
            string str = "";
            if (playerPos[0] == playerPos[1] && playerPos[1] == i)
            {
                Console.Write("<>");
            }
            else if (playerPos[0] == i)
            {
                Console.Write("Ａ");
            }
            else if (playerPos[1] == i)
            {
                Console.Write("Ｂ");
            }
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        str = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        str = "X";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        str = "[]";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        str = "{ }";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        public static void PlayGame(int playerNumber)
        {
            Console.WriteLine("任意のボタンを押してゲームをはじめましょう");
            Console.ReadKey(true);
            #region  骰子を投げ
            Random r = new Random();
            int rNumber = r.Next(1, 7);
            #endregion
            Console.WriteLine("プレイヤ{0}投げた{1}.", playerName[playerNumber], rNumber);
            Console.ReadKey(true);
            Console.ReadKey(true);
            playerPos[playerNumber] += rNumber;
            chanPos();
            Console.WriteLine("プレイヤ{0}前進{1}", playerName[playerNumber], rNumber);
            if (playerPos[1] == playerPos[0])
            {
                Console.ReadKey(true);
                Console.WriteLine("プレイヤ{0}と{1}重ね,プレイヤ{2}1後退.", playerName[playerNumber], playerName[1 - playerNumber], playerName[1 - playerNumber]);
                playerPos[1 - playerNumber] -= 1;
                chanPos();
                Console.ReadKey(true);
                Console.WriteLine("プレイヤ{0}行動終了", playerName[1 - playerNumber]);
            }
            else
            {
                Console.ReadKey(true);
                switch (Maps[playerPos[playerNumber]])
                {
                    case 0:
                        Console.ReadKey(true);
                        Console.WriteLine("効果なし");
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.ReadKey(true);
                        Console.WriteLine("プレイヤ{0}罠に落とした，後退5", playerName[playerNumber]);
                        playerPos[playerNumber] -= 5;
                        chanPos();
                        break;
                    case 2:
                        Console.ReadKey(true);
                        Console.WriteLine("プレイヤ{0}一回休み", playerName[playerNumber]);
                        flags[playerNumber] = true;
                        break;
                    case 3:
                        Console.ReadKey(true);
                        Console.WriteLine("プレイヤ{0}ジャンプした6前進", playerName[playerNumber]);
                        playerPos[playerNumber] += 6;
                        chanPos();
                        break;
                    default:
                        break;
                }
            }
            Console.Clear();
            drawMap();
        }


        public static void chanPos()
        {
            if (playerPos[0] <= 0)
            {
                playerPos[0] = 0;
            }
            if (playerPos[0] >= 99)
            {
                playerPos[0] = 99;
            }
            if (playerPos[1] <= 0)
            {
                playerPos[1] = 0;
            }
            if (playerPos[1] >= 99)
            {
                playerPos[1] = 99;
            }
        }

        //勝利
        public static void Win()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*****************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("************ゲーム終了************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*****************************");
        }
    }
}
