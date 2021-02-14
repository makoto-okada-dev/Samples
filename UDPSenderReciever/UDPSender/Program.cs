using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace UDPSender
{
    class Program
    {
        static Dictionary<int, string> _dic;

        // UDPでメッセージをある程度無限送信
        static void Main(string[] args)
        {
            CreateDummyData();

            //データを送信するリモートホストとポート番号
            string remoteHost = "127.0.0.1";
            int remotePort = 2002;

            //UdpClientオブジェクトを作成する
            using (var udp = new UdpClient())
            {
                // ループカウンタはテストプログラムなのでぐっとこらえて我慢。
                for (int i = 0; i < int.MaxValue; i++)
                {
                    // 送信データ作成
                    var now = DateTime.Now;
                    var mSec = now.Millisecond;
                    var r = new Random();
                    var tempMsg = _dic[((mSec + i) % 10)];// 不規則なコメントを出すための適当な計算
                    var sendMsg = now.ToString("yyyyMMdd_HHmmssfff") + "-" + tempMsg + "(" + r.Next(mSec + i) + ")";
                    var sendBytes = Encoding.UTF8.GetBytes(sendMsg);
                    Console.WriteLine(sendMsg);

                    //リモートホストを指定してデータを送信する
                    udp.Send(sendBytes, sendBytes.Length, remoteHost, remotePort);
                }
            }
        }

        static void CreateDummyData() {
            _dic = new Dictionary<int, string>();
            _dic.Add(0, "[Method Info]Dummy Proc is Started.                                                                 ");
            _dic.Add(1, "Intializing...                                                                                      ");
            _dic.Add(2, "Create  Process [proc1] : id_1                                                                      ");
            _dic.Add(3, "Start Process A                                                                                     ");
            _dic.Add(4, "End   Process A(Success)                                                                            ");
            _dic.Add(5, "Exception Occur. Application Exception( NullReferenceException ) StackTrace:xxxxxxxxxxxxxxxxxxxxxxxx");
            _dic.Add(6, "Process Success.                                                                                    ");
            _dic.Add(7, "Process Error:COMException ( 0x800A03EC )                                                           ");
            _dic.Add(8, "Dispose Process [proc1] : id_1                                                                      ");
            _dic.Add(9, "Exit.                                                                                               ");
        }
    }
}
